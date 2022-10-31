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
    /// <summary>
    /// 用户操作审计
    /// </summary>
    public class User_Op_Audit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string Op { get; set; }
        /// <summary>
        /// 操作数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        public long SponsorId { get; set; }
        public virtual User Sponsor { get; }
        /// <summary>
        /// 审计人
        /// </summary>
        public long AuditorId { get; set; }
        public int UserTreeId { get; set; }
        public virtual User Auditor { get; }
    }
}
