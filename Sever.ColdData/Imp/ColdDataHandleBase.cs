using MyDBContext.Main;

namespace Sever.ColdData.Imp
{
    public abstract class ColdDataHandleBase
    {

        public abstract string Name { get; }

        /// <summary>
        /// 对传入参数的修改也会同步到数据库中
        /// </summary>
        /// <param name="colddata"></param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public abstract Task Store(Device_DataPoint_Cold colddata, byte[] bytes);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colddata"></param>
        /// <returns>null if not success</returns>
        /// <exception cref="Exception"/>
        public abstract Task<byte[]?> Load(Device_DataPoint_Cold colddata);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="colddata"></param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public abstract Task<bool> Delet(Device_DataPoint_Cold colddata);
        /// <summary>
        /// 获取数据状态
        /// </summary>
        /// <param name="colddata"></param>
        /// <returns><see cref="Device_DataPoint_Cold.status"></returns>
        /// <exception cref="Exception"/>
        public abstract Task<int> GetStatus(Device_DataPoint_Cold colddata);
    }

}
