using MQTTnet;
using MQTTnet.Client;
using System.Text;






namespace Mock.MQTT
{
    static public class MQTTManager
    {
        const string UserName = "admin";
        const string UserPass = "admin123";
        const string HostIP = "127.0.0.1";
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
                Port = Port,
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
        public static async Task<MqttClientPublishResult> SendData(long dvid, Dictionary<long, float> datas)
        {
            await GetClient("a", new byte[] { 1, 2, 3 });
            var s = "";
            foreach (var item in datas)
            {
                s += item.Key + "," + item.Value + ",";
            }
            var res = await Client.PublishAsync(new MqttApplicationMessage()
            {
                Topic = "/" + dvid + "/data",
                Payload = MQTTUtility.MyEncode(System.Text.Encoding.UTF8.GetBytes(s))
            });
            return res;
        }

        internal static async Task ListenCmdAsync(long deviceID, Action<string> value)
        {
            await GetClient("a", new byte[] { 1, 2, 3 });
            _ = Client.SubscribeAsync(new MqttClientSubscribeOptions
            {
                TopicFilters = new List<MQTTnet.Packets.MqttTopicFilter> {
                    new MQTTnet.Packets.MqttTopicFilter( ){
                     Topic="/" + deviceID + "/cmd"
                    }
                 }
            });
            Client.ApplicationMessageReceivedAsync += a =>
            {
                return Task.Run(() =>
                {
                    var s = UTF8Encoding.UTF8.GetString(MQTTUtility.MyDecode(a.ApplicationMessage.Payload));
                    value?.Invoke(s);
                });
            };
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