using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace PPTVideo
{
    class FlaskConnect
    {

        private string m_ipPort;
        public FlaskConnect(string ip)
        {
            m_ipPort = ip;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///参数
        public object m_enhancer = null;
        public double m_expression_scale = 1.0;


        public string m_user = null;
        public string m_index = "0";
        public string m_imageBase64 = null;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///接口

        //登录
        public bool SendLogin(string jsonData)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/Login");
            if (response != null)
            {
                bool result = FlaskResponse(response);
                return result;
            }
            else
            {
                return false;
            }
        }


        //发送照片
        public bool SendImage(string jsonData)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/Send_Image");
            if (response != null)
            {
                bool result = FlaskResponse(response);
                return result;
            }
            else
            {
                return false;
            }

        }

        //选择vits模型
        public bool SendSelectVITSModel(string jsonData)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/Send_Select_VITS_Model");
            if (response != null)
            {
                bool result = FlaskResponse(response);
                return result;
            }
            else
            {
                return false;
            }
        }

        //选择训练的VITS模型
        public bool SelectTrainVitsModel(string jsonData)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/Send_Select_Train_VITS_Model");
            if (response != null)
            {
                bool result = FlaskResponse(response);
                return result;
            }
            else
            {
                return false;
            }
        }

        //配置参数
        public bool SendConfig(string jsonData)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/Send_Config");
            if (response != null)
            {
                bool result = FlaskResponse(response);
                return result;
            }
            else
            {
                return false;
            }
        }

        //发送ppt内容
        public bool SendPptRemakes(string jsonData)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/Send_PPT_Remakes");
            if (response != null)
            {
                bool result = FlaskResponse(response);
                return result;
            }
            else
            {
                return false;
            }
        }

        //推理VITS跟Sadtalker
        public bool GetInference(string jsonData)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/Get_Inference");
            if (response != null)
            {
                bool result = FlaskResponseState(response, 60000, jsonData);
                return result;
            }
            else
            {
                return false;
            }
        }

        //接收音频时长
        public bool ReciveWavTime(string jsonData, string savePath)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/Recive_Wav_Time");
            if (response != null)
            {
                bool result = FlaskResponseJsonSave(response, savePath);
                return result;
            }
            else
            {
                return false;
            }
        }

        //发送ppt视频
        public bool SendVideo(string jsonData, string videoFile)
        {
            HttpResponseMessage response = ConnectFlask(jsonData,videoFile , m_ipPort + "/Send_Video");
            if (response != null)
            {
                bool result = FlaskResponse(response);
                return result;
            }
            else
            {
                //SendVideo(jsonData);
                return false;
            }
        }

        //推理融合的视频
        public bool PptVideoMerge(string jsonData)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/PPT_Video_Merge");
            if (response != null)
            {
                bool result = FlaskResponseState(response, 60000, jsonData);
                return result;
            }
            else
            {
                return false;
            }
        }

        //拉取融合的视频
        public bool PullVideoMerge(string jsonData, string saveName)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/Pull_Video_Merge");
            if (response != null)
            {
                bool result = FlaskResponseFileSave(response, "./Video/" + saveName + ".mp4");
                return result;
            }
            else
            {
                return false;
            }
        }


        //发送训练音频
        public bool SendTrainWav(string jsonData, string wavPath)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, wavPath, m_ipPort + "/Send_Tarin_Audio");
            if (response != null)
            {
                bool result = FlaskResponse(response);
                return result;
            }
            else
            {
                return false;
            }
        }

        //发送标注跟训练模型
        public bool SendLabelTrain(string jsonData)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/Train_VITS_Model");
            if (response != null)
            {
                bool result = FlaskResponseState(response, 60000, jsonData);
                return result;
            }
            else
            {
                return false;
            }
        }

        //发送参考音频跟文字
        public bool SendRefWavAndText(string jsonData, string wavPath)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, wavPath, m_ipPort + "/Send_Ref_Wav_And_Text");
            if (response != null)
            {
                bool result = FlaskResponse(response);
                return result;
            }
            else
            {
                return false;
            }
        }

        //"该项目由Ai Horizons团队开源"
        //"2024/8/31"
        //"开发成员：魏伟辉、林训仪"

        //推理VITS
        public bool GetInferenceVITS(string jsonData)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/Get_Inference_VITS");
            if (response != null)
            {
                bool result = FlaskResponse(response);
                return result;
            }
            else
            {
                return false;
            }
        }

        //拉取推理的VITS声音
        public bool PullVITSAudio(string jsonData)
        {
            HttpResponseMessage response = ConnectFlask(jsonData, m_ipPort + "/Pull_VITS_Audio");
            if (response != null)
            {
                bool result = FlaskResponseFileSave(response, "./Audio/VITS_Wav.wav");
                return result;
            }
            else
            {
                return false;
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///一些功能

        //对文件进行base64编码
        public string EncodeBase64(string path)
        {
            byte[] Bytes = File.ReadAllBytes(path);
            string data_base64 = Convert.ToBase64String(Bytes);
            return data_base64;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///连接

        //连接Flask
        private HttpResponseMessage ConnectFlask(string jsonData, string ipPort)
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {

                    client.Timeout = TimeSpan.FromSeconds(3600); // 设置超时时间为1小时，根据需要调整

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(ipPort, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message + "服务器连接失败");
                return null;
            }
        }

        //连接Flask，发送json跟文件
        private HttpResponseMessage ConnectFlask(string jsonData, string videoFile, string ipPort)
        {
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(jsonData), "Json");

                if (File.Exists(videoFile))
                {
                    using (var fileStream = File.OpenRead(videoFile))
                    {
                        var fileContent = new StreamContent(fileStream);
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                        formData.Add(fileContent, "File", Path.GetFileName(videoFile));

                        try
                        {
                            using (var client = new System.Net.Http.HttpClient())
                            {
                                client.Timeout = TimeSpan.FromSeconds(7200); // 设置超时时间为2小时，根据需要调整

                                var response = client.PostAsync(ipPort, formData).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    return response;
                                    // 处理成功响应
                                }
                                else
                                {
                                    // 处理失败响应
                                    return null;
                                }
                            }
                        }
                        catch (HttpRequestException ex)
                        {
                            MessageBox.Show(ex.Message + " 发送请求失败");
                            return null;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("文件不存在：" + videoFile);
                    return null;
                }
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///解析

        //解析Response
        private bool FlaskResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                // 读取 JSON 响应
                string responseJson = response.Content.ReadAsStringAsync().Result;

                // 反序列化 JSON 响应
                dynamic responseData = JsonConvert.DeserializeObject(responseJson);

                // 检查 "result" 属性
                if (responseData.result == "Success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //解析Response,检测状态
        private bool FlaskResponseState(HttpResponseMessage response,int time, string jsonData)
        {
            if (response.IsSuccessStatusCode)
            {
                string result = FlaskResponseResult(response);
                if (result != "Failed")
                {
                    // 反序列化 JSON 字符串,获取User
                    var deserializedData = JsonConvert.DeserializeObject<dynamic>(jsonData);
                    var usere = deserializedData.User;

                    var newJsonData = new
                    {
                        User = usere,
                        Task = result
                    };
                    string newData = JsonConvert.SerializeObject(newJsonData);

                    while (true)
                    {
                        HttpResponseMessage newResponse = ConnectFlask(newData, m_ipPort + "/Get_State");
                        string newResult = FlaskResponseResult(newResponse);
                        if (newResult == "True")
                        {
                            Console.WriteLine("运行完成");
                            return true;
                        }
                        Console.WriteLine("运行中");
                        Thread.Sleep(time);
                    }

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //解析Response,返回Result
        private string FlaskResponseResult(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                // 读取 JSON 响应
                string responseJson = response.Content.ReadAsStringAsync().Result;

                // 反序列化 JSON 响应
                dynamic responseData = JsonConvert.DeserializeObject(responseJson);

                return responseData.result;
            }
            else
            {
                return null;
            }
        }

        //解析Response的json保存数据
        private bool FlaskResponseJsonSave(HttpResponseMessage response, string savePath)
        {
            if (response.IsSuccessStatusCode)
            {
                // 读取 JSON 响应
                string responseJson = response.Content.ReadAsStringAsync().Result;

                // 反序列化 JSON 响应
                dynamic responseData = JsonConvert.DeserializeObject(responseJson);

                dynamic data = responseData.result;
                if (data.ToString() != "Failed")
                {
                    // 将 dynamic 数据转换为 JSON 字符串
                    string jsonData = JsonConvert.SerializeObject(data);

                    // 将 JSON 字符串写入到文件中
                    File.WriteAllText(savePath, jsonData);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }

        //解析Response的video保存数据
        private bool FlaskResponseFileSave(HttpResponseMessage response, string savePath)
        {
            if (response.IsSuccessStatusCode)
            {
                string responseJson = response.Content.ReadAsStringAsync().Result;

                // 反序列化 JSON 响应
                dynamic responseData = JsonConvert.DeserializeObject(responseJson);
                dynamic data = responseData.result;
                if (data.ToString() != "Failed")
                {
                    // 将 dynamic 数据转换为字符串
                    string fileDataBase64 = data;

                    byte[] fileData = Convert.FromBase64String(fileDataBase64);
                    // 将字节数组保存为文件
                    File.WriteAllBytes(savePath, fileData);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///配置参数

        //是否脸部增强
        public void SetEnhancers(bool select)
        {
            if (select)
            {
                m_enhancer = "gfpgan";
            }
            else
            {
                m_enhancer = null;
            }
        }

        //脸部表达能力
        public void SetExpression(double value)
        {
            m_expression_scale = value;
        }

        //设置用户名字
        public void SetName(string name)
        {
            m_user = name;
        }

        //设置声音模型
        public void SetModel(string index)
        {
            m_index = index;
        }

        //设置照片base64
        public void SetImagePath(string imgDataBase64)
        {
            m_imageBase64 = imgDataBase64;
        }

    }

}

