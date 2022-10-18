using System.Collections.Generic;

namespace XNYAPI.Response.Data
{
    public class DeviceSoc
    {
        public uint ID;
        public double Max;
        public double Current;

        public DeviceSoc(uint iD, double max, double current)
        {
            ID = iD;
            Max = max;
            Current = current;
        }
    }
    public class GetDeviceSocResponse : XNYResponseBase
    {
        public List<DeviceSoc> Data;
    }

}
