namespace MyDBContext.Main
{
    public class DeviceCmdHistory
    {
        public long ID { set; get; }
        public long DeviceId { set; get; }
        public string Cmd { set; get; }
        /// <summary>
        /// <see cref="DeviceCmdSenderType">
        /// </summary>
        public byte SenderType { set; get; }
        public long SenderId { set; get; }
        public long SendTime { set; get; }
        public bool Success { set; get; }
        public DeviceCmdHistory()
        {

        }
        public DeviceCmdHistory(long deviceId, string cmd, DeviceCmdSenderType senderType, long senderId, long sendTime)
        {
            DeviceId = deviceId;
            Cmd = cmd;
            SenderType = (byte)senderType;
            SenderId = senderId;
            SendTime = sendTime;
        }
    }
}