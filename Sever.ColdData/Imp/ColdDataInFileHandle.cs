using MyDBContext.Main;

namespace Sever.ColdData.Imp
{
    public class ColdDataInFileHandle : ColdDataHandleBase
    {
        public override string Name => "InFile";
        public string FilePath = "ColdData/";
        public string SettingFileName = "_setting";
        public string DiskID;
        public ColdDataInFileHandle()
        {
            var settingpath = FilePath + SettingFileName;
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            if (File.Exists(settingpath))
            {
                DiskID = File.ReadAllText(settingpath);
            }
            else
            {
                DiskID = Guid.NewGuid().ToString();
                File.AppendAllText(settingpath, DiskID);
            }
        }
        public override Task<bool> Delet(Device_DataPoint_Cold colddata)
        {
            var exist = File.Exists(FilePath + colddata.Id);
            if (exist)
            {
                File.Delete(FilePath + colddata.Id);
            }
            return Task.FromResult(exist);
        }

        public override Task<int> GetStatus(Device_DataPoint_Cold colddata)
        {
            if (DiskID != colddata.Pars)
            {
                return Task.FromResult(0);
            }
            else if (File.Exists(FilePath + colddata.Id))
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
            var name = FilePath + colddata.Id;
            if (DiskID == colddata.Pars && File.Exists(FilePath + colddata.Id))
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

            var name = FilePath + colddata.Id;
            if (File.Exists(FilePath + colddata.Id))
            {
                throw new Exception("文件存在");
            }
            await File.WriteAllBytesAsync(name, bytes);
            colddata.Pars = DiskID;
        }
    }

}
