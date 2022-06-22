using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace NumberPlace
{
    public class Usi : IDisposable
    {
#if DEBUG
        const bool WINDOW_VISIBLE = true;
#else
        const bool WINDOW_VISIBLE = false;
#endif

        private Process prs;

        // エンジンが稼働しているか
        public bool UsiWorking { get; private set; } = false;

        public Usi()
        {
        }

        public void Dispose()
        {
            Close();
        }

        public void Start()
        {
            string filename = (string)Setting.GetData(Setting.DataType.EnginePath);

            // ファイルが存在するか
            if (filename == "")
            {
                MessageBox.Show("思考エンジンのファイルが設定されていません。\n「設定」からファイルの場所を指定して下さい。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Startup.Fm_Main.AllowAnalyze = false;
            }
            else if (!System.IO.File.Exists(filename))
            {
                MessageBox.Show("思考エンジンのファイルが存在しません。\n「設定」からファイルの場所を指定し直して下さい。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Startup.Fm_Main.AllowAnalyze = false;
            }
            else
            {
                prs = new Process();

                // 入力できるようにする
                prs.StartInfo.UseShellExecute = false;
                prs.StartInfo.RedirectStandardInput = true;

                // 非同期で出力を読み取れるようにする
                prs.StartInfo.RedirectStandardOutput = true;
                prs.OutputDataReceived += Prs_OutputDataReceived;

                prs.StartInfo.FileName = filename;
                // ワーキングディレクトリを指定
                prs.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(filename);

                prs.StartInfo.CreateNoWindow = !WINDOW_VISIBLE;

                // 起動
                prs.Start();

                // 非同期で出力の読み取りを開始
                prs.BeginOutputReadLine();

                // USIエンジンかどうか
                if (!AwakeUsi())
                {
                    MessageBox.Show("指定された思考エンジンのファイルが正しくありません。\n「設定」からファイルの場所を指定し直して下さい。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    Startup.Fm_Main.AllowAnalyze = false;
                }
            }
        }

        private void Prs_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            // OutputDataReceivedイベントハンドラ
            // 行が出力されるたびに呼び出される

            if (e.Data == "") return;

            Startup.Fm_Debug.ReadLineWithPrint(e.Data);
            ReadInfo(e.Data);
        }

        private void Close()
        {
            if (prs != null)
            {
                WriteLine("quit");
                prs.Close();
                prs.Dispose();
                prs = null;
            }
        }

        public void WriteLine(string str)
        {
            Startup.Fm_Debug.WriteLineWithPrint(ref prs, str);
        }

        private bool AwakeUsi()
        {
            WriteLine("usi");

            for (int i = 0; i < 100 && !UsiWorking; ++i)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }

            return UsiWorking;
        }

        private void Setoption()
        {
            // 設定を送信
        }

        public void Consider(Board board)
        {
            Setoption();
            Setposition(board);
            WriteLine("solve");
        }

        public void SearchAll(Board board)
        {
            Setoption();
            Setposition(board);
            WriteLine("solve all");
        }

        public void MakeProblem(Board board)
        {
            Setoption();
            Setposition(board);
            WriteLine("make");
        }

        private void Setposition(Board board)
        {
            WriteLine("problem " + Converter.ToString(board));
        }

        public void Stop()
        {
            WriteLine("stop");
        }

        // 送信されたコマンドを処理
        private void ReadInfo(string str)
        {
            // 終了時はnullになる
            if (str == null) return;

            var cmds = str.Split(' ');

            switch (cmds[0])
            {
                case "sol":
                    ReadSolution(cmds);
                    break;

                case "solved":
                    Startup.Fm_Main.AddSolution(new SolSolved());
                    break;

                case "giveup":
                    Startup.Fm_Main.AddSolution(new SolGiveUp());
                    break;

                case "ans":
                    ReadAns(cmds);
                    break;

                case "thatsall":
                    FmProcessing.Close();
                    Startup.Fm_Main.EndProcess(false);
                    break;

                case "stopped":
                    FmProcessing.Close();
                    Startup.Fm_Main.EndProcess(true);
                    break;

                case "made":
                    FmProcessing.Close();
                    Startup.Fm_Main.SetMadeProblem(Converter.ToBoard(cmds[1]));
                    break;

                case "info":
                    break;

                case "NumberPlaceSolver_usiok":
                    UsiWorking = true;
                    break;
            }
        }

        private void ReadAns(string[] cmds)
        {
            try
            {
                var board = Converter.ToBoard(cmds[1]);
                board.Types.CopyFrom(Startup.Fm_Main.Board.Types);
                Startup.Fm_Main.AddAns(board);
            }
            catch
            {
                Startup.Fm_Debug.Print("Invalid answer.");
            }
        }

        private void ReadSolution(string[] cmds)
        {
            try
            {
                Solution sol;
                switch (cmds[1])
                {
                    case "alonenum":
                        {
                            var cell = Converter.ToCell(cmds[2]);
                            var num = Converter.ToNumber(cmds[3][0]);
                            sol = new SolAloneNum(cell, num);
                            break;
                        }

                    case "alone":
                        {
                            var cell = Converter.ToCell(cmds[2]);
                            var num = Converter.ToNumber(cmds[3][0]);
                            var group = Converter.ToGroup(cmds[4][0]);
                            sol = new SolAloneNumInGroup(cell, num, group);
                            break;
                        }

                    case "occupy":
                        {
                            var group = Converter.ToGroup(cmds[2][0]);
                            Debug.Assert(cmds[3] == "num");
                            var nums = new NumList(Collect(cmds, 4, s => Converter.ToNumber(s[0]), out int end));
                            Debug.Assert(cmds[end] == "sq");
                            var cells = Collect(cmds, end + 1, Converter.ToCell, out _);
                            sol = new SolOccupy(Startup.Fm_Main.Board, cells, nums, group);
                            break;
                        }

                    case "reserve":
                        {
                            var group1 = Converter.ToGroup(cmds[2][0]);
                            var group2 = Converter.ToGroup(cmds[3][0]);
                            Debug.Assert(cmds[4] == "num");
                            var num = Converter.ToNumber(cmds[5][0]);
                            Debug.Assert(cmds[6] == "sq");
                            var cells = Collect(cmds, 7, Converter.ToCell, out _);
                            sol = new SolReserve(cells, num, group1, group2);
                            break;
                        }

                    case "xwing":
                        {
                            var num = Converter.ToNumber(cmds[2][0]);
                            Debug.Assert(cmds[3] == "src");
                            var group1 = Collect(cmds, 4, i => Converter.ToGroup(i[0]), out int end);
                            Debug.Assert(cmds[end] == "tar");
                            var group2 = Collect(cmds, end + 1, i => Converter.ToGroup(i[0]), out _);
                            sol = new SolXWing(num, group1, group2);
                            break;
                        }

                    case "xychain":
                        {
                            var chain = Collect(cmds, 2, Converter.ToCell, out int end);
                            Debug.Assert(cmds[end] == "num");
                            var nums = Collect(cmds, end + 1, i => Converter.ToNumber(i[0]), out _);
                            sol = new SolXYChain(chain, nums);
                            break;
                        }

                    case "xychain_disc":
                        {
                            var chain = Collect(cmds, 2, Converter.ToCell, out int end);
                            Debug.Assert(cmds[end] == "num");
                            var nums = Collect(cmds, end + 1, i => Converter.ToNumber(i[0]), out _);
                            sol = new SolXYChainDisc(chain, nums);
                            break;
                        }

                    case "simple_chain":
                        {
                            var chain = Collect(cmds, 2, Converter.ToCell, out int end);
                            Debug.Assert(cmds[end] == "num");
                            var num = Converter.ToNumber(cmds[end + 1][0]);
                            sol = new SolSimpleChain(num, chain);
                            break;
                        }

                    case "hamada":
                        {
                            var chain = Collect(cmds, 2, Converter.ToCell, out int end);
                            Debug.Assert(cmds[end] == "num");
                            var num = Converter.ToNumber(cmds[end + 1][0]);
                            Debug.Assert(cmds[end + 2] == "group");
                            var grp = Converter.ToGroup(cmds[end + 3][0]);
                            sol = new SolHamada(num, grp, chain);
                            break;
                        }

                    default:
                        throw new ArgumentException();
                }
                Startup.Fm_Main.AddSolution(sol);
            }
            catch
            {
                Startup.Fm_Debug.Print("Invalid solution.");
            }
        }

        private List<T> Collect<T>(string[] cmds, int start, Func<string, T> converter, out int end)
        {
            var list = new List<T>();
            int count = int.Parse(cmds[start]);
            for (int i = 1; i <= count; ++i)
            {
                list.Add(converter(cmds[start + i]));
            }
            end = start + count + 1;
            return list;
        }
    }
}
