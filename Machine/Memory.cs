using System;
using System.Collections.Generic;

namespace Machine
{
    public class Memory<T>
    {
        private List<T> cells;
        private int index;
        public int Index
        {
            get
            {
                return index;
            }
        }
        private T empty;

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The current cell</returns>
        public T Read()
        {
            return cells[index];
        }

        /// <summary>
        /// Writes to the current cell
        /// </summary>
        /// <param name="item">The new cell value</param>
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