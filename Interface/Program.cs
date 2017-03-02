using Machine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Interface
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			string mem = "_";

			if (args.Length == 0)
			{
				Console.WriteLine("Error: no arguments provided.");
				return;
			}
			else if (args.Length == 2)
			{
				mem = args[1];
			}

			if (!File.Exists(args[0]))
			{
				Console.WriteLine("Error: file doesn't exist.");
				return;
			}

			Run(args[0], mem);
		}

		static void Run(string path, string mem)
		{
			Machine.Machine m = new Machine.Machine();

			m.SetProgram(new List<string>(File.ReadAllLines(path)));
			m.Memory = new List<char>(mem);

			Console.Write("Computing...");
			Stopwatch s = new Stopwatch();
			s.Start();
			m.Run("0");
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
	}
}