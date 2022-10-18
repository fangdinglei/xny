
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using XNYAPI.DAL;
using XNYAPI.Response;
using XNYAPI.Response.Data;
using XNYAPI.Utility;

namespace XNYAPI.Controllers
{
    [TokenCheckFilter(new string[] { })]
    public class DataServiceController : Controller
    {

        /// <summary>
        /// 获取设备数据点
        /// </summary>
        /// <param name="deviceids"></param>
        /// <param name="streamnames"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public string GetDataStreams(string deviceids, string streamnames, string starttime, string endtime)
        {
            long starttimex, endtimex;
            try
            {
                starttimex = DateTime.Parse(starttime).BeijingTimeToJavaTicket();
                endtimex = DateTime.Parse(endtime).BeijingTimeToJavaTicket();
            }
            catch (Exception)
            {
                return this.Error(XNYResponseBase.EErrorCode.ParameterWrong);
            }
            var streams = streamnames.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in streams)
                if (!item.IsSqlSafeString())
                    return this.Error(XNYResponseBase.EErrorCode.ParameterNotSafe);
            var ids = Utility.Utility.PraseIDS(deviceids);

            UserPayLoad payload = this.ViewBag.payload;
            using (var cnn = DBCnn.GetCnn())
            {
                var rsp = new GetDataStreamsResponse() { Data = new List<DataStreamsData>() };
                var cmd = cnn.CreateCommand();
                foreach (var id in ids)
                {
                    bool has = UserDeviceDAL.HasDevice(payload.UserID, id);
                    if (!has)
                        continue;
                    List<DataStreamData> ds = new List<DataStreamData>();
                    foreach (var stream in streams)
                    {
                        cmd.CommandText = $"SELECT Value,Time FROM device_datapoints" +
                        $" WHERE DeviceID={id} and  DataName='{stream}' and Time >={starttimex} and Time <={endtimex} ";
                        var rd = cmd.ExecuteReader();
                        List<DataPoint> points = new List<DataPoint>();
                        while (rd.Read())
                        {
                            points.Add(new DataPoint(rd.GetString(0), rd.GetInt64(1)));
                        }
                        rd.Close();
                        ds.Add(new DataStreamData() { StreamName = stream, Points = points });
                    }
                    rsp.Data.Add(new DataStreamsData() { DeviceID = id, Streams = ds });
                }
                return JsonConvert.SerializeObject(rsp);
            }

        }

        public string GetLatestData(string deviceids)
        {
            UserPayLoad payload = this.ViewBag.payload;
            var ids = Utility.Utility.PraseIDS(deviceids);
            try
            {
                List<LatestData> latestDatas = new List<LatestData>();
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    foreach (var id in ids)
                    {
                        cmd.CommandText = "SELECT DataNames FROM userdevice INNER JOIN deviceinfo " +
                              "ON  userdevice.DeviceID =deviceinfo.DeviceID " +
                              "INNER JOIN devicetype ON devicetype.ID =deviceinfo.Type WHERE userdevice.DeviceID =" + id;
                        var r = cmd.ExecuteScalar();
                        if (r == null)
                            continue;
                        string dn = Convert.ToString(r);
                        var dns = dn.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        List<DataPointWithName> dpwn = new List<DataPointWithName>();
                        foreach (var dname in dns)
                        {
                            cmd.CommandText = "SELECT Value,Time FROM device_datapoints " +
                               $"WHERE DeviceID={id} AND DataName='{dname}' ORDER BY Time DESC LIMIT 1";
                            var rd = cmd.ExecuteReader();
                            if (rd.Read())
                            {
                                dpwn.Add(new DataPointWithName(dname, rd.GetString(0), rd.GetInt64(1)));
                            }
                            rd.Close();
                        }
                        latestDatas.Add(new LatestData(id, dpwn));
                    }
                    return JsonConvert.SerializeObject(new GetLatestDataResponse(latestDatas));
                }
            }
            catch (Exception)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }
        }
        //public string GetDataStreamsFeature(string deviceids, string streamnames, string starttime, string endtime)
        //{
        //    long starttimex, endtimex;
        //    try
        //    {
        //        starttimex = DateTime.Parse(starttime).Ticks  ;
        //        endtimex = DateTime.Parse(endtime).Ticks  ;
        //    }
        //    catch (Exception)
        //    {
        //        return this.Error(XNYResponseBase.EErrorCode.ParameterWrong);
        //    }
        //    var ids = deviceids.Split(',', StringSplitOptions.RemoveEmptyEntries);
        //    foreach (var item in ids)
        //        if (!item.IsSqlSafeString())
        //            return this.Error(XNYResponseBase.EErrorCode.ParameterNotSafe);
        //    var streams = streamnames.Split(',', StringSplitOptions.RemoveEmptyEntries);
        //    foreach (var item in streams)
        //        if (!item.IsSqlSafeString())
        //            return this.Error(XNYResponseBase.EErrorCode.ParameterNotSafe);

        //    UserPayLoad payload = this.ViewBag.payload;

        //    using (var cnn = DBCnn.GetCnn())
        //    {
        //        var rsp = new GetDataStreamsFeatureResponse() { Data = new List<DataStreamsFeatureData>() };
        //        var cmd = cnn.CreateCommand();
        //        foreach (var id in ids)
        //        {
        //            List<DataStreamFeatureData> ds = new List<DataStreamFeatureData>();
        //            foreach (var stream in streams)
        //            {
        //                cmd.CommandText = $"SELECT AVG(Value),MAX(CAST(Value  AS DOUBLE)),MIN(CAST(Value  AS DOUBLE))  FROM device_datapoints" +
        //                $" WHERE DeviceID={id}  and DataName='{ stream }' and Time >={starttimex } and Time <={endtimex } ";
        //                var rd = cmd.ExecuteReader();
        //                double avg = 0;
        //                double max = 0;
        //                double min = 0;
        //                if (rd.Read())
        //                {
        //                    if (!rd.IsDBNull(0))
        //                    {
        //                        avg = Convert.ToDouble(rd.GetDouble(0));
        //                        max = Convert.ToDouble(rd.GetDouble(1));
        //                        min = Convert.ToDouble(rd.GetDouble(2));
        //                    }
        //                }
        //                ds.Add(new DataStreamFeatureData(stream, avg, max, min));
        //            }
        //            rsp.Data.Add(new DataStreamsFeatureData() { DeviceID = id, Streams = ds });
        //        }
        //        return JsonConvert.SerializeObject(rsp);
        //    }
        //}


        public ActionResult DataVisible()
        {
            return View();
        }



    }
}
