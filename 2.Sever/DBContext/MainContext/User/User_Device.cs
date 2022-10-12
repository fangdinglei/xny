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
    public class User_Device {
        public long UserId { get; set; }
        public long DeviceId { get; set; }
        public long User_Device_GroupId { get; set; }
        //数据读取权限
        public bool PData { get; set; }
        //实时状态获取权限
        public bool PStatus { get; set; }
        //设备控制权限
        public bool PControl { get; set; }

        public virtual User User { get; set; }
        public virtual Device Device { get; set; }
        public virtual User_Device_Group User_Device_Group { get; set; }
        //todo
    }
}
