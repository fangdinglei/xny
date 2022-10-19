namespace GrpcMain
{
    public interface IDeviceColdDataHandle
    {
        bool UsingColdData { get; }
        Task<List<(long, float)>> DeCompressDeviceData(long starttime, long endtime, long deviceid, long streamid, ref long Cursor, int count);


    }
}
