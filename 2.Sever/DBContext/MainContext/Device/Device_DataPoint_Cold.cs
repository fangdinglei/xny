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
    [Index(nameof(DeviceId), nameof(StreamId), nameof(StartTime), nameof(EndTime))]
    public class Device_DataPoint_Cold
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long DeviceId { get; set; }
        public long StreamId { get; set; }
        /// <summary>
        /// 冷数据创建时间
        /// </summary>
        public long CreatTime { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public int Count { get; set; }
        public string Pars { get; set; }
        /// <summary>
        /// 数据状态 0未知 1在线 2离线 3删除中 4已删除 5 创建中
        /// </summary>
        public byte status { get; set; }
        public string ManagerName { get; set; }
        public long TreeId { get; set; }
    }

}