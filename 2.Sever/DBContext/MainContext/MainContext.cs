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
    public interface IHasCreator {
        long Id { get;  }
        User Creator { get;  }
        long CreatorId { get;  }
    }

    public class User: IHasCreator
    { 
        public long Id { get; set; }
        public String Name { get; set; }
        public string Pass { get; set; }
        public string Phone { get; set; }
        public byte Status { get; set; }
        public long LastLogin { get; set; }

        public virtual User Creator { get; set; }
        public long CreatorId { get; set; }

        public virtual ICollection< Device> Devices { get; set; }
        public virtual ICollection<User_Login_History> User_Login_Histories { get; set; }
        public virtual ICollection<User_Device_Group> User_Device_Groups { get; set; }
        public virtual ICollection<User_Device> User_Devices { get; set; }

    }
    public class User_Login_History {
        public long Id { get; set; }
        public string Ip;
        public long Time;
        public bool Success; 
    }
    /// <summary>
    /// 记录用户父子映射 也包含自己和自己
    /// </summary>
    public class User_SF {

        public long SonId { get; set; }
        public long FatherId { get; set; }
        public virtual User Son { get; set; }
        public virtual User Father { get; set; }
    }
    public class Device_Type : IHasCreator
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DataPoints { get; set; }
        public string Script{ get; set; }

        public long CreatorId { get; set; }
        public virtual User Creator { get;  }
     }
    public class Device : IHasCreator
    { 
        public long Id { get; set; }
        public string Name { get; set; }
        public int Device_TypeId  { get; set; }
        public virtual Device_Type DeviceType { get; set; }

        public virtual User Creator { get;   }
        public long CreatorId { get; set; } 

        public virtual Device_AutoControl Device_AutoControl { get; set; } 
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
        //数据读取权限
        public bool Data { get; set; }
        //实时状态获取权限
        public bool Status { get; set; }
        //设备控制权限
        public bool Control { get; set; }

        public virtual User User { get; set; }
        public virtual Device Device { get; set; }
        public virtual User_Device_Group User_Device_Group { get; set; }
        //todo
    }
    /// <summary>
    /// 用户操作审计
    /// </summary>
    public class User_Op_Audit { 
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
        public virtual User Sponsor { get;   }
        /// <summary>
        /// 审计人
        /// </summary>
        public long AuditorId { get; set; }
        public virtual User Auditor { get;  }
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
        public DbSet<User_Op_Audit> User_Op_Audits { get; set; }

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
