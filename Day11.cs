using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text;

namespace AoC2018
{
    internal static class Day11
    {
        internal static void Run()
        {
            //PartOne(9810);
            PartTwo(9810);
        }

        private static void PartOne(int serial)
        {
            var highestPowerLevel = 0;
            var topX = 0;
            var topY = 0;
            for (var x = 1; x <= 297; x++)
            {
                for (var y = 1; y <= 297; y++)
                {
                    //Unroll the inner loop. That's optimisation or something.
                    var a = CalcPower(x, y, serial);
                    var b = CalcPower(x, y + 1, serial);
                    var c = CalcPower(x, y + 2, serial);
                    var d = CalcPower(x + 1, y, serial);
                    var e = CalcPower(x + 1, y + 1, serial);
                    var f = CalcPower(x + 1, y + 2, serial);
                    var g = CalcPower(x + 2, y, serial);
                    var h = CalcPower(x + 2, y + 1, serial);
                    var i = CalcPower(x + 2, y + 2, serial);
                    var totalPower = a + b + c + d + e + f + g + h + i;
                    if (totalPower > highestPowerLevel)
                    {
                        highestPowerLevel = totalPower;
                        topX = x;
                        topY = y;
                    }
                }
            }
            Console.WriteLine($"HighestPower = {highestPowerLevel}, Coordinate: {topX},{topY}");
        }

        private static void PartTwo(int serial)
        {
            var sw = new Stopwatch();
            sw.Start();
            var highestPowerLevel = 0;
            var topX = 0;
            var topY = 0;
            var topSize = 0;
            var grid = BuildGrid(serial);
            //Make a cup of tea...
            for (var x = 1; x <= 297; x++)
            {
                Console.WriteLine(x);
                for (var y = 1; y <= 297; y++)
                {
                    for (var size = 2; size <= 300; size++)
                    {
                        if (x + size < 300 && y + size < 300)
                        {
                            var gridPower = CalcPowerForGrid(x, y, grid, size);
                            if (gridPower > highestPowerLevel)
                            {
                                highestPowerLevel = gridPower;
                                topX = x;
                                topY = y;
                                topSize = size + 1;
                            }
                        }
                    }
                }
            }
            sw.Stop();
            Console.WriteLine($"HighestPower = {highestPowerLevel}, Coordinate: {topX},{topY},{topSize} Time(s): {sw.Elapsed.TotalSeconds}");
        }

        private static int CalcPower(int x, int y, int serial)
        {
            var rackId = x + 10;
            var powerLevel = rackId * y;
            powerLevel += serial;
            powerLevel *= rackId;
            var hundredthDigit = Math.Abs(powerLevel / 100 % 10);
            return hundredthDigit - 5;
        }

        private static int CalcPowerForGrid(int startX, int startY, int[,] grid, int size)
        {
            var totalPower = 0;

                for (var x = startX; x <= startX + size; x++)
                {
                    for (var y = startY; y <= startY + size; y++)
                    {
                        totalPower += grid[x,y];
                    }
                }
            

            return totalPower;
        }

        private static int[,] BuildGrid(int serial)
        {
            var grid = new int[300,300];
            for (var x = 1; x <= 297; x++)
            {
                for (var y = 1; y <= 297; y++)
                {
                    grid[x,y] = CalcPower(x, y, serial);
                }
            }
            return grid;
        }

    }
}
