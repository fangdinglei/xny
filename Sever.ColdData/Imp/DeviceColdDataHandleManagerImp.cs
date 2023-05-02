using MyDBContext.Main;

namespace Sever.ColdData.Imp
{
    internal class DeviceColdDataHandleManagerImp : IDeviceColdDataHandleManager
    {

        static Dictionary<string, ColdDataHandleBase> mgrs = new();
        static DeviceColdDataHandleManagerImp()
        {
            foreach (var tp in typeof(ColdDataHandleBase).Assembly.GetTypes())
            {
                if (tp.BaseType == typeof(ColdDataHandleBase))
                {
                    var mgr = (ColdDataHandleBase)Activator.CreateInstance(tp);
                    mgrs.Add(mgr.Name, mgr);
                }
            }
        }


        public async Task<bool> DoDelet(Device_DataPoint_Cold colddata)
        {
            return await mgrs[colddata.ManagerName].Delet(colddata);
        }

        public async Task<int> DoGetStatus(Device_DataPoint_Cold colddata)
        {
            try
            {
                return await mgrs[colddata.ManagerName].GetStatus(colddata);
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public async Task<byte[]?> DoLoad(Device_DataPoint_Cold colddata, Dictionary<string, object>? pars)
        {
            var bytes = await mgrs[colddata.ManagerName].Load(colddata);
            return bytes;
        }

        public async Task DoStore(Device_DataPoint_Cold colddata, byte[] bytes, Dictionary<string, object>? pars)
        {
            await mgrs[colddata.ManagerName].Store(colddata, bytes);
        }

        public List<string> GetManagerNames()
        {
            return mgrs.Keys.ToList();
        }
    }

}
