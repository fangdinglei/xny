using Grpc.Core;
using GrpcMain.Attributes;
using GrpcMain.Extensions;
using GrpcMain.Interceptors;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MyDBContext.Main;
using MyJwtHelper;
using MyUtility;
using System.Reflection;

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

        public MQTTSeverClient()
        {
            //TODO 开启服务
        }
        public Task<bool> SendCmd(string deviceid, string cmd) {
            throw new NotImplementedException();
        }
        public void OnMsg(string title, byte[] data) { 
            
        }

    }

    
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
}
