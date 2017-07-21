using System;
using System.Collections.Generic;

namespace Machine
{
public class Memory<T> : ICollection<T>
    {
        private List<T> cells;
        private int size;

        private int index;
        private T defaultState;
        
        public Memory(ICollection<T> mem, defState)
        {
            index = 0;
            defaultState=defState;
            cells = new List<T>(mem);
        }

        public T moveRight()
        {
            if (index == Count - 1)
                cells.Add(defaultState);

            index++;

            return cells[index];
        }

        public T moveLeft()
        {
            if (index == 0)
                cells.Insert(0, defaultState);
            else
                index--;

            return cells[index];
        }

        public T Read()
        {
            return cells[index];
        }

        public void Write(T item)
        {
            cells[index]=item;
        }
    }
}