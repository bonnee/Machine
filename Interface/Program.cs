using System;
using System.Reflection;
using Machine;
using System.Collections.Generic;
using System.IO;

namespace Interface
{
	class MainClass
	{
        static Machine.Machine m = new Machine.Machine(new List<char> { '0', '0', '1', '0' });

		public static void Main (string[] args)
		{
			m.SetProgram (new List<string> (File.ReadAllLines (Path.Combine (Path.GetDirectoryName (Assembly.GetEntryAssembly ().Location)
				, @"Examples/invert.tm"))));

			m.Run ("0", 500);

            Console.ReadLine();
		}
	}
}
