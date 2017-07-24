using System;
using System.Collections.Generic;
using System.Linq;
using Machine;

namespace Machine
{
    public class Code
    {
        private CodeMatch<string, string, string[]> lines;

        public Code(string[] program)
        {
            lines = new CodeMatch<string, string, string[]>();

            for (int i = 0; i < program.Length; i++)
            {
                if (!program[i].StartsWith("#"))
                {
                    string[] tmp = program[i].Split(' ');
                    if (tmp.Length == 5)
                    {
                        if (!lines.ContainsKey(tmp[0]))
                        {
                            lines.Add(tmp[0], new Dictionary<string, string[]>());
                        }

                        lines[tmp[0]].Add(tmp[1], tmp.Skip(2).ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Matches the right command line to the actual program state
        /// </summary>
        /// <param name="state">The actual program state</param>
        /// <param name="cell">The indexed memory cell</param>
        /// <returns>The right command line</returns>
        public string[] Match(string state, string cell)
        {
            Dictionary<string, string[]> right;
            String[] cmd;

            if (lines.TryGetValue(state, out right))
            {
                if (!right.TryGetValue(cell, out cmd))
                {
                    return right["*"];
                }
            }
            else
            {
                throw new Exception("Match not found in program on state: " + state);
            }

            return cmd;
        }
    }

    /// <summary>
    /// Implements a 2 key dictionary
    /// </summary>
    public class CodeMatch<T1, T2, T3> : Dictionary<T1, Dictionary<T2, T3>>
    {
        new public Dictionary<T2, T3> this[T1 key]
        {
            get
            {
                if (!ContainsKey(key))
                    Add(key, new Dictionary<T2, T3>());

                Dictionary<T2, T3> returnObj;
                TryGetValue(key, out returnObj);

                return returnObj;
            }
        }
    }
}