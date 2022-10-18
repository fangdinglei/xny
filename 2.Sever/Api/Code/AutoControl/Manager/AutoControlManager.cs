
using System;
using System.Collections.Generic;

using TimerMvcWeb.Filters;
using XNYAPI.AutoControl.Script;
using XNYAPI.DAL;

using XNYAPI.Model.AutoControl;
using XNYAPI.Model.Device;
using XNYAPI.Utility;


namespace XNYAPI.AutoControl
{
    [AutoTask(OnTimeCall = "AutoUpdate", OnLoadCall = "Init", Name = "autocontrol", IntervalSeconds = 1)]
    public class AutoControlManager
    {
        static int step = -1;//每个step为1秒
        static public Dictionary<uint, DeviceTypeInfo> DTODefine;// = new Dictionary<uint, DeviceTypeInfo>();
        static public Dictionary<uint, DeviceData> Devices;// = new Dictionary<uint, DeviceData>();

        static public bool Inited = false;
        static public void Init()
        {
            try
            {
                AutoControlScriptManager.RegistServices();

                DTODefine = new Dictionary<uint, DeviceTypeInfo>();
                Devices = new Dictionary<uint, DeviceData>();
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    var tinfos = DeviceTypeDAL.GetTypeInfos(cmd);
                    foreach (var item in tinfos)
                    {
                        if (string.IsNullOrWhiteSpace(item.ScriptStringB64))
                            item.Script = null;
                        else
                            item.Script = AutoControlScriptManager.Prase(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(item.ScriptStringB64)));
                        DTODefine.Add(item.ID, item);
                    }

                    var devices = DeviceDAL.GetAllDevice_ID_TypeID(cmd);
                    foreach (var item in devices)
                    {
                        var d = new DeviceData();
                        d.DeviceID = item.Item1;
                        d.DeviceTypeID = item.Item2;
                        d.DeviceRealID = DeviceDAL.GetRealID(d.DeviceID, cmd);
                        Devices.Add(item.Item1, d);
                    }
                }
                Inited = true;
            }
            catch (Exception e)
            {
                Logger.Log(e);
                throw;
            }


        }

        static bool run = false;
        static object locker = new object();
        static public void AutoUpdate()
        {
            if (!Inited)
                return;
            lock (locker)
            {
                if (run)
                    return;
                else
                    run = true;
            }
            step++;
            step = step % 10000;
            try
            {
                AutoControlScriptManager.OnStart(step);
                foreach (var dv in Devices.Values)
                {
                    DeviceTypeInfo tinfo;
                    if (!DTODefine.TryGetValue(dv.DeviceTypeID, out tinfo))
                    {
                        Logger.Log(Logger.ERROR, $"设备{dv.DeviceID}所属的类型不存在");
                        continue;
                    }
                    if (tinfo.Script == null)
                        continue;
                    AutoControlScriptManager.RunScript(tinfo.Script, dv.DeviceID, dv.DeviceRealID, tinfo, step);
                }
                AutoControlScriptManager.OnEnd(step);
            }
            catch (Exception)
            {
            }

            lock (locker)
            {
                run = false;
            }
        }



    }
}
