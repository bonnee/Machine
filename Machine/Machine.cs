using System;
using System.Collections.Generic;
using System.Threading;

namespace Machine
{
    public class Machine
    {
        List<char> mem = new List<char>();

        public List<char> Memory
        {
            get { return mem; }
            set { mem = value; }
        }

        private List<string[]> Program { get; set; }

        public string state { get; set; }

        public int index = 0;
        public int count = 0;

        /// <summary>
        /// Raises the cycle event.
        /// </summary>
        /// <param name="e">The event args</param>
        protected virtual void OnCycle(TuringEventArgs e)
        {
            EventHandler<TuringEventArgs> ev = Cycle;
            if (ev != null)
                ev(this, e);
        }

        public event EventHandler<TuringEventArgs> Cycle;

        /// <summary>
        /// Raises the Finish event
        /// </summary>
        /// <param name="e">The event args</param>
        protected virtual void OnFinish(TuringEventArgs e)
        {
            EventHandler<TuringEventArgs> ev = Finish;
            if (ev != null)
                ev(this, e);
        }
        public event EventHandler<TuringEventArgs> Finish;

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

        /// <summary>
        /// Starts the turing machine
        /// </summary>
        /// <param name="s">The initial state</param>
        /// <param name="delay">The delay to apply to each cycle of the machine</param>
        public void Run(string s, int delay = 0)
        {
            state = s;
            string[] command;
            while (state != "halt")
            {
                command = GetCommand();

                if (command[1] == "*" || Memory[index] == Convert.ToChar(command[1]))
                {
                    if (command[2] != "*")
                        mem[index] = Convert.ToChar(command[2]);
                    if (command[3] == "r")
                        if (index == Memory.Count - 1)
                            Memory.Add('_');
                        else
                            index++;
                    else if (command[3] == "l")
                        if (index == 0)
                            Memory.Insert(0, '_');
                        else
                            index--;

                    state = command[4];
                }
                OnCycle(new TuringEventArgs(Memory.ToArray(), state, index, count));
                count++;
                if (delay > 0)
                    Thread.Sleep(delay);
            }
            OnFinish(new TuringEventArgs(Memory.ToArray(), state, index, count));
        }

        /// <summary>
        /// Gets the command to be executed between all the commands of the current state
        /// </summary>
        /// <returns>The command to execute</returns>
        string[] GetCommand()
        {
            Dictionary<string, string[]> commands = new Dictionary<string, string[]>();

            foreach (string[] cmd in Program)
                if (cmd[0] == state)
                    commands.Add(cmd[1], cmd);

            string[] outp;
            if(!commands.TryGetValue(mem[index].ToString(), out outp))
                return commands["*"];
            return outp;

            /*if (commands.ContainsKey(mem[index].ToString()))
                return commands[mem[index].ToString()]; //inp.IndexOf(mem[index].ToString())
            return commands[mem[index].ToString()];*/
        }

        #endregion
    }

    public class TuringEventArgs : EventArgs
    {
        public char[] Memory { get; set; }

        public string State { get; set; }

        public int Index { get; set; }

        public int Count { get; set; }

        public TuringEventArgs(char[] memory, string state, int index, int count)
        {
            Memory = memory;
            State = state;
            Index = index;
            Count = count;
        }
    }
}

