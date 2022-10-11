using System;


namespace FDL.Program
{
    /// <summary>
    /// 当id所对应的设备不存在时抛出
    /// </summary>
    public class NoThisDeviceException : Exception
    {
        public NoThisDeviceException(string deviceid) :
            base($"设备 {deviceid} 不存在")
        { }
    }

}
