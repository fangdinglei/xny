namespace GrpcMain
{

    public class DeviceColdDataHandleImp : IDeviceColdDataHandle
    {
        public bool UsingColdData => throw new NotImplementedException();

        public Task<List<(long, float)>> DeCompressDeviceData(long starttime, long endtime, long deviceid, long streamid, ref long Cursor, int count)
        {
            throw new NotImplementedException();
        }
    }
}
