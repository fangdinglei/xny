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
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDBContext.Main
{
   
    public class User_Device
    {
        public long UserId { get; set; }
        public long DeviceId { get; set; }
        public long User_Device_GroupId { get; set; }

        public int Authority { get; set; }
        [NotMapped]
        public UserDeviceAuthority _Authority
        {
            get => Enum.IsDefined(typeof(UserDeviceAuthority), Authority)
                ? (UserDeviceAuthority)Authority
                : UserDeviceAuthority.No;
            set =>
               Authority = (int)value;
        }


        public virtual User User { get; set; }
        public virtual Device Device { get; set; }
        public virtual User_Device_Group User_Device_Group { get; set; }
        //todo

        static internal void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Device>().HasKey(it => new { it.UserId, it.DeviceId });
        }
    }
}
