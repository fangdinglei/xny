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
    /// 用户设备分组 仅对创建者可见
    /// </summary>
    public class User_Device_Group:IHasCreator { 
        public long Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 所属者
        /// </summary>
        public long CreatorId { get; set; }
        /// <summary>
        /// 所属者
        /// </summary>
        public virtual User Creator { get; set; }
    }
}
