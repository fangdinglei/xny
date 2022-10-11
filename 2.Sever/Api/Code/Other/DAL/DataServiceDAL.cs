 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using XNYAPI.Utility;

namespace XNYAPI.DAL
{
    public class DataServiceDAL
    {
        /// <summary>
        /// 获取数据流 数据时间大于等于starttime 小于等于endtime
        /// </summary>
        /// <param name="dvid"></param>
        /// <param name="streamname"></param>
        /// <param name="starttime">java时间戳</param>
        /// <param name="endtime">java时间戳</param>
        /// <param name="maxcount"></param>
        /// <returns></returns>
        /// <exception cref = "Exception" />
        static public List<ValueTuple<DateTime, string>> GetDataPoints(uint dvid, string streamname, long starttime,long endtime, int maxcount = 1)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                List<ValueTuple<DateTime, string>> res = new List<(DateTime, string)>();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT Time,Value FROM device_datapoints " +
                    $"WHERE DeviceID={dvid}AND Time>={starttime} AND Time<={endtime} AND DataName='{streamname}'  ORDER by Time DESC LIMIT {maxcount}";
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                { 
                    res.Add((rd.GetInt64(0).JavaTicketToBeijingTime(),rd.GetString(1)));
                }
                return res;
            }
        }

        /// <summary>
        /// 获取double数据流 按时间顺序 数据时间大于等于starttime java时间戳
        /// </summary>
        /// <param name="starttime">java时间戳</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        static public List<ValueTuple<DateTime, double>> GetDoubleDataPoints(uint dvid, string streamname, long starttime,int maxcount=1)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                List<ValueTuple<DateTime, double>> res = new List<(DateTime, double)>();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT Value,Time FROM device_datapoints " +
                    $"WHERE DeviceID={dvid} AND Time>={starttime} AND DataName='{streamname}' ORDER by Time  LIMIT {maxcount}";
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string v = rd.GetString(0);
                    double dv;
                    if (!double.TryParse(v.Replace("\"",""), out dv))
                        continue;
                    long t = rd.GetInt64(1);
                    res.Add((t.JavaTicketToBeijingTime(), dv));
                }
                return res;
            }
        }

        /// <summary>
        /// 获取double数据流 按时间倒序 数据时间大于等于starttime java时间戳
        /// </summary>
        /// <param name="dvid"></param>
        /// <param name="streamname"></param>
        /// <param name="starttime">java时间戳</param>
        /// <param name="maxcount"></param>
        /// <returns></returns>
        ///  <exception cref="Exception" />
        static public List<double> GetDoubleDataPoints2(uint dvid, string streamname, long starttime, int maxcount = 1)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                List<double> res = new List<double>();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT Value FROM device_datapoints " +
                    $"WHERE DeviceID={dvid} AND Time>={starttime} AND DataName='{streamname}'  ORDER by Time DESC LIMIT {maxcount}";
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string v = rd.GetString(0);
                    double dv;
                    if (!double.TryParse(v.Replace("\"",""), out dv))
                        continue; 
                    res.Add(dv);
                }
                res.Reverse();
                return res;
            }
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="dvid"></param>
        /// <param name="dpname"></param>
        /// <param name="dps"></param>
        /// <exception cref="Exception" />
        static public void AddDataPoints(uint dvid,string dpname,List<ValueTuple<DateTime,string>> dps ) {
            using (var cnn = DBCnn.GetCnn())
            {
                List<double> res = new List<double>();
                var cmd = cnn.CreateCommand();
                StringBuilder sb = new StringBuilder();
                dps.RunByBatch(50,(ls,s,e)=> {
                    sb.Clear();
                    sb.Append("INSERT IGNORE INTO device_datapoints(DeviceID,DataName,Value,Time)VALUES");
                    for (int i = s; i <e; i++)
                    {
                        var dp = ls[i];
                        sb.Append($"({dvid},'{dpname}','{dp.Item2}',{dp.Item1.BeijingTimeToJavaTicket()}),");
                          
                    }
                    sb.Remove(sb.Length-1,1);
                    cmd.CommandText = sb.ToString();
                    cmd.ExecuteNonQuery();
                }); 
            }
        }
    }

   
}
