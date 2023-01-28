using Grpc.Core;
using GrpcMain.Attributes;
using GrpcMain.Extensions;
using GrpcMain.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MyDBContext.Main;
using MyJwtHelper;
using MyUtility;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using TimerMvcWeb.Filters;
using XNYAPI.Model.AutoControl;

namespace GrpcMain
{
    /// <summary>
    /// 传输协议接口
    /// </summary>
    public interface IProto
    {
        public Task<bool> SendCmd(string deviceid, string cmd);
    }
    static public class MQTTExtension
    {
        static public void UseMQTT(this IServiceCollection services )
        {
            services.TryAddSingleton<MQTTSeverClient>(new MQTTSeverClient());
        }
    }

    public class MQTTSeverClient
    {
        /*
         协议
        /deviceid/cmd:string 向设备发送命令
        /deviceid/data:string 设备上传数据

         */

       

        string UserName = "admin";
        string UserPass = "admin";
        string HostIP = "localhost";
        int Port = 1883;

        public MQTTSeverClient()
        {
            //TODO 开启服务
        }

        public MQTTSeverClient(string userName, string userPass, string hostIP, int port)
        {
            UserName = userName;
            UserPass = userPass;
            HostIP = hostIP;
            Port = port;
        }

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
            _mqttClient.ApplicationMessageReceivedAsync += arg => { return OnMsg(arg.ApplicationMessage.Topic,arg.ApplicationMessage.Payload); };
            //_mqttClient.ConnectedAsync += arg => { return MQTTHandler.OnConnect(_mqttClient, arg); };
            await _mqttClient.ConnectAsync(options);
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



        public async Task<bool> SendCmd(string deviceid, string cmd)
        {
            var r = await Client.PublishAsync(new MqttApplicationMessage()
            {
                Topic = $"/{deviceid}/cmd",
                Payload = UTF8Encoding.UTF8.GetBytes(cmd)
            });
            return r.IsSuccess;
        }
        public async Task OnMsg(string title, byte[] data) {
            Console.WriteLine("*******************");
            Console.WriteLine(title + ":" + UTF32Encoding.UTF8.GetString(data));
        }

    }

    /// <summary>
    /// 设备工具 负责设备通讯相关操作
    /// </summary>
    public class DeviceUtility
    {
        ITimeUtility _tu;
        IProto _proto;
        public DeviceUtility(ITimeUtility tu, IProto proto)
        {
            this._tu = tu;
            _proto = proto;
        }

        /// <summary>
        /// 向设备发送命令并记录操作到数据库<br/>该方法不鉴权
        /// </summary>
        /// <param name="deviceid"></param>
        /// <param name="cmd"></param>
        /// <param name="sendertype"></param>
        /// <param name="senderid"></param>
        public async Task<bool> SendCmd(long deviceid, string cmd, DeviceCmdSenderType sendertype, long senderid)
        {
            using var ct = new MainContext();
            var record = new DeviceCmdHistory(deviceid, cmd, sendertype, senderid, _tu.GetTicket(DateTime.Now));
            ct.DeviceCmdHistorys.Add(record);
            await ct.SaveChangesAsync();
            var suc=await _proto.SendCmd(deviceid.ToString(),cmd);
            if (suc)
            {
                record.Success = true;
                await ct.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    static public class DeviceAutoControlUtility
    {
        static TimeUtility tu = new TimeUtility();

        /// <summary>
        /// 给定时间是否在此时间内
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        static public bool IsTimeIn(this Device_AutoControl_Settings_Item item, DateTime time)
        {
            switch ((TimeTriggerType)item.TriggerType)
            {
                case TimeTriggerType.ALL:
                    return true;
                case TimeTriggerType.Once:
                    return (time.Ticks >= item.TimeStart) && (time.Ticks <= item.TimeEnd);
                case TimeTriggerType.EveryWeek:
                    //time = time.AddHours(8);
                    long t = tu.GetTicket(time) - tu.GetTicket(time.Date);
                    return (t >= item.TimeStart) && (t <= item.TimeEnd) && (item.Week & (1 << (int)time.DayOfWeek)) > 0;
                default:
                    return false;
            }
        }

        static public string? GetCmd(this List<Device_AutoControl_Settings_Item> item, DateTime time)
        {
            item = item.OrderBy(it => it.Order).ToList();
            for (int i = item.Count - 1; i >= 0; i--)
            {
                if (item[i].IsTimeIn(time))
                {
                    return item[i].Cmd;
                }
            }
            return null;
        }



        const long TicketADay = 24L * 60 * 60 * 1000;
        /// <summary>
        /// 校验时间信息是否合法
        /// </summary>
        /// <returns></returns>
        static public bool Check(this Device_AutoControl_Settings_Item item)
        {
            switch ((TimeTriggerType)item.TriggerType)
            {
                case TimeTriggerType.ALL:
                    return true;
                case TimeTriggerType.Once:
                    return true;
                case TimeTriggerType.EveryWeek:

                    return (item.TimeStart >= 0 && item.TimeStart < TicketADay) && (item.TimeEnd >= 0 && item.TimeEnd < TicketADay);
                default:
                    return true;
            }
        }
    }

    public class DeviceTimePlanManager {
        IProto _proto;

        public DeviceTimePlanManager(IProto proto)
        {
            _proto = proto;
            var runc = async () =>
            {
                using var ct = new MainContext();
                //TODO 性能优化
                foreach (var dvid in ct.Devices.Select(it => it.Id))
                {
                    var settings = await ct.Device_AutoControl_Settings_Items.AsNoTracking()
                       .Where(it => it.OwnerID == dvid && it.Open == true)
                       .OrderBy(it => it.Name)
                       .ThenBy(it => it.Order).ToListAsync();
                    var names = settings.Select(it => it.Name).Distinct().ToList();
                    var dic = new Dictionary<string, List<Device_AutoControl_Settings_Item>>();
                    names.ForEach(it => dic[it] = new List<Device_AutoControl_Settings_Item>());
                    settings.ForEach(it => dic[it.Name].Add(it));
                    foreach (var kv in dic)
                    {
                        var cmd = kv.Value.GetCmd(DateTime.Now);
                        if (string.IsNullOrWhiteSpace(cmd))
                            continue;
                        Task.Run(() => {
                            _proto.SendCmd(dvid + "", cmd);
                        });
                    }

                }

            };
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        await Task.Delay(1000 * 60 * 5);
                        await runc();
                    }
                    catch (Exception)
                    {

                    }
                }


            });
        }
    }
}
