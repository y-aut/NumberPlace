using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumberPlace
{
    public enum StateEnum
    {
        Normal, Analyze, SearchAll, MakeProblem,
    }

    public partial class FmMain : Form
    {
        // 罫線の太さ
        private const float GRID_WIDTH = 1f;
        // 太枠の太さ
        private const float GRID_BOLD_WIDTH = 2f;
        // コントロール間の余白
        private const int CONTROL_XMARGIN = 0;
        private const int CONTROL_YMARGIN = 10;
        // コントロールと壁との余白
        private const int MARGIN_LEFT = 20;
        private const int MARGIN_RIGHT = 10;
        private const int MARGIN_HEIGHT = 36;
        // リストの最低幅
        private const int LISTBOX_MINWIDTH = 100;
        // テキストボックスの高さ
        private const int TEXTBOX_HEIGHT = 50;

        private bool flgSave = true;
        private bool disableEvent = true;
        private List<(Solution sol, Board board)> Sols = new List<(Solution sol, Board board)>();
        private List<Board> Answers = new List<Board>();

        public StateEnum State { get; set; }

        public Board Board { get; set; }
        public bool ShowCand { get; set; }

        private bool _editting = false;
        private bool Editting
        {
            get => _editting;
            set
            {
                if (_editting = value)
                {
                    ClearList();
                    Board.SetEmptyCellTypes(NumType.Problem);
                }
                AllowAnalyze = !value;
                TsbDeleteAnswer.Enabled = TsbApplyAnswer.Enabled = TsbEraseAll.Enabled = value;
                EditFocus = new Cell(0);
            }
        }

        public bool AllowAnalyze
        {
            set
            {
                TsdAnalyze.Enabled = value;
            }
        }

        public bool Analyzing
        {
            set
            {
                TsdFile.Enabled = TsdEdit.Enabled = TsdAnalyze.Enabled = TsbSetting.Enabled = !value;
                Editting = false;
            }
        }

        private Cell _editFocus;
        private Cell EditFocus
        {
            get => _editFocus;
            set
            {
                _editFocus = value;
                DrawBoard();
                Refresh();
            }
        }

        public bool Debug
        {
            get => Startup.Fm_Debug.Visible;
            set
            {
                Startup.Fm_Debug.Visible = TsbDebugWindow.Checked = value;
            }
        }

        private float CellSize => (ClientSize.Height - MARGIN_HEIGHT) / Board.CELL_YCNT;

        public FmMain()
        {
            InitializeComponent();
        }

        public void LoadSettings()
        {
            var rect = (Rectangle)Setting.GetData(Setting.DataType.FmMain_Rectangle);
            Location = rect.Location;
            Size = rect.Size;
            Board = (Board)Setting.GetData(Setting.DataType.FmMain_Board);
        }

        private void FmMain_Load(object sender, EventArgs e)
        {
            Startup.LoadSettings();
            ResizeBoard();
            Debug = false;
            Editting = false;
            State = StateEnum.Normal;
        }

        private void DrawBoard()
        {
            Solution sol = null;
            if (Sols.Count != 0 && LsbSolution.SelectedIndex != -1)
                sol = Sols[LsbSolution.SelectedIndex].sol;

            using (var grp = Graphics.FromImage(PcbBoard.Image))
            {
                grp.Clear(Color.White);
                DrawBack(grp, sol);
                DrawGrid(grp);
                DrawNumbers(grp);
            }
        }

        private RectangleF CellRect(Cell cell)
            => new RectangleF(CellSize * cell.X, CellSize * cell.Y, CellSize, CellSize);

        private void DrawGrid(Graphics grp)
        {
            var gridPen = new Pen(Color.Black, GRID_WIDTH);
            var gridBoldPen = new Pen(Color.Black, GRID_BOLD_WIDTH);

            for (int i = 0; i <= Board.CELL_XCNT; ++i)
            {
                grp.DrawLine(i % 3 == 0 ? gridBoldPen : gridPen,
                    CellSize * i, 0, CellSize * i, CellSize * Board.CELL_YCNT);
            }

            for (int i = 0; i <= Board.CELL_YCNT; ++i)
            {
                grp.DrawLine(i % 3 == 0 ? gridBoldPen : gridPen,
                    0, CellSize * i, CellSize * Board.CELL_XCNT, CellSize * i);
            }
        }

        private void DrawBack(Graphics grp, Solution sol = null)
        {
            if (Editting)
            {
                grp.FillRectangle(Brushes.Yellow, CellRect(EditFocus));
                return;
            }

            if (!(sol is null))
            {
                foreach (var (cell, brush) in sol.Colors)
                {
                    grp.FillRectangle(brush, CellRect(cell));
                }
            }
        }

        private static Brush[] typeBrush =
        {
            Brushes.Black,      // Problem
            Brushes.Blue,       // Answer
        };

        private void DrawNumbers(Graphics grp)
        {
            const float FONTSIZE_SCALE = 0.5f;
            const float CAND_FONTSIZE_SCALE = 0.195f;
            const float DEL_Y_SCALE = 0.09f;
            const float CAND_DEL_Y_SCALE = 0.035f;

            var font = new Font("Yu Gothic", CellSize * FONTSIZE_SCALE, FontStyle.Bold);
            var candFont = new Font("Yu Gothic", CellSize * CAND_FONTSIZE_SCALE, FontStyle.Bold);
            var format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
            };

            for (File x = 0; x < Board.CELL_XCNT; ++x)
                for (Rank y = 0; y < Board.CELL_YCNT; ++y)
                {
                    var cell = new Cell(x, y);
                    if (Board[cell] != Number.NUM_NONE)
                    {
                        grp.DrawString(Converter.ToString(Board[cell]), font, typeBrush[(int)Board.Types[cell]],
                            new RectangleF(CellSize * x, CellSize * (y + DEL_Y_SCALE), CellSize, CellSize),
                            format);
                    }
                    else if (ShowCand)
                    {
                        foreach (var i in Board.Cand[cell].ToList())
                        {
                            grp.DrawString(Converter.ToString(i), candFont, Brushes.Gray,
                                new RectangleF(
                                    CellSize * x + CellSize / 3f * ((int)i % 3),
                                    CellSize * (y + CAND_DEL_Y_SCALE) + CellSize / 3f * ((int)i / 3),
                                    CellSize / 3f, CellSize / 3f),
                                format);
                        }
                    }
                }
        }

        private void TsbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TsbCloseWithoutSaving_Click(object sender, EventArgs e)
        {
            flgSave = false;
            Close();
        }

        private void FmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flgSave)
                Startup.SaveSettings();
        }

        private void TsbSetting_Click(object sender, EventArgs e)
        {
            SettingChanged(() => Startup.Fm_Setting.ShowDialog());
        }

        // 設定ウィンドウでする設定が変わるかもしれない動作を行うときに呼び出す
        private void SettingChanged(Action action)
        {
            action();
        }

        private void FmMain_SizeChanged(object sender, EventArgs e)
        {
            if (disableEvent) return;

            if (Width < MinimumSize.Width || Height < MinimumSize.Height)
            {
                return;
            }

            ResizeBoard();
        }

        private void ResizeBoard()
        {
            disableEvent = true;

            if (PcbBoard.Image != null) PcbBoard.Image.Dispose();
            PcbBoard.Image = new Bitmap(
                (int)Math.Ceiling(CellSize * Board.CELL_XCNT),
                (int)Math.Ceiling(CellSize * Board.CELL_YCNT));
            PcbBoard.Size = PcbBoard.Image.Size;

            int listSize = Math.Max(LISTBOX_MINWIDTH,
                ClientSize.Width - PcbBoard.Width - CONTROL_XMARGIN - MARGIN_LEFT - MARGIN_RIGHT);

            LsbSolution.Left = MARGIN_LEFT + PcbBoard.Width + CONTROL_XMARGIN;
            LsbSolution.Size = new Size(listSize, PcbBoard.Height - TEXTBOX_HEIGHT - CONTROL_YMARGIN);
            TxbDesc.Location = new Point(LsbSolution.Left, LsbSolution.Bottom + CONTROL_YMARGIN);
            TxbDesc.Size = new Size(listSize, PcbBoard.Bottom - TxbDesc.Top);

            ClientSize = new Size(
                PcbBoard.Width + listSize + CONTROL_XMARGIN + MARGIN_LEFT + MARGIN_RIGHT, ClientSize.Height);
            disableEvent = false;

            DrawBoard();
            Refresh();
        }

        private void TsbShowCand_Click(object sender, EventArgs e)
        {
            ShowCand = TsbShowCand.Checked;
            DrawBoard();
            Refresh();
        }

        private void TsbEdit_Click(object sender, EventArgs e)
        {
            Editting = !Editting;
            TsbEdit.Text = Editting ? "編集終了" : "編集開始";
        }

        private void PcbBoard_MouseClick(object sender, MouseEventArgs e)
        {
            var cell = new Cell((int)(e.X / CellSize), (int)(e.Y / CellSize));
            if (cell.IsOk && Editting)
            {
                EditFocus = cell;
            }
        }

        private void FmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Editting) return;

            if (Keys.D1 <= e.KeyCode && e.KeyCode <= Keys.D9)
            {
                Board[EditFocus] = (Number)(e.KeyCode - Keys.D1);
                Board.Types[EditFocus] = NumType.Problem;
                EditFocus = EditFocus.NextCell;
            }
            else if (Keys.NumPad1 <= e.KeyCode && e.KeyCode <= Keys.NumPad9)
            {
                Board[EditFocus] = (Number)(e.KeyCode - Keys.NumPad1);
                Board.Types[EditFocus] = NumType.Problem;
                EditFocus = EditFocus.NextCell;
            }
            else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                if (Board[EditFocus] != Number.NUM_NONE)
                {
                    Board[EditFocus] = Number.NUM_NONE;
                    Board.Types[EditFocus] = NumType.Problem;
                    EditFocus = EditFocus;
                }
                else
                {
                    Board[EditFocus.PrevCell] = Number.NUM_NONE;
                    Board.Types[EditFocus.PrevCell] = NumType.Problem;
                    EditFocus = EditFocus.PrevCell;
                }
            }
            else if (Keys.Left <= e.KeyCode && e.KeyCode <= Keys.Down)
            {
                // Left, Up, Right, Down
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        EditFocus = EditFocus.PrevCell;
                        break;
                    case Keys.Right:
                        EditFocus = EditFocus.NextCell;
                        break;
                    case Keys.Up:
                        EditFocus = new Cell(EditFocus.Index - Board.CELL_XCNT);
                        break;
                    case Keys.Down:
                        EditFocus = new Cell(EditFocus.Index + Board.CELL_XCNT);
                        break;
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                EditFocus = EditFocus.NextCell;
            }
        }

        private void TsbDebugWindow_Click(object sender, EventArgs e)
        {
            Debug = !Debug;
        }

        private void ClearList()
        {
            LsbSolution.Items.Clear();
            Sols.Clear();
            Answers.Clear();
        }

        private void TsbAnalyze_Click(object sender, EventArgs e)
        {
            State = StateEnum.Analyze;
            Analyzing = true;
            ClearList();
            Board.SetEmptyCellTypes(NumType.Answer);
            Board.EraseAnswer();
            Startup.USI.Consider(Board);
            AddSolution(new SolStart());
        }

        private void TsbSearchAll_Click(object sender, EventArgs e)
        {
            State = StateEnum.SearchAll;
            Analyzing = true;
            ClearList();
            Board.SetEmptyCellTypes(NumType.Answer);
            Board.EraseAnswer();
            Startup.USI.SearchAll(Board);
            FmProcessing.Show(this, "全解探索中...", "0 個の解が見つかりました。", "全解探索",
                () => Startup.USI.Stop());
        }

        private void TsbMakeProblem_Click(object sender, EventArgs e)
        {
            State = StateEnum.MakeProblem;
            Analyzing = true;
            Startup.USI.MakeProblem(new Board());
            FmProcessing.Show(this, "問題作成中...", "問題を作成しています。", "問題作成",
                () => Startup.USI.Stop());
        }

        public void AddAns(Board board)
        {
            if (LsbSolution.InvokeRequired)
            {
                LsbSolution.Invoke(new Action<Board>(AddAns), board);
                return;
            }

            LsbSolution.Items.Add("解" + (LsbSolution.Items.Count + 1));
            Answers.Add(board);
            if (FmProcessing.Visible)
            {
                FmProcessing.Description = $"{Answers.Count} 個の解が見つかりました。";
            }
        }

        public void AddSolution(Solution sol)
        {
            if (LsbSolution.InvokeRequired)
            {
                LsbSolution.Invoke(new Action<Solution>(AddSolution), sol);
                return;
            }

            sol.Apply(Board);
            LsbSolution.Items.Add(sol.ToString());
            Sols.Add((sol, new Board(Board)));
            if (sol.IsEnd)
            {
                State = StateEnum.Normal;
                Analyzing = false;
                DrawBoard();
                Refresh();
            }
        }

        private void LsbSolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LsbSolution.SelectedIndex == -1) return;

            if (Sols.Count != 0)
            {
                var (sol, board) = Sols[LsbSolution.SelectedIndex];
                TxbDesc.Text = sol.Description;
                Board = board;
            }
            else
            {
                Board = Answers[LsbSolution.SelectedIndex];
            }
            DrawBoard();
            Refresh();
        }

        private void Tsd_EnabledChanged(object sender, EventArgs e)
        {
            // 子アイテムの Enabled を全て変更
            var tsd = (ToolStripDropDownButton)sender;
            foreach (ToolStripItem item in tsd.DropDownItems)
            {
                item.Enabled = tsd.Enabled;
            }
        }

        private void TsbEraseAnswer_Click(object sender, EventArgs e)
        {
            Board.EraseAnswer();
            Board.SetCellTypes(NumType.Problem);
            DrawBoard();
            Refresh();
        }

        private void TsbApplyAnswer_Click(object sender, EventArgs e)
        {
            Board.SetCellTypes(NumType.Problem);
            DrawBoard();
            Refresh();
        }

        private void TsbWriteFile_Click(object sender, EventArgs e)
        {
            var fm = new SaveFileDialog()
            {
                FileName = "問題1.txt",
                Filter = "テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*",
                Title = "問題を保存",
                RestoreDirectory = true,
            };

            if (fm.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(fm.FileName, Converter.ToString(Board.GetProblem()));
            }
        }

        private void TsbReadFile_Click(object sender, EventArgs e)
        {
            var fm = new OpenFileDialog()
            {
                Filter = "テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*",
                Title = "問題を読み込み",
                RestoreDirectory = true,
            };

            if (fm.ShowDialog() == DialogResult.OK)
            {
                var str = System.IO.File.ReadAllText(fm.FileName);
                try
                {
                    Board = Converter.ToBoard_Lax(str);
                    DrawBoard();
                    Refresh();
                }
                catch
                {
                    MessageBox.Show("問題の読み込みに失敗しました。", "問題を読み込み", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TsbPaste_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText()) return;

            try
            {
                Board = Converter.ToBoard_Lax(Clipboard.GetText());
                DrawBoard();
                Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("クリップボードからの読み込みに失敗しました。", "貼り付け", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TsbEraseAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("数字を全て削除してもよろしいですか？", "全て削除", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Board.EraseAll();
                DrawBoard();
                Refresh();
            }
        }

        private void TsbCopyNC_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Converter.ToString(Board.GetProblem()));
        }

        private void TsbCopyExcel_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Converter.ToExcelString(Board.GetProblem()));
        }

        public void EndProcess(bool canceled)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(c => EndProcess(c)), canceled);
                return;
            }

            if (State == StateEnum.SearchAll)
            {
                MessageBox.Show(this, $"全部で {Answers.Count} 個の解が見つかりました。",
                    "解析" + (canceled ? "中止" : "終了"));
            }
            else if (State == StateEnum.MakeProblem)
            {
                MessageBox.Show(this, "問題の作成を中止しました。", "作成中止");
            }
            State = StateEnum.Normal;
            Analyzing = false;
        }

        public void SetMadeProblem(Board board)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Board>(b => SetMadeProblem(b)), board);
                return;
            }

            ClearList();
            Board = board;
            DrawBoard();
            Refresh();

            MessageBox.Show(this, $"問題を作成しました。", "作成終了");
            State = StateEnum.Normal;
            Analyzing = false;
        }
    }
}
