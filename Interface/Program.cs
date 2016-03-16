using Machine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Interface
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Machine.Machine m = new Machine.Machine ();

			m.SetProgram (new List<string> (File.ReadAllLines (args [0])));
			m.Memory = new List<char> (args [1]);

            Console.Write("Computing...");
            Stopwatch s = new Stopwatch();
            s.Start();
			m.Run ("0");
            s.Stop();

            Print(m.Memory.ToArray(), m.count, s.Elapsed);

            Console.ReadLine();
		}


        static void Print(char[] memory, int count, TimeSpan elapsed)
		{
			Console.Clear ();
			foreach (char c in memory) {
				Console.Write (c);
			}
			Console.WriteLine ();
			Console.Write ("^\nCount: " + count + " Elapsed: " + elapsed.ToString());
		}
	}
}
