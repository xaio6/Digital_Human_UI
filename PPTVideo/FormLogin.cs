using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PPTVideo
{
    public partial class FormLogin : Form
    {

        string m_user = null;
        string m_passWord = null;

        public FormLogin()
        {
            InitializeComponent();
        }

        private Point mouseLocation;//表示鼠标对于窗口左上角的坐标的负数
        private bool isDragging;//标识鼠标是否按下
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            m_user = tbxUser.Text;
            m_passWord = tbxPassword.Text;



            if (m_user != null && m_passWord != null)
            {
                var json = new
                {
                    User = m_user,
                    Password = m_passWord
                };

                string josnData = JsonConvert.SerializeObject(json);
                if (Program.Flask.SendLogin(josnData))
                {
                    Program.Flask.SetName(m_user);
                    MessageBox.Show("登录成功", "提示");

                    global.strLoginUser = tbxUser.Text;

                    Program.fm.UpdateUserName(global.strLoginUser);

 

                    Program.fm.OpenFunctions();

                    Program.fm.PlayFormPerson();

                    Program.fm.Show();

                    this.Close();
                }
                else
                {
                    Program.Flask.SetName(null);
                    MessageBox.Show("登录失败", "提示");
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Program.fm.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
             Program.fm.Show();
            this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        private void FormLogin_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;//鼠标已抬起，标识为false
            }
        }

        private void FormLogin_MouseDown(object sender, MouseEventArgs e)
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

        private void FormLogin_MouseMove(object sender, MouseEventArgs e)
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
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

        private void panel1_MouseMove(object sender, MouseEventArgs e)
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

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;//鼠标已抬起，标识为false
            }
        }
    }
}
