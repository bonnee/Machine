using System;
using System.Collections.Generic;
using System.Threading;

namespace Machine
{
    public class Instance
    {
        private Memory<char> mem;
        public Memory<char> memory
        {
            get
            {
                return mem;
            }
        }

        private Code code;

        public string state { get; set; }

        public int count = 0;

        /// <summary>
        /// Raises the cycle event.
        /// </summary>
        /// <param name="e">The event args</param>
        protected virtual void OnCycle(MachineEventArgs e)
        {
            EventHandler<MachineEventArgs> ev = Cycle;
            if (ev != null)
                ev(this, e);
        }

        public event EventHandler<MachineEventArgs> Cycle;

        /// <summary>
        /// Raises the Finish event
        /// </summary>
        /// <param name="e">The event args</param>
        protected virtual void OnFinish(MachineEventArgs e)
        {
            EventHandler<MachineEventArgs> ev = Finish;
            if (ev != null)
                ev(this, e);
        }
        public event EventHandler<MachineEventArgs> Finish;

        #region Constructors

        public Instance()
        {

        }
        public Instance(List<char> mem)
        {
            this.mem = new Memory<char>(mem, '_');
        }

        public Instance(List<char> mem, string[] prog) : this(mem)
        {
            code = new Code(prog);
        }

        #endregion
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
                command = code.Match(state, mem.Read().ToString());

                if (command[0] != "*")
                    mem.Write(Convert.ToChar(command[0]));
                if (command[1] == "r")
                    mem.MoveRight();
                else if (command[1] == "l")
                    mem.MoveLeft();

                state = command[2];

                OnCycle(new MachineEventArgs(mem, state, mem.Index, count));
                count++;
                if (delay > 0)
                    Thread.Sleep(delay);
            }
            OnFinish(new MachineEventArgs(mem, state, mem.Index, count));
        }
        #endregion
    }

    public class MachineEventArgs : EventArgs
    {
        public Memory<char> Memory { get; set; }

        public string State { get; set; }

        public int Index { get; set; }

        public int Count { get; set; }

        public MachineEventArgs(Memory<char> memory, string state, int index, int count)
        {
            Memory = memory;
            State = state;
            Index = index;
            Count = count;
        }
    }
}
