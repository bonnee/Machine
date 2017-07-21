using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Interface
{
	class MainClass
	{
		static Machine.Machine m = new Machine.Machine();

		public static void Main(string[] args)
		{
			m.Cycle += cycle;

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

			Run(args[0], mem, delay);
		}

		static void Run(string path, string mem, int delay = 0)
		{
			m.SetProgram(new List<string>(File.ReadAllLines(path)));
			m.Memory = new List<char>(mem);

			Console.Write("Computing...");
			Stopwatch s = new Stopwatch();
			s.Start();
			m.Run("0", delay);
			s.Stop();

			Console.WriteLine("Done.");
			Print(m.Memory.ToArray(), m.count, s.Elapsed);
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
			Print(e.Memory, e.Count, new TimeSpan(0));
		}
	}
}