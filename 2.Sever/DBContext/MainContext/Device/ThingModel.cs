using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyDBContext.Main
{
    public class ThingModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long UserTreeId { get; set; }
        public virtual Device_Type DeviceType { get; set; }
        public long DeviceTypeId { get; set; }
        public string Name { get; set; }
        public byte Type { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        public float MinValue { get; set; }
        public float MaxValue { get; set; }

        public float AlertLowValue { get; set; }
        public float AlertHighValue { get; set; }

        /// <summary>
        /// 处于异常值多久（分钟）触发预警
        /// </summary>
        public int AlertTime { get; set; }

        public bool Abandonted { get; set; }

        [NotMapped]
        public ThingModelValueType _Type
        {
            get => Enum.IsDefined(typeof(ThingModelValueType), Type)
                ? (ThingModelValueType)Type
                : ThingModelValueType.None;
            set =>
              Type = (byte)value;
        }
    }
}