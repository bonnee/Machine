using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Xml.XPath;

namespace Machine
{
	class Program
	{
		static List<char> mem = new List<char> (new char[]{ '0', '1', ' ', ' ', '1', '0' });
		static int memIndex = 0;

		static List<string> prg;
		static int prgIndex = 0;

		static string state = "0";

		static bool end = false;

		static void Main (string[] args)
		{
			load (args [1]);
			cycle ();
			Console.Read ();
		}

		static void load (string path)
		{
			prg = new List<string> (File.ReadAllLines (path));
		}

		static void cycle ()
		{
			while (!end) {
				Thread.Sleep (300);
				read ();
				prgIndex++;
				print ();
			}
		}

		static void print ()
		{
			Console.Clear ();
			foreach (char b in mem) {
				Console.Write (b);
			}
			Console.WriteLine ();
			Console.CursorLeft = memIndex;
			if (end)
				Console.ForegroundColor = ConsoleColor.Red;
			Console.Write ('^');

		}

		static void read ()
		{
		}
	}
}
