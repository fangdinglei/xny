using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations; 
using Org.BouncyCastle.Utilities.Date;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class Device_AutoControl_Settings_Item_Data
    {

    }
    public class Device_AutoControl_Settings_Item
    {
        //public string Name { get; set; } 
        public long Id { get; set; }
        public long DeviceId { get; set; }
        public bool Open { get; set; }
        public string Data { get; set; }
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
        public DbSet<Device_AutoControl_Settings_Item> Device_AutoControl_Settings_Items { get; set; }
        public DbSet<User_Op_Audit> User_Op_Audits { get; set; }
        public DbSet<Device_DataPoint> Device_DataPoints { get; set; }
        public DbSet<Device_DataPoint_Cold> Device_DataPoint_Colds { get; set; }
        public DbSet<Internal_Mail> Internal_Mails { get; set; }
        public DbSet<Device_Repair> Device_Repairs { get; set; }
        public DbSet<KeyValue> KeyValues { get; set; }

        static SqliteConnection? _connection;

        [DebuggerStepThrough]
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IMigrationsModelDiffer, MigrationsModelDifferWithoutForeignKey>();
            if (_connection == null)
            {
                _connection = new SqliteConnection("Filename=:memory:");
                _connection.Open();
            }
            base.OnConfiguring(optionsBuilder);
            string s = "server=fdlmaindb.mysql.rds.aliyuncs.com;database=dbbs;user id=fangdinglei;password=FdlMainDB@;port=3306;sslmode=None";
            //optionsBuilder.UseMySql(s, ServerVersion.AutoDetect(s));
            optionsBuilder.UseSqlite(_connection);
            optionsBuilder.UseBatchEF_Sqlite();
        }
        [DebuggerStepThrough]
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
            var time =new MyUtility.TimeUtility();
            /*
             初始化
                  admin-系统用户[SystemUser,测试权限1]
                  
                  user2-顶级用户[测试权限1,测试权限2]      -设备1(分组1) 2 3    邮件
                  /   \
               user3   \              -设备1(分组2) 2   邮件
                /      user4          -设备1
             user5                    -设备1

                 user10-顶级用户
                 /   \
              user11  \  
               /      user12
            user13 

                设备1 -类型1 维修记录1
                设备2 -类型1 维修记录2
                设备3 -类型2
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
                CreatorId = 0,
                Authoritys = "[\"SystemUser\",\"测试权限1\"]"
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
                Authoritys = "[\"测试权限1\",\"测试权限2\"]"
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
                Id = 13,
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

            #region 设备添加
            modelBuilder.Entity<ThingModel>().HasData(new ThingModel()
            {
                Id = 1,
                DeviceTypeId = 1,
                MinValue = 0,
                MaxValue = 100,
                Name = "温度",
                Remark = "备注温度",
                Unit = "摄氏度",
                Abandonted = false,
            });
            modelBuilder.Entity<Device_Type>().HasData(
               new Device_Type()
               {
                   Id = 1,
                   Name = "测试类型1",
                   CreatorId = 2,
                   Script = "",
               }
                , new Device_Type()
                {
                    Id = 2,
                    Name = "测试类型2",
                    CreatorId = 2,
                    ThingModels = new List<ThingModel>(),
                    Script = "",
                }

                );
            modelBuilder.Entity<User_Device_Group>().HasData(
              new User_Device_Group()
              {
                  Id = 1,
                  Name = "测试分组1",
                  CreatorId = 2,
              },
               new User_Device_Group()
               {
                   Id = 2,
                   Name = "测试分组2",
                   CreatorId = 3,
               }
              );
            modelBuilder.Entity<Device>().HasData(
                new Device()
                {
                    Id = 1,
                    Name = "设备1",
                    CreatorId = 2,
                    DeviceTypeId = 1,
                    LatestData = "{}",
                    LocationStr = "",
                    Status = 2,
                }
                , new Device()
                {
                    Id = 2,
                    Name = "设备2",
                    CreatorId = 2,
                    DeviceTypeId = 1,
                    LatestData = "{}",
                    LocationStr = "",
                    Status = 2,
                }
                , new Device()
                {
                    Id = 3,
                    Name = "设备3",
                    CreatorId = 2,
                    DeviceTypeId = 2,
                    LatestData = "{}",
                    LocationStr = "",
                    Status = 2,
                }
            );
            modelBuilder.Entity<User_Device>().HasData(
                new User_Device()
                {
                    UserId = 2,
                    DeviceId = 1,
                    PControl = true,
                    PData = true,
                    PStatus = true,
                    User_Device_GroupId = 1,
                }
                , new User_Device()
                {
                    UserId = 2,
                    DeviceId = 2,
                    PControl = true,
                    PData = true,
                    PStatus = true,
                    User_Device_GroupId = 0,
                }
                , new User_Device()
                {
                    UserId = 2,
                    DeviceId = 3,
                    PControl = true,
                    PData = true,
                    PStatus = true,
                    User_Device_GroupId = 0,
                }
                , new User_Device()
                {
                    UserId = 3,
                    DeviceId = 1,
                    PControl = true,
                    PData = true,
                    PStatus = true,
                    User_Device_GroupId = 2,
                }
                , new User_Device()
                {
                    UserId = 3,
                    DeviceId = 2,
                    PControl = true,
                    PData = true,
                    PStatus = true,
                    User_Device_GroupId = 0,
                }
                , new User_Device()
                {
                    UserId = 4,
                    DeviceId = 1,
                    PControl = true,
                    PData = true,
                    PStatus = true,
                    User_Device_GroupId = 0,
                }
            );
            #endregion

            #region 数据点
            modelBuilder.Entity<Device_DataPoint>().HasData(
                    new Device_DataPoint()
                    {
                        Id = 1,
                        DeviceId = 1,
                        StreamId = 1,
                        Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8)) / 1000,
                        Value = 90,
                    },
                     new Device_DataPoint()
                     {
                         Id = 2,
                         DeviceId = 2,
                         StreamId = 1,
                         Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8)) / 1000,
                         Value = 80,
                     }
                );
            #endregion

            #region 维修记录
            modelBuilder.Entity<Device_Repair>().HasData(
                    new Device_Repair()
                    {
                        Id = 1,
                        DeviceId = 1, 
                        DiscoveryTime = time.GetTicket(new System.DateTime(2023,1,2)),
                        CompletionTime = time.GetTicket(new System.DateTime(2023, 1, 5)),
                        Context="测试维修1",
                        CreatorId=2, 
                        
                    },
                     new Device_Repair()
                     {
                         Id = 2,
                         DeviceId =2,
                         DiscoveryTime = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8)) / 1000,
                         CompletionTime = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8)) / 1000,
                         Context = "测试维修2",
                         CreatorId = 2,

                     }
                );
            #endregion

            #region 站内信
            modelBuilder.Entity<Internal_Mail>().HasData(
                    new Internal_Mail()
                    {
                        Id = 1,
                        LastEMailTime = 0,
                        Context = "测试1",
                        Readed = false,
                        ReceiverId = 2,
                        SenderId = 3,
                        Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8)) / 1000

                    },
                      new Internal_Mail()
                      {
                          Id = 2,
                          LastEMailTime = 0,
                          Context = "测试2",
                          Readed = false,
                          ReceiverId = 3,
                          SenderId = 2,
                          Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8)) / 1000 
                      }
                );
            #endregion

        }
    }

}