using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPTVideo
{
    public partial class FormTrain : Form
    {

        FormCustomize fct;
        public FormTrain()
        {
            InitializeComponent();
            fct = new FormCustomize();
        }

        public void PlayTrainPerson()
        {

         
            fct.TopLevel = false;
            panel2.Controls.Clear();
            panel2.Controls.Add((Control)fct);
            fct.FormBorderStyle = FormBorderStyle.None; //将子面板显示风格为None，即没有标题栏
            fct.Dock = DockStyle.Fill; //在将自面板填充在panel控件上

            fct.Show();
        }

        private void btnTrainAudio_Click(object sender, EventArgs e)
        {
            PlayTrainPerson();
        }

        private void btnTrainPerson_Click(object sender, EventArgs e)
        {
            MessageBox.Show("开发中......", "提示");
        }

        private void FormTrain_Load(object sender, EventArgs e)
        {
            Console.WriteLine("该项目由Ai Horizons团队开源");
            Console.WriteLine("2024/8/31");
            Console.WriteLine("开发成员：魏伟辉、林训仪");
        }
    }
}
