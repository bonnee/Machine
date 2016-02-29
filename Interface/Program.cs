using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using Machine;

namespace Interface
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Machine.Machine m = new Machine.Machine ();

			m.SetProgram (new List<string> (File.ReadAllLines (args [0])));
			m.Memory = new List<char> (args [1]);

			m.Cycle += Print;
			m.Run ("0", Convert.ToInt32 (args [2]));

			Console.ReadLine ();
		}


		static void Print (object sender, CycleEventArgs e)
		{
			Console.Clear ();
			foreach (char c in e.Memory) {
				Console.Write (c);
			}
			Console.WriteLine ();
			Console.CursorLeft = e.Index;
			Console.Write ("^\nCount: " + e.Count + " State: " + e.State);
		}
	}
}
