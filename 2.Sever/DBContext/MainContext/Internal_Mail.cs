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
    public class Internal_Mail { 
        public long Id { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public long Time { get; set; }
        public bool Readed { get; set; }

        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
    }
}
