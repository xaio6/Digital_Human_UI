using Newtonsoft.Json;
using System;

using System.Drawing;

using System.Windows.Forms;

namespace PPTVideo
{
    public partial class FormPerson : Form
    {

        string m_imagePath = null;

        bool m_enhancers = false;
        double value = 1.0;

        bool m_openEnhancers = true;
        bool m_openExpression = true;


        public FormPerson()
        {
            InitializeComponent();
        }

        private void cbYes_CheckedChanged(object sender, EventArgs e)
        {
            if (cbYes.Checked)
            {
                cbNO.Checked = false;
                m_openEnhancers = false;

                m_enhancers = true;
            }
        }

        private void cbNO_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNO.Checked)
            {
                cbYes.Checked = false;
                m_openEnhancers = false;

                m_enhancers = false;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            m_openExpression = false;
            double expressionValue = trackBar1.Value / 10;
            value = (expressionValue / 10) + 1;

            lblNumber.Text = value.ToString();



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // 创建一个OpenFileDialog对象
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.png, *.gif)|*.jpg;*.png;*.gif";

            // 如果用户选择了一个文件
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取选择的文件路径
                m_imagePath = openFileDialog.FileName;

                // 将图片显示在pictureBox1上
                pictureBox1.Image = Image.FromFile(m_imagePath);
            }
        }



        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Program.Flask.m_user == null)
            {
                MessageBox.Show("还没有登录，请先登录！", "提示");
            }
            else if (m_openEnhancers)
            {
                MessageBox.Show("没有选择是否脸部增强", "提示");
            }
            else if (m_openExpression)
            {
                MessageBox.Show("没有选择表达增强", "提示");
            }
            else if (m_imagePath == null)
            {
                MessageBox.Show("没有上传教师形象", "提示");
            }
            else
            {
                Program.Flask.SetEnhancers(m_enhancers);
                Program.Flask.SetExpression(value);

                string imgDataBase64 = Program.Flask.EncodeBase64(m_imagePath);
                Program.Flask.SetImagePath(imgDataBase64);

                var json = new
                {
                    User = Program.Flask.m_user,
                    Img = Program.Flask.m_imageBase64
                };
                string jsonData = JsonConvert.SerializeObject(json);

                if (Program.Flask.SendImage(jsonData))
                {
                    MessageBox.Show("提交成功", "提示");
                    global.numberPerson = false;
                }
                else
                {
                    MessageBox.Show("提交失败", "提示");
                }
            }
        }
    }
}
