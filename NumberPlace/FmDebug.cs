using System;
using System.IO;
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
    public partial class FmDebug : Form
    {
        // 現在入力されているコマンドのindex
        int command_index = 0;

        public FmDebug()
        {
            InitializeComponent();
        }

        private void FmDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Startup.Fm_Main.Debug = false;
            }
        }

        private void BtnErase_Click(object sender, EventArgs e)
        {
            TxbMain.Clear();
        }

        public void ReadLineWithPrint(string str)
        {
            SetText("<:" + str + "\r\n");
        }

        public void WriteLineWithPrint(ref System.Diagnostics.Process prs, string str)
        {
            prs.StandardInput.WriteLine(str);
            SetText(">:" + str + "\r\n");
        }

        public void Print(string str)
        {
            SetText("- " + str + "\r\n");
        }

        private void SetText(string str)
        {
            if (TxbMain.InvokeRequired)
                Invoke(new Action<string>(SetText), str);
            else
            {
                TxbMain.Text += str;
                if (Visible)
                {
                    TxbMain.Select(TxbMain.Text.Length, 0);
                    TxbMain.ScrollToCaret();
                }
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (TxbCommand.Text != "")
            {
                // cmdから始まる文字列はUIが処理する
                if (TxbCommand.Text.Length >= 3 && TxbCommand.Text.Substring(0, 3) == "cmd")
                {
                    SetText("UI>:" + TxbCommand.Text + "\r\n");
                    DoCommand(TxbCommand.Text);
                }
                else
                    Startup.USI.WriteLine(TxbCommand.Text);
                TxbCommand.Clear();
            }
        }

        private void TxbCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                BtnSend_Click(sender, e);
                command_index = 0;
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                string str = GetPreviousCommand(++command_index);
                if (str != "")
                {
                    TxbCommand.Text = str;
                    TxbCommand.Select(str.Length, 0);
                }
                else
                    --command_index;
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                if (command_index > 1)
                {
                    string str = GetPreviousCommand(--command_index);
                    TxbCommand.Text = str;
                    TxbCommand.Select(str.Length, 0);
                }
                else if (command_index == 1)
                {
                    --command_index;
                    TxbCommand.Text = "";
                }
            }
        }

        private void TxbCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;
        }

        private string GetPreviousCommand(int index /* 1から */)
        {
            // 最後からindex番目に送ったコマンドを探す
            int cnt = 0;

            var cmds = TxbMain.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = cmds.Length - 1; i >= 0; --i)
            {
                if (cmds[i].Length > 2 && cmds[i].Substring(0, 2) == ">:")
                {
                    if (++cnt == index)
                        return cmds[i].Substring(2);
                }
                else if (cmds[i].Length > 4 && cmds[i].Substring(0, 4) == "UI>:")
                {
                    if (++cnt == index)
                        return cmds[i].Substring(4);
                }
            }
            return "";
        }

        private void ShowCommand()
        {
            string str = "";
            str += "UI<:cmd clearset:\r\n"
                + "UI<:\tReset all the settings.\r\n"
                + "UI<:\tTo reflect this command, you need to close this application without saving.\r\n";
            str += "UI<:cmd copyfrom (debug|release):\r\n"
                + "UI<:\tCopy setting.config from the counterpart in the directory of the specified version.\r\n"
                + "UI<:\tTo reflect this command, you need to close this application without saving.\r\n";

            SetText(str);
        }

        private void DoCommand(string cmd)
        {
            var cmds = cmd.Split(' ');

            if (cmd == "cmd")
            {
                ShowCommand();
                return;
            }

            switch (cmds[1])
            {
                case "clearset":
                    {
                        string filename = Setting.GetFileName();
                        if (System.IO.File.Exists(filename))
                        {
                            System.IO.File.Delete(filename);
                            SetText("UI<:success\r\n");
                        }
                        else
                            SetText("UI<:failure: setting file does not exist.\r\n");
                        break;
                    }

                case "copyfrom":
                    {
                        if (cmds.Length < 3)
                            ShowCommand();
                        else
                        {
                            string sourcefile = Setting.GetFileName();
                            string filename = Setting.GetFileName(cmds[2]);
                            if (filename == null)
                                SetText("UI<:unsupported command:" + cmds[2] + "\r\n");
                            else if (!System.IO.File.Exists(filename))
                                SetText("UI<:failure: setting file does not exist.\r\n");
                            else if (sourcefile == filename)
                                SetText("UI<:failure: the source file and the destination file is same.\r\n");
                            else
                            {
                                System.IO.File.Copy(filename, Setting.GetFileName(), true);
                                SetText("UI<:success\r\n");
                            }
                        }
                        break;
                    }

                default:
                    SetText("UI<:unsupported command:" + cmds[1] + "\r\n");
                    break;
            }
        }
    }
}
