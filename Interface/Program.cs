using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

namespace Interface
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Machine.Machine m = new Machine.Machine();
            m.SetProgram(new List<string>(File.ReadAllLines(args[0])));
            m.Memory = new List<char> { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', };
            //m.SetProgram(new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Examples/bintodec.tm"))));

			m.Run ("0", Convert.ToInt32(args[1]));

            Console.ReadLine();
		}
	}
}
