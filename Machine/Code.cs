using System;
using System.Collections.Generic;
using System.Linq;
using Machine;

namespace Machine
{
    public class Code
    {
        private List<string[]> lines;

        public Code(string program)
        {
            lines = new List<string[]>();

            String[] tmp = program.Split('\n');
            tmp.ToList().ForEach(i => lines.Add(i.Split(' ')));
        }
        public Code(IEnumerable<string[]> program)
        {
            lines = new List<string[]>(program);
        }

        /// <summary>
        /// Matches the right command line to the actual program state
        /// </summary>
        /// <param name="state">The actual program state</param>
        /// <param name="cell">The indexed memory cell</param>
        /// <returns>The right command line</returns>
        public string[] Match(string state, string cell)
        {
            Dictionary<string, string[]> commands = new Dictionary<string, string[]>();
            string[] rightCMD;

            foreach (string[] cmd in lines)
            {
                if (cmd[0] == state)
                {
                    commands.Add(cmd[1], cmd);
                }
            }

            if (!commands.TryGetValue(cell, out rightCMD))
                return commands["*"];

            return rightCMD;
        }
    }
}