//Add-Migration [--context MainContext]
//Remove-Migration 取消最近一次迁移
//Update-Database [迁移名称  迁移直到(包含)或回退直到(不回退指定的版本) 0表示一开始]
//Drop-Database 
//Get-Help about_EntityFrameworkCore
//get-help Add-Migration
//dotnet tool install --global dotnet-ef
//dotnet ef -h
//
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDBContext.Main
{
    /// <summary>
    /// 设备维修记录表
    /// </summary>
    [Index(nameof(DiscoveryTime), nameof(Id))]
    [Index(nameof(CompletionTime))]
    [Index(nameof(DeviceId))]
    public class Device_Repair : IHasCreator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int UserTreeId { get; set; }

        public long DeviceId { get; set; }

        public virtual Device Device { get; set; }

        /// <summary>
        /// 设备损坏发现时间
        /// </summary>
        public long DiscoveryTime { get; set; }
        /// <summary>
        /// 设备维修完成时间
        /// </summary>
        public long CompletionTime { get; set; }
        /// <summary>
        /// 维修详情
        /// </summary>
        public string Context { get; set; }

        public long CreatorId { get; set; }
        public virtual User Creator { get; }

    }
}
