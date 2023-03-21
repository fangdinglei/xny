using MyDBContext.Main;

namespace Sever.ColdData
{
    /// <summary>
    /// 设备冷数据加载器
    /// </summary>
    public interface IDeviceColdDataService
    {
        bool UsingColdData { get; }
        /// <summary>
        /// 获取对应的数据并解析为集合
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="deviceid"></param>
        /// <param name="streamid"></param>
        /// <param name="Cursor"></param>
        /// <param name="count"></param>
        /// <param name="setcursor"></param>
        /// <exception cref="Exception"/>
        /// <returns></returns>
        Task<List<(long, float)>> DeCompressDeviceData(long starttime, long endtime, long deviceid, long streamid, long Cursor, int count, Action<long> setcursor);
        /// <summary>
        /// 获取冷数据信息
        /// </summary>
        /// <param name="deviceid"></param>
        /// <param name="steamid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="Cursor"></param>
        /// <param name="count"></param>
        /// <param name="setcursor"></param>
        /// <exception cref="Exception"/>
        /// <returns></returns>
        Task<List<Device_DataPoint_Cold>> GetDataInfo(long? deviceid, long? steamid, long? starttime, long? endtime, long Cursor, int count, Action<long> setcursor);

        /// <summary>
        /// 删除冷数据
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"/>
        /// <returns></returns>
        Task<bool> DoDelet(long id);
        Task<bool> DoCombine(long id1, long id2);

        Task<bool> DoStore(Device_DataPoint_Cold data, byte[] data2);

        List<string> GetManagerNames();

    }
}
