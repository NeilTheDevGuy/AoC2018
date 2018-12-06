using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AoC2018
{
    internal static class Day6
    {
        internal static void Run()
        {
            //PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var coords = new List<Point>();

            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day6.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var coordLine = reader.ReadLine().Split(",");
                    coords.Add(new Point { X = int.Parse(coordLine[0]), Y = int.Parse(coordLine[1])});
                }
            }

            var maxX = coords.Max(c => c.X);
            var maxY = coords.Max(c => c.Y);
            var minX = coords.Min(c => c.X);
            var minY = coords.Min(c => c.Y);
            var closest = new Point[maxX, maxY];
            var dict = new Dictionary<Point, int>();

            for (var i = minX; i < maxX; i++)
            {
                for (var j = minY; j < maxY; j++)
                {
                    var orderedCoords = coords.Select(
                        c => new
                        {
                            coord = c,
                            distance = Math.Abs(c.X - i) + Math.Abs(c.Y - j)
                        })
                        .OrderBy(d => d.distance);

                    if (orderedCoords.First().distance != orderedCoords.ElementAt(1).distance)
                    {
                        closest[i, j] = orderedCoords.First().coord;
                    }
                }
            }

            //How close is each
            for (var i = minX; i < maxX; i++)
            {
                for (var j = minY; j < maxY; j++)
                {
                    if (!dict.ContainsKey(closest[i, j]))
                    {
                        dict[closest[i, j]] = 0;
                        
                    }
                    dict[closest[i, j]]++;
                }
            }

            //Remove the ones on the edges (the grid is infinite)
            for (var i = minX; i < maxX; i++)
            {
                for (var j = minY; j < maxY; j++)
                {
                    if (i == minX || j == minY || i == maxX - 1 || j == maxY - 1)
                    {
                        dict.Remove(closest[i,j]);
                    }
                }
            }
            var answer = dict.OrderByDescending(d => d.Value).First();
            Console.WriteLine(answer);;
        }

        private static void PartTwo()
        {
            var coords = new List<Point>();

            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day6.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var coordLine = reader.ReadLine().Split(",");
                    coords.Add(new Point { X = int.Parse(coordLine[0]), Y = int.Parse(coordLine[1]) });
                }
            }
            var maxX = coords.Max(c => c.X);
            var maxY = coords.Max(c => c.Y);
            var minX = coords.Min(c => c.X);
            var minY = coords.Min(c => c.Y);
            var size = 0;

            for (var i = minX; i < maxX; i++)
            {
                for (var j = minY; j < maxY; j++)
                {
                    var totalDist = coords.Select(c => Math.Abs(c.X - i) + Math.Abs(c.Y - j)).Sum();
                    if (totalDist < 10000)
                    {
                        size++;
                    }
                }
            }
            Console.WriteLine(size);
        }
    }
}
