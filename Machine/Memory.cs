using System.Collections.Generic;

namespace Emulator
{
    public class Memory<T>
    {
        private readonly List<T> cells;
        private int index;
        private readonly T empty;

        /// <param name="memory">The initial memory state</param>
        /// <param name="emptyCell">The empty cell state. Used when expanding memory</param>
        public Memory(List<T> memory, T emptyCell)
        {
            index = 0;
            empty = emptyCell;
            cells = new List<T>(memory);
        }

        /// <summary>
        /// Moves the index to the right.
        /// </summary>
        /// <returns>The current cell</returns>
        public T MoveRight()
        {
            if (index == cells.Count - 1)
                cells.Add(empty);

            return cells[++index];
        }

        /// <summary>
        /// Moves the index to the left
        /// </summary>
        /// <returns>The current cell</returns>
        public T MoveLeft()
        {
            if (index == 0)
                cells.Insert(0, empty);
            else
                index--;

            return Read();
        }

        public T Read()
        {
            return cells[index];
        }

        public void Write(T item)
        {
            cells[index] = item;
        }

        public override string ToString()
        {
            return cells.ToString();
        }

        public T[] ToArray()
        {
            return cells.ToArray();
        }
    }
}