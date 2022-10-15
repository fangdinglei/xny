using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Migrations;
using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Options;
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
    /// 自动控制服务
    /// </summary>
    public enum DeviceServiceTypeEnum : int
    {
        Unknown = 0,
        LedSever = 1,
    }
    public class Device_AutoControl
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public string TimedControlSetting { get; set; }
    }


    public class MainContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<History> Historys { get; set; }
        public DbSet<User_SF> User_SFs { get; set; }
        public DbSet<Device_Type> Device_Types { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<User_Device_Group> User_Device_Groups { get; set; }
        public DbSet<User_Device> User_Devices { get; set; }
        public DbSet<Device_AutoControl> Device_AutoControls { get; set; }
        public DbSet<User_Op_Audit> User_Op_Audits { get; set; }
        public DbSet<Device_DataPoint> Device_DataPoints { get; set; }
        public DbSet<Internal_Mail> Internal_Mails { get; set; }

        static SqliteConnection? _connection;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IMigrationsModelDiffer, MigrationsModelDifferWithoutForeignKey>();
            if (_connection==null)
            {
                _connection = new SqliteConnection("Filename=:memory:");
                _connection.Open();
            }  
            base.OnConfiguring(optionsBuilder);
            string s = "server=fdlmaindb.mysql.rds.aliyuncs.com;database=dbbs;user id=fangdinglei;password=FdlMainDB@;port=3306;sslmode=None";
            //optionsBuilder.UseMySql(s, ServerVersion.AutoDetect(s));
            optionsBuilder.UseSqlite(_connection);

        }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
            User_Device.OnModelCreating(modelBuilder);
            User_SF.OnModelCreating(modelBuilder);
            new BaseValueBuilder().OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>().HasMany(it => it.Devices).WithMany(it => it.Creator).
            //    UsingEntity<User_Device>( 
            //    it=> it.HasOne(it => it.Device).WithMany(it=>it.User_Devices),
            //     it => it.HasOne(it => it.User).WithMany(it => it.User_Devices)
            //    ); 
        }
    }

    public class BaseValueBuilder : IDBValueBuilder
    {
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             用户初始化
                        admin-系统用户[SystemUser]
                        
                        user2-顶级用户
                        /   \
                     user3  user4
                      /
                   user5

                       user10-顶级用户
                       /   \
                    user11  user12
                     /
                  user13
             */

            #region 用户

            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                Name = "admin",
                EMail = "2432114474@qq.com",
                LastLogin = 0,
                Pass = "123",
                Phone = "15850798245",
                CreatorId =0,
                Authoritys="[SystemUser]" 
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 2,
                Name = "user2",
                EMail = "2432114474@qq.com",
                LastLogin = 0,
                Pass = "123",
                Phone = "15850798245",
                CreatorId = 0,
                Authoritys = "[]" 
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 3,
                Name = "user3",
                EMail = "2432114474@qq.com",
                LastLogin = 0,
                Pass = "123",
                Phone = "15850798245",
                CreatorId = 2,
                Authoritys = "[]"
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 4,
                Name = "user4",
                EMail = "2432114474@qq.com",
                LastLogin = 0,
                Pass = "123",
                Phone = "15850798245",
                CreatorId = 2,
                Authoritys = "[]"
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 5,
                Name = "user5",
                EMail = "2432114474@qq.com",
                LastLogin = 0,
                Pass = "123",
                Phone = "15850798245",
                CreatorId = 3,
                Authoritys = "[]"
            });

            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 10,
                Name = "user10",
                EMail = "2432114474@qq.com",
                LastLogin = 0,
                Pass = "123",
                Phone = "15850798245",
                CreatorId = 0,
                Authoritys = "[]"
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 11,
                Name = "user11",
                EMail = "2432114474@qq.com",
                LastLogin = 0,
                Pass = "123",
                Phone = "15850798245",
                CreatorId = 10,
                Authoritys = "[]"
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 12,
                Name = "user12",
                EMail = "2432114474@qq.com",
                LastLogin = 0,
                Pass = "123",
                Phone = "15850798245",
                CreatorId = 10,
                Authoritys = "[]"
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id =13,
                Name = "user13",
                EMail = "2432114474@qq.com",
                LastLogin = 0,
                Pass = "123",
                Phone = "15850798245",
                CreatorId = 10,
                Authoritys = "[]"
            });
            #endregion

            #region 关系映射
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 1,
                User2Id = 1,
            });


            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 2,
                User2Id = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 2,
                User2Id = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 3,
                User2Id = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 2,
                User2Id = 4,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 4,
                User2Id = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 2,
                User2Id = 5,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 5,
                User2Id = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 3,
                User2Id = 5,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 5,
                User2Id = 3,
            }); 
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 3,
                User2Id = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 4,
                User2Id = 4,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 5,
                User2Id = 5,
            });


            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 10,
                User2Id = 10,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 10,
                User2Id = 11,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 11,
                User2Id = 10,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 10,
                User2Id = 12,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 12,
                User2Id = 10,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 10,
                User2Id = 13,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 13,
                User2Id = 10,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 11,
                User2Id = 13,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 13,
                User2Id = 11,
            }); 
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 11,
                User2Id = 11,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 12,
                User2Id = 12,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 13,
                User2Id = 13,
            });
            #endregion





        }
    }

} 