using System;
using System.Collections.Generic;
using System.Threading;

namespace Machine
{
    public class Machine
    {
        List<char> mem = new List<char>();
        public List<char> Memory { get { return mem; } set { mem = value; mem.Insert(0, '_'); mem.Add('_'); } }

        private List<string[]> Program { get; set; }

        public string state { get; set; }

        public int index = 0;
        public int count = 0;

        #region Constructors

        public Machine()
        {

        }

        public Machine(List<char> memory)
        {
            Memory = memory;
        }

        public Machine(List<char> memory, List<string[]> program)
        {
            Memory = memory;
            Program = program;
        }

        #endregion

        /// <summary>
        /// Sets the content of the program to be executed
        /// </summary>
        /// <param name="program">Program.</param>
        public void SetProgram(List<string> program)
        {
            Program = new List<string[]>();
            foreach (string line in program)
                Program.Add(line.Split(' '));
        }

        #region Runtime

        public void Run(string s, int delay = 0)
        {
            state = s;
            string[] command;
            Print();
            while (state != "halt")
            {
                Print();

                command = GetCommand();

                if (command[1] == "*" || Memory[index] == Convert.ToChar(command[1]))
                {
                    if (command[2] != "*")
                        mem[index] = Convert.ToChar(command[2]);
                    switch (command[3])
                    {
                        case "r":
                            if (index == Memory.Count - 1)
                                Memory.Add('_');
                            else
                                index++;
                            break;
                        case "l":
                            if (index == 0)
                                Memory.Insert(0, '_');
                            else
                                index--;
                            break;
                    }

                    state = command[4];
                }
                count++;
                if (delay > 0)
                    Thread.Sleep(delay);
            }
            Print();
        }


        void Print()
        {
            Console.Clear();
            foreach (char c in Memory)
            {
                Console.Write(c);
            }
            Console.WriteLine();
            Console.CursorLeft = index;
            Console.Write("^\nCount" + count + " State: " + state);
        }

        /// <summary>
        /// Gets the command to be executed between all the overloads of the current state
        /// </summary>
        /// <returns>The line.</returns>
        string[] GetCommand()
        {
            List<string[]> cmds = new List<string[]>();
            List<string> inp = new List<string>();

            foreach (string[] cmd in Program)
            {
                if (cmd[0] == state)
                {
                    cmds.Add(cmd);
                    inp.Add(cmd[1]);
                }
            }

            try
            {
                return cmds[inp.IndexOf(mem[index].ToString())];
            }
            catch
            {
                return cmds[inp.IndexOf("*")];
            }
        }
    }
        #endregion
}

