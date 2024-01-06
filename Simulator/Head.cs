using System.Collections;

namespace Simulator;

public class Head<T>
{
    private readonly Tape<T> _tape;
    private int _index;
    private readonly T _empty;

    /// <param name="tape">paper tape</param>
    /// <param name="emptyCell">The empty cell state. Used when expanding tape</param>
    public Head(Tape<T> tape, T emptyCell)
    {
        _index = 0;
        _empty = emptyCell;
        _tape = tape;
    }

    /// <summary>
    /// Moves the _index to the right.
    /// </summary>
    /// <returns>The current cell</returns>
    public void MoveRight()
    {
        if (_index == _tape.Count - 1)
            _tape.Add(_empty);

        _index++;
    }

    /// <summary>
    /// Moves the _index to the left
    /// </summary>
    /// <returns>The current cell</returns>
    public void MoveLeft()
    {
        if (_index == 0)
            _tape.Insert(0, _empty);
        else
            _index--;
    }

    public T Read() => _tape[_index];

    public void Write(T item)
    {
        _tape[_index] = item;
    }
}

public class Tape<T> : IEnumerable
{
    private readonly List<T> _cells;

    /// <summary>
    /// tape
    /// </summary>
    /// <param name="symbols">The initial tape state</param>
    public Tape(List<T> symbols)
    {
        _cells = symbols;
    }

    public int Count => _cells.Count;

    public T this[int index]
    {
        get => _cells[index];
        set => _cells[index] = value;
    }

    public void Add(T item)
    {
        _cells.Add(item);
    }

    public void Insert(int index, T item)
    {
        _cells.Insert(index, item);
    }

    public override string ToString() => string.Join(string.Empty, _cells);

    IEnumerator IEnumerable.GetEnumerator()
    {
        foreach (var item in _cells)
        {
            yield return item;
        }
    }
}