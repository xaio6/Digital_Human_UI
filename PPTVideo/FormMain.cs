using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PPTVideo
{
    public partial class FormMain : Form
    {

        FormNumberPerson newPerson;
        PPTVideoProduce newVideo;
        FormTrain newTrain;

        public void ReductionTrain()
        {

            while (true)
            {
                if (global.bRunProress == false)
                {
                    global.bOpenFunctions = true;

                    Program.fm.VideoProcess(global.bOpenFunctions);

                    global.bOpenFunctions = false;

                    Program.fm.PlayTrain();

                    global.bRunProress = false;
                    break;
                }

            }

        }
        public void Reduction()
        {

            while (true)
            {
                if (global.bRunProress == false)
                {
                    global.bOpenFunctions = true;

                    Program.fm.VideoProcess(global.bOpenFunctions);

                    global.bOpenFunctions = false;

                    Program.fm.PlayFormVideo();


                    break;
                }

            }

        }

        public FormMain()
        {
            InitializeComponent();
            newVideo = new PPTVideoProduce();
            newPerson = new FormNumberPerson();
            newTrain = new FormTrain();
        }


        public void UpdateUserName(string Username) 
        {
            lblLogin.Text = Username;
        }
        private Point mouseLocation;//表示鼠标对于窗口左上角的坐标的负数
        private bool isDragging;//标识鼠标是否按下

        public void OpenFunctions( ) 
        {
            this.btnNumberPerson.Enabled = true;
            this.btnVideo.Enabled = true;
            this.btnTrain.Enabled = true;

            // 保存按钮的背景色
            Color originalBackColor = Color.FromArgb(240, 243, 248);

            this.lblLogin.Enabled = false;

            // 恢复按钮的背景色
            this.lblLogin.BackColor = originalBackColor;


            
            this.pictureBox1.Enabled =  false;
 


        }

        public void VideoProcess(bool bVideoProess) 
        {


            this.btnNumberPerson.Enabled = bVideoProess;
            this.btnTrain.Enabled = bVideoProess;
            this.btnVideo.Enabled = bVideoProess;

            this.btnClose.Enabled = bVideoProess; 

        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            SetWindowRegion();
            
            this.Left = Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2;//桌面的宽度的一半减去自身宽的的一半
            this.Top = Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2;//桌面的高度的一半减去自身高度的一半

            
        }   
        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
              
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 20);
            this.Region = new Region(FormPath);
        }
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            // 左上角  
            path.AddArc(arcRect, 180, 90);
            // 右上角  
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            // 右下角  
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            // 左下角  
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线  
            return path;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
             
            this.Close();
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseLocation = new Point(-e.X, -e.Y);
                //表示鼠标当前位置相对于窗口左上角的坐标，
                //并取负数,这里的e是参数，
                //可以获取鼠标位置
                isDragging = true;//标识鼠标已经按下
            }
 
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newMouseLocation = MousePosition;
                //获取鼠标当前位置
                newMouseLocation.Offset(mouseLocation.X, mouseLocation.Y);
                //用鼠标当前位置加上鼠标相较于窗体左上角的
                //坐标的负数，也就获取到了新的窗体左上角位置
                Location = newMouseLocation;//设置新的窗体左上角位置
            }
 
        }

        
        public void PlayFormVideo() 
        {

            newVideo.TopLevel = false;
            panel3.Controls.Clear();
            panel3.Controls.Add((Control)newVideo);
            newVideo.FormBorderStyle = FormBorderStyle.None; //将子面板显示风格为None，即没有标题栏
            newVideo.Dock = DockStyle.Fill; //在将自面板填充在panel控件上

            newVideo.Show();
        }

        public void PlayFormPerson()
        {

            newPerson.TopLevel = false;
            panel3.Controls.Clear();
            panel3.Controls.Add((Control)newPerson);
            newPerson.FormBorderStyle = FormBorderStyle.None; //将子面板显示风格为None，即没有标题栏
            newPerson.Dock = DockStyle.Fill; //在将自面板填充在panel控件上

            newPerson.Show();
        }

        public void PlayTrain() 
        {

            newTrain.TopLevel = false;
            panel3.Controls.Clear();
            panel3.Controls.Add((Control)newTrain);
            newTrain.FormBorderStyle = FormBorderStyle.None; //将子面板显示风格为None，即没有标题栏
            newTrain.Dock = DockStyle.Fill; //在将自面板填充在panel控件上

            newTrain.Show();

        }


        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;//鼠标已抬起，标识为false
            }
        }

        public void PlayFormInit()
        {
            global.fi.TopLevel = false;
            panel3.Controls.Clear();
            panel3.Controls.Add((Control)global.fi);
            global.fi.FormBorderStyle = FormBorderStyle.None; //将子面板显示风格为None，即没有标题栏
            global.fi.Dock = DockStyle.Fill; //在将自面板填充在panel控件上

            global.fi.Show();
        }




        private void btnVideo_Click(object sender, EventArgs e)
        {
            PlayFormVideo();
        }

        private void btnNumberPerson_Click(object sender, EventArgs e)
        {

            PlayFormPerson();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
 

        private void radiusButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin fl = new FormLogin();
            fl.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin fl = new FormLogin();
            fl.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin fl = new FormLogin();
            fl.ShowDialog();
        }


        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin fl = new FormLogin();
            fl.ShowDialog();
        }


        private void btnTrain_Click(object sender, EventArgs e)
        {
            PlayTrain();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin fl = new FormLogin();
            fl.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("该项目由Ai Horizons团队开源");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FormSponsor FS = new FormSponsor();
            FS.ShowDialog();
        }
    }
}
