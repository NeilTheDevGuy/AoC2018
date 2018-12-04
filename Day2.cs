using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2018
{
    internal static class Day2
    {
        internal static void Run()
        {
            //PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var twoCount = 0;
            var threeCount = 0;
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day2.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var gotTwo = false;
                    var gotThree = false;
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    var chars = line.ToCharArray();
                    foreach (var c in chars)
                    {
                        if (chars.Count(a => a == c) == 2 && !gotTwo)
                        {
                            twoCount++;
                            gotTwo = true;
                        }
                        if (chars.Count(a => a == c) == 3 && !gotThree)
                        {
                            threeCount++;
                            gotThree = true;
                        }
                    }
                }
            }
            Console.WriteLine($"Twos: {twoCount}, Threes: {threeCount}. Checksum: {twoCount * threeCount}");
        }

        private static void PartTwo()
        {
            var lines = new List<string>();
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day2.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    lines.Add(line);
                }
            }
            var index = 0;
            foreach (var outerLine in lines)
            {
                var linePos = 0;
                foreach (var innerLine in lines)
                {
                    if (linePos == index)
                        continue;
                    
                    var charMatches = innerLine.Where((t, i) => t == outerLine[i]).Count();
                    if (charMatches == innerLine.Length - 1)
                    {
                        Console.WriteLine(innerLine);
                        Console.WriteLine(outerLine);
                        for (var i = 0; i < innerLine.Length; i++)
                        {
                            if (innerLine[i] == outerLine[i])
                            {
                                Console.Write(innerLine[i]);
                            }
                        }
                    }
                    linePos++;
                }
                index++;
            }
        }
    }
}
