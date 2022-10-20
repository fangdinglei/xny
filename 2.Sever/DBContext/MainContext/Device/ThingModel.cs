using System.ComponentModel.DataAnnotations.Schema;

namespace MyDBContext.Main
{
    public class ThingModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public virtual Device_Type DeviceType{ get; set; }
        public long DeviceTypeId { get; set; }
        public string Name { get; set; }
        public ThingModelValueType Type { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        public float MinValue { get; set; }
        public float MaxValue { get; set; }

        public bool Abandonted { get; set; }
    }
}