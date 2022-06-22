using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace
{
    using BoardNum = CellArray<Number>;

    [Serializable]
    public enum NumType
    {
        Problem, Answer,
    }

    [Serializable]
    public class Board
    {
        public const int NUM_CNT = 9;
        public const int GROUP_CNT = 9;

        public const int CELL_XCNT = 9;
        public const int CELL_YCNT = 9;
        public const int CELL_CNT = CELL_XCNT * CELL_YCNT;

        private BoardNum Num { get; }
        public CellArray<NumList> Cand { get; }
        public CellArray<NumType> Types { get; }

        public Board()
        {
            Num = new BoardNum(Number.NUM_NONE);
            Cand = new CellArray<NumList>(NumList.All);
            Types = new CellArray<NumType>(NumType.Problem);
        }

        public Board(Board src)
        {
            Num = new BoardNum(src.Num);
            Cand = new CellArray<NumList>(src.Cand);
            Types = new CellArray<NumType>(src.Types);
        }

        public Board(BoardNum nums)
        {
            Num = nums;
            Cand = new CellArray<NumList>(NumList.All);
            Types = new CellArray<NumType>(NumType.Problem);

            ReviveCandAll();
            SetEmptyCellTypes(NumType.Answer);
        }

        public Number this[Cell cell]
        {
            get => Num[cell];
            set
            {
                Num[cell] = value;
                if (value == Number.NUM_NONE)
                    ReviveCandAround(cell);
                else
                    EraseCandAround(cell);
            }
        }

        // 問題部分のみを取得
        public BoardNum GetProblem()
            => new BoardNum(c => Types[c] == NumType.Problem ? this[c] : Number.NUM_NONE);

        // 特定のセルに関係する候補を削除
        private void EraseCandAround(Cell cell)
        {
            Cand[cell] = NumList.Zero;
            foreach (var g in new Group[] { cell.X, cell.Y, cell.Block })
                foreach (var c in g.Cells)
                {
                    var tmp = Cand[c];
                    tmp[this[cell]] = false;
                    Cand[c] = tmp;
                }
        }

        // 特定のセルの候補を再計算
        private void ReviveCand(Cell cell)
        {
            if (this[cell] != Number.NUM_NONE)
            {
                Cand[cell] = NumList.Zero;
                return;
            }

            var nums = NumList.All;
            foreach (var g in new Group[] { cell.X, cell.Y, cell.Block })
                foreach (var c in g.Cells)
                {
                    if (this[c] != Number.NUM_NONE)
                        nums[this[c]] = false;
                }
            Cand[cell] = nums;
        }

        // 特定のセルに関係する候補を再計算
        private void ReviveCandAround(Cell cell)
        {
            foreach (var g in new Group[] { cell.X, cell.Y, cell.Block })
                foreach (var c in g.Cells)
                    ReviveCand(c);
        }

        // 全セルの候補を再計算
        private void ReviveCandAll()
        {
            for (int i = 0; i < CELL_CNT; ++i)
                ReviveCand(new Cell(i));
        }

        // 空白のセルの Type を変更
        public void SetEmptyCellTypes(NumType type)
        {
            for (int i = 0; i < CELL_CNT; ++i)
            {
                if (Num[i] == Number.NUM_NONE)
                    Types[i] = type;
            }
        }

        // 全セルの Type を変更
        public void SetCellTypes(NumType type)
        {
            Types.SetValue(type);
        }

        // 全セルを削除
        public void EraseAll()
        {
            Num.SetValue(Number.NUM_NONE);
            Cand.SetValue(NumList.All);
            Types.SetValue(NumType.Problem);
        }

        // 解答を削除
        public void EraseAnswer()
        {
            for (int i = 0; i < CELL_CNT; ++i)
            {
                if (Types[i] != NumType.Problem)
                    Num[i] = Number.NUM_NONE;
            }
            ReviveCandAll();
        }

        public override string ToString() => Converter.ToString(Num);
    }
}
