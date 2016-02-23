using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

namespace Interface
{
	class MainClass
	{
        static Machine.Machine m = new Machine.Machine(new List<char> { '1', '0', '1', '0', '1', '1', '1' });

		public static void Main (string[] args)
		{
            m.SetProgram(new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Examples/bintodec.tm"))));

			m.Run ("0", 10);

            Console.ReadLine();
		}
	}
}
