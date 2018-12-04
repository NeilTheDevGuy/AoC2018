using System;
using System.Collections.Generic;
using System.IO;

namespace AoC2018
{
    internal static class Day1
    {
        internal static void Run()
        {
            //PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var freq = 0;
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day1.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    int.TryParse(line.Substring(1, line.Length - 1), out int change);
                    if (line.StartsWith("+"))
                    {
                        freq += change;
                    }
                    else
                    {
                        freq -= change;
                    }
                }
            }
            Console.WriteLine(freq);
        }

        private static void PartTwo()
        {
            var freq = 0;
            var distinctFrequencies = new Dictionary<int,int>();
            var lines = new string[10000];
            var lineIndex = 0;
            var maxLines = 0;
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day1.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    lines[lineIndex] = line;
                    lineIndex++;
                }
            }
            maxLines = lineIndex;
            lineIndex = 0;
            while (true)
            {
                if (lineIndex == maxLines)
                    lineIndex = 0; //Loop the list if it gets to the end - might take multiple iterations.
                var line = lines[lineIndex];
                int.TryParse(line.Substring(1, line.Length - 1), out int change);
                if (line.StartsWith("+"))
                {
                    freq += change;
                }
                else
                {
                    freq -= change;
                }
                if (distinctFrequencies.ContainsKey(freq))
                {
                    Console.WriteLine(freq);
                    return;
                }
                distinctFrequencies.Add(freq,freq);
                lineIndex++;
            }
        }
    }
}
