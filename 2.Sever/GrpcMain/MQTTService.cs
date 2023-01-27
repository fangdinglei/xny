using MyDBContext.Main;
using MyUtility;

namespace GrpcMain
{
    public class MQTTSeverClient
    {
        public Task<bool> SendCmd(string deviceid, string cmd) {
            throw new NotImplementedException();
        }
        public void OnMsg(string title, byte[] data) { 
            
        }

    }

    public class MQTTUtility
    {
        public Task<bool> SendCmd(string deviceid, string cmd)
        {
            //TODO
            return new Task<bool>(() => { return true; });
        }

    }
    public class DeviceUtility
    {
        ITimeUtility tu;

        public DeviceUtility(ITimeUtility tu)
        {
            this.tu = tu;
        }

        /// <summary>
        /// 向设备发送命令并记录操作到数据库<br/>该方法不鉴权
        /// </summary>
        /// <param name="deviceid"></param>
        /// <param name="cmd"></param>
        /// <param name="sendertype"></param>
        /// <param name="senderid"></param>
        public async void SendCmd(long deviceid, string cmd, DeviceCmdSenderType sendertype, long senderid)
        {
            using var ct = new MainContext();
            var record = new DeviceCmdHistory(deviceid, cmd, sendertype, senderid, tu.GetTicket(DateTime.Now));
            ct.DeviceCmdHistorys.Add(record);
            await ct.SaveChangesAsync();
            //TODO 发送命令
            record.Success = true;
            await ct.SaveChangesAsync();
        }

    }
}
