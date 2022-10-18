
using System;
using System.Collections.Generic;
using XNYAPI.AutoControl.Script;
using XNYAPI.AutoControl.Script.Model;
using XNYAPI.DAL;

namespace XNYAPI.AutoControl
{
    [AutoService(Name = "dataread", OnScript = "Run")]
    public class DataReaderManager
    {
        public static DataManager Instance = new DataManager();
        public int laststep = -1;
        DateTime now = DateTime.Now;


        public void Start(int step)
        {
            now = DateTime.Now;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="Exception" />
        public void Run(ScriptContext context, PageItem item)
        {
            if (context.Datas_double == null)
                context.Datas_double = new Dictionary<string, DataStream_Double>();
            //二氧化碳,10,10,double
            if (item.Parm.Length != 4)
                throw new Exception($"此服务只支持4个参数");
            int t;
            if (!int.TryParse(item.Parm[1], out t) || t <= 0 || t > 9999)
                throw new Exception($"参数2必须为[1,9999]的整数");
            int c;
            if (!int.TryParse(item.Parm[2], out c) || c <= 0 || c > 9999)
                throw new Exception($"参数3必须为[1,9999]的整数");
            if (item.Parm[3] == "double")
            {
                if (context.Datas_double == null)
                    context.Datas_double = new Dictionary<string, DataStream_Double>();
                context.Datas_double.Add(item.Parm[0],
                    new DataStream_Double(
                        DataServiceDAL.GetDoubleDataPoints2(context.DeviceID, item.Parm[0], now.AddSeconds(-t).BeijingTimeToJavaTicket(), c)
                        )
                    );
            }
            else
            {
                throw new Exception($"参数4必须为[double,] ");
            }



        }

    }
}
