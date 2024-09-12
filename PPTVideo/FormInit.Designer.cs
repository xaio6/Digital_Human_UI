namespace PPTVideo
{
    partial class FormInit
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
            this.circleProgramBar1 = new PPTVideo.CircleProgramBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // circleProgramBar1
            // 
            this.circleProgramBar1.BackColor = System.Drawing.Color.White;
            this.circleProgramBar1.BottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.circleProgramBar1.FinishedColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(179)))), ((int)(((byte)(63)))));
            this.circleProgramBar1.Location = new System.Drawing.Point(1, 2);
            this.circleProgramBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.circleProgramBar1.MaxValue = 100;
            this.circleProgramBar1.Name = "circleProgramBar1";
            this.circleProgramBar1.Progress = 0;
            this.circleProgramBar1.Size = new System.Drawing.Size(914, 570);
            this.circleProgramBar1.TabIndex = 0;
            this.circleProgramBar1.Text = "circleProgramBar1";
            this.circleProgramBar1.TopColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(179)))), ((int)(((byte)(63)))));
            this.circleProgramBar1.Click += new System.EventHandler(this.circleProgramBar1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文中宋", 20F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(119)))));
            this.label1.Location = new System.Drawing.Point(322, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "生成中，请勿退出";
            // 
            // FormInit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(918, 572);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.circleProgramBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormInit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormInit";
            this.Load += new System.EventHandler(this.FormInit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CircleProgramBar circleProgramBar1;
        private System.Windows.Forms.Label label1;
    }
}