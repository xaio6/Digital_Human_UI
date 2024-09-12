using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPTVideo
{
    public partial class FormInit : Form
    {
        Thread thread;
        public FormInit()
        {
            InitializeComponent();
        }

        private void FormInit_Load(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(new Action(delegate
            {

                while (global.bProgramBar)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        Thread.Sleep(100);
                        this.circleProgramBar1.Progress = i + 1;
                    }
                }

                this.Close();


            })));
            thread.IsBackground = true;
            thread.Start();
        }

        private void circleProgramBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
