namespace NumberPlace
{
    partial class FmMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmMain));
            this.TsMain = new System.Windows.Forms.ToolStrip();
            this.TsdFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.TsbReadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbWriteFile = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbCopyNC = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbCopyExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbCloseWithoutSaving = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbClose = new System.Windows.Forms.ToolStripMenuItem();
            this.TsdEdit = new System.Windows.Forms.ToolStripDropDownButton();
            this.TsbEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbDeleteAnswer = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbApplyAnswer = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbEraseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.TsdAnalyze = new System.Windows.Forms.ToolStripDropDownButton();
            this.TsbAnalyze = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbSearchAll = new System.Windows.Forms.ToolStripMenuItem();
            this.TsdDisplay = new System.Windows.Forms.ToolStripDropDownButton();
            this.TsbShowCand = new System.Windows.Forms.ToolStripMenuItem();
            this.TsdWindow = new System.Windows.Forms.ToolStripDropDownButton();
            this.TsbDebugWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbSetting = new System.Windows.Forms.ToolStripButton();
            this.PcbBoard = new System.Windows.Forms.PictureBox();
            this.LsbSolution = new System.Windows.Forms.ListBox();
            this.TxbDesc = new System.Windows.Forms.TextBox();
            this.TsbMakeProblem = new System.Windows.Forms.ToolStripMenuItem();
            this.TsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PcbBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // TsMain
            // 
            this.TsMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.TsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsdFile,
            this.TsdEdit,
            this.TsdAnalyze,
            this.TsdDisplay,
            this.TsdWindow,
            this.TsbSetting});
            this.TsMain.Location = new System.Drawing.Point(0, 0);
            this.TsMain.Name = "TsMain";
            this.TsMain.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.TsMain.Size = new System.Drawing.Size(728, 34);
            this.TsMain.TabIndex = 0;
            this.TsMain.Text = "toolStrip1";
            // 
            // TsdFile
            // 
            this.TsdFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsdFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbReadFile,
            this.TsbWriteFile,
            this.TsbCopy,
            this.TsbPaste,
            this.toolStripSeparator1,
            this.TsbCloseWithoutSaving,
            this.TsbClose});
            this.TsdFile.Image = ((System.Drawing.Image)(resources.GetObject("TsdFile.Image")));
            this.TsdFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsdFile.Name = "TsdFile";
            this.TsdFile.Size = new System.Drawing.Size(81, 29);
            this.TsdFile.Text = "ファイル";
            this.TsdFile.EnabledChanged += new System.EventHandler(this.Tsd_EnabledChanged);
            // 
            // TsbReadFile
            // 
            this.TsbReadFile.Name = "TsbReadFile";
            this.TsbReadFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.TsbReadFile.Size = new System.Drawing.Size(340, 34);
            this.TsbReadFile.Text = "問題を読み込み...";
            this.TsbReadFile.Click += new System.EventHandler(this.TsbReadFile_Click);
            // 
            // TsbWriteFile
            // 
            this.TsbWriteFile.Name = "TsbWriteFile";
            this.TsbWriteFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.TsbWriteFile.Size = new System.Drawing.Size(340, 34);
            this.TsbWriteFile.Text = "問題を保存...";
            this.TsbWriteFile.Click += new System.EventHandler(this.TsbWriteFile_Click);
            // 
            // TsbCopy
            // 
            this.TsbCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbCopyNC,
            this.TsbCopyExcel});
            this.TsbCopy.Name = "TsbCopy";
            this.TsbCopy.Size = new System.Drawing.Size(340, 34);
            this.TsbCopy.Text = "コピー";
            this.TsbCopy.Click += new System.EventHandler(this.TsbCopyNC_Click);
            // 
            // TsbCopyNC
            // 
            this.TsbCopyNC.Name = "TsbCopyNC";
            this.TsbCopyNC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.TsbCopyNC.Size = new System.Drawing.Size(303, 34);
            this.TsbCopyNC.Text = "NumChar 形式";
            this.TsbCopyNC.Click += new System.EventHandler(this.TsbCopyNC_Click);
            // 
            // TsbCopyExcel
            // 
            this.TsbCopyExcel.Name = "TsbCopyExcel";
            this.TsbCopyExcel.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.TsbCopyExcel.Size = new System.Drawing.Size(303, 34);
            this.TsbCopyExcel.Text = "Excel 形式";
            this.TsbCopyExcel.Click += new System.EventHandler(this.TsbCopyExcel_Click);
            // 
            // TsbPaste
            // 
            this.TsbPaste.Name = "TsbPaste";
            this.TsbPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.TsbPaste.Size = new System.Drawing.Size(340, 34);
            this.TsbPaste.Text = "貼り付け";
            this.TsbPaste.Click += new System.EventHandler(this.TsbPaste_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(337, 6);
            // 
            // TsbCloseWithoutSaving
            // 
            this.TsbCloseWithoutSaving.Name = "TsbCloseWithoutSaving";
            this.TsbCloseWithoutSaving.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.TsbCloseWithoutSaving.Size = new System.Drawing.Size(340, 34);
            this.TsbCloseWithoutSaving.Text = "保存せずに終了";
            this.TsbCloseWithoutSaving.Click += new System.EventHandler(this.TsbCloseWithoutSaving_Click);
            // 
            // TsbClose
            // 
            this.TsbClose.Name = "TsbClose";
            this.TsbClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.TsbClose.Size = new System.Drawing.Size(340, 34);
            this.TsbClose.Text = "終了";
            this.TsbClose.Click += new System.EventHandler(this.TsbClose_Click);
            // 
            // TsdEdit
            // 
            this.TsdEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsdEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbEdit,
            this.TsbDeleteAnswer,
            this.TsbApplyAnswer,
            this.TsbEraseAll});
            this.TsdEdit.Image = ((System.Drawing.Image)(resources.GetObject("TsdEdit.Image")));
            this.TsdEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsdEdit.Name = "TsdEdit";
            this.TsdEdit.Size = new System.Drawing.Size(66, 29);
            this.TsdEdit.Text = "編集";
            this.TsdEdit.EnabledChanged += new System.EventHandler(this.Tsd_EnabledChanged);
            // 
            // TsbEdit
            // 
            this.TsbEdit.Name = "TsbEdit";
            this.TsbEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.TsbEdit.Size = new System.Drawing.Size(270, 34);
            this.TsbEdit.Text = "編集開始";
            this.TsbEdit.Click += new System.EventHandler(this.TsbEdit_Click);
            // 
            // TsbDeleteAnswer
            // 
            this.TsbDeleteAnswer.Name = "TsbDeleteAnswer";
            this.TsbDeleteAnswer.Size = new System.Drawing.Size(270, 34);
            this.TsbDeleteAnswer.Text = "解答を削除";
            this.TsbDeleteAnswer.Click += new System.EventHandler(this.TsbEraseAnswer_Click);
            // 
            // TsbApplyAnswer
            // 
            this.TsbApplyAnswer.Name = "TsbApplyAnswer";
            this.TsbApplyAnswer.Size = new System.Drawing.Size(270, 34);
            this.TsbApplyAnswer.Text = "解答を適用";
            this.TsbApplyAnswer.Click += new System.EventHandler(this.TsbApplyAnswer_Click);
            // 
            // TsbEraseAll
            // 
            this.TsbEraseAll.Name = "TsbEraseAll";
            this.TsbEraseAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.TsbEraseAll.Size = new System.Drawing.Size(270, 34);
            this.TsbEraseAll.Text = "全て削除";
            this.TsbEraseAll.Click += new System.EventHandler(this.TsbEraseAll_Click);
            // 
            // TsdAnalyze
            // 
            this.TsdAnalyze.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsdAnalyze.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbAnalyze,
            this.TsbSearchAll,
            this.TsbMakeProblem});
            this.TsdAnalyze.Image = ((System.Drawing.Image)(resources.GetObject("TsdAnalyze.Image")));
            this.TsdAnalyze.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsdAnalyze.Name = "TsdAnalyze";
            this.TsdAnalyze.Size = new System.Drawing.Size(66, 29);
            this.TsdAnalyze.Text = "解析";
            this.TsdAnalyze.EnabledChanged += new System.EventHandler(this.Tsd_EnabledChanged);
            // 
            // TsbAnalyze
            // 
            this.TsbAnalyze.Name = "TsbAnalyze";
            this.TsbAnalyze.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.TsbAnalyze.Size = new System.Drawing.Size(293, 34);
            this.TsbAnalyze.Text = "解析";
            this.TsbAnalyze.Click += new System.EventHandler(this.TsbAnalyze_Click);
            // 
            // TsbSearchAll
            // 
            this.TsbSearchAll.Name = "TsbSearchAll";
            this.TsbSearchAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.TsbSearchAll.Size = new System.Drawing.Size(293, 34);
            this.TsbSearchAll.Text = "全解探索";
            this.TsbSearchAll.Click += new System.EventHandler(this.TsbSearchAll_Click);
            // 
            // TsdDisplay
            // 
            this.TsdDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsdDisplay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbShowCand});
            this.TsdDisplay.Image = ((System.Drawing.Image)(resources.GetObject("TsdDisplay.Image")));
            this.TsdDisplay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsdDisplay.Name = "TsdDisplay";
            this.TsdDisplay.Size = new System.Drawing.Size(66, 29);
            this.TsdDisplay.Text = "表示";
            this.TsdDisplay.EnabledChanged += new System.EventHandler(this.Tsd_EnabledChanged);
            // 
            // TsbShowCand
            // 
            this.TsbShowCand.CheckOnClick = true;
            this.TsbShowCand.Name = "TsbShowCand";
            this.TsbShowCand.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.TsbShowCand.Size = new System.Drawing.Size(263, 34);
            this.TsbShowCand.Text = "候補を表示";
            this.TsbShowCand.Click += new System.EventHandler(this.TsbShowCand_Click);
            // 
            // TsdWindow
            // 
            this.TsdWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsdWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbDebugWindow});
            this.TsdWindow.Image = ((System.Drawing.Image)(resources.GetObject("TsdWindow.Image")));
            this.TsdWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsdWindow.Name = "TsdWindow";
            this.TsdWindow.Size = new System.Drawing.Size(93, 29);
            this.TsdWindow.Text = "ウィンドウ";
            this.TsdWindow.EnabledChanged += new System.EventHandler(this.Tsd_EnabledChanged);
            // 
            // TsbDebugWindow
            // 
            this.TsbDebugWindow.Name = "TsbDebugWindow";
            this.TsbDebugWindow.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.TsbDebugWindow.Size = new System.Drawing.Size(296, 34);
            this.TsbDebugWindow.Text = "デバッグウィンドウ";
            this.TsbDebugWindow.Click += new System.EventHandler(this.TsbDebugWindow_Click);
            // 
            // TsbSetting
            // 
            this.TsbSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbSetting.Image = ((System.Drawing.Image)(resources.GetObject("TsbSetting.Image")));
            this.TsbSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbSetting.Name = "TsbSetting";
            this.TsbSetting.Size = new System.Drawing.Size(52, 29);
            this.TsbSetting.Text = "設定";
            this.TsbSetting.Click += new System.EventHandler(this.TsbSetting_Click);
            // 
            // PcbBoard
            // 
            this.PcbBoard.Location = new System.Drawing.Point(12, 37);
            this.PcbBoard.Name = "PcbBoard";
            this.PcbBoard.Size = new System.Drawing.Size(312, 233);
            this.PcbBoard.TabIndex = 1;
            this.PcbBoard.TabStop = false;
            this.PcbBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PcbBoard_MouseClick);
            // 
            // LsbSolution
            // 
            this.LsbSolution.FormattingEnabled = true;
            this.LsbSolution.ItemHeight = 23;
            this.LsbSolution.Location = new System.Drawing.Point(450, 37);
            this.LsbSolution.Name = "LsbSolution";
            this.LsbSolution.Size = new System.Drawing.Size(258, 188);
            this.LsbSolution.TabIndex = 2;
            this.LsbSolution.SelectedIndexChanged += new System.EventHandler(this.LsbSolution_SelectedIndexChanged);
            // 
            // TxbDesc
            // 
            this.TxbDesc.Location = new System.Drawing.Point(458, 419);
            this.TxbDesc.Multiline = true;
            this.TxbDesc.Name = "TxbDesc";
            this.TxbDesc.ReadOnly = true;
            this.TxbDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxbDesc.Size = new System.Drawing.Size(258, 99);
            this.TxbDesc.TabIndex = 3;
            // 
            // TsbMakeProblem
            // 
            this.TsbMakeProblem.Name = "TsbMakeProblem";
            this.TsbMakeProblem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.TsbMakeProblem.Size = new System.Drawing.Size(293, 34);
            this.TsbMakeProblem.Text = "問題作成";
            this.TsbMakeProblem.Click += new System.EventHandler(this.TsbMakeProblem_Click);
            // 
            // FmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(728, 530);
            this.Controls.Add(this.TxbDesc);
            this.Controls.Add(this.LsbSolution);
            this.Controls.Add(this.PcbBoard);
            this.Controls.Add(this.TsMain);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "FmMain";
            this.Text = "ナンバープレース";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmMain_FormClosing);
            this.Load += new System.EventHandler(this.FmMain_Load);
            this.SizeChanged += new System.EventHandler(this.FmMain_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FmMain_KeyDown);
            this.TsMain.ResumeLayout(false);
            this.TsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PcbBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip TsMain;
        private System.Windows.Forms.ToolStripDropDownButton TsdFile;
        private System.Windows.Forms.ToolStripDropDownButton TsdWindow;
        private System.Windows.Forms.ToolStripButton TsbSetting;
        private System.Windows.Forms.ToolStripMenuItem TsbDebugWindow;
        private System.Windows.Forms.ToolStripMenuItem TsbCloseWithoutSaving;
        private System.Windows.Forms.ToolStripMenuItem TsbClose;
        private System.Windows.Forms.PictureBox PcbBoard;
        private System.Windows.Forms.ToolStripDropDownButton TsdEdit;
        private System.Windows.Forms.ToolStripDropDownButton TsdAnalyze;
        private System.Windows.Forms.ToolStripDropDownButton TsdDisplay;
        private System.Windows.Forms.ToolStripMenuItem TsbShowCand;
        private System.Windows.Forms.ListBox LsbSolution;
        private System.Windows.Forms.TextBox TxbDesc;
        private System.Windows.Forms.ToolStripMenuItem TsbAnalyze;
        private System.Windows.Forms.ToolStripMenuItem TsbSearchAll;
        private System.Windows.Forms.ToolStripMenuItem TsbEdit;
        private System.Windows.Forms.ToolStripMenuItem TsbDeleteAnswer;
        private System.Windows.Forms.ToolStripMenuItem TsbApplyAnswer;
        private System.Windows.Forms.ToolStripMenuItem TsbReadFile;
        private System.Windows.Forms.ToolStripMenuItem TsbWriteFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem TsbCopy;
        private System.Windows.Forms.ToolStripMenuItem TsbPaste;
        private System.Windows.Forms.ToolStripMenuItem TsbEraseAll;
        private System.Windows.Forms.ToolStripMenuItem TsbCopyNC;
        private System.Windows.Forms.ToolStripMenuItem TsbCopyExcel;
        private System.Windows.Forms.ToolStripMenuItem TsbMakeProblem;
    }
}

