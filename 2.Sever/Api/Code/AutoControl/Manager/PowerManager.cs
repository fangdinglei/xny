using System;
using System.Collections.Generic;
using System.Linq;
using XNYAPI.AutoControl.Script;
using XNYAPI.AutoControl.Script.Model;
using XNYAPI.DAL;
using XNYAPI.Model.AutoControl;
using XNYAPI.Utility;

namespace XNYAPI.AutoControl
{
    [AutoService(Name = "power", OnScript = "Run")]
    public class PowerManager
    {
        static public PowerManager Instance = new PowerManager();

        Func<double, double, double, double, bool, double> m_Calculater;

        Func<double, double, double, double, bool, double> DefaultCalculater = (soc, sec, lastp, newp, discharge) =>
        {
            double re;
            if (discharge)
            {
                re = soc - (sec * (lastp + newp)) / 2;
                ///Debuger.Log($"{(discharge?"放电":"充电")}\t原\t{soc.ToString("f1")}\t现\t{re.ToString("f1")}\t-{(soc-re).ToString("f1")}");
            }
            else
            {
                re = soc + (sec * (lastp + newp)) / 2;
                //Debuger.Log($"{(discharge ? "放电" : "充电")}\t原\t{soc.ToString("f1")}\t现\t{re.ToString("f1")}\t+{(re- soc).ToString("f1")}");
            }
            Logger.Log(Logger.CALCLULATION, "电量管理", "{" +
                $"\"开始电量\":{soc.ToString("f2")},\"时间间隔\":{sec.ToString("f2")},\"上次功率\":{lastp.ToString("f2")}" +
                $",\"此次功率\":{newp.ToString("f2")},\"放电\":{(discharge ? "是" : "否")}"
                + "}");
            return re;

        };

        public Func<double, double, double, double, bool, double> Calculater { get => m_Calculater; set => m_Calculater = value; }
        //public Action<Dictionary<uint, PowerInfo>> OnDataChanged { get => m_OnDataChanged; set => m_OnDataChanged = value; }

        public PowerManager()
        {

        }


        /// <summary>
        /// 获取设备剩余电量
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <returns>0-1剩余电量</returns>
        //public Tuple<bool, double> GetSocRate(uint deviceId)
        //{
        //    if (!AutoControlManager.Devices.ContainsKey(deviceId))
        //        return new Tuple<bool, double>(false, 0);
        //    return new Tuple<bool, double>(true,
        //        AutoControlManager.Devices[deviceId].power.SOC
        //        / AutoControlManager.Devices[deviceId].power.SOC);
        //}
        ///// <summary>
        ///// 获取设备电量信息
        ///// </summary>
        ///// <param name="deviceId"></param>
        ///// <returns>是否存在   当前电量  总电量</returns>
        //public Tuple<bool, double, double> GetSocInfo(uint deviceId)
        //{
        //    if (!AutoControlManager.Devices.ContainsKey(deviceId))
        //        return new Tuple<bool, double, double>(false, 0, 0);
        //    return new Tuple<bool, double, double>(true,
        //        AutoControlManager.Devices[deviceId].power.SOC
        //        , AutoControlManager.Devices[deviceId].power.MaxSOC);
        //}

        /// <summary>
        /// 更新电量数据 充电
        /// </summary>
        /// <param name="deviceId">设备id</param>
        /// <param name="datalist">Item：数据产生时间，功率值</param>
        /// <exception cref="Exception"/>
        void UpdateSingleCharge(PowerInfo info, List<ValueTuple<DateTime, double>> datalist)
        {
            if (datalist.Count == 0)
                return;
            Func<double, double, double, double, bool, double> calculater = Calculater ?? DefaultCalculater;
            //对每个有效数据进行计算
            for (int ptr = 0; ptr < datalist.Count - 1; ptr++)
            {
                var item = datalist[ptr];
                var item2 = datalist[ptr + 1];
                double sec = (item2.Item1 - item.Item1).TotalSeconds;
                info.SOC = calculater(info.SOC, sec, info.LastChargeP, item.Item2, false);
            }
            if (datalist.Count > 0)
            {
                info.LastChargeP = datalist[datalist.Count - 1].Item2;
                info.LastChargePUpdate = datalist[datalist.Count - 1].Item1;
            }
        }
        /// <summary>
        /// 更新电量数据 放电
        /// </summary>
        /// <param name="deviceId">设备id</param>
        /// <param name="datalist">Item：数据产生时间，功率值</param>
        void UpdateSingleDisCharge(PowerInfo info, List<ValueTuple<DateTime, double>> datalist)
        {
            if (datalist.Count == 0)
                return;

            Func<double, double, double, double, bool, double> calculater = Calculater ?? DefaultCalculater;
            //对每个有效数据进行计算
            for (int ptr = 0; ptr < datalist.Count - 1; ptr++)
            {
                var item = datalist[ptr];
                var item2 = datalist[ptr + 1];
                double sec = (item2.Item1 - item.Item1).TotalSeconds;
                info.SOC = calculater(info.SOC, sec, info.LastChargeP, item.Item2, false);
            }
            if (datalist.Count > 0)
            {
                info.LastChargeP = datalist[datalist.Count - 1].Item2;
                info.LastChargePUpdate = datalist[datalist.Count - 1].Item1;
            }
        }
        void UpdateSingle(ScriptContext dv, PageItem item, out PowerInfo info)
        {
            info = null;
            uint dvid = dv.DeviceID;
            string mode = item.Parm[0];
            if (mode == "cal")
            {
                int timeout; double maxsoc;
                if (!int.TryParse(item.Parm[1], out timeout))
                    throw new Exception("参数2异常");
                if (!double.TryParse(item.Parm[2], out maxsoc))
                    throw new Exception("参数3异常");
                info = PowerServiceDAL.LoadPowerInfo(dvid);
                if (info == null)
                {
                    info = new PowerInfo(dvid, DateTime.Now, DateTime.Now, maxsoc, maxsoc);
                    PowerServiceDAL.SetPowerInfo(info);
                    dv.PowerRate = info.SocRate;
                    return;
                }

                DateTime dt = (DateTime.Now - info.LastChargePUpdate).TotalDays > 2.0 ? DateTime.Now.AddDays(-2) : info.LastChargePUpdate;
                //TODO 数据类型
                var pin = DataServiceDAL.GetDoubleDataPoints(dv.DeviceID, "Power", dt.BeijingTimeToJavaTicket());
                if (pin.Count == 0)
                {
                    dv.PowerRate = info.SocRate;
                    return;
                }
                bool bk = false;
                int i;
                for (i = pin.Count - 1; i >= 1; i++)
                {
                    if ((pin[i].Item1 - pin[i - 1].Item1).TotalMinutes > timeout)
                    {
                        bk = true;
                        break;
                    }
                }
                if (bk)
                {
                    pin = pin.Skip(i).ToList();
                    info.SOC = maxsoc;
                }
                else
                {
                    if ((pin[0].Item1 - info.LastChargePUpdate).TotalMinutes > timeout)
                    {
                        info.SOC = maxsoc;
                    }
                }
                UpdateSingleCharge(info, pin);
                dv.PowerRate = info.SocRate;
                PowerServiceDAL.SetPowerInfo(info);
            }
            else if (mode == "rd")
            {
                throw new Exception("尚未完成");
            }
            else if (mode == "db")
            {
                var rd = PowerServiceDAL.GetPowerRate(dv.DeviceID);
                if (rd == null)
                    dv.PowerRate = 0;
                else
                    dv.PowerRate = rd.Rate;
            }
            else
            {
                throw new Exception("参数1只能是指定的几种");
            }

        }


        public void Run(ScriptContext context, PageItem item)
        {
            if (!context.Online)
                return;

            UpdateSingle(context, item, out PowerInfo power);
            if (power != null)
                PowerServiceDAL.SetPowerInfo(power);
        }
    }
}
