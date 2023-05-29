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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyDBContext.Main
{
    [Index(nameof(UserTreeId))]
    [Index(nameof(Name))]
    public class Device : IHasCreator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int UserTreeId { get; set; }


        public string Name { get; set; }

        /// <summary>
        /// 1未激活 2离线 3在线 4
        /// </summary>
        public int Status { get; set; }
        public string LatestData { get; set; }
        public string LocationStr { get; set; }

        /// <summary>
        /// 是否启用邮件预警
        /// </summary>
        [DefaultValue("")]
        [NotNull]
        public string AlertEmail { get; set; } = "";
        /// <summary>
        /// 是否正在预警
        /// </summary>
        public bool Alerting { get; set; }


        public long DeviceTypeId { get; set; }
        public virtual Device_Type DeviceType { get; set; }

        public long CreatorId { get; set; }
        public virtual User Creator { get; }

        //public virtual Device_AutoControl Device_AutoControl { get; set; } 
    }
}
