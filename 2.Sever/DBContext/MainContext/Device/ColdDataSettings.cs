using System.ComponentModel.DataAnnotations;
namespace MyDBContext.Main
{
    public class DeviceColdDataSettings
    {
        [Key]
        public long TreeId { get; set; }
        public string ManagerName { get; set; }
        /// <summary>
        /// 单位 天
        /// </summary>
        public long ColdDownTime { get; set; }
        public long MinCount { get; set; }
        public bool Open { get; set; }
        //optional int64 AutoDeletTime=5;
    }

}