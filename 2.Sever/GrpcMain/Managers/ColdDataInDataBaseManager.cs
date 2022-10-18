using MyDBContext.Main;

namespace GrpcMain.Managers
{
    public class ColdDataInDataBaseManager : ColdDataManagerBase
    {
        public override string Name => "InDB";

        public override Task<byte[]?> Load(Device_DataPoint_Cold colddata)
        {
            return Task.FromResult(colddata.Data); ;
        }

        public override Task Store(Device_DataPoint_Cold colddata)
        {
            return Task.FromResult(true);
        }


    }

}
