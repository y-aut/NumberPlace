using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace
{
    [Serializable]
    public struct Cell
    {
        public File X { get; set; }
        public Rank Y { get; set; }
        public int Index
        {
            get => X + Y * Board.CELL_XCNT;
            set
            {
                var val = ((value % Board.CELL_CNT) + Board.CELL_CNT) % Board.CELL_CNT;
                (X, Y) = (val % Board.CELL_XCNT, val / Board.CELL_XCNT);
            }
        }
        public Block Block => X / 3 + Y / 3 * 3;
        public bool IsOk => 0 <= X && X < Board.CELL_XCNT && 0 <= Y && Y < Board.CELL_YCNT;
        public Cell NextCell => new Cell(Index + 1);
        public Cell PrevCell => new Cell(Index - 1);
        public IEnumerable<Cell> Effect => X.Cells.Union(Y.Cells).Union(Block.Cells);

        public Cell(File x, Rank y) { X = x; Y = y; }
        public Cell(int index) { X = 0; Y = 0; Index = index; }

        public static bool operator ==(Cell left, Cell right) => left.X == right.X && left.Y == right.Y;
        public static bool operator !=(Cell left, Cell right) => !(left == right);
        public override bool Equals(object obj) => obj is Cell c && this == c;
        public override int GetHashCode() => Index;

        public override string ToString() => X.ToString() + Y.ToString();
    }

    [Serializable]
    public enum Number
    {
        NUM_1, NUM_2, NUM_3, NUM_4, NUM_5, NUM_6, NUM_7, NUM_8, NUM_9,
        NUM_NB, NUM_ZERO = NUM_1, NUM_NONE = -1,
    }

    [Serializable]
    public abstract class Group
    {
        private readonly int value;

        protected Group(int v) { value = v; }

        public static implicit operator int(Group v) => v.value;

        public abstract IEnumerable<Cell> Cells { get; }

        public static bool operator ==(Group left, Group right)
            => left.GetType() == right.GetType() && left.value == right.value;
        public static bool operator !=(Group left, Group right) => !(left == right);
        public override bool Equals(object obj) => value.Equals(obj);
        public override int GetHashCode() => value.GetHashCode();
        public abstract override string ToString();
    }

    [Serializable]
    public class File : Group
    {
        private File(int v) : base(v) { }
        public static implicit operator File(int v) => new File(v);
        public override IEnumerable<Cell> Cells =>
            Enumerable.Range(0, Board.CELL_YCNT).Select(y => new Cell(this, y));
        public override string ToString() => ((char)('A' + this)).ToString();
    }

    [Serializable]
    public class Rank : Group
    {
        private Rank(int v) : base(v) { }
        public static implicit operator Rank(int v) => new Rank(v);
        public override IEnumerable<Cell> Cells =>
            Enumerable.Range(0, Board.CELL_XCNT).Select(x => new Cell(x, this));
        public override string ToString() => ((char)('1' + this)).ToString();
    }

    [Serializable]
    public class Block : Group
    {
        private Block(int v) : base(v) { }
        public static implicit operator Block(int v) => new Block(v);
        public override IEnumerable<Cell> Cells
        {
            get
            {
                for (int x = 0; x < 3; ++x)
                    for (int y = 0; y < 3; ++y)
                        yield return new Cell(this % 3 * 3 + x, this / 3 * 3 + y);
            }
        }
        public override string ToString() => ((char)('a' + this)).ToString();
    }

}
