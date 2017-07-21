using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Machine;

namespace Interface
{
    class MainClass
    {
        static Instance m;

        public static void Main(string[] args)
        {
            string mem = "_";

            int delay = 0;

            if (args.Length == 0)
            {
                Console.WriteLine("Error: no arguments provided.");
                return;
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
                Console.WriteLine("Error: file doesn't exist.");
                return;
            }

            m = new Instance(new List<char>(mem), File.ReadAllText(args[0]));
            m.Cycle += cycle;

            Run(delay);
        }

        static void Run(int delay = 0)
        {
            Console.Write("Computing...");
            Stopwatch s = new Stopwatch();
            s.Start();
            m.Run("0", delay);
            s.Stop();

            Console.WriteLine("Done.");
            Print(m.memory.ToArray(), m.count, s.Elapsed);
        }

        static void Help()
        {
            Console.WriteLine("Provide some parameters.");
        }

        static void Print(char[] memory, int count, TimeSpan elapsed)
        {
            foreach (char c in memory)
                Console.Write(c);
            Console.WriteLine("\n\nCount: " + count + " Elapsed: " + elapsed.ToString());
        }

        static void cycle(object sender, Machine.MachineEventArgs e)
        {
            Print(e.memory.ToArray(), e.Count, new TimeSpan(0));
        }
    }
}