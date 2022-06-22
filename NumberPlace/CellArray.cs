using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace
{
    [Serializable]
    public class CellArray<T>
    {
        private readonly T[] data;

        public CellArray()
        {
            data = new T[Board.CELL_CNT];
        }
        
        public CellArray(T def)
        {
            data = Enumerable.Repeat(def, Board.CELL_CNT).ToArray();
        }

        public CellArray(Func<Cell, T> fun)
        {
            data = new T[Board.CELL_CNT];
            for (int i = 0; i < Board.CELL_CNT; ++i)
                data[i] = fun(new Cell(i));
        }

        public CellArray(CellArray<T> src)
        {
            data = new T[Board.CELL_CNT];
            Array.Copy(src.data, data, Board.CELL_CNT);
        }

        public T this[Cell cell]
        {
            get => data[cell.Index];
            set => data[cell.Index] = value;
        }

        public T this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }

        public T this[File x, Rank y]
        {
            get => this[new Cell(x, y)];
            set => this[new Cell(x, y)] = value;
        }

        public void SetValue(T val)
        {
            for (int i = 0; i < Board.CELL_CNT; ++i)
                data[i] = val;
        }

        public void CopyFrom(CellArray<T> src)
        {
            Array.Copy(src.data, data, Board.CELL_CNT);
        }

        public T[] ToArray() => data;
    }
}
