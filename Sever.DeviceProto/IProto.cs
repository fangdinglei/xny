namespace Sever.DeviceProto
{
    /// <summary>
    /// 传输协议接口
    /// </summary>
    public interface IProto
    {
        public Task<bool> SendCmd(string deviceid, byte[] cmd);
    }
}