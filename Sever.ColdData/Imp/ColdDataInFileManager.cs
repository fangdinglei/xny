using MyDBContext.Main;

namespace Sever.ColdData.Imp
{
    public class ColdDataInFileManager : ColdDataManagerBase
    {
        public override string Name => "InFile";

        public override Task<bool> Delet(Device_DataPoint_Cold colddata)
        {
            File.Delete("ColdData" + colddata.Id);
            return Task.FromResult(true);
        }

        public override Task<int> GetStatus(Device_DataPoint_Cold colddata)
        {
            if (File.Exists("ColdData" + colddata.Id))
            {
                return Task.FromResult(1);
            }
            else
            {
                return Task.FromResult(2);
            }
        }

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


        public override async Task Store(Device_DataPoint_Cold colddata, byte[] bytes)
        {
            var name = "ColdData" + colddata.Id;
            if (File.Exists("ColdData" + colddata.Id))
            {
                throw new Exception("文件存在");
            }
            await File.WriteAllBytesAsync(name, bytes);
        }
    }

}
