namespace NumberPlace
{
    partial class FmProcessing
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
            this.LblCaption = new System.Windows.Forms.Label();
            this.LblDesc = new System.Windows.Forms.Label();
            this.BtnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblCaption
            // 
            this.LblCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblCaption.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblCaption.Location = new System.Drawing.Point(12, 70);
            this.LblCaption.Name = "LblCaption";
            this.LblCaption.Size = new System.Drawing.Size(484, 79);
            this.LblCaption.TabIndex = 0;
            this.LblCaption.Text = "処理中...";
            this.LblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblDesc
            // 
            this.LblDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblDesc.Location = new System.Drawing.Point(16, 175);
            this.LblDesc.Name = "LblDesc";
            this.LblDesc.Size = new System.Drawing.Size(480, 64);
            this.LblDesc.TabIndex = 1;
            this.LblDesc.Text = "処理中です...";
            this.LblDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BtnStop
            // 
            this.BtnStop.Location = new System.Drawing.Point(192, 242);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(124, 41);
            this.BtnStop.TabIndex = 2;
            this.BtnStop.Text = "中止";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // FmProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 335);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.LblDesc);
            this.Controls.Add(this.LblCaption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FmProcessing";
            this.Text = "処理中...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmProcessing_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblCaption;
        private System.Windows.Forms.Label LblDesc;
        private System.Windows.Forms.Button BtnStop;
    }
}