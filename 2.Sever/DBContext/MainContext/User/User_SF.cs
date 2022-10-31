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

namespace MyDBContext.Main
{
    /// <summary>
    /// 记录用户父子映射  
    /// </summary>
    public class User_SF
    {
        public long User1Id { get; set; }
        public long User2Id { get; set; }
        public bool IsFather { get; set; }
        public bool IsSelf { get; set; }
        public int UserTreeId { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }

        internal static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_SF>().HasKey(it => new { it.User1Id, it.User2Id });
            //modelBuilder.Entity<User_SF>().HasIndex(it => new { it.SonId, it.FatherId });
        }
    }

}
