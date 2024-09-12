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
    public partial class FormNumberPerson : Form
    {

        FormAudio newAudio;
        FormPerson newPerson;
       

        public FormNumberPerson()
        {
            InitializeComponent();
            newAudio = new FormAudio();
            newPerson = new FormPerson();
       

            PlayPerson();

        }

       

        public void PlayAudio() 
        {

            newAudio.TopLevel = false;
            panel2.Controls.Clear();
            panel2.Controls.Add((Control)newAudio);
            newAudio.FormBorderStyle = FormBorderStyle.None; //将子面板显示风格为None，即没有标题栏
            newAudio.Dock = DockStyle.Fill; //在将自面板填充在panel控件上

            newAudio.Show();
        }
        public void PlayPerson() 
        {

            newPerson.TopLevel = false;
            panel2.Controls.Clear();
            panel2.Controls.Add((Control)newPerson);
            newPerson.FormBorderStyle = FormBorderStyle.None; //将子面板显示风格为None，即没有标题栏
            newPerson.Dock = DockStyle.Fill; //在将自面板填充在panel控件上

            newPerson.Show();
        }

        private void btnPerson_Click(object sender, EventArgs e)
        {
            PlayPerson();
        }

        private void btnAudio_Click(object sender, EventArgs e)
        {
            PlayAudio();
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            MessageBox.Show("开发中......","提示");
        }

        private void FormNumberPerson_Load(object sender, EventArgs e)
        {

        }

       
 
    }
}
