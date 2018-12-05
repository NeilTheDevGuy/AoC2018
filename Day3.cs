using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2018
{
    internal static class Day3
    {
        private static Dictionary<string, Claim> _fabric = new Dictionary<string, Claim>();
        internal static void Run()
        {
           PartOne();
            PartTwo();
        }


        private static void PartOne()
        {
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day3.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    ParseLineAndUpdateFabric(line);
                }
            }
            var count = _fabric.Values.Count(c => c.Count >= 2);
            Console.WriteLine(count);
        }

        private static void PartTwo()
        {
            var zeroOverlap = new List<int>();
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day3.txt"))
            {
                var fabric = new int[1000, 1000];
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;

                    var id = int.Parse(line.Split(' ')[0].Replace("#", ""));
                    zeroOverlap.Add(id);

                    var usefulBit = line.Substring(line.IndexOf('@') + 2);
                    var size = usefulBit.Substring(usefulBit.IndexOf(':') + 1).Split('x');
                    var width = int.Parse(size[0]);
                    var height = int.Parse(size[1]);
                    var coords = usefulBit.Split(',');
                    var x = int.Parse(coords[0]);
                    var y = int.Parse(coords[1].Remove(coords[1].IndexOf(':')));
                    
                    for (var i = 0; i < width; i++)
                    {
                        for (var j = 0; j < height; j++)
                        {
                            var prevId = fabric[x + i, y + j];
                            if (prevId == 0)
                            {
                                fabric[x + i, y + j] = id;
                            }
                            else
                            {
                                zeroOverlap.Remove(id);
                                zeroOverlap.Remove(prevId);
                            }
                        }
                    }
                }
            }
            Console.WriteLine(zeroOverlap.FirstOrDefault());
       }

        private static void ParseLineAndUpdateFabric(string line)
        {
            var id = int.Parse(line.Split(' ')[0].Replace("#", ""));
            var usefulBit = line.Substring(line.IndexOf('@') + 2);
            var size = usefulBit.Substring(usefulBit.IndexOf(':') + 1).Split('x');
            var width = int.Parse(size[0]);
            var height = int.Parse(size[1]);
            var coords = usefulBit.Split(',');
            var x = int.Parse(coords[0]);
            var y = int.Parse(coords[1].Remove(coords[1].IndexOf(':')));
            var claim = new Claim
            {
                Id = id,
                X = x,
                Y = y,
                Width = width,
                Height = height,
                Count = 1,
            };
            

            for (var i = x; i < x + width; i++)
            {
                for (var j = y; j < y + height; j++)
                {
                    var key = i + "," + j;
                    if (!_fabric.ContainsKey(key))
                    {
                        _fabric.Add(key, claim);
                    }
                    else
                    {
                        var thisClaim = _fabric[key];
                        thisClaim.Count++;
                        _fabric[key] = thisClaim;
                    }
                }
            }
        }

        private struct Claim
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;
            public int Count;
            public int Id;
        }
    }
}
