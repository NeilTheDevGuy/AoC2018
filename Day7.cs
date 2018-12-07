using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2018
{
    internal static class Day7
    {
        internal static void Run()
        {
            //PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var lines = new List<Step>();
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day7.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var input = reader.ReadLine().Split(" ");
                    lines.Add(new Step
                    {
                        StepName = input[1],
                        StepDependsOn = input[7]
                    });
                }
            }

            //Get a list of all the available steps/dependancies in alphabetical order.
            //This actually just ends up with the entire alphabet but I suppose you can't necessarily expect that.
            //That said, it would probably work ok with just the alphabet anyway.
            var orderedSteps = lines
                .Select(s => s.StepName)
                .Concat(lines.Select(d => d.StepDependsOn))
                .Distinct()
                .OrderBy(o => o)
                .ToList();

            var steps = "";
            while (orderedSteps.Any())
            {
                var nextStep = orderedSteps.First(s => lines.All(d => d.StepDependsOn != s));
                steps += nextStep;
                lines.RemoveAll(d => d.StepName == nextStep); //Done with these now.
                orderedSteps.Remove(nextStep); //Also done with this.
            }
            Console.WriteLine(steps);
        }

        public static void PartTwo()
        {
            var lines = new List<Step>();

            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day7.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var input = reader.ReadLine().Split(" ");
                    lines.Add(new Step
                    {
                        StepName = input[1],
                        StepDependsOn = input[7]
                    });
                }
            }

            var allSteps = lines
                .Select(s => s.StepName)
                .Concat(lines
                .Select(d => d.StepDependsOn))
                .Distinct()
                .OrderBy(o => o)
                .ToList();

            var elves = new int[5];
            var thisSec = 0;
            var completedSteps = new List<Step>();

            while (allSteps.Any() || elves.Any(e => e > thisSec))
            {
                var nextStep = allSteps.Where(s => lines.All(d => d.StepDependsOn != s)).ToList();
                for (var i = 0; i < 5; i++)
                {
                    if (!nextStep.Any()) continue; //End of the line on this step
                    if (elves[i] > thisSec) continue; //Nothing to do yet
                    elves[i] = elves[i] = nextStep.First()[0] - 4 + thisSec; //60 seconds plus 1 sec per alphabet position. Works out to taking 4 off the ascii value.
                    allSteps.Remove(nextStep.First());
                    completedSteps.Add(new Step {StepName = nextStep.First(), Finished = elves[i]});
                    nextStep.RemoveAt(0);
                }

                thisSec++;
                var done = completedSteps.Where(d => d.Finished <= thisSec);
                foreach (var doneStep in done)
                {
                    lines.RemoveAll(d => d.StepName == doneStep.StepName); //Get rid of any we've done from here.
                }
                completedSteps.RemoveAll(d => d.Finished <= thisSec);
            }
            Console.WriteLine(thisSec.ToString());
        }
    }

    internal class Step
    {
        public string StepName;
        public string StepDependsOn;
        public int Finished;
    }
}
