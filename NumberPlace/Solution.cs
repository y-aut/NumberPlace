using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace
{
    public abstract class Solution
    {
        protected static readonly Brush Highlight1 = Brushes.Yellow;
        protected static readonly Brush Highlight2 = Brushes.LightSkyBlue;
        protected static readonly Brush Highlight3 = Brushes.LightGreen;
        protected static readonly Brush Highlight4 = Brushes.Plum;
        protected const string Highlight1Str = "黄色";
        protected const string Highlight2Str = "水色";
        protected const string Highlight3Str = "緑色";
        protected const string Highlight4Str = "紫色";

        public virtual bool IsEnd => false;
        public abstract override string ToString();
        public abstract string Description { get; }
        public virtual List<(Cell, Brush)> Colors => new List<(Cell, Brush)>();
        public virtual void Apply(Board board) { }
    }

    public class SolStart : Solution
    {
        public override string ToString() => "解析開始";
        public override string Description => "開始図です。";
    }

    public class SolSolved : Solution
    {
        public override bool IsEnd => true;
        public override string ToString() => "解析終了";
        public override string Description => "完成図です。";
    }

    public class SolGiveUp : Solution
    {
        public override bool IsEnd => true;
        public override string ToString() => "解析中止";
        public override string Description => "これ以上は解けません。";
    }

    public class SolAloneNum : Solution
    {
        private Cell cell;
        private Number num;

        public SolAloneNum(Cell c, Number n) { cell = c; num = n; }

        public override string ToString() => $"[{Converter.ToString(cell)}] 消去法(数): {Converter.ToString(num)}";
        public override string Description => $"このマスには {Converter.ToString(num)} しか入りません。";
        public override List<(Cell, Brush)> Colors =>
            new List<(Cell, Brush)> { (cell, Highlight1) };
        public override void Apply(Board board)
        {
            board[cell] = num;
        }
    }

    public class SolAloneNumInGroup : Solution
    {
        private Cell cell;
        private Number num;
        private Group group;

        public SolAloneNumInGroup(Cell c, Number n, Group g) { cell = c; num = n; group = g; }

        public override string ToString() =>
            $"[{Converter.ToString(cell)}] 消去法({Converter.ToTypeString(group)}): {Converter.ToString(num)}";
        public override string Description
            => $"この{Converter.ToTypeString(group)}で {Converter.ToString(num)} はこのマスにしか入りません。";
        public override List<(Cell, Brush)> Colors =>
            group.Cells.Select(c => (c, Highlight2))
            .Union(new List<(Cell, Brush)> { (cell, Highlight1) })
            .ToList();
        public override void Apply(Board board)
        {
            board[cell] = num;
        }
    }

    public class SolOccupy : Solution
    {
        private List<Cell> cells;
        private NumList nums;
        private Group group;
        // cells * 2 が group の空きマスの個数を超えるとき、逆にした方が良い
        private readonly bool reverted;

        public SolOccupy(Board board, List<Cell> c, NumList n, Group g)
        {
            group = g;

            var empty = group.Cells.Where(i => board[i] == Number.NUM_NONE);
            if (reverted = c.Count * 2 > empty.Count())
            {
                cells = empty.Except(c).ToList();
                nums = empty.Aggregate(NumList.Zero, (nums, cell) => nums | board.Cand[cell]) ^ n;
            }
            else
            {
                cells = c;
                nums = n;
            }
        }

        public override string ToString()
        {
            if (reverted)
                return $"[{string.Join(",", cells.Select(i => Converter.ToString(i)))}] " +
                    $"逆{cells.Count}国同盟: {string.Join(",", nums.ToList().Select(i => Converter.ToString(i)))}";
            else
                return $"[{string.Join(",", cells.Select(i => Converter.ToString(i)))}] " +
                    $"{cells.Count}国同盟: ^{string.Join(",", nums.ToList().Select(i => Converter.ToString(i)))}";
        }
            
        public override string Description
        {
            get
            {
                if (reverted)
                    return $"この{Converter.ToTypeString(group)}で " +
                        string.Join(",", nums.ToList().Select(i => Converter.ToString(i))) +
                        $" が入るのはこれらのマスだけです。よって、これらのマスに他の数字は入りません。";
                else
                    return $"これらのマスには " +
                        string.Join(",", nums.ToList().Select(i => Converter.ToString(i))) +
                        $" のいずれかが入ります。よって、この{Converter.ToTypeString(group)}の他のマスにこれらの数字は入りません。";
            }
        }
        
        public override List<(Cell, Brush)> Colors =>
            group.Cells.Select(c => (c, Highlight2))
            .Union(cells.Select(c => (c, Highlight1)))
            .ToList();

        public override void Apply(Board board)
        {
            if (reverted)
            {
                foreach (var cell in cells)
                    board.Cand[cell] &= nums;
            }
            else
            {
                foreach (var cell in group.Cells.Except(cells))
                    board.Cand[cell] &= ~nums;
            }
        }
    }

    public class SolReserve : Solution
    {
        private List<Cell> cells;
        private Number num;
        private Group group1;
        private Group group2;

        public SolReserve(List<Cell> c, Number n, Group g1, Group g2)
        {
            cells = c;
            num = n;
            group1 = g1;
            group2 = g2;
        }

        public override string ToString()
            => $"[{string.Join(",", cells.Select(i => Converter.ToString(i)))}] " +
            $"予約({Converter.ToTypeString(group1)}→{Converter.ToTypeString(group2)}): " +
            $"{Converter.ToString(num)}";
        public override string Description
            => $"この{Converter.ToTypeString(group1)}で、{Converter.ToString(num)} はこれらのマスにしか入りません。" +
            $"よって、この{Converter.ToTypeString(group2)}の残りのマスには {Converter.ToString(num)} は入りません。";
        public override List<(Cell, Brush)> Colors =>
            group1.Cells.Select(c => (c, Highlight2))
            .Union(group2.Cells.Select(c => (c, Highlight3)))
            .Union(cells.Select(c => (c, Highlight1)))
            .ToList();
        public override void Apply(Board board)
        {
            foreach (var cell in group2.Cells.Except(cells))
            {
                var tmp = board.Cand[cell];
                tmp[num] = false;
                board.Cand[cell] = tmp;
            }
        }
    }

    public class SolXWing : Solution
    {
        private Number num;
        private List<Group> group1;
        private List<Group> group2;

        public SolXWing(Number n, List<Group> g1, List<Group> g2)
        {
            num = n;
            group1 = g1;
            group2 = g2;
        }

        public override string ToString()
            => $"[{string.Join(",", group2.Select(i => Converter.ToString(i)))}] " +
            $"X-Wing: {string.Join(",", group1.Select(i => Converter.ToString(i)))}";
        public override string Description
            => $"これらの{Converter.ToTypeString(group1[0])}で、{Converter.ToString(num)} はこれらのマスにしか入りません。" +
            $"よって、これらの{Converter.ToTypeString(group2[0])}の残りのマスには {Converter.ToString(num)} は入りません。";
        public override List<(Cell, Brush)> Colors =>
            group1.SelectMany(i => i.Cells).Select(c => (c, Highlight2))
            .Union(group2.SelectMany(i => i.Cells).Select(c => (c, Highlight3)))
            .Union(group1.SelectMany(i => i.Cells).Intersect(group2.SelectMany(i => i.Cells)).Select(c => (c, Highlight1)))
            .ToList();
        public override void Apply(Board board)
        {
            foreach (var cell in group2.SelectMany(i => i.Cells)
                .Except(group1.SelectMany(i => i.Cells).Intersect(group2.SelectMany(i => i.Cells))))
            {
                var tmp = board.Cand[cell];
                tmp[num] = false;
                board.Cand[cell] = tmp;
            }
        }
    }

    public class SolXYChain : Solution
    {
        private List<Cell> chain;
        private List<Number> nums;

        public SolXYChain(List<Cell> c, List<Number> n)
        {
            chain = c;
            nums = n;
        }

        public override string ToString()
            => $"[{Converter.ToString(chain[0])},{Converter.ToString(chain.Last())}] " +
            $"XY-Chain(完全): {string.Join("-", chain.Select(i => Converter.ToString(i)))}";
        public override string Description
            => $"これらのマスについて、考えうる数字の入れ方は二通りありますが、" +
            $"いずれにせよこれらの影響領域に特定の数字は入りません。";
        public override List<(Cell, Brush)> Colors => GetColors();

        private List<(Cell, Brush)> GetColors()
        {
            var ans = new List<(Cell, Brush)>();
            for (int i = 0; i < chain.Count; ++i)
            {
                int ip = (i + 1) % chain.Count;
                ans.AddRange(chain[i].Effect.Intersect(chain[ip].Effect).Select(c => (c, Highlight2)));
            }
            return ans.Union(chain.Select(c => (c, Highlight1))).ToList();
        }

        public override void Apply(Board board)
        {
            for (int i = 0; i < chain.Count; ++i)
            {
                int ip = (i + 1) % chain.Count;
                foreach (var cell in chain[i].Effect.Intersect(chain[ip].Effect)
                    .Except(new List<Cell>() { chain[i], chain[ip] }))
                {
                    var tmp = board.Cand[cell];
                    tmp[nums[ip]] = false;
                    board.Cand[cell] = tmp;
                }
            }
        }
    }

    public class SolXYChainDisc : Solution
    {
        private List<Cell> chain;
        private List<Number> nums;

        public SolXYChainDisc(List<Cell> c, List<Number> n)
        {
            chain = c;
            nums = n;
        }

        public override string ToString()
            => $"[{Converter.ToString(chain[0])},{Converter.ToString(chain.Last())}] " +
            $"XY-Chain: {string.Join("-", chain.Select(i => Converter.ToString(i)))}";
        public override string Description
            => $"これらのマスに注目すると、{Highlight3Str}のマスの一方に {Converter.ToString(nums[0])} が" +
            $"入らないとき、もう一方には {Converter.ToString(nums[0])} が入ることがわかります。" +
            $"よって、これらのマスの共通の影響領域には {Converter.ToString(nums[0])} は入りません。";
        public override List<(Cell, Brush)> Colors =>
            chain[0].Effect.Intersect(chain.Last().Effect).Select(c => (c, Highlight2))
            .Union(chain.Select(c => (c, Highlight1)))
            .Union(new List<(Cell, Brush)> { (chain[0], Highlight3), (chain.Last(), Highlight3) })
            .ToList();
        public override void Apply(Board board)
        {
            foreach (var cell in chain[0].Effect.Intersect(chain.Last().Effect))
            {
                var tmp = board.Cand[cell];
                tmp[nums[0]] = false;
                board.Cand[cell] = tmp;
            }
        }
    }

    public class SolSimpleChain : Solution
    {
        private Number num;
        private List<Cell> chain;

        public SolSimpleChain(Number n, List<Cell> c)
        {
            num = n;
            chain = c;
        }

        public override string ToString()
            => $"[{Converter.ToString(chain[0])},{Converter.ToString(chain.Last())}] " +
            $"Simple Chain: {Converter.ToString(num)}, {string.Join("-", chain.Select(i => Converter.ToString(i)))}";
        public override string Description
            => $"これらのマスに注目すると、{Highlight3Str}のマスの一方に {Converter.ToString(num)} が" +
            $"入らないとき、もう一方には {Converter.ToString(num)} が入ることがわかります。" +
            $"よって、これらのマスの共通の影響領域には {Converter.ToString(num)} は入りません。";
        public override List<(Cell, Brush)> Colors =>
            chain[0].Effect.Intersect(chain.Last().Effect).Select(c => (c, Highlight2))
            .Union(chain.Select(c => (c, Highlight1)))
            .Union(new List<(Cell, Brush)> { (chain[0], Highlight3), (chain.Last(), Highlight3) })
            .ToList();
        public override void Apply(Board board)
        {
            foreach (var cell in chain[0].Effect.Intersect(chain.Last().Effect))
            {
                var tmp = board.Cand[cell];
                tmp[num] = false;
                board.Cand[cell] = tmp;
            }
        }
    }

    public class SolHamada : Solution
    {
        private Number num;
        private Group grp;
        private List<Cell> chain;

        public SolHamada(Number n, Group g, List<Cell> c)
        {
            num = n;
            grp = g;
            chain = c;
        }

        public override string ToString()
            => $"[{Converter.ToString(chain[0])}] " +
            $"浜田ロジック: {Converter.ToString(num)}, {string.Join("-", chain.Select(i => Converter.ToString(i)))}";
        public override string Description
            => $"{Highlight1Str}のマスに {Converter.ToString(num)} を入れると、" +
            $"{Highlight3Str}のマスには{(chain.Count > 3 ? "全て" : "")} {Converter.ToString(num)} が入りますが、" +
            $"このとき{Highlight4Str}の{Converter.ToTypeString(grp)}には " +
            $"{Converter.ToString(num)} が入らなくなります。" +
            $"よって、{Highlight1Str}のマスには {Converter.ToString(num)} は入りません。";
        public override List<(Cell, Brush)> Colors =>
            chain.Select((c, i) => i % 2 == 0 ? (c, Highlight3) : (c, Highlight2))
            .Union(new List<(Cell, Brush)> { (chain[0], Highlight1) })
            .Union(grp.Cells.Select(c => (c, Highlight4)))
            .ToList();
        public override void Apply(Board board)
        {
            var tmp = board.Cand[chain[0]];
            tmp[num] = false;
            board.Cand[chain[0]] = tmp;
        }
    }

}
