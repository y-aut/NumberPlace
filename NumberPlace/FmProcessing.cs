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
    public partial class FmProcessing : Form
    {
        private static FmProcessing fm;
        private Action stop;

        public static new bool Visible => !(fm is null);

        public static string Title
        {
            get => fm.Text;
            set => fm.Text = value;
        }

        public static string Caption
        {
            get => fm.LblCaption.Text;
            set => fm.LblCaption.Text = value;
        }

        public static string Description
        {
            get => fm.LblDesc.Text;
            set => fm.LblDesc.Text = value;
        }

        public FmProcessing()
        {
            InitializeComponent();
        }

        public static void Show(IWin32Window owner, string caption, string description, string title,
            Action stopAction)
        {
            if (!(fm is null)) throw new Exception("既に開かれています。");

            fm = new FmProcessing();
            fm.LblCaption.Text = caption;
            fm.LblDesc.Text = description;
            fm.Text = title;
            fm.stop = stopAction;

            fm.Show(owner);
        }

        public static new void Close()
        {
            if (fm is null) return;

            if (fm.InvokeRequired)
            {
                fm.Invoke(new Action(() => Close()));
                return;
            }

            fm.Hide();
            fm.Dispose();
            fm = null;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            BtnStop.Enabled = false;
            stop();
        }

        private void FmProcessing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }
    }
}
