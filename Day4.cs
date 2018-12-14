using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AoC2018
{
    internal static class Day4
    {
        internal static void Run()
        {
            PartOneAndTwo();
        }

        internal static void PartOneAndTwo()
        {
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day4.txt"))
            {
                var events = new List<Event>();
                while (!reader.EndOfStream)
                {
                    var entry = reader.ReadLine();
                    events.Add(new Event(entry));
                }
                var guardSleeps = new Dictionary<int,List<Sleep>>();
                var currentGuardId = 0;
                var sleepTime = DateTime.MinValue;
                var wakeTime = DateTime.MinValue;
                var orderedEvents = events.OrderBy(o => o.EventTime).ToList();
                foreach (var entry in orderedEvents)
                {
                    if (entry.EventType == EventType.BeginShift)
                    {
                        currentGuardId = entry.GuardId;
                    }
                    if (entry.EventType == EventType.WakeUp)
                    {
                        wakeTime = entry.EventTime;
                    }
                    if (entry.EventType == EventType.FallAsleep)
                    {
                        sleepTime = entry.EventTime;
                    }
                    if (wakeTime != DateTime.MinValue && sleepTime != DateTime.MinValue)
                    {
                        if (!guardSleeps.ContainsKey(currentGuardId))
                        {
                            guardSleeps.Add(currentGuardId, new List<Sleep>());
                        }
                        guardSleeps[currentGuardId].Add(new Sleep
                        {
                                GuardId = currentGuardId,
                                WakeTime = wakeTime,
                                SleepTime = sleepTime
                        });
                        wakeTime = DateTime.MinValue;
                        sleepTime = DateTime.MinValue;
                    }
                }

                //Work out the sleepiest guard
                var highestSleepTime = 0;
                var sleepiestGuard = 0;
                foreach (var guard in guardSleeps)
                {
                    var thisGuardSleepTime = guard.Value.Sum(a => a.GetSleepTime());
                    if (thisGuardSleepTime > highestSleepTime)
                    {
                        highestSleepTime = thisGuardSleepTime;
                        sleepiestGuard = guard.Key;
                    }
                }

                //Work out when they were most often asleep:
                var minutes = new Dictionary<int,int>();
                foreach (var sleep in guardSleeps[sleepiestGuard])
                {
                    var sleepMin = sleep.SleepTime.Minute;
                    var wakeMin = sleep.WakeTime.Minute;
                    var range = Enumerable.Range(sleepMin, wakeMin - sleepMin);
                    foreach (var minute in range)
                    {
                        if (!minutes.ContainsKey(minute)) 
                        {
                            minutes.Add(minute,0);
                        }
                        minutes[minute]++;
                    }
                }
                var highestMinuteValue = minutes.Max(m => m.Value);
                var mostCommonMinute = minutes.First(m => m.Value == highestMinuteValue).Key;
                Console.WriteLine($"HighestSleep: {highestSleepTime}, Guard: {sleepiestGuard}, Most Common Minute: {mostCommonMinute}, Result: {sleepiestGuard * mostCommonMinute}");

                //Which one has most occurences of being asleep during mostCommonMinute
                var minsCount = new Dictionary<int,int>();
                foreach (var guardSleep in guardSleeps)
                {
                    foreach (var sleep in guardSleep.Value.Where(s => s.IsAsleep(mostCommonMinute)))
                    {
                        if (!minsCount.ContainsKey(sleep.GuardId))
                        {
                            minsCount.Add(sleep.GuardId, 0);
                        }
                        minsCount[sleep.GuardId]++;
                    }
                }
                var highestMinuteCount = minsCount.Max(m => m.Value);
                var whatGuard = minsCount.First(m => m.Value == highestMinuteCount).Key;
                Console.WriteLine($"Guard most asleep at minute {mostCommonMinute} - {whatGuard}. Result: {mostCommonMinute * whatGuard}");
            }
        }


        internal class Event
        {
            public DateTime EventTime;
            public EventType EventType;
            public int GuardId;
            public string EventText;
            internal Event(string entry)
            {
                EventText = entry;
                EventTime = DateTime.Parse(entry.Substring(1, 16));
                if (entry.Contains("Guard"))
                {
                    EventType = EventType.BeginShift;
                    GuardId = int.Parse(entry.Split("]")[1].Split(" ")[2].Replace("#", ""));
                }
                if (entry.Contains("wakes up"))
                {
                    EventType = EventType.WakeUp;
                }
                if (entry.Contains("falls asleep"))
                {
                    EventType = EventType.FallAsleep;
                }
            }
        }

        internal class Sleep
        {
            public int GuardId;
            public DateTime SleepTime;
            public DateTime WakeTime;
            public int GetSleepTime() => (int) (WakeTime - SleepTime).TotalMinutes;
            public bool IsAsleep(int minute) => minute >= SleepTime.Minute && minute < WakeTime.Minute;
        }


        internal enum EventType
        {
            BeginShift,
            FallAsleep,
            WakeUp
        }
    }
}
