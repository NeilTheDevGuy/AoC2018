using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AoC2018
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine($"Started at {DateTime.Now.ToString("dd MM yyyy HH:mm:ss")}");
            //Day1.Run();
            //Day2.Run();
            //Day3.Run();
            //Day4.Run();
            //Day5.Run();
            //Day6.Run();
            //Day7.Run();
            //Day8.Run();
            //Day9.Run();
            //Day11.Run();
            //Day12.Run();
            Day13.Run();
            sw.Stop();
            Console.WriteLine($"Finished at {DateTime.Now.ToString("dd MM yyyy HH:mm:ss")}");
            Console.WriteLine($"Elapsed time (seconds): {sw.Elapsed.Seconds}");
            Console.ReadKey();
        }
    }
}
