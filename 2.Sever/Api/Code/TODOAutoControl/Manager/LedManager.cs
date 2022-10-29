
using System;
using System.Collections.Generic;
using XNYAPI.AutoControl.Script;
using XNYAPI.AutoControl.Script.Model;
using XNYAPI.DAL;
using XNYAPI.Model;
using XNYAPI.Model.AutoControl;
using XNYAPI.Utility;

namespace XNYAPI.AutoControl
{
    [AutoService(Name = "led", OnScript = "Run", OnEnd = "End")]
    public class LedManager
    {
        static public LedManager Instance = new LedManager();

        Func<ScriptContext, bool, bool> m_Calculater;
        Func<ScriptContext, bool, bool> DefaultCalculater = (data, open) =>
        {
            try
            {
                //double 光照强度=3000;double 湿度 = 110;double 温度 = 30; double 二氧化碳 = 450;
                Func<string, bool> checker = (name) =>
                    {
                        if (!data.Datas_double.ContainsKey(name) || data.Datas_double[name].points.Count == 0)
                            return false;
                        //return data.Datas[name].Full;
                        return true;
                    };
                bool c = checker("Temperature") && checker("Lumination") && checker("eCO2") && checker("Humidity") && data.PowerRate > 0;
                if (!c)
                    return false;
                double 光照强度 = data.Datas_double["Lumination"].Avg();
                double 湿度 = data.Datas_double["Humidity"].Avg();
                double 温度 = data.Datas_double["Temperature"].Avg();
                double 二氧化碳 = data.Datas_double["eCO2"].Avg();

                Logger.Log("LED-PData-1", new Dictionary<string, object>{
                    { "光照强度", 光照强度 } ,   { "湿度", 湿度 } ,
                    { "温度", 温度 } , { "二氧化碳", 二氧化碳 }
                });

                double 二氧化碳得分 = (Math.Atan((二氧化碳 - 380) / 110) - Math.Atan((二氧化碳 - 520) / 130)) / 1.0621;
                double 温度得分 = (Math.Atan((温度 - 20) / 15) - Math.Atan((温度 - 34) / 10)) / 1.056;
                double 湿度优化 = (Math.Atan((湿度 - 60) / 13) - Math.Atan((湿度 - 110) / 15)) / 2.122;
                double 净光合速率 = 595.5414 * ((Math.Atan((光照强度 + 2000) / (205 * 55))) - (Math.Atan(光照强度 / (205 * 55)))) / 2000;
                double 与极限转化率比值 = 净光合速率 / 0.052276;
                double 补光效果 = 0.4 * 与极限转化率比值 + 0.15 * 湿度优化 + 0.3 * 温度得分 + 0.15 * 二氧化碳得分;
                double 开灯电量 = -0.6 * Math.Atan((补光效果 - 1) / 0.08) + 0.15;
                double 关灯电量 = -0.6 * Math.Atan((补光效果 - 1) / 0.18) + 0.1;

                Logger.Log("LED-PData-2", new Dictionary<string, object>{
                    { "二氧化碳得分",二氧化碳得分 } ,   { "温度得分", 温度得分 } ,
                    { "湿度优化", 湿度优化 } , { "净光合速率", 净光合速率 },
                      { "与极限转化率比值",与极限转化率比值 } ,   { "补光效果", 补光效果 } ,
                    { "开灯电量", 开灯电量 } , { "关灯电量", 关灯电量 },
                });

                bool re;
                if (与极限转化率比值 < 0.4f || 湿度优化 < 0.4f || 温度得分 < 0.4f || 二氧化碳得分 < 0.4f)
                    re = false;
                if (open && data.PowerRate <= 关灯电量)
                    re = false;
                else if (!open && data.PowerRate >= 开灯电量)
                    re = true;
                else
                    re = open;

                Logger.Log("LED-PData-3", new Dictionary<string, object> { { "开关", re } });
                return re;
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return false;
            }
        };
        public Func<ScriptContext, bool, bool> Calculater { get => m_Calculater; set => m_Calculater = value; }

        public LedManager()
        {
        }

        public List<uint> OpenList = new List<uint>();
        public List<uint> CloseList = new List<uint>();
        #region 控制器执行
        bool SetLedState()
        {
            try
            {

                DeviceUtility.SendCMD(OpenList, "status:2", 1);
                DeviceUtility.SendCMD(CloseList, "status:3", 1);
                OpenList.Clear();
                CloseList.Clear();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 定时计划
        /// </summary>
        /// <param name="info"></param>
        /// <param name="data"></param>
        void UpdateSingle_RunShedule(ScriptContext context, uint dvid, LedInfo data)
        {
            DateTime now = DateTime.Now;
            //计划没有加载或者没有项目
            if (data.Schedule == null || data.Schedule.Data == null || data.Schedule.Data.Count == 0)
                return;
            LEDAutoControlEvent evt = (LEDAutoControlEvent)data.Schedule.GetValue(now, (int)LEDAutoControlEvent.Non);
            if (evt == LEDAutoControlEvent.Close)
            {
                CloseList.Add(dvid);
                return;
            }
            else if (evt == LEDAutoControlEvent.Open)
            {
                OpenList.Add(dvid);
                return;
            }
            else if (evt == LEDAutoControlEvent.Advanced)
            {
                if (data.Settings.AdvancedControlEnabled)
                {
                    UpdateSingle_RunAdvancedAuto(context, dvid, data);
                }
                return;
            }
            else if (evt == LEDAutoControlEvent.Non)
            {
                return;
            }
            else
            {//>2
                Logger.Log(Logger.DBDATAERROR, $"LED {dvid} 定时任务 出现异常值");
            }
        }
        /// <summary>
        /// 智能控制
        /// </summary>
        /// <param name="info"></param>
        /// <param name="data"></param>
        void UpdateSingle_RunAdvancedAuto(ScriptContext context, uint dvid, LedInfo data)
        {
            Func<ScriptContext, bool, bool> cal = Calculater ?? DefaultCalculater;
            var open = cal(context, data.LedOpen);
            if (open)
                OpenList.Add(dvid);
            else
                CloseList.Add(dvid);
        }
        /// <summary>
        /// 更新单个设备的状态
        /// </summary>
        void UpdateSingle(ScriptContext context, Dictionary<uint, LedInfo> group)
        {
            if (!context.Online)
                return;
            var linfo = new LedInfo();
            var dvid = context.DeviceID;
            linfo.Settings = LedServiceDAL.GetAutoControlSetting(dvid, false);
            if (linfo.Settings.GroupID == 0)
            {
                linfo.Schedule = LedServiceDAL.GetAutoControlScheduleData(dvid, false, ServiceType.DeviceLEDControl);
            }
            else
            {
                var gid = linfo.Settings.GroupID;
                if (!group.ContainsKey(gid))
                {//没有加载分组
                    group[gid] = new LedInfo()
                    {
                        LedOpen = false,
                        Settings = LedServiceDAL.GetAutoControlSetting(gid, true),
                        Schedule = LedServiceDAL.GetAutoControlScheduleData(gid, true, ServiceType.DeviceLEDControl)
                    };
                }
                //将信息设置为组的信息
                linfo.Settings = group[gid].Settings;
                linfo.Schedule = group[gid].Schedule;
            }

            if (linfo.Settings.TimeScheduleEnabled)
            {//优先使用定时控制
                UpdateSingle_RunShedule(context, dvid, linfo);
            }
            else if (linfo.Settings.AdvancedControlEnabled)
            {//使用控制器指定的算法控制
                UpdateSingle_RunAdvancedAuto(context, dvid, linfo);
            }
            else
            {

            }
        }


        public void Run(ScriptContext context, PageItem item)
        {
            Dictionary<uint, LedInfo> groupinfos = new Dictionary<uint, LedInfo>();
            UpdateSingle(context, groupinfos);
        }
        public void End(int step)
        {
            SetLedState();
        }
        #endregion
    }

}
