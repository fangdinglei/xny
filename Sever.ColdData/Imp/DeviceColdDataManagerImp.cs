using MyDBContext.Main;

namespace Sever.ColdData.Imp
{
    public class DeviceColdDataManagerImp : IDeviceColdDataManager
    {

        static Dictionary<string, ColdDataManagerBase> mgrs = new();
        static DeviceColdDataManagerImp()
        {
            foreach (var tp in typeof(ColdDataManagerBase).Assembly.GetTypes())
            {
                if (tp.BaseType == typeof(ColdDataManagerBase))
                {
                    var mgr = (ColdDataManagerBase)Activator.CreateInstance(tp);
                    mgrs.Add(mgr.Name, mgr);
                }
            }
        }

        public async Task<byte[]> DoLoad(Device_DataPoint_Cold colddata, Dictionary<string, object>? pars)
        {
            var bytes = await mgrs[colddata.ManagerName].Load(colddata);
            return bytes;
        }

        public async Task DoStore(Device_DataPoint_Cold colddata, Dictionary<string, object>? pars)
        {
            await mgrs[colddata.ManagerName].Store(colddata);
        }

    }

}
