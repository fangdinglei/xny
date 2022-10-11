using XNYAPI.AutoControl.Script.Model;
using Newtonsoft.Json;
namespace XNYAPI.Model.Device
{
    public class DeviceTypeInfo {
        public uint ID;
        public string Name;
        public string ScriptStringB64;
        public string DataNames;
        [JsonIgnore]
        public AutoScript Script;

        public DeviceTypeInfo(uint iD, string name, string scriptStringb64, string dataNames )
        {
            ID = iD;
            Name = name;
            ScriptStringB64 = scriptStringb64;
            DataNames = dataNames; 
        }
    }
}
