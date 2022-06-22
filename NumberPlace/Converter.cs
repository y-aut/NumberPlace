using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NumberPlace
{
    using BoardNum = CellArray<Number>;

    public static class Converter
    {
        public static string ToString(Cell cell) => cell.ToString();
        public static string ToString(Number num) => num == Number.NUM_NONE ? "." : ((char)('1' + (int)num)).ToString();
        public static string ToString(Group group) => group.ToString();
        public static string ToString(Board board) => board.ToString();
        public static string ToString(BoardNum nums)
        {
            string ans = "";
            foreach (var n in nums.ToArray()) ans += ToString(n);
            return ans;
        }

        public static string ToExcelString(BoardNum nums)
        {
            string ans = "w9h9";
            int space = 0;
            for (int i = 0; i < Board.CELL_CNT; ++i)
            {
                if (nums[i] == Number.NUM_NONE)
                    space++;
                else
                {
                    if (space != 0)
                    {
                        ans += $"s{space}";
                        space = 0;
                    }
                    ans += $"n{ToString(nums[i])}";
                }
            }
            return ans;
        }

        public static string ToTypeString(Group group)
        {
            if (group is File) return "タテ列";
            else if (group is Rank) return "ヨコ列";
            else return "ブロック";
        }

        public static Number ToNumber(char c)
        {
            if (c == '.') return Number.NUM_NONE;
            else if ('1' <= c && c <= '9') return (Number)(c - '1');
            else throw new ArgumentException();
        }

        public static Group ToGroup(char c)
        {
            if ('A' <= c && c < 'A' + Board.GROUP_CNT) return (File)(c - 'A');
            else if ('1' <= c && c < '1' + Board.GROUP_CNT) return (Rank)(c - '1');
            else if ('a' <= c && c < 'a' + Board.GROUP_CNT) return (Block)(c - 'a');
            else throw new ArgumentException();
        }

        public static Cell ToCell(string s)
        {
            if (s.Length != 2) throw new ArgumentException();
            if (ToGroup(s[0]) is File f && ToGroup(s[1]) is Rank r)
                return new Cell(f, r);
            else
                throw new ArgumentException();
        }

        public static Board ToBoard(string s)
        {
            if (s.Length != Board.CELL_CNT) throw new ArgumentException();
            var ans = new Board();
            for (int i = 0; i < Board.CELL_CNT; ++i)
                ans[new Cell(i)] = ToNumber(s[i]);
            return ans;
        }

        // 文字列をがんばって Board に変換する
        public static Board ToBoard_Lax(string s)
        {
            var match = Regex.Match(s, @"[.1-9]+");
            if (match.Success && match.Length >= Board.CELL_CNT)
            {
                return ToBoard(match.Value.Substring(0, Board.CELL_CNT));
            }
            match = Regex.Match(s, @"w9h9[sn0-9]+");
            if (match.Success)
            {
                var str = match.Value.Substring(4);
                int ptr = 0;
                var ans = new Board();
                while (str.Length != 0)
                {
                    match = Regex.Match(str, @"^n[1-9]");
                    if (match.Success)
                    {
                        ans[new Cell(ptr++)] = ToNumber(match.Value[1]);
                        str = str.Substring(match.Length);
                        continue;
                    }
                    match = Regex.Match(str, @"^s[0-9]*");
                    if (match.Success)
                    {
                        ptr += match.Value == "s" ? 1 : int.Parse(match.Value.Substring(1));
                        str = str.Substring(match.Length);
                        continue;
                    }
                    throw new ArgumentException();
                }
                return ans;
            }
            throw new ArgumentException();
        }
    }
}
