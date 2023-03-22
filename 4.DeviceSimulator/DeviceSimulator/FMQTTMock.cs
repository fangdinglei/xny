using MQTTnet;
using MQTTnet.Client;
using System.Text;

namespace DeviceSimulator
{
    public partial class FMQTTMock : Form
    {
        long DeviceID = 1;

        public FMQTTMock()
        {
            InitializeComponent();
            textBox1.Text = DeviceID + "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "开始")
            {
                if (!long.TryParse(textBox1.Text, out DeviceID))
                {
                    MessageBox.Show("请输入数字设备ID");
                    return;
                }
                timer1.Start();
                textBox1.Enabled = false;
                button1.Text = "结束";
            }
            else
            {
                timer1.Stop();
                textBox1.Enabled = true;
                button1.Text = "开始";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                var dic = new Dictionary<string, float>();
                float Temperature, Humidity, Lumination, PowerRate;
                Temperature = bar1.Value / 10f;
                Humidity = bar2.Value / 10f;
                Lumination = bar3.Value / 10f;
                PowerRate = bar4.Value / 10f;
                dic.Add(nameof(Temperature), Temperature);
                dic.Add(nameof(Humidity), Humidity);
                dic.Add(nameof(Lumination), Lumination);
                dic.Add(nameof(PowerRate), PowerRate);
                Mock.MQTT.MQTTManager.SendData(DeviceID, dic).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
            }

        }
    }
}






namespace Mock.MQTT
{
    static public class MQTTUtility
    {
        static short CRC16(byte[] data, int start, int end)
        {
            uint CRC = 0xFFFF;
            int i = 0, j = 0;
            for (i = start; i < end; i++)
            {
                CRC = CRC ^ data[i];
                for (j = 0; j < 8; j++)
                {
                    if ((CRC & 0x01) != 0)
                        CRC = ((CRC >> 1) ^ 0xA001);
                    else
                        CRC = CRC >> 1;
                }
            }
            return (short)(CRC);
        }
        static public byte[] MyEncode(byte[] data)
        {
            byte[] res = new byte[2 + data.Length];
            var crc = CRC16(data, 0, data.Length);
            res[0] = (byte)(crc & 0xFF);
            res[1] = (byte)((crc >> 8) & 0xFF);
            Array.Copy(data, 0, res, 2, data.Length);
            return res;
        }
        static public byte[] MyDecode(byte[] data)
        {
            if (data.Length < 2)
            {
                throw new Exception("内容长度异常");
            }
            byte[] res = new byte[data.Length - 2];
            var crc = CRC16(data, 2, data.Length);
            if ((crc & 0xFFFF) != (data[0] & 0xFF | (data[1] & 0xFF) << 8))
            {
                throw new Exception("数据无法通过校验");
            }
            Array.Copy(data, 2, res, 0, data.Length - 2);
            return res;
        }
    }
    static public class MQTTManager
    {
        const string UserName = "admin";
        const string UserPass = "admin";
        const string HostIP = "localhost";
        const int Port = 1883;

        static public MqttClient Client;

        public static async Task<MqttClient> GetClient(string willtopic, byte[] willmessage)
        {
            if (Client != null)
                return Client;
            var options = new MqttClientOptions();

            options.ClientId = Guid.NewGuid().ToString().Replace("-", "").ToUpper();

            //设置服务器地址与端口
            options.ChannelOptions = new MqttClientTcpOptions()
            {

                Server = HostIP,
                Port = Port
            };
            //设置账号与密码
            options.Credentials = new MqttClientCredentials(UserName, Encoding.Default.GetBytes(UserPass));
            options.CleanSession = true;

            options.WillPayload = willmessage;
            options.WillTopic = willtopic;

            //保持期
            options.KeepAlivePeriod = TimeSpan.FromSeconds(100);

            //构建客户端对象
            var _mqttClient = new MqttFactory().CreateMqttClient() as MqttClient;
            await _mqttClient.ConnectAsync(options);
            Client = _mqttClient;
            return _mqttClient;
        }
        public static async Task<MqttClientPublishResult> SendData(long dvid, Dictionary<string, float> datas)
        {
            await GetClient("a", new byte[] { 1, 2, 3 });

            var send = Newtonsoft.Json.JsonConvert.SerializeObject(datas); ;
            var res = await Client.PublishAsync(new MqttApplicationMessage()
            {
                Topic = "/" + dvid + "/data",
                Payload = MQTTUtility.MyEncode(System.Text.Encoding.UTF8.GetBytes(send))
            });
            return res;
        }
        //public static async Task TestSendCMD(MqttClient client, string proidanddvid, string cmd)
        //{
        //    var send = cmd;
        //    await client.PublishAsync(new MqttApplicationMessage()
        //    {
        //        Topic = proidanddvid + "/cmd/get",
        //        Payload = System.Text.Encoding.UTF8.GetBytes(send)
        //    });
        //    await Task.Delay(1000);
        //    Console.WriteLine("发送数据" + send);
        //}
        //public static async Task TestSendStatus(MqttClient client, string proidanddvid, bool online)
        //{

        //    await client.PublishAsync(new MqttApplicationMessage()
        //    {
        //        Topic = $"{proidanddvid}/status/post",
        //        Payload = MyEncode(System.Text.Encoding.UTF8.GetBytes(
        //             " {"
        //              + $"\"status\":{(online ? 3 : 4)},"   // （1-未激活，2-禁用，3-在线，4-离线）
        //               + "\"isShadow\":\"0\""    // （0-禁用，1-启用）
        //             + " } "
        //          ))
        //    });
        //    Console.WriteLine("发送状态");
        //}

    }
}