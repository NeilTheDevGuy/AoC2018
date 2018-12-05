using System;
using System.IO;

namespace AoC2018
{
    internal static class Day5
    {
        internal static void Run()
        {
           //PartOne();
           PartTwo();
        }

        private static void PartOne()
        {
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day5.txt"))
            {
                var input = reader.ReadToEnd().Trim();
                Console.WriteLine(ReduceString(input).Length);
            }
        }

        private static string ReduceString(string polymer)
        {
            for (var i = 0; i < polymer.Length - 1; i++)
            {
                if (i < 0) i = 0;
                var c1 = (int)polymer[i];
                var c2 = (int)polymer[i + 1];
                if (c2 - c1 == 32 || c1 - c2 == 32)
                {
                    polymer = polymer.Remove(i, 2);
                    i -= 2;
                }
            }
            return polymer;
        }

        private static void PartTwo()
        {
            var lowestCount = int.MaxValue;
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day5.txt"))
            {
                var input = reader.ReadToEnd().Trim();
                for (int i = 65; i < 91; i++)
                {
                    var upper = (char) i;
                    var lower = (char) (i + 32);
                    var attempt = input.Replace(lower.ToString(), "");
                    attempt = attempt.Replace(upper.ToString(), "");
                    var result = ReduceString(attempt);
                    if (result.Length < lowestCount)
                        lowestCount = result.Length;
                }
            }
            Console.WriteLine(lowestCount);
        }
    }
}
