namespace NumberPlace
{
    partial class FmDebug
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblCommand = new System.Windows.Forms.Label();
            this.BtnErase = new System.Windows.Forms.Button();
            this.BtnSend = new System.Windows.Forms.Button();
            this.TxbCommand = new System.Windows.Forms.TextBox();
            this.TxbMain = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LblCommand
            // 
            this.LblCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblCommand.AutoSize = true;
            this.LblCommand.Location = new System.Drawing.Point(10, 329);
            this.LblCommand.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LblCommand.Name = "LblCommand";
            this.LblCommand.Size = new System.Drawing.Size(41, 15);
            this.LblCommand.TabIndex = 9;
            this.LblCommand.Text = "コマンド";
            // 
            // BtnErase
            // 
            this.BtnErase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnErase.Location = new System.Drawing.Point(480, 321);
            this.BtnErase.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.BtnErase.Name = "BtnErase";
            this.BtnErase.Size = new System.Drawing.Size(90, 30);
            this.BtnErase.TabIndex = 7;
            this.BtnErase.Text = "ログ消去";
            this.BtnErase.UseVisualStyleBackColor = true;
            this.BtnErase.Click += new System.EventHandler(BtnErase_Click);
            // 
            // BtnSend
            // 
            this.BtnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSend.Location = new System.Drawing.Point(398, 321);
            this.BtnSend.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(72, 30);
            this.BtnSend.TabIndex = 6;
            this.BtnSend.Text = "送信";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(BtnSend_Click);
            // 
            // TxbCommand
            // 
            this.TxbCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxbCommand.Location = new System.Drawing.Point(56, 325);
            this.TxbCommand.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.TxbCommand.Name = "TxbCommand";
            this.TxbCommand.Size = new System.Drawing.Size(332, 23);
            this.TxbCommand.TabIndex = 5;
            this.TxbCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(TxbCommand_KeyDown);
            this.TxbCommand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxbCommand_KeyPress);
            // 
            // TxbMain
            // 
            this.TxbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxbMain.BackColor = System.Drawing.Color.White;
            this.TxbMain.Location = new System.Drawing.Point(0, 0);
            this.TxbMain.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.TxbMain.Multiline = true;
            this.TxbMain.Name = "TxbMain";
            this.TxbMain.ReadOnly = true;
            this.TxbMain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxbMain.Size = new System.Drawing.Size(584, 313);
            this.TxbMain.TabIndex = 8;
            this.TxbMain.WordWrap = false;
            // 
            // FmDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.LblCommand);
            this.Controls.Add(this.BtnErase);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.TxbCommand);
            this.Controls.Add(this.TxbMain);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Name = "FmDebug";
            this.Text = "デバッグウィンドウ";
            this.ResumeLayout(false);
            this.PerformLayout();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FmDebug_FormClosing);

        }

        private void BtnSend_Click1(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Label LblCommand;
        private System.Windows.Forms.Button BtnErase;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.TextBox TxbCommand;
        private System.Windows.Forms.TextBox TxbMain;
    }
}