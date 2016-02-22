using System;
using System.Collections.Generic;
using System.IO;

namespace Machine
{
	public class Machine
	{
		public List<char> Memory { get; set; }

		public List<string[]> Program { get; set; }

		public string state { get; set; }

		int index = 0;

		#region Constructors

		public Machine ()
		{
			
		}

		public Machine (List<char> memory)
		{
			Memory = memory;
		}

		public Machine (List<char> memory, List<string[]> program)
		{
			Memory = memory;
			Program = program;
		}

		#endregion

		public void Run (string state = "0", int delay = 0)
		{
			string[] command = GetCommand ();

			if (command [1] == "*" || Memory [index] == Convert.ToChar (command [1])) {
				if (command [2] != "*")
					Memory [index] = Convert.ToChar (command [2]);
				switch (command [3]) {
				case "r":
					index++;
					break;
				case "l":
					index--;
					break;
				}
			}
		}

		string[] GetCommand ()
		{
			List<string[]> cmds = new List<string[]> ();
			List<string> inp = new List<string> ();

			foreach (string[] cmd in Program) {
				if (cmd [0] == state) {
					cmds.Add (cmd);
					inp.Add (cmd [1]);
				}
			}

			try {
				return cmds [inp.IndexOf (Memory [index].ToString ())];
			} catch {
				return cmds [inp.IndexOf ("*")];
			}
		}
	}
}

