using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPTVideo
{
    public partial class PPTVideoProduce : Form
    {
        bool m_openVideo = true;
        string m_pptPath = null;
        string m_pptVaideSaveName = null;
        string m_allVideoPath;


        public PPTVideoProduce()
        {
            InitializeComponent();
        }
        



        //获取ppt内容
        private string GetPptRemakes(string inputString)
        {
            // 调用python脚本
            Process p = new Process();
            p.StartInfo.FileName = @"GetTxt.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            // 注意，这前面得有一个空格，不然就连在一起了
            p.StartInfo.Arguments = " " + inputString;
            p.Start();

            p.WaitForExit(60000);
            p.Close();
            return @"./Data/comments.json";
        }

        //根据时间生成对应视频
        private string Time2Video(string inputString)
        {

            // 调用python脚本
            Process p = new Process();
            p.StartInfo.FileName = "CreateVideo.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            // 注意，这前面得有一个空格，不然就连在一起了
            p.StartInfo.Arguments = " " + inputString;
            p.Start();

            p.WaitForExit(180000);
            p.Close();

            return @"./Data/PPTVideo.mp4";
        }


        private void CreatePptVideo(string jsonData)
        {
            //对音频、视频进行推理
            if (Program.Flask.GetInference(jsonData))
            {
                //获取所有音频的时间
                if (Program.Flask.ReciveWavTime(jsonData, @"./Data/Time.json"))
                {
                    //根据Time生成视频
                    string pptVideoPath = Time2Video(m_pptPath);
                    Console.WriteLine(pptVideoPath);
                    //发送PPT视频
                    if (Program.Flask.SendVideo(jsonData, pptVideoPath))
                    {
                        //ppt视频跟数字人融合
                        if (Program.Flask.PptVideoMerge(jsonData))
                        {
                            //拉取融合视频
                            if (Program.Flask.PullVideoMerge(jsonData, m_pptVaideSaveName))
                            {
                                MessageBox.Show("生成成功", "提示");
                            }
                            else
                            {
                                MessageBox.Show("拉取失败", "提示");
                            }
                        }
                        else
                        {
                            MessageBox.Show("生成失败", "提示");
                        }
                    }
                    else
                    {
                        MessageBox.Show("发送失败", "提示");
                    }

                }
                else
                {
                    MessageBox.Show("获取时间失败", "提示");
                }
            }
            else
            {
                MessageBox.Show("推理失败", "提示");
            }

            global.bRunProress = false;
        }


        private void btnSubmitPPT_Click(object sender, EventArgs e)
        {
            if (Program.Flask.m_user == null)
            {
                MessageBox.Show("还没有登录，请先登录！", "提示");
            }
            else if (global.numberPerson)
            {
                MessageBox.Show("没有配置数字人", "提示");
            }
            else if (global.vitsModel)
            {
                MessageBox.Show("没有配置语音模型", "提示");
            }
            else
            {
                // 创建一个OpenFileDialog对象
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PowerPoint Files (*.ppt, *.pptx)|*.ppt;*.pptx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 获取选择的文件路径
                    m_pptPath = openFileDialog.FileName;
                    m_pptVaideSaveName = Path.GetFileNameWithoutExtension(m_pptPath);

                    //ppt备注内容
                    string remakePath = GetPptRemakes(m_pptPath);
                    byte[] jsonBytes = File.ReadAllBytes(remakePath);
                    string jsonContent = Encoding.UTF8.GetString(jsonBytes);
                    JObject jsonObject = JObject.Parse(jsonContent);

                    //json
                    var PptRemakes = new
                    {
                        User = Program.Flask.m_user,
                        PPT_Remakes = jsonObject
                    };

                    string jsonData = JsonConvert.SerializeObject(PptRemakes);

                    if (Program.Flask.SendPptRemakes(jsonData))
                    {
                        MessageBox.Show("上传成功", "提示");
                        m_openVideo = false;
                    }
                    else
                    {
                        MessageBox.Show("提交失败", "提示");
                    }
                }

            }
        }

        private async void btnProduceVideo_Click(object sender, EventArgs e)
        {
            if (Program.Flask.m_user == null)
            {
                MessageBox.Show("还没有登录，请先登录！", "提示");
            }
            else if (global.numberPerson)
            {
                MessageBox.Show("没有配置数字人", "提示");
            }
            else if (global.vitsModel)
            {
                MessageBox.Show("没有配置语音模型", "提示");
            }
            else if (m_openVideo)
            {
                MessageBox.Show("没有上传PPT", "提示");
            }
            else
            {
                global.bOpenFunctions = false;

                Program.fm.VideoProcess(global.bOpenFunctions); ///按钮功能flase

                Program.fm.PlayFormInit();

                global.bRunProress = true;
 
                var jsonName = new
                {
                    User = Program.Flask.m_user
                };
                string jsonData = JsonConvert.SerializeObject(jsonName);

                //Time2Video(m_pptPath);

                await Task.Run(() => CreatePptVideo(jsonData));
                //CreatePptVideo(jsonData);

                Program.fm.Reduction();

            }

        }
        public void UpdateVideoList() 
        {
            m_allVideoPath = AppDomain.CurrentDomain.BaseDirectory + "/Video/";
            Console.WriteLine("当前应用程序路径：" + m_allVideoPath);

            string[] mp4Files = Directory.GetFiles(m_allVideoPath, "*.mp4");

            lvPPTicon.Items.Clear();
            foreach (string mp4File in mp4Files)
            {

                ListViewItem li = new ListViewItem();//初始化每一项
                // 获取文件名
                string fileName = Path.GetFileName(mp4File);

                li.Text = fileName;//获取文本
                li.ImageIndex = 0;//获取图片

                // 将 ListViewItem 添加到 ListView
                lvPPTicon.Items.Add(li);
            }
        }

        private void PPTVideoProduce_Load(object sender, EventArgs e)
        {

            UpdateVideoList();
        }

        private void lvPPTicon_MouseDown(object sender, MouseEventArgs e)
        {
            // 检查是否单击了项
            if (e.Button == MouseButtons.Left)
            {
                // 获取单击的项
                ListViewItem clickedItem = lvPPTicon.GetItemAt(e.X, e.Y);

                if (clickedItem != null)
                {
                    // 获取单击项的文件名
                    string fileName = clickedItem.Text;

                    // 构造 MP4 文件的完整路径
                    string filePath = Path.Combine(m_allVideoPath, fileName);

                    // 检查文件是否存在
                    if (File.Exists(filePath))
                    {
                        // 如果文件存在，使用默认关联程序打开 MP4 文件
                        System.Diagnostics.Process.Start(filePath);
                    }
                    else
                    {
                        MessageBox.Show("文件不存在！");
                    }
                }
            }
        }

        private void radiusButton1_Click(object sender, EventArgs e)
        {
            UpdateVideoList();

        }
    }

}
