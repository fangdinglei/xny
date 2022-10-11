namespace XNYAPI.Model.AutoControl
{
    public struct AutoControlSettings
    {
        /// <summary>
        /// 其所有者的ID设备ID或者组ID
        /// </summary>
        public uint OwnerID;
        public uint GroupID;
        public bool TimeScheduleEnabled;
        public bool AdvancedControlEnabled;
        public bool IsGroup;
        public ServiceType Type;

        public AutoControlSettings(uint ownerID, uint groupID, bool timeScheduleEnabled, bool advancedControlEnabled, bool isGroup, ServiceType type)
        {
            OwnerID = ownerID;
            GroupID = groupID;
            TimeScheduleEnabled = timeScheduleEnabled;
            AdvancedControlEnabled = advancedControlEnabled;
            IsGroup = isGroup;
            Type = type;
        }
    }
}

