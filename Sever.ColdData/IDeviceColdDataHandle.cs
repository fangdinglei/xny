namespace Sever.ColdData
{
    /// <summary>
    /// 设备冷数据加载器
    /// </summary>
    public interface IDeviceColdDataLoader
    {
        bool UsingColdData { get; }
        Task<List<(long, float)>> DeCompressDeviceData(long starttime, long endtime, long deviceid, long streamid, ref long Cursor, int count);


    }
}
