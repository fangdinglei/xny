//Add-Migration [--context MainContext]
//Remove-Migration 取消最近一次迁移
//Update-Database [迁移名称  迁移直到(包含)或回退直到(不回退指定的版本) 0表示一开始]
//Drop-Database 
//Get-Help about_EntityFrameworkCore
//get-help Add-Migration
//dotnet tool install --global dotnet-ef
//dotnet ef -h
//
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDBContext.Main
{
    public class Device : IHasCreator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 1未激活 2离线 3在线 4
        /// </summary>
        public int Status { get; set; }
        public string LatestData { get; set; }
        public string LocationStr { get; set; }

        public long DeviceTypeId { get; set; }
        public virtual Device_Type DeviceType { get; set; }

        public long CreatorId { get; set; }
        public virtual User Creator { get; }

        //public virtual Device_AutoControl Device_AutoControl { get; set; } 
    }
}
