namespace NumberPlace
{
    partial class FmSetting
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
            this.BtnEngineLocation = new System.Windows.Forms.Button();
            this.TxbEngineLocation = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.BtnCencel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnEngineLocation
            // 
            this.BtnEngineLocation.Location = new System.Drawing.Point(886, 24);
            this.BtnEngineLocation.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.BtnEngineLocation.Name = "BtnEngineLocation";
            this.BtnEngineLocation.Size = new System.Drawing.Size(40, 33);
            this.BtnEngineLocation.TabIndex = 15;
            this.BtnEngineLocation.Text = "...";
            this.BtnEngineLocation.UseVisualStyleBackColor = true;
            this.BtnEngineLocation.Click += new System.EventHandler(this.BtnEngineLocation_Click);
            // 
            // TxbEngineLocation
            // 
            this.TxbEngineLocation.Location = new System.Drawing.Point(195, 24);
            this.TxbEngineLocation.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.TxbEngineLocation.Name = "TxbEngineLocation";
            this.TxbEngineLocation.Size = new System.Drawing.Size(674, 30);
            this.TxbEngineLocation.TabIndex = 14;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(21, 28);
            this.label34.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(160, 23);
            this.label34.TabIndex = 13;
            this.label34.Text = "思考エンジンの場所:";
            // 
            // BtnCencel
            // 
            this.BtnCencel.Location = new System.Drawing.Point(740, 196);
            this.BtnCencel.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.BtnCencel.Name = "BtnCencel";
            this.BtnCencel.Size = new System.Drawing.Size(188, 51);
            this.BtnCencel.TabIndex = 17;
            this.BtnCencel.Text = "キャンセル";
            this.BtnCencel.UseVisualStyleBackColor = true;
            this.BtnCencel.Click += new System.EventHandler(this.BtnCencel_Click);
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(537, 196);
            this.BtnOK.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(188, 51);
            this.BtnOK.TabIndex = 16;
            this.BtnOK.Text = "決定";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // FmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(948, 267);
            this.Controls.Add(this.BtnCencel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.BtnEngineLocation);
            this.Controls.Add(this.TxbEngineLocation);
            this.Controls.Add(this.label34);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FmSetting";
            this.Text = "設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmSetting_FormClosing);
            this.Shown += new System.EventHandler(this.FmSetting_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnEngineLocation;
        private System.Windows.Forms.TextBox TxbEngineLocation;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button BtnCencel;
        private System.Windows.Forms.Button BtnOK;
    }
}