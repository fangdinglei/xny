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
    /// <summary>
    /// 记录用户父子映射 也包含自己和自己
    /// </summary>
    public class User_SF {

        public long SonId { get; set; }
        public long FatherId { get; set; }
        public virtual User Son { get; set; }
        public virtual User Father { get; set; }
    } 

}
