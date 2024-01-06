namespace Simulator;

public class Table
{
    private readonly CodeMatch<string, string, string[]> codeDictionary;
    private readonly string comment = ";";

    /// <param name="program">The code splitted by lines</param>
    public Table(string[] program)
    {
        codeDictionary = new CodeMatch<string, string, string[]>();

        foreach(var line in program)
        {
            if (line.StartsWith(comment)) 
                continue;

            var tmp = line.Split(' ');
            if (tmp.Length != 5) 
                continue;

            if (!codeDictionary.ContainsKey(tmp[0]))
            {
                codeDictionary.Add(tmp[0], new Dictionary<string, string[]>());
            }

            codeDictionary[tmp[0]].Add(tmp[1], tmp.Skip(2).ToArray());
        }
    }

    /// <param name="program">The code splitted by lines</param>
    /// <param name="comment">The comment delimiter to use</param>
    public Table(string[] program, string comment) : this(program)
    {
        this.comment = comment;
    }

    /// <summary>
    /// Matches the right command line to the actual program state
    /// </summary>
    /// <param name="state">The program state to match</param>
    /// <param name="cell">The indexed tape cell</param>
    /// <returns>The right command line</returns>
    public Instruction Match(string state, string cell)
    {
        if (cell == " ")
        {
            cell = "_";
        }

        if (!codeDictionary.TryGetValue(state, out var right))
            throw new KeyNotFoundException("Match not found in state '" + state + "' for cell '" + cell + "'");

        if (right.TryGetValue(cell, out var cmd)) 
            return new Instruction(cmd);

        try
        {
            return new Instruction(right["*"]);
        }
        catch (KeyNotFoundException)
        {
            throw new KeyNotFoundException("Match not found in state '" + state + "' for cell '" + cell + "'");
        }
    }
}

public class Instruction
{
    private readonly string[] command;

    public Instruction(string[] cmd)
    {
        if (cmd?.Length != 3)
        {
            throw new ArgumentException(nameof(cmd));
        }

        this.command = cmd;
    }

    public void Execute(Head<char> head, State state)
    {
        if (command[0] != "*")
            head.Write(Convert.ToChar(command[0] == "_" ? " " : command[0]));

        if (command[1] == "r")
            head.MoveRight();
        else if (command[1] == "l")
            head.MoveLeft();

        state.Transition(command[2]);
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