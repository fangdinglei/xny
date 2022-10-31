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
    [Index(nameof(SenderId))]
    [Index(nameof(ReceiverId))]
    [Index(nameof(Time))]
    [Index(nameof(UserTreeId))]
    public class Internal_Mail : IHasCreator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int UserTreeId { get; set; }

        public string Title { get; set; }
        public string Context { get; set; }
        public long Time { get; set; }
        public bool Readed { get; set; }

        public long SenderId { get; set; }
        public long ReceiverId { get; set; }

        public long LastEMailTime { get; set; }

        [NotMapped]
        public virtual User Creator => throw new System.NotImplementedException();
        [NotMapped]
        public long CreatorId =>SenderId;
    }
}
