using System.Collections.Generic;

namespace XNYAPI.Response.Data
{
    public class DataPoint
    {
        public string Value;
        public long Time; 

        public DataPoint(string value, long time)
        {
            Value = value;
            Time = time;
        }
    }
    public class DataStreamData
    {
        public string StreamName;
        public List<DataPoint> Points;
    }
    public class DataStreamsData
    {
        public uint DeviceID;
        public List<DataStreamData> Streams;
    }
    public class GetDataStreamsResponse : XNYResponseBase
    {
        public List<DataStreamsData> Data;
    }


    public class DataPointWithName {
        public string Name;
        public string Value;
        public long Time;

        public DataPointWithName(string name, string value, long time)
        {
            Name = name;
            Value = value;
            Time = time;
        }
    }
    public class LatestData {
        public uint DeviceID;
        public List<DataPointWithName> Datas;

        public LatestData(uint deviceID, List<DataPointWithName> datas)
        {
            DeviceID = deviceID;
            Datas = datas;
        }
    }
    public class GetLatestDataResponse : XNYResponseBase
    {
        public List<LatestData> Data;

        public GetLatestDataResponse(List<LatestData> data)
        {
            Data = data;
        }
    }


    public class DataStreamFeatureData
    {
        public string Name;
        public double Avg;
        public double Max;
        public double Min;
        public DataStreamFeatureData()
        {

        }
        public DataStreamFeatureData(string name, double avg, double max, double min)
        {
            Name = name;
            Avg = avg;
            Max = max;
            Min = min;
        }
    }
    public class DataStreamsFeatureData
    {
        public string DeviceID;
        public List<DataStreamFeatureData> Streams;
    }
    public class GetDataStreamsFeatureResponse : XNYResponseBase
    {
        public List<DataStreamsFeatureData> Data;
    }

   
   
}
