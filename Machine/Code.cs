using System.Collections.Generic;
using System.Linq;

namespace Emulator
{
    public class Code
    {
        private readonly CodeMatch<string, string, string[]> lines;
        private readonly string comment = ";";

        /// <param name="program">The code splitted by lines</param>
        public Code(string[] program)
        {
            lines = new CodeMatch<string, string, string[]>();

            foreach(var line in program)
            {
                if (line.StartsWith(comment)) 
                    continue;

                var tmp = line.Split(' ');
                if (tmp.Length != 5) 
                    continue;

                if (!lines.ContainsKey(tmp[0]))
                {
                    lines.Add(tmp[0], new Dictionary<string, string[]>());
                }

                lines[tmp[0]].Add(tmp[1], tmp.Skip(2).ToArray());
            }
        }

        /// <param name="program">The code splitted by lines</param>
        /// <param name="comment">The comment delimiter to use</param>
        public Code(string[] program, string comment) : this(program)
        {
            this.comment = comment;
        }

        /// <summary>
        /// Matches the right command line to the actual program state
        /// </summary>
        /// <param name="state">The program state to match</param>
        /// <param name="cell">The indexed memory cell</param>
        /// <returns>The right command line</returns>
        public string[] Match(string state, string cell)
        {
            if (cell == " ")
            {
                cell = "_";
            }

            if (!lines.TryGetValue(state, out var right))
                throw new KeyNotFoundException("Match not found in state '" + state + "' for cell '" + cell + "'");

            if (right.TryGetValue(cell, out var cmd)) 
                return cmd;

            try
            {
                return right["*"];
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("Match not found in state '" + state + "' for cell '" + cell + "'");
            }
        }
    }

    /// <summary>
    /// A two-key dictionary
    /// </summary>
    public class CodeMatch<K1, K2, V1> : Dictionary<K1, Dictionary<K2, V1>>
    {
        public new Dictionary<K2, V1> this[K1 key]
        {
            get
            {
                if (!ContainsKey(key))
                    Add(key, new Dictionary<K2, V1>());

                TryGetValue(key, out var returnObj);

                return returnObj;
            }
        }
    }
}