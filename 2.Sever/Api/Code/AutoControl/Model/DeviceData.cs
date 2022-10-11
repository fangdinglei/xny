
using System;
using System.Collections.Generic;
using XNYAPI.Model.Device;

namespace XNYAPI.Model.AutoControl
{
    public class LedInfo {
        public bool LedOpen=false;
        public ScheduleInfo Schedule;
        public AutoControlSettings Settings;
    }
    //public class DataServiceInfo
    //{
    //    public bool Open;
    //}
    public class DeviceData
    {
       
        public uint DeviceID;
        public string DeviceRealID;
        public uint DeviceTypeID;
        
        public DeviceData()
        {
          
        }
         
        public struct DataStream
        {
            public List<double> points;

            public DataStream(List<double> points)
            {
                this.points = points;
            }

            public double Avg()
            {
                double sum = 0;
                foreach (var pt in points)
                    sum += pt;
                return sum / points.Count;
            }

        }
    }
}
