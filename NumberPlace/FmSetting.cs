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
    public partial class FmSetting : Form
    {
        private bool FlgChanged;

        public FmSetting()
        {
            InitializeComponent();
        }

        private void FmSetting_Shown(object sender, EventArgs e)
        {
            TxbEngineLocation.Text = (string)Setting.GetData(Setting.DataType.EnginePath);

            FlgChanged = false;
        }

        private void SaveAndHide(bool FlgSave)
        {
            if (FlgSave)
            {
                if (TxbEngineLocation.Text != (string)Setting.GetData(Setting.DataType.EnginePath))
                {
                    MessageBox.Show("思考エンジンの場所の設定は、次回起動時に反映されます。", "設定");
                    Setting.SetData(Setting.DataType.EnginePath, TxbEngineLocation.Text);
                }
            }

            Hide();
        }

        private void FmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                if (FlgChanged)
                {
                    if (MessageBox.Show("現在の変更は保存されませんがよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                        return;
                }
                SaveAndHide(false);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            SaveAndHide(true);
        }

        private void BtnCencel_Click(object sender, EventArgs e)
        {
            SaveAndHide(false);
        }

        private void BtnEngineLocation_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                FileName = "NumberPlaceSolver.exe",
                Title = "思考エンジンを選択",
                Filter = "実行可能ファイル (*.exe)|*.exe|すべてのファイル (*.*)|*.*",
                RestoreDirectory = true,
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    TxbEngineLocation.Text = ofd.FileName;
                }
            }
        }
    }
}
