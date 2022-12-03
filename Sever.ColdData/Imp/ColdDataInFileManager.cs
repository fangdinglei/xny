using MyDBContext.Main;

namespace Sever.ColdData.Imp
{
    public class ColdDataInFileManager : ColdDataManagerBase
    {
        public override string Name => "InFile";

        public override async Task<byte[]?> Load(Device_DataPoint_Cold colddata)
        {
            var name = "ColdData" + colddata.Id;
            if (File.Exists("ColdData" + colddata.Id))
            {
                var bytes = await File.ReadAllBytesAsync(name);
                return bytes;
            }
            else
            {
                return null;
            }
        }

        public override async Task Store(Device_DataPoint_Cold colddata)
        {
            var name = "ColdData" + colddata.Id;
            if (File.Exists("ColdData" + colddata.Id))
            {
                throw new Exception("文件存在");
            }
            await File.WriteAllBytesAsync(name, colddata.Data);
            colddata.Data = new byte[0];
        }

    }

}
