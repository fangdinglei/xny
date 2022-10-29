
using System;
using XNYAPI.AutoControl.Script;
using XNYAPI.AutoControl.Script.Model;
using XNYAPI.Utility;

namespace XNYAPI.AutoControl
{
    [AutoService(Name = "data", OnScript = "Run", OnStart = "Start", OnEnd = "End")]
    public class DataManager
    {
        public static DataManager Instance = new DataManager();

        DateTime lastupdatedt = DateTime.Now;
        DateTime now = DateTime.Now;

        /// <summary>
        /// 拉取平台信息并存储到数据库中
        /// </summary>
        /// <param name="last"></param>
        public void LoadNewData(uint ownerid, string realid, string streamids, DateTime last)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                Func<string, string> valueselector = (it) =>
                {
                    return it.Replace("\"", "");
                };

                string cur = "";
                while (cur != null)
                {
                    //var onenetrsp2 = OneNetUtility.GetClient().Execute(new OneNET.Api.Request.GetDataPointsRequest()
                    //{
                    //    DeviceID = realid,
                    //    DataStreamId = streamids,
                    //    StartTimeTxt = OneNetUtility.fmtDateTime(last),
                    //    EndTimeTxt = OneNetUtility.fmtDateTime(now),
                    //    Cursor = cur
                    //});
                    //todo error
                    //foreach (var stream in onenetrsp2.Data.Datastreams)
                    //{
                    //    DataServiceDAL.AddDataPoints(ownerid, stream.ID, stream.Datapoints.Select((it) => (DateTime.Parse(it.At),
                    //       valueselector((string)it.Value.Value))).ToList());
                    //}
                    //cur = onenetrsp2.Data.Cursor;
                }
            }
        }

        public void Start(int step)
        {
            if (step % 30 != 0)
                return;
            string lastupdate = DBUtility.GetKV("DataPointsLastUpdate", (DateTime.Now.AddDays(-10).BeijingTimeToJavaTicket().ToString()));
            lastupdatedt = long.Parse(lastupdate).JavaTicketToBeijingTime();
            now = DateTime.Now;

        }
        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="Exception" />
        public void Run(ScriptContext context, PageItem item)
        {
            LoadNewData(context.DeviceID, context.RealID, context.Type.DataNames, lastupdatedt);
        }

        public void End(int step)
        {
            if (step % 30 != 0)
                return;
            DBUtility.SetKV("DataPointsLastUpdate", now.BeijingTimeToJavaTicket().ToString());
        }
    }
}
