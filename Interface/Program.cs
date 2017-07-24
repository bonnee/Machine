using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Emulator
{
    class MainClass
    {
        static Machine m;
        static Stopwatch s = new Stopwatch();
        public static void Main(string[] args)
        {
            string mem = "_";

            int delay = 0;

            if (args.Length == 0)
            {
                throw new ArgumentException("No arguments provided.");
            }
            else if (args.Length == 2)
            {
                mem = args[1];
            }
            if (args.Length == 3)
            {
                delay = Convert.ToInt32(args[2]);
            }

            if (!File.Exists(args[0]))
            {
                throw new FileNotFoundException(args[0]);
            }

            Console.Write("Loading...");
            m = new Machine(new List<char>(mem), File.ReadAllLines(args[0]));
            //m.Cycle += cycle;
            m.Finish += cycle;

            Run(delay);
        }

        static void Run(int delay)
        {
            Console.Write("Computing...");

            s.Start();
            m.Run("0", delay);
            s.Stop();

            Console.WriteLine("Done.");
        }

        static void Help()
        {
            Console.WriteLine("Provide some parameters.");
        }

        static void Print(Memory<char> memory, int cycles, TimeSpan elapsed)
        {
            Console.WriteLine();
            foreach (char c in memory.ToArray())
                Console.Write(c);
            Console.WriteLine("\n\nCycles: " + cycles + " Elapsed: " + elapsed.ToString() + " Op/s: " + cycles / (elapsed.TotalMilliseconds / 1000));
        }

        static void cycle(object sender, MachineEventArgs e)
        {
            Print(e.Memory, e.Cycles, s.Elapsed);
        }
    }
}