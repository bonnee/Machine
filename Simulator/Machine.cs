namespace Simulator;

/*
 * head, tape, table, state(state register)
 */
public class Machine
{
    private readonly Table _table;
    private readonly Head<char> _head;
    private readonly Tape<char> _tape;
    private readonly State _state;
    private int _cycles;

    public event EventHandler<MachineEventArgs> Cycle;

    public event EventHandler<MachineEventArgs> Finish;

    protected virtual void OnCycle(MachineEventArgs e)
    {
        var ev = Cycle;
        if (ev != null)
            ev(nameof(Machine), e);
    }

    /// <summary>
    /// Raises the Finish event
    /// </summary>
    /// <param name="e">The event args</param>
    protected virtual void OnFinish(MachineEventArgs e)
    {
        var ev = Finish;
        if (ev != null)
            ev(nameof(Machine), e);
    }

    public Machine(List<char> symbols, string[] prog)
    {
        _tape = new Tape<char>(symbols);
        _head = new Head<char>(_tape, '_');
        _table = new Table(prog);
        _state = new State();
    }

    /// <summary>
    /// Starts the computation
    /// </summary>
    /// <param name="state">The starting program _state</param>
    /// <param name="delay">The delay to apply to each cycle of the machine</param>
    public void Run(string state = State.ANY, int delay = 0)
    {
        _state.Transition(state);

        while (_state.IsHalted == false)
        {
            var symbol = _head.Read().ToString();
            var instruction = _table.Match(_state.Value, symbol);
            instruction.Execute(_head, _state);

            OnCycle(new MachineEventArgs(_tape.ToString(), _state.Value, _cycles++));

            if (delay > 0)
                Thread.Sleep(delay);
        }

        OnFinish(new MachineEventArgs(_tape.ToString(), _state.Value, _cycles));
    }
}

public class State
{
    public const string ZERO = "0";
    public const string HALTED = "halt";
    public const string ANY = "*";  // wildcard

    private string _value;

    public string Value => _value;

    public State() : this(ZERO) { }

    public State(string val)
    {
        _value = val;
    }

    public void Transition(string stat)
    {
        _value = stat;
    }

    public bool IsHalted => Value == HALTED;
}

public class MachineEventArgs : EventArgs
{
    public string Tape { get; }

    public string State { get; }

    public int Cycles { get; }

    public MachineEventArgs(string tape, string state, int cycles)
    {
        Tape = tape;
        State = state;
        Cycles = cycles;
    }
}