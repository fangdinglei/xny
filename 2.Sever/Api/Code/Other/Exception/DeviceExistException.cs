using System;


namespace FDL.Program
{
    /// <summary>
    /// 当id所对应的设备已经存在时抛出
    /// </summary>
    public class DeviceExistException : Exception
    {
        public DeviceExistException(string deviceid) :
            base($"设备 {deviceid} 已经存在")
        { }
    }

}
