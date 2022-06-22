using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumberPlace
{
    public static class Startup
    {
        public static FmMain Fm_Main;
        public static FmDebug Fm_Debug;
        public static FmSetting Fm_Setting;
        public static Usi USI;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SetInstance();
            using (USI = new Usi())
            {
                Application.Run(Fm_Main);
            }
        }

        static void SetInstance()
        {
            Fm_Debug = new FmDebug();
            Fm_Main = new FmMain();
            Fm_Setting = new FmSetting();
        }

        public static void LoadSettings()
        {
            Setting.Load();

            USI.Start();

            Fm_Main.LoadSettings();
        }

        public static void SaveSettings()
        {
            if (Fm_Main.Visible && Fm_Main.WindowState == FormWindowState.Normal)
            {
                Setting.SetData(Setting.DataType.FmMain_Rectangle, new Rectangle(Fm_Main.Location, Fm_Main.Size));
            }
            Setting.SetData(Setting.DataType.FmMain_Board, Fm_Main.Board);

            Setting.Save();
        }
    }
}
