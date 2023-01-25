using System.ComponentModel.DataAnnotations;
namespace MyDBContext.Main
{
    public class ColdDataSettings {
        [Key]
        public long TreeId { get; set; }
        public string ManagerName { get; set; }
        public long ColdDownTime { get; set; }
        public long MinCount { get; set; }
        //optional int64 AutoDeletTime=5;
    }

}