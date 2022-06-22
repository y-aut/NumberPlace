#if DEBUG
// 最大１つまで
//#define USE_SETTING_IN_RELEASE      // Release内のsetting.configを使用
//#define COPY_SETTING_TO_RELEASE     // setting.configをReleaseにコピーする
//#define COPY_SETTING_FROM_RELEASE   // setting.configをReleaseからコピーする
#endif

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;

namespace NumberPlace
{
    public static class Setting
    {
        public enum DataType
        {
            FmMain_Rectangle,       // Rectangle
            FmMain_Board,           // Board
            
            // 以下、設定ウィンドウで設定
            EnginePath,             // string
        }

        // 保存先のファイル名
        private static readonly string FileName =
#if !USE_SETTING_IN_RELEASE
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\setting.config";
#else
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).Replace("Debug", "Release") + "\\setting.config";
#endif

        public static string GetFileName() => FileName;

        public static string GetFileName(string str)
        {
            switch (str)
            {
                case "debug":       str = "Debug"; break;
                case "release":     str = "Release"; break;
                default: return null;
            }

            return Path.GetDirectoryName(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)) + "\\" + str + "\\setting.config";
        }

        // 設定をハッシュテーブルに保存
        private static Hashtable Datas;

        // 設定を読み書き
        public static void SetData(DataType dt, object value)
        {
            Datas[dt.ToString()] = value;
        }
        public static object GetData(DataType dt) => Datas[dt.ToString()];

        public static void Save()
        {
            // binファイルに書き込む
            // BinaryFormatterオブジェクトを作成
            BinaryFormatter bf = new BinaryFormatter();
            // ファイルを開く
            FileStream fs;
            if (System.IO.File.Exists(FileName))
                fs = new FileStream(FileName, FileMode.Create);
            else
                fs = System.IO.File.Create(FileName);
            // Seriarizeし、binファイルに保存する
            bf.Serialize(fs, Datas);
            // 閉じる
            fs.Close();
        }

        public static void Load()
        {
#if COPY_SETTING_TO_RELEASE
            System.IO.File.Copy(FileName, FileName.Replace("Debug", "Release"), true);
#elif COPY_SETTING_FROM_RELEASE
            System.IO.File.Copy(FileName.Replace("Debug", "Release"), FileName, true);
#endif

            if (System.IO.File.Exists(FileName))
            {
                // binファイルから読み込む
                // BinaryFormatterオブジェクトを作成
                BinaryFormatter bf = new BinaryFormatter();
                // ファイルを開く
                FileStream fs = new FileStream(FileName, FileMode.Open);
                // binファイルから読み込み、逆シリアル化する
                try
                {
                    Datas = (Hashtable)bf.Deserialize(fs);
                    Arrange();
                }
                catch (Exception)
                {
                    Initialize();
                }
                // 閉じる
                fs.Close();
            }
            else
                Initialize();
        }

        // 設定を初期化
        private static void Initialize()
        {
            Datas = new Hashtable
            {
                { DataType.FmMain_Rectangle.ToString(), new Rectangle(0, 0, 500, 300) },
                { DataType.FmMain_Board.ToString(), new Board() },
                { DataType.EnginePath.ToString(), "" },
            };
        }

        // 設定を整理
        private static void Arrange()
        {
            List<string> setMem = new List<string>(Enum.GetNames(typeof(DataType)));
            // 古いデータをコピー
            Hashtable oldDatas = new Hashtable(Datas);
            Initialize();

            foreach (string i in setMem)
                if (oldDatas.ContainsKey(i)) Datas[i] = oldDatas[i];
        }
    }
}
