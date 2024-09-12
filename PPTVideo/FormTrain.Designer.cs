namespace PPTVideo
{
    partial class FormTrain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTrain));
            this.btnTrainAudio = new System.Windows.Forms.Button();
            this.btnTrainPerson = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnTrainAudio
            // 
            this.btnTrainAudio.FlatAppearance.BorderSize = 0;
            this.btnTrainAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrainAudio.Font = new System.Drawing.Font("华文中宋", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTrainAudio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(119)))));
            this.btnTrainAudio.Location = new System.Drawing.Point(615, 3);
            this.btnTrainAudio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTrainAudio.Name = "btnTrainAudio";
            this.btnTrainAudio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnTrainAudio.Size = new System.Drawing.Size(209, 44);
            this.btnTrainAudio.TabIndex = 17;
            this.btnTrainAudio.Text = "训练人物声音";
            this.btnTrainAudio.UseVisualStyleBackColor = true;
            this.btnTrainAudio.Click += new System.EventHandler(this.btnTrainAudio_Click);
            // 
            // btnTrainPerson
            // 
            this.btnTrainPerson.FlatAppearance.BorderSize = 0;
            this.btnTrainPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrainPerson.Font = new System.Drawing.Font("华文中宋", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTrainPerson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(119)))));
            this.btnTrainPerson.Location = new System.Drawing.Point(102, 3);
            this.btnTrainPerson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTrainPerson.Name = "btnTrainPerson";
            this.btnTrainPerson.Size = new System.Drawing.Size(209, 44);
            this.btnTrainPerson.TabIndex = 16;
            this.btnTrainPerson.Text = "训练人物形象";
            this.btnTrainPerson.UseVisualStyleBackColor = true;
            this.btnTrainPerson.Click += new System.EventHandler(this.btnTrainPerson_Click);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Location = new System.Drawing.Point(24, 51);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(888, 573);
            this.panel2.TabIndex = 15;
            // 
            // FormTrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(956, 654);
            this.Controls.Add(this.btnTrainAudio);
            this.Controls.Add(this.btnTrainPerson);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormTrain";
            this.ShowIcon = false;
            this.Text = "FormYrain";
            this.Load += new System.EventHandler(this.FormTrain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTrainAudio;
        private System.Windows.Forms.Button btnTrainPerson;
        private System.Windows.Forms.Panel panel2;
    }
}