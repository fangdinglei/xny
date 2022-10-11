namespace XNYAPI.Model.Device
{
    public class DeviceInfo {
        public uint  ID;
        public uint TypeID;
        public uint GroupID;
        public string Name;
        public string Location;

        public DeviceInfo()
        {
        }

        public DeviceInfo(uint iD, uint groupID, string name, string location,uint typeid)
        {
            ID = iD;
            GroupID = groupID;
            Name = name;
            Location = location;
            TypeID = typeid  ;
        }
    }
}
