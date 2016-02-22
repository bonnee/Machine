using System;
using System.Collections.Generic;
using System.IO;

namespace Machine
{
	public class Machine
	{
		public List<char> Memory { get; set; }

		public List<string> Program { get; set; }

		string state { get; set; }

		int index;

		#region Constructors

		public Machine ()
		{
			
		}

		public Machine (List<char> memory)
		{
			Memory = memory;
		}

		public Machine (List<char> memory, List<string> program)
		{
			Memory = memory;
			Program = program;
		}

		#endregion

	}
}

