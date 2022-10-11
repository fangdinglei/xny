using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Add-Migration
//Remove-Migration
//Update-Database
//
namespace MyDBContext.Main
{
    [Table("t_kv")]
    public class KeyValue { 
        [Key]
        [Required]
        [StringLength(maximumLength:50, MinimumLength = 1)]
        public string Key { get; set; }

        [Required]
        [StringLength(maximumLength: 50,MinimumLength =1)]
        public string Value { get; set; }

        [DefaultValue(1)]
        [ConcurrencyCheck()]//并发一致性控制
        [Required(ErrorMessage = "Version不能为空")]
        public int Version { get; set; } = 1;
    }

}
