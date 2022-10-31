using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Add-Migration [--context MainContext]
//Remove-Migration 取消最近一次迁移
//Update-Database [迁移名称  迁移直到(包含)或回退直到(不回退指定的版本) 0表示一开始]
//Drop-Database 
//Get-Help about_EntityFrameworkCore
//get-help Add-Migration
//dotnet tool install --global dotnet-ef
//dotnet ef -h
//
namespace MyDBContext.Main
{
    [Index(nameof(UserTreeId))]
    [Index(nameof(Name))]
    public class User : IHasCreator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public String Name { get; set; }
        public string Pass { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public byte Status { get; set; }
        public long LastLogin { get; set; }

        [Comment("表示用户属于哪一个用户族")]
        public int UserTreeId { get; set; }
        public int TreeDeep { get; set; }
        public int MaxSubUser { get; set; }
        public int MaxSubUserDepth { get; set; }

        [Required]
        public string Authoritys { get; set; }

        public virtual User? Creator { get; set; }
        public long CreatorId { get; set; }

        //public virtual ICollection< Device> Devices { get; set; }

        //public virtual ICollection<User_Device_Group> User_Device_Groups { get; set; }
        //public virtual ICollection<User_Device> User_Devices { get; set; }

    }
}
