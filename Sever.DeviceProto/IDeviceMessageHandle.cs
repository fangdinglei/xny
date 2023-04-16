namespace Sever.DeviceProto
{
    /// <summary>
    ///  协议
    ///deviceid/cmd:string 向设备发送命令
    ///deviceid/data:string 设备上传数据
    ///name:long,data
    /// </summary>
    public interface IDeviceMessageHandle
    {
        public void OnMsg(string topic, byte[] data);
    }
}