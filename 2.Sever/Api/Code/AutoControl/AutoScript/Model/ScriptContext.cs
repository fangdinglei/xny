using System.Collections.Generic;
using XNYAPI.Model.Device;

namespace XNYAPI.AutoControl.Script.Model
{
    public class ScriptContext
    {
        /// <summary>
        /// 设备当前是否在线
        /// </summary>
        public bool Online;
        /// <summary>
        /// 设备在OneNet平台上的ID
        /// </summary>
        public string RealID;
        /// <summary>
        /// 设备ID
        /// </summary>
        public uint DeviceID;
        /// <summary>
        /// 设备电量
        /// </summary>
        public double PowerRate;
        /// <summary>
        /// 当前的时间
        /// </summary>
        public int Step;
        public Dictionary<string, DataStream_Double> Datas_double = new Dictionary<string, DataStream_Double>();
        public DeviceTypeInfo Type;

        public ScriptContext(uint deviceID, string realID, int step)
        {
            DeviceID = deviceID;
            RealID = realID;
            Step = step;
        }
    }

}

