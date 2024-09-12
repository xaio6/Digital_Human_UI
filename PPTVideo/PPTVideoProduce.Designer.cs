namespace PPTVideo
{
    partial class PPTVideoProduce
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PPTVideoProduce));
            this.lvPPTicon = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radiusButton1 = new PPTVideo.RadiusButton();
            this.btnProduceVideo = new PPTVideo.RadiusButton();
            this.btnSubmitPPT = new PPTVideo.RadiusButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvPPTicon
            // 
            this.lvPPTicon.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvPPTicon.HideSelection = false;
            this.lvPPTicon.LargeImageList = this.imageList1;
            this.lvPPTicon.Location = new System.Drawing.Point(27, 43);
            this.lvPPTicon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvPPTicon.Name = "lvPPTicon";
            this.lvPPTicon.Size = new System.Drawing.Size(892, 364);
            this.lvPPTicon.SmallImageList = this.imageList1;
            this.lvPPTicon.TabIndex = 0;
            this.lvPPTicon.UseCompatibleStateImageBehavior = false;
            this.lvPPTicon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvPPTicon_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "无11112.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvPPTicon);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(119)))));
            this.groupBox1.Location = new System.Drawing.Point(39, 152);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(935, 425);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "视频选择:";
            // 
            // radiusButton1
            // 
            this.radiusButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(112)))), ((int)(((byte)(254)))));
            this.radiusButton1.FlatAppearance.BorderSize = 0;
            this.radiusButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radiusButton1.Font = new System.Drawing.Font("华文中宋", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radiusButton1.ForeColor = System.Drawing.Color.White;
            this.radiusButton1.IsRoundCorner = true;
            this.radiusButton1.Location = new System.Drawing.Point(327, 608);
            this.radiusButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radiusButton1.Name = "radiusButton1";
            this.radiusButton1.RoundRadius = 70;
            this.radiusButton1.Size = new System.Drawing.Size(323, 74);
            this.radiusButton1.TabIndex = 22;
            this.radiusButton1.Text = "刷新";
            this.radiusButton1.UseVisualStyleBackColor = false;
            this.radiusButton1.Click += new System.EventHandler(this.radiusButton1_Click);
            // 
            // btnProduceVideo
            // 
            this.btnProduceVideo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(112)))), ((int)(((byte)(254)))));
            this.btnProduceVideo.FlatAppearance.BorderSize = 0;
            this.btnProduceVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProduceVideo.Font = new System.Drawing.Font("华文中宋", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnProduceVideo.ForeColor = System.Drawing.Color.White;
            this.btnProduceVideo.IsRoundCorner = true;
            this.btnProduceVideo.Location = new System.Drawing.Point(539, 43);
            this.btnProduceVideo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnProduceVideo.Name = "btnProduceVideo";
            this.btnProduceVideo.RoundRadius = 70;
            this.btnProduceVideo.Size = new System.Drawing.Size(323, 74);
            this.btnProduceVideo.TabIndex = 21;
            this.btnProduceVideo.Text = "生成视频";
            this.btnProduceVideo.UseVisualStyleBackColor = false;
            this.btnProduceVideo.Click += new System.EventHandler(this.btnProduceVideo_Click);
            // 
            // btnSubmitPPT
            // 
            this.btnSubmitPPT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(112)))), ((int)(((byte)(254)))));
            this.btnSubmitPPT.FlatAppearance.BorderSize = 0;
            this.btnSubmitPPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmitPPT.Font = new System.Drawing.Font("华文中宋", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSubmitPPT.ForeColor = System.Drawing.Color.White;
            this.btnSubmitPPT.IsRoundCorner = true;
            this.btnSubmitPPT.Location = new System.Drawing.Point(127, 43);
            this.btnSubmitPPT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSubmitPPT.Name = "btnSubmitPPT";
            this.btnSubmitPPT.RoundRadius = 70;
            this.btnSubmitPPT.Size = new System.Drawing.Size(323, 74);
            this.btnSubmitPPT.TabIndex = 20;
            this.btnSubmitPPT.Text = "上传PPT";
            this.btnSubmitPPT.UseVisualStyleBackColor = false;
            this.btnSubmitPPT.Click += new System.EventHandler(this.btnSubmitPPT_Click);
            // 
            // PPTVideoProduce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(986, 824);
            this.Controls.Add(this.radiusButton1);
            this.Controls.Add(this.btnProduceVideo);
            this.Controls.Add(this.btnSubmitPPT);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PPTVideoProduce";
            this.Text = "PPTVideoProduce";
            this.Load += new System.EventHandler(this.PPTVideoProduce_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvPPTicon;
        private System.Windows.Forms.GroupBox groupBox1;
        private RadiusButton btnSubmitPPT;
        private RadiusButton btnProduceVideo;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private RadiusButton radiusButton1;
    }
}