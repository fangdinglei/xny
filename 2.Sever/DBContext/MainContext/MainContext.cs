using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations.Schema;
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
    public class User { 
        public int Id { get; set; }
        public String Name { get; set; }
        public string Pass { get; set; }
        public string Phone { get; set; }
        public byte Status { get; set; }
        public long LastLogin { get; set; }

        public virtual ICollection< Device> Devices { get; set; }
        public virtual ICollection<User_Login_History> User_Login_Histories { get; set; }
        public virtual ICollection<User_Device_Group> User_Device_Groups { get; set; }
        public virtual ICollection<User_Device> User_Devices { get; set; }

    }
    public class User_Login_History {
        public string Ip;
        public long Time;
        public bool Success; 
    }
    public class User_SF {
        public virtual User Son { get; set; }
        public virtual User Father { get; set; }
    }
    public class Device_Type {
        public long Id { get; set; }
        public string DataPoints { get; set; }
        public string Script{ get; set; }
    }
    public class Device { 
        public long Id { get; set; }
        public string Name { get; set; }
        public int Device_TypeId  { get; set; }
        public virtual Device_Type DeviceType { get; set; }

        public virtual Device_AutoControl Device_AutoControl { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<User_Device> User_Devices { get; set; }
        public virtual ICollection<Device_Cmd_History> Device_Cmd_Historys { get; set; }
   
    } 
    public class Device_Cmd_History { 
        public long Id { get; set; }
        public virtual Device Device { get; set; }
        public string Cmd { get; set; }
        public long UserId  { get; set; }
        public User User { get; set; }

    }
    public class User_Device_Group { 
        public long Id { get; set; }
        public string Name { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
    public class User_Device {
        public long UserId { get; set; }
        public long DeviceId { get; set; }
        public long User_Device_GroupId { get; set; }
        public virtual User User { get; set; }
        public virtual Device Device { get; set; }
        public virtual User_Device_Group User_Device_Group { get; set; }
        //todo
    }

    /// <summary>
    /// 自动控制服务
    /// </summary>
    public enum DeviceServiceTypeEnum :int
    {
        Unknown=0,
        LedSever=1, 
    }
    public class Device_AutoControl { 
        public long Id { get; set; }
        public virtual Device Device { get; set; } 

        public int Type { get; set; }
        [NotMapped]
        public DeviceServiceTypeEnum _Type
        {
            get => Enum.IsDefined(typeof(DeviceServiceTypeEnum), Type)
                ? (DeviceServiceTypeEnum)Type
                : DeviceServiceTypeEnum.Unknown;
            set =>
              Type = (int)value;
        }

        public bool TimedControl { get; set; }
        public string TimedControlSetting  { get; set; }
    } 


    public class MainContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<User_Login_History> User_Login_Historys { get; set; }
        public DbSet<User_SF> User_SFs { get; set; }
        public DbSet<Device_Type> Device_Types { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Device_Cmd_History> Device_Cmd_Historys { get; set; }
        public DbSet<User_Device_Group> User_Device_Groups { get; set; }
        public DbSet<User_Device> User_Devices { get; set; }
        public DbSet<Device_AutoControl> Device_AutoControls { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            base.OnConfiguring(optionsBuilder);
            string s = "server=fdlmaindb.mysql.rds.aliyuncs.com;database=dbpgv2;user id=fangdinglei;password=FdlMainDB@;port=3306;sslmode=None";
            optionsBuilder.UseMySql(s,
                  ServerVersion.AutoDetect(s));
        } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new BaseValueBuilder().OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasMany(it => it.Devices).WithMany(it => it.Users).
                UsingEntity<User_Device>( 
                it=> it.HasOne(it => it.Device).WithMany(it=>it.User_Devices),
                 it => it.HasOne(it => it.User).WithMany(it => it.User_Devices)
                );
        }
    }

    public class BaseValueBuilder : IDBValueBuilder
    {
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Admin>().HasData(new Admin()
            //{
            //    Authority = "[\"creat_activity\"]",
            //    Password = "abcdef",
            //     Name = "admin"
            //});
        }
    }
}
