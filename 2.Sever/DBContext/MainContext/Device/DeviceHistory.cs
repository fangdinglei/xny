using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDBContext.Main
{
    public class DeviceHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int UserTreeId { get; set; }

        public short Type { get; set; }
        public long Time { get; set; }
        public bool Success { get; set; }

        public string Data { get; set; }

        public long DeviceId { get; set; }


        [NotMapped]
        public DeviceHistoryType _Type
        {
            get => Enum.IsDefined(typeof(DeviceHistoryType), Type)
                ? (DeviceHistoryType)Type
                : DeviceHistoryType.Unknown;
            set =>
               Type = (byte)value;
        }

        public DeviceHistory()
        {

        }
        public DeviceHistory(long deviceid, string cmd, DeviceCmdSenderType sendertype, long senderid, long time)
        {
            DeviceId = deviceid;
            Success = true;
            _Type = DeviceHistoryType.Command;
            Data = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                cmd,
                sendertype,
                senderid,
            });
            Time = time;
        }

    }
}