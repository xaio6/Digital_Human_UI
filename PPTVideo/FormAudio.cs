using Newtonsoft.Json;
using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPTVideo
{
    public partial class FormAudio : Form
    {
        string m_index = "0"; //0是男,1是女 

        bool m_slectIndex = true;
        bool m_pull = false;
        string m_refWav = "";
        string m_wav = "./Audio/VITS_Wav.wav";

        bool m_preModel = false;
        bool m_trainModel = false;


        string[] marks = new string[] {
                                        "./Audio/Man.wav",
                                        "./Audio/Grill.wav"
                                      };

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

        public void TrainVits()
        {
            var json = new
            {
                User = Program.Flask.m_user,
            };
            string jsonData = JsonConvert.SerializeObject(json);
            if (Program.Flask.GetInferenceVITS(jsonData))
            {
                m_pull = true;
            }
            
        }

        static async Task WaitForPlaybackCompletion(double duration)
        {
            await Task.Delay((int)duration);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public FormAudio()
        {
            InitializeComponent();
        }

         

        private void cbMan_CheckedChanged(object sender, EventArgs e)
        {

            if (cbMan.Checked)
            {
                m_index = "0";
                m_slectIndex = false;
                cbGirl.Checked = false;
            }
        }

        private void cbGirl_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGirl.Checked)
            {
                m_index = "1";
                m_slectIndex = false;
                cbMan.Checked = false;
            }
        }

        //"该项目由Ai Horizons团队开源"
        //"2024/8/31"
        //"开发成员：魏伟辉、林训仪"
        private void btnSubmit_Click(object sender, EventArgs e)
        {

            //预训练模型选择
            if (m_preModel)
            {
                if (Program.Flask.m_user == null)
                {
                    MessageBox.Show("还没有登录，请先登录！", "提示");
                }
                else if (m_slectIndex)
                {
                    MessageBox.Show("没有选择声色", "提示");
                }
                else
                {
                    Program.Flask.SetModel(m_index);

                    //SadTalker参数
                    var SadTalkerParmes = new
                    {
                        enhancer = Program.Flask.m_enhancer,
                        expression_scale = Program.Flask.m_expression_scale
                    };

                    //VITS参数（这里参数是没有用上的，真正选择VITS模型是在下面）
                    var VitsParmes = new
                    {
                        User = Program.Flask.m_user,
                        Index = Program.Flask.m_index   //0是男,1是女 string类型
                    };

                    //总配置
                    var Config = new
                    {
                        User = Program.Flask.m_user,
                        VITS_Config = VitsParmes,
                        SadTalker_Config = SadTalkerParmes
                    };

                    string vitsJson = JsonConvert.SerializeObject(VitsParmes);
                    if (Program.Flask.SendSelectVITSModel(vitsJson))   //这里才是真正选择VITS模型
                    {
                        string configJson = JsonConvert.SerializeObject(Config);
                        //配置参数
                        if (Program.Flask.SendConfig(configJson))
                        {
                            global.vitsModel = false;
                            MessageBox.Show("提交成功", "提示");
                        }
                        else
                        {
                            MessageBox.Show("提交失败", "提示");
                        }
                    }
                    else
                    {
                        MessageBox.Show("提交失败", "提示");
                    }
                }
            }

            //自训练模型选择
            else if (m_trainModel)
            {
                if (textBox11.Text != "")
                {
                    if (m_refWav != "")
                    {
                        //SadTalker参数
                        var SadTalkerParmes = new
                        {
                            enhancer = Program.Flask.m_enhancer,
                            expression_scale = Program.Flask.m_expression_scale
                        };

                        //VITS参数（这里参数是没有用上的，真正选择VITS模型是在下面）
                        var VitsParmes = new
                        {
                            User = Program.Flask.m_user,
                        };

                        //总配置
                        var Config = new
                        {
                            User = Program.Flask.m_user,
                            VITS_Config = VitsParmes,
                            SadTalker_Config = SadTalkerParmes
                        };
                        string vitsJson = JsonConvert.SerializeObject(VitsParmes);
                        if (Program.Flask.SelectTrainVitsModel(vitsJson))   //这里才是真正选择VITS模型
                        {
                            string configJson = JsonConvert.SerializeObject(Config);
                            //配置参数
                            if (Program.Flask.SendConfig(configJson))
                            {
                                global.vitsModel = false;
                                MessageBox.Show("提交成功", "提示");
                            }
                            else
                            {
                                MessageBox.Show("提交失败", "提示");
                            }
                        }
                        else
                        {
                            MessageBox.Show("提交失败", "提示");
                        }
                    }
                    else
                    {
                        MessageBox.Show("没有上传参考音频", "提示");
                    }
                }
                else
                {
                    MessageBox.Show("没有填写参考文字", "提示");
                }
            }
            else
            {
                MessageBox.Show("请选择使用的模型", "提示");
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (Program.Flask.m_user == null)
            {
                MessageBox.Show("还没有登录，请先登录！", "提示");
            }
            else if (m_slectIndex)
            {
                MessageBox.Show("没有选择声色", "提示");
            }
            else
            {
                try
                {
                    int indexs = Convert.ToInt32(m_index);
                    string wavPath = marks[indexs];
                    // 播放音频
                    SoundPlayer player = new SoundPlayer(wavPath);
                    player.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("播放音频时出错：" + ex.Message, "错误");
                }
            }
        }

        private void FormAudio_Load(object sender, EventArgs e)
        {

        }

        private void radiusButton11_Click(object sender, EventArgs e)
        {
            if (textBox11.Text != "")
            {
                AddWavPath(ref m_refWav);
                if (m_refWav != "")
                {
                    var json = new
                    {
                        User = Program.Flask.m_user,
                        Ref_Text = textBox11.Text
                    };
                    string jsonData = JsonConvert.SerializeObject(json);

                    if (Program.Flask.SendRefWavAndText(jsonData, m_refWav))
                    {
                        var newJson = new
                        {
                            User = Program.Flask.m_user,
                        };
                        string newJsonData = JsonConvert.SerializeObject(newJson);

                        //选择自训练VITS模型
                        Program.Flask.SelectTrainVitsModel(newJsonData);

                        //推理VITS
                        Task.Run(() => TrainVits());
                        MessageBox.Show("上传成功", "提示");
                        //"该项目由Ai Horizons团队开源"
                        //"2024/8/31"
                        //"开发成员：魏伟辉、林训仪"
                    }
                    else
                    {
                        MessageBox.Show("上传失败", "提示");
                    }
                }
            }
            else
            {
                MessageBox.Show("没有填写参考音频","提示");
            }
        }


        private void radiusButton1_Click(object sender, EventArgs e)
        {
            if (m_refWav != "")
            {
                if (m_pull)
                {
                    var json = new
                    {
                        User = Program.Flask.m_user,
                    };
                    string jsonData = JsonConvert.SerializeObject(json);
                    if (Program.Flask.PullVITSAudio(jsonData))
                    {
                        try
                        {
                            SoundPlayer player = new SoundPlayer(m_wav);
                            player.Play();
                        }
                        catch
                        {
                            MessageBox.Show("请上传参考文字", "提示");
                        }
                    }
                    else
                    {
                        MessageBox.Show("生成失败", "提示");
                    }
                }
                else
                {
                    MessageBox.Show("生成中，稍等", "提示");
                }
            }
            else
            {
                MessageBox.Show("请上传参考音频", "提示");
            }
            

        }


        private void cbBaseAi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBaseAi.Checked)
            {
                cbTrainAi.Checked = false;

                groupBox2.Enabled = true;
                groupBox1.Enabled = false;

                //上传的模型
                m_preModel = true;
                m_trainModel = false;


            }
        }

        private void cbTrainAi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTrainAi.Checked)
            {
                cbBaseAi.Checked = false;

                groupBox2.Enabled = false;
                groupBox1.Enabled = true;

                //上传的模型
                m_preModel = false;
                m_trainModel = true;
            }
        }
    }
}
