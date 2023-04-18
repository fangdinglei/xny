using MQTTnet;
using MQTTnet.Client;
using System.Text;

namespace Sever.DeviceProto
{
    /// <summary>
    ///  协议
    ///deviceid/data:string 设备上传数据
    ///name:long,data
    /// </summary>
    public class MQTTSeverClient : IProto
    {
        string UserName = "admin";
        string UserPass = "admin123";
        string HostIP = "localhost";
        int Port = 1883;
        IDeviceMessageHandle _handle;
        public MQTTSeverClient(IDeviceMessageHandle handle)
        {
            _handle = handle;
            //TODO 开启服务
            RunAsync();
        }

        //public MQTTSeverClient(string userName, string userPass, string hostIP, int port, IDeviceMessageHandle handle)
        //{
        //    UserName = userName;
        //    UserPass = userPass;
        //    HostIP = hostIP;
        //    Port = port;
        //    _handle = handle;
        //}

        public MqttClient Client;

        async Task<MqttClient> GetClient()
        {
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

            //保持期
            options.KeepAlivePeriod = TimeSpan.FromSeconds(100.5);

            //构建客户端对象
            var _mqttClient = new MqttFactory().CreateMqttClient() as MqttClient;
            _mqttClient.ApplicationMessageReceivedAsync += arg => { return OnMsg(arg.ApplicationMessage.Topic, arg.ApplicationMessage.Payload); };
            //_mqttClient.ConnectedAsync += arg => { return MQTTHandler.OnConnect(_mqttClient, arg); };
            await _mqttClient.ConnectAsync(options);
            await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptions()
            {
                SubscriptionIdentifier = (uint)Math.Abs((new Random()).Next()),
                TopicFilters = new List<MQTTnet.Packets.MqttTopicFilter>() {
                    new MQTTnet.Packets.MqttTopicFilter() {
                     Topic="/+/data"
                    }
                },
            });
            return _mqttClient;
        }
        public async void RunAsync()
        {
            try
            {
                var c = await GetClient();
                Client = c;
            }
            catch (Exception e)
            {
                //Logger.Log(Logger.ERROR, "MQTT无法启动", e.Message + "||" + e.StackTrace);
            }

        }



        public async Task<bool> SendCmd(string deviceid, byte[] cmd)
        {
            try
            {
                var c = await GetClient();
                var r = await c.PublishAsync(new MqttApplicationMessage()
                {
                    Topic = $"/{deviceid}/cmd",
                    Payload = cmd
                });
                return r.IsSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task OnMsg(string title, byte[] data)
        {
#if DEBUG
            Console.WriteLine("*******************");
            Console.WriteLine(title + ":" + UTF32Encoding.UTF8.GetString(data));
#endif
            try
            {
                _handle.OnMsg(title, data);
            }
            catch (Exception)
            {
            }
        }

    }
}