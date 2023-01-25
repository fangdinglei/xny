using MyDBContext.Main;


namespace Sever.ColdData
{
    internal interface IDeviceColdDataManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colddata"></param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        Task DoStore(Device_DataPoint_Cold colddata, byte[] bytes, Dictionary<string, object>? pars);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colddata"></param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        Task<byte[]> DoLoad(Device_DataPoint_Cold colddata, Dictionary<string, object>? pars = null);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="colddata"></param>
        /// <returns>是否删除成功,如不存在则返回false</returns>
        Task<bool> DoDelet(Device_DataPoint_Cold colddata);
        /// <summary>
        /// 获取数据状态 直接修改data中的状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns>状态</returns>
        Task<int> DoGetStatus(Device_DataPoint_Cold data);

        List<string> GetManagerNames();
    }
}
