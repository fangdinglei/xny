namespace XNYAPI.Model.UserDevice
{
    public class DeviceGroup
    {
        public uint GroupID;
        public string Name;

        public DeviceGroup(uint groupID, string name)
        {
            GroupID = groupID;
            Name = name;
        }
    }
}
