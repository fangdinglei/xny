using MyDBContext.Main;

namespace GrpcMain.Managers
{
    public abstract class ColdDataManagerBase
    {


        public abstract string Name { get; }

        /// <summary>
        /// 对传入参数的修改也会同步到数据库中
        /// </summary>
        /// <param name="colddata"></param>
        /// <returns></returns>
        public abstract Task Store(Device_DataPoint_Cold colddata);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colddata"></param>
        /// <returns>null if not success</returns>
        public abstract Task<byte[]> Load(Device_DataPoint_Cold colddata);

        static Dictionary<string, ColdDataManagerBase> mgrs = new();
        static public async Task DoStore(Device_DataPoint_Cold colddata)
        {
            await mgrs[colddata.ManagerName].Store(colddata);
        }
        static public async Task<byte[]> DoLoad(Device_DataPoint_Cold colddata)
        {
            return await mgrs[colddata.ManagerName].Load(colddata);
        }
    }

}
