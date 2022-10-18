using System;


namespace XNYAPI.Model.AutoControl
{
    public enum CmdType : byte
    {
        Non = 0,
        LedStatusChange = 1,
        LedStatusChange_Group = 2,
    }
    public class DeviceCmd
    {
        public uint ID;
        public CmdType Type;
        public uint OwnerID;
        public string CMDString;
        public DateTime CreatTime;
        public DateTime TimeOut;
        public bool Sended;

        public DeviceCmd()
        {
        }

        public DeviceCmd(CmdType type, uint ownerID, string cMDString, DateTime creatTime, DateTime timeOut)
        {
            Type = type;
            OwnerID = ownerID;
            CMDString = cMDString;
            CreatTime = creatTime;
            TimeOut = timeOut;
            Sended = false;
        }
    }
}

