using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPTVideo
{
    public partial class FormCustomize : Form
    {
        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 20);
            this.Region = new Region(FormPath);
        }


        private Point mouseLocation;//表示鼠标对于窗口左上角的坐标的负数
        private bool isDragging;//标识鼠标是否按下

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



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private string m_labelJson = "";
        
        //初始化字符串列表
        List<string> wavStrings;
        List<string> textStrings;

        //生成标注json,并且上传音频
        private bool CreateLabelJson()
        {
            // 初始化字典，用于存储wav文件名和标签文本
            Dictionary<string, string> labelDictionary = new Dictionary<string, string>();

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    string wavName = $"{i}.wav";
                    string labelText = textStrings[i];
                    var json = new
                    {
                        User = Program.Flask.m_user,
                        Audio_Name = wavName
                    };
                    string jsonData = JsonConvert.SerializeObject(json);
                    Program.Flask.SendTrainWav(jsonData, wavStrings[i]);
                    // 添加键值对到字典
                    labelDictionary.Add(wavName, labelText);

                }
                // 将字典序列化为JSON字符串
                string jsonString = JsonConvert.SerializeObject(labelDictionary, Formatting.Indented);
                // 将JSON字符串保存到文件
                File.WriteAllText("./Data/labels.json", jsonString);

                m_labelJson = "./Data/labels.json";
                return true;
            }
            catch
            {
                return false;
            }
        }

        //判断哪个音频没有上传
        private bool DetectorWav()
        {

            wavStrings = new List<string>
        {
            global.strOneWav,
            global.strTwoWav,
            global.strThreeWav,
            global.strFourWav,
            global.strFiveWav,
            global.strSixWav,
            global.strSevenWav,
            global.strEightWav,
            global.strNineWav,
            global.strTenWav
        };
            int index = 1;
            bool result = true;
            // 判断并输出空字符串
            foreach (var wav in wavStrings)
            {
                if (string.IsNullOrEmpty(wav))
                {
                    MessageBox.Show($"第{index}音频没有上传", "提示");
                    result = false;
                }
                index += 1;
            }
            if (result)
            {
                return true;
            }
            return false;
        }

        //判断哪个标签没有填写
        private bool DetectorText()
        {

            textStrings = new List<string>
            {
                textBox1.Text,
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox6.Text,
                textBox7.Text,
                textBox8.Text,
                textBox9.Text,
                textBox10.Text
            };
            int index = 1;
            bool result = true;
            // 判断并输出空字符串
            foreach (var wav in textStrings)
            {
                if (string.IsNullOrEmpty(wav))
                {
                    MessageBox.Show($"第{index}标注没有填写", "提示");
                    result = false;
                }
                index += 1;
            }
            if (result)
            {
                return true;
            }
            return false;

        }

        //训练vits


        private void TrainVits(string labelJson)
        {
            byte[] jsonBytes = File.ReadAllBytes(labelJson);
            string jsonContent = Encoding.UTF8.GetString(jsonBytes);
            JObject jsonObject = JObject.Parse(jsonContent);

            var json = new
            {
                User = Program.Flask.m_user,
                Label = jsonObject
            };

            string jsonData = JsonConvert.SerializeObject(json);
            if (Program.Flask.SendLabelTrain(jsonData))
            {
                MessageBox.Show("训练完成", "提示");
            }
            else
            {
                MessageBox.Show("训练失败", "提示");
            }
            global.bRunProress = false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public FormCustomize()
        {
            InitializeComponent();
        }

        private void FormCustomize_Load(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Program.fm.Show();
            this.Close();
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

        private void FormCustomize_MouseDown(object sender, MouseEventArgs e)
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

        private void FormCustomize_MouseMove(object sender, MouseEventArgs e)
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

        private void FormCustomize_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;//鼠标已抬起，标识为false
            }

        }

        public void PlayWav(string wavPath)
        {
            // 播放音频
            SoundPlayer player = new SoundPlayer(wavPath);

            player.Play();
        }

        public  double GetAudioDuration(string filePath)
        {
            using (var audioFile = new AudioFileReader(filePath))
            {
                return audioFile.TotalTime.TotalMilliseconds;
            }
        }

        public void AddWavPath(ref string strWavPath) 
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "音频文件(*.mp3;*.wav;*.wma;*.aac;*.flac)|*.mp3;*.wav;*.wma;*.aac;*.flac|所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                strWavPath = fileDialog.FileName;//返回文件的完整路径               
            }
        }
             
        private void btnTextOneWav_Click(object sender, EventArgs e)
        {   
            AddWavPath(ref global.strOneWav);
 
        }

        static async Task WaitForPlaybackCompletion(double duration)
        {
            await Task.Delay((int)duration);
        }

        private async void btnTextOnePlay_Click(object sender, EventArgs e)
        {


            try
            {
               
                SoundPlayer player = new SoundPlayer(global.strOneWav);
                double duration = GetAudioDuration(global.strOneWav);
                this.btnTextOnePlay.BackgroundImage = Properties.Resources.stop;
                player.Play();
                await WaitForPlaybackCompletion(duration);
                this.btnTextOnePlay.BackgroundImage = Properties.Resources.play;
            }
            catch 
            {
                MessageBox.Show("请上传对应音频", "提示");
            }
        }

        private async void btnTextTwoPlay_Click(object sender, EventArgs e)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(global.strTwoWav);
                double duration = GetAudioDuration(global.strTwoWav);
                this.btnTextTwoPlay.BackgroundImage = Properties.Resources.stop;
                player.Play();
                await WaitForPlaybackCompletion(duration);
                this.btnTextTwoPlay.BackgroundImage = Properties.Resources.play;
             
            }
            catch
            {
                MessageBox.Show("请上传对应音频", "提示");
            }

        }

        private void btnTextTwoWav_Click(object sender, EventArgs e)
        {
            AddWavPath(ref global.strTwoWav);
        }

        private async void btnTextThreePlay_Click(object sender, EventArgs e)
        {
            try
            {

                SoundPlayer player = new SoundPlayer(global.strThreeWav);
                double duration = GetAudioDuration(global.strThreeWav);
                this.btnTextThreePlay.BackgroundImage = Properties.Resources.stop;
                player.Play();
                await WaitForPlaybackCompletion(duration);
                this.btnTextThreePlay.BackgroundImage = Properties.Resources.play;
                
            }
            catch
            {
                MessageBox.Show("请上传对应音频", "提示");
            }
        }

        private void btnTextThreeWav_Click(object sender, EventArgs e)
        {
            AddWavPath(ref global.strThreeWav);
        }

        private async void btnTextFourPlay_Click(object sender, EventArgs e)
        {
            try
            {

                SoundPlayer player = new SoundPlayer(global.strFourWav);
                double duration = GetAudioDuration(global.strFourWav);
                player.Play();
                this.btnTextFourPlay.BackgroundImage = Properties.Resources.stop;
                await WaitForPlaybackCompletion(duration);
                this.btnTextFourPlay.BackgroundImage = Properties.Resources.play;
                
            }
            catch
            {
                MessageBox.Show("请上传对应音频", "提示");
            }
        }

        //"该项目由Ai Horizons团队开源"
        //"2024/8/31"
        //"开发成员：魏伟辉、林训仪"
        private void btnTextFourWav_Click(object sender, EventArgs e)
        {
            AddWavPath(ref global.strFourWav);
        }

        private async void btnTextFivePlay_Click(object sender, EventArgs e)
        {
            try
            {

                SoundPlayer player = new SoundPlayer(global.strFiveWav);
                double duration = GetAudioDuration(global.strFiveWav);

                this.btnTextFivePlay.BackgroundImage = Properties.Resources.stop;
                player.Play();
                await WaitForPlaybackCompletion(duration);
                this.btnTextFivePlay.BackgroundImage = Properties.Resources.play;

               

            }
            catch
            {
                MessageBox.Show("请上传对应音频", "提示");
            }
        }

        private void btnTextFiveWav_Click(object sender, EventArgs e)
        {
            AddWavPath(ref global.strFiveWav);
        }

        private async void btnTextSixPlay_Click(object sender, EventArgs e)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(global.strSixWav);
                double duration = GetAudioDuration(global.strSixWav);
                this.btnTextSixPlay.BackgroundImage = Properties.Resources.stop;
                player.Play();
                await WaitForPlaybackCompletion(duration);
                this.btnTextSixPlay.BackgroundImage = Properties.Resources.play;
  
            }
            catch
            {
                MessageBox.Show("请上传对应音频", "提示");
            }
        }

        private void btnTextSixWav_Click(object sender, EventArgs e)
        {
            AddWavPath(ref global.strSixWav);
        }

        private async void btnTextSevenPlay_Click(object sender, EventArgs e)
        {
            try
            {

                SoundPlayer player = new SoundPlayer(global.strSevenWav);
                double duration = GetAudioDuration(global.strSevenWav);
                this.btnTextSevenPlay.BackgroundImage = Properties.Resources.stop;
                player.Play();
                await WaitForPlaybackCompletion(duration);
                this.btnTextSevenPlay.BackgroundImage = Properties.Resources.play;

            
            }
            catch
            {
                MessageBox.Show("请上传对应音频", "提示");
            }
        }

        private void btnTextSevenWav_Click(object sender, EventArgs e)
        {
            AddWavPath(ref global.strSevenWav);
        }

        private async void btnTextEightPlay_Click(object sender, EventArgs e)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(global.strEightWav);
                double duration = GetAudioDuration(global.strEightWav);
                this.btnTextEightPlay.BackgroundImage = Properties.Resources.stop;
                player.Play();
                await WaitForPlaybackCompletion(duration);
                this.btnTextEightPlay.BackgroundImage = Properties.Resources.play;

            }
            catch
            {
                MessageBox.Show("请上传对应音频", "提示");
            }
        }

        private void btnTextEightWav_Click(object sender, EventArgs e)
        {
            AddWavPath(ref global.strEightWav);
        }

        private async void btnTextNinePlay_Click(object sender, EventArgs e)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(global.strNineWav);
                double duration = GetAudioDuration(global.strNineWav);
                this.btnTextNinePlay.BackgroundImage = Properties.Resources.stop;
                player.Play();
                await WaitForPlaybackCompletion(duration);
                this.btnTextNinePlay.BackgroundImage = Properties.Resources.play;

 
                
            }
            catch
            {
                MessageBox.Show("请上传对应音频", "提示");
            }
        }

        private void btnTextNineWav_Click(object sender, EventArgs e)
        {
            AddWavPath(ref global.strNineWav);
        }

        private async void btnTextTenPlay_Click(object sender, EventArgs e)
        {
            try
            {

                SoundPlayer player = new SoundPlayer(global.strTenWav);
                double duration = GetAudioDuration(global.strTenWav);
                this.btnTextTenPlay.BackgroundImage = Properties.Resources.stop;
                player.Play();
                await WaitForPlaybackCompletion(duration);
                this.btnTextTenPlay.BackgroundImage = Properties.Resources.play;
               
            }
            catch
            {
                MessageBox.Show("请上传对应音频", "提示");
            }
        }

        private void btnTextTenWav_Click(object sender, EventArgs e)
        {
            AddWavPath(ref global.strTenWav);
        }

        private void radiusButton11_Click(object sender, EventArgs e)
        {
            //判断哪个音频没有上传
            if (DetectorWav())
            {
                //判断哪个标签没有填写
                if (DetectorText())
                {
                    //生成标注json,并且上传音频
                    if (CreateLabelJson())
                    {
                        MessageBox.Show("上传成功", "提示");
                    }
                    else
                    {
                        MessageBox.Show("上传失败", "提示");
                    }
                }
            }
        }

        private async void btnTrain_Click(object sender, EventArgs e)
        {
            //标注内容
            if (m_labelJson != "")
            {
                global.bOpenFunctions = false;

                Program.fm.VideoProcess(global.bOpenFunctions); ///按钮功能flase

                Program.fm.PlayFormInit();
 

                await Task.Run(() => TrainVits(m_labelJson));

                Program.fm.ReductionTrain();
                btnTrain.Text = "重新训练";
            }
            else
            {
                MessageBox.Show("请先上传音频", "提示");
            }

        }
    }
}
