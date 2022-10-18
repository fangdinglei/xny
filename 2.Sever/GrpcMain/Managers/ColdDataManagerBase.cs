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
        public abstract Task<byte[]?> Load(Device_DataPoint_Cold colddata);

        static Dictionary<string, ColdDataManagerBase> mgrs = new();
        static public void DoStore(Device_DataPoint_Cold colddata)
        {
            mgrs[colddata.ManagerName].Store(colddata);
        }
    }

}
