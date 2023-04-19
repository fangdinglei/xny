using BaseDefines;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Org.BouncyCastle.Utilities.Date;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

    //public class ScheduleItem
    //{

    //    private ScheduleItem() { }
    //    public ScheduleItem(ServiceType type, uint iD, TimeTriggerType triggertype, long timeStart, long timeEnd, byte week, uint ownerID, int value, byte priority, DateTime updateTime, uint updater)
    //    {
    //        ID = iD;
    //        Type = type;
    //        TriggerType = triggertype;
    //        TimeStart = timeStart;
    //        TimeEnd = timeEnd;
    //        OwnerID = ownerID;
    //        Value = value;
    //        Week = week;
    //        Priority = priority;
    //        UpdateTime = updateTime;
    //        Updater = updater;
    //    }

    //    ///  <summary>
    //    ///   创建一个星期定时任务
    //    ///  </summary>
    //    ///  <param name="ownerid"></param>
    //    ///  <param name="value"></param>
    //    ///  <param name="start"></param>
    //    ///  <param name="end"></param>
    //    ///  <param name="week">[周日,周六]是否生效</param>
    //    /// <param name="userid"></param>
    //    /// <returns></returns>
    //    static public ScheduleItem Creat(ServiceType type, uint ownerid, int value, long start, long end, bool[] week, uint userid)
    //    {
    //        var re = new ScheduleItem()
    //        {
    //            Type = type,
    //            TriggerType = TimeTriggerType.EveryWeek,
    //            TimeStart = start,
    //            TimeEnd = end,
    //            OwnerID = ownerid,
    //            Value = value,
    //            UpdateTime = DateTime.Now,
    //            Updater = userid
    //        };
    //        re.Week = 0;
    //        for (int i = 0; i < week.Length; i++)
    //        {
    //            if (week[i])
    //                re.Week |= (byte)(1 << i);
    //        }

    //        return re;
    //    }
    //    /// <summary>
    //    /// 创建一个时间任务
    //    /// </summary>
    //    /// <param name="type"></param>
    //    /// <param name="ownerid"></param>
    //    /// <param name="value"></param>
    //    /// <param name="start"></param>
    //    /// <param name="end"></param>
    //    /// <param name="userid"></param>
    //    /// <returns></returns>
    //    static public ScheduleItem Creat(ServiceType type, uint ownerid, int value, long start, long end, uint userid)
    //    {
    //        return new ScheduleItem()
    //        {
    //            Type = type,
    //            TriggerType = TimeTriggerType.Once,
    //            TimeStart = start,
    //            TimeEnd = end,
    //            OwnerID = ownerid,
    //            Value = value,
    //            UpdateTime = DateTime.Now,
    //            Updater = userid
    //        };
    //    }
    //    /// <summary>
    //    /// 创建一个一直生效的任务 
    //    /// </summary>
    //    /// <param name="type"></param>
    //    /// <param name="ownerid"></param>
    //    /// <param name="value"></param>
    //    /// <param name="userid"></param>
    //    /// <returns></returns>
    //    static public ScheduleItem Creat(ServiceType type, uint ownerid, int value, uint userid)
    //    {
    //        return new ScheduleItem()
    //        {
    //            Type = type,
    //            TriggerType = TimeTriggerType.ALL,
    //            OwnerID = ownerid,
    //            Value = value,
    //            UpdateTime = DateTime.Now,
    //            Updater = userid
    //        };
    //    }

    //    /// <summary>
    //    /// 给定时间是否在此时间内
    //    /// </summary>
    //    /// <param name="time"></param>
    //    /// <returns></returns>
    //    public bool IsTimeIn(DateTime time)
    //    {
    //        switch (TriggerType)
    //        {
    //            case TimeTriggerType.ALL:
    //                return true;
    //            case TimeTriggerType.Once:
    //                return (time >= TimeStart.JavaTicketToBeijingTime()) && (time <= TimeEnd.JavaTicketToBeijingTime());
    //            case TimeTriggerType.EveryWeek:
    //                time = time.AddHours(8);
    //                long t = time.BeijingTimeToJavaTicket() - time.Date.BeijingTimeToJavaTicket();
    //                return (t >= TimeStart) && (t <= TimeEnd) && (Week & (1 << (int)time.DayOfWeek)) > 0;
    //            default:
    //                return false;
    //        }
    //    }

    //    public DateTime GetTimeStart()
    //    {
    //        switch (TriggerType)
    //        {

    //            case TimeTriggerType.Once:
    //                return TimeStart.JavaTicketToBeijingTime();
    //            case TimeTriggerType.EveryWeek:
    //                return TimeStart.JavaTicketToBeijingTime().AddHours(-8);
    //            default:
    //                return DateTime.Now;
    //        }
    //    }
    //    public DateTime GetTimeEnd()
    //    {
    //        switch (TriggerType)
    //        {
    //            case TimeTriggerType.Once:
    //                return TimeEnd.JavaTicketToBeijingTime();
    //            case TimeTriggerType.EveryWeek:
    //                return TimeEnd.JavaTicketToBeijingTime().AddHours(-8);
    //            default:
    //                return DateTime.Now;
    //        }

    //    }
    //    /// <summary>
    //    /// 获取存储的真实值
    //    /// </summary>
    //    /// <returns></returns>
    //    public int GetValue()
    //    {
    //        return Value;
    //    }
    //    /// <summary>
    //    /// 获取计划中在某个时间的值
    //    /// </summary>
    //    /// <param name="val"></param>
    //    public void SetValue(int val)
    //    {
    //        Value = val;
    //    }

    //    const long TicketADay = 24L * 60 * 60 * 1000;
    //    /// <summary>
    //    /// 校验时间信息是否合法
    //    /// </summary>
    //    /// <returns></returns>
    //    public bool Check()
    //    {
    //        switch (TriggerType)
    //        {
    //            case TimeTriggerType.ALL:
    //                return true;
    //            case TimeTriggerType.Once:
    //                return true;
    //            case TimeTriggerType.EveryWeek:

    //                return (TimeStart >= 0 && TimeStart < TicketADay) && (TimeEnd >= 0 && TimeEnd < TicketADay);
    //            default:
    //                return true;
    //        }
    //    }

    //    /// <summary>
    //    /// 校验数据是否合法
    //    /// </summary>
    //    /// <returns></returns>
    //    public bool Validate_Json()
    //    {
    //        if (!Enum.IsDefined(typeof(TimeTriggerType), TriggerType))
    //            return false;
    //        if (Week >= 128)
    //            return false;
    //        return Check();
    //    }
    //}

    public class Device_AutoControl_Settings_Item
    {
        //public string Name { get; set; } 
        public long Id { get; set; }
        public bool Open { get; set; }
        public byte Order { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// <see cref="TimeTriggerType"/>
        /// </summary>
        public byte TriggerType { get; set; }
        /// <summary>
        /// 时间开始 java 时间戳 周定时中为 1970.1.1.8+时长，反序列化时时间-8
        /// </summary>
        public long TimeStart { get; set; }
        /// <summary>
        /// 时间结束 java 时间戳 周定时中为 1970.1.1.8+时长，反序列化时时间-8
        /// </summary>
        public long TimeEnd { get; set; }
        /// <summary>
        /// 关联的设备的ID
        /// </summary>
        public long OwnerID { get; set; }
        /// <summary>
        ///   周信息 1位代表1天  低位为周日
        /// </summary>
        public byte Week { get; set; }
        public string Cmd { get; set; }
        ///// <summary>
        ///// 存储的值
        ///// </summary>
        //public int Value { get; set; }
        ///// <summary>
        ///// 该记录更新的时间
        ///// </summary>
        //public DateTime UpdateTime { get; set; }
        ///// <summary>
        ///// 更新者
        ///// </summary>
        //public long Updater { get; set; }
    }

    public class MainContext : DbContext
    {
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// 用户历史操作信息
        /// </summary>
        public DbSet<AccountHistory> AccountHistorys { get; set; }
        /// <summary>
        /// 用户关系映射表
        /// </summary>
        public DbSet<User_SF> User_SFs { get; set; }
        /// <summary>
        /// 设备类型数据
        /// </summary>
        public DbSet<Device_Type> Device_Types { get; set; }
        /// <summary>
        /// 设备类型数据-数据定义
        /// </summary>
        public DbSet<ThingModel> ThingModels { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public DbSet<Device> Devices { get; set; }
        /// <summary>
        /// 用户设备分组
        /// </summary>
        public DbSet<User_Device_Group> User_Device_Groups { get; set; }
        /// <summary>
        /// 用户设备
        /// </summary>
        public DbSet<User_Device> User_Devices { get; set; }
        /// <summary>
        /// 设备自动控制信息
        /// </summary>
        public DbSet<Device_AutoControl_Settings_Item> Device_AutoControl_Settings_Items { get; set; }
        /// <summary>
        /// 操作审计
        /// </summary>
        public DbSet<User_Op_Audit> User_Op_Audits { get; set; }
        /// <summary>
        /// 设备数据
        /// </summary>
        public DbSet<Device_DataPoint> Device_DataPoints { get; set; }
        /// <summary>
        /// 设备冷数据摘要
        /// </summary>
        public DbSet<Device_DataPoint_Cold> Device_DataPoint_Colds { get; set; }
        /// <summary>
        /// 站内信
        /// </summary>
        public DbSet<Internal_Mail> Internal_Mails { get; set; }
        /// <summary>
        /// 设备维修信息
        /// </summary>
        public DbSet<Device_Repair> Device_Repairs { get; set; }
        /// <summary>
        /// 租户冷数据配置
        /// </summary>
        public DbSet<DeviceColdDataSettings> ColdDataSettings { get; set; }
        /// <summary>
        /// 系统kv表
        /// </summary>
        public DbSet<KeyValue> KeyValues { get; set; }
        /// <summary>
        /// 设备命令历史信息
        /// </summary>
        public DbSet<DeviceCmdHistory> DeviceCmdHistorys { get; set; }


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
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information).EnableSensitiveDataLogging().EnableDetailedErrors();
        }
        [DebuggerStepThrough]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            User_SF.OnModelCreating(modelBuilder);
            new BaseValueBuilder().OnModelCreating(modelBuilder);
            new ColdDataBuilder().OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>().HasMany(it => it.Devices).WithMany(it => it.Creator).
            //    UsingEntity<User_Device>( 
            //    it=> it.HasOne(it => it.Device).WithMany(it=>it.User_Devices),
            //     it => it.HasOne(it => it.User).WithMany(it => it.User_Devices)
            //    ); 
        }
    }

    public class BaseValueBuilder : IDBValueBuilder
    {
        string buildAuthorityAdmin()
        {
            var ls = new List<UserAuthorityEnum> {
                UserAuthorityEnum.TopUserAdd,
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(ls.Select(it => it.ToString()));
        }
        string buildAuthorityTopUser()
        {
            var ls = new List<UserAuthorityEnum> {
                UserAuthorityEnum.DeviceAdd,
                UserAuthorityEnum.DeviceTypeW,
                UserAuthorityEnum.DeviceTypeAdd,
                UserAuthorityEnum.ColdDataR,
                UserAuthorityEnum.ColdDataW,
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(ls.Select(it => it.ToString()));
        }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            var time = new MyUtility.TimeUtility();
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
                Authoritys = buildAuthorityAdmin(),
                UserTreeId = 1,
                MaxSubUser = 100,
                MaxSubUserDepth = 10,
                TreeDeep = 0,
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
                Authoritys = buildAuthorityTopUser(),
                UserTreeId = 2,
                MaxSubUser = 100,
                MaxSubUserDepth = 10,
                TreeDeep = 0,
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
                Authoritys = "[]",
                UserTreeId = 2,
                MaxSubUser = 100,
                MaxSubUserDepth = 10,
                TreeDeep = 0,
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
                Authoritys = "[]",
                UserTreeId = 2,
                MaxSubUser = 100,
                MaxSubUserDepth = 10,
                TreeDeep = 0,
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
                Authoritys = "[]",
                UserTreeId = 2,
                MaxSubUser = 100,
                MaxSubUserDepth = 10,
                TreeDeep = 0,
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
                Authoritys = "[]",
                UserTreeId = 3,
                MaxSubUser = 100,
                MaxSubUserDepth = 10,
                TreeDeep = 0,
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
                Authoritys = "[]",
                UserTreeId = 3,
                MaxSubUser = 100,
                MaxSubUserDepth = 10,
                TreeDeep = 0,
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
                Authoritys = "[]",
                UserTreeId = 3,
                MaxSubUser = 100,
                MaxSubUserDepth = 10,
                TreeDeep = 0,
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 13,
                Name = "user13",
                EMail = "2432114474@qq.com",
                LastLogin = 0,
                Pass = "123",
                Phone = "15850798245",
                CreatorId = 11,
                Authoritys = "[]",
                UserTreeId = 3,
                MaxSubUser = 100,
                MaxSubUserDepth = 10,
                TreeDeep = 0,
            });
            #endregion

            #region 关系映射
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 1,
                User2Id = 1,
                UserTreeId = 1,
            });


            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 2,
                User2Id = 2,
                UserTreeId = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 2,
                User2Id = 3,
                UserTreeId = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 3,
                User2Id = 2,
                UserTreeId = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 2,
                User2Id = 4,
                UserTreeId = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 4,
                User2Id = 2,
                UserTreeId = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 2,
                User2Id = 5,
                UserTreeId = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 5,
                User2Id = 2,
                UserTreeId = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 3,
                User2Id = 5,
                UserTreeId = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 5,
                User2Id = 3,
                UserTreeId = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 3,
                User2Id = 3,
                UserTreeId = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 4,
                User2Id = 4,
                UserTreeId = 2,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 5,
                User2Id = 5,
                UserTreeId = 2,
            });


            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 10,
                User2Id = 10,
                UserTreeId = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 10,
                User2Id = 11,
                UserTreeId = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 11,
                User2Id = 10,
                UserTreeId = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 10,
                User2Id = 12,
                UserTreeId = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 12,
                User2Id = 10,
                UserTreeId = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 10,
                User2Id = 13,
                UserTreeId = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 13,
                User2Id = 10,
                UserTreeId = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = true,
                IsSelf = false,
                User1Id = 11,
                User2Id = 13,
                UserTreeId = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = false,
                User1Id = 13,
                User2Id = 11,
                UserTreeId = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 11,
                User2Id = 11,
                UserTreeId = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 12,
                User2Id = 12,
                UserTreeId = 3,
            });
            modelBuilder.Entity<User_SF>().HasData(new User_SF()
            {
                IsFather = false,
                IsSelf = true,
                User1Id = 13,
                User2Id = 13,
                UserTreeId = 3,
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
                AlertLowValue = 20,
                AlertHighValue = 80,
                _Type = ThingModelValueType.Float,
                Abandonted = false,
            });
            modelBuilder.Entity<Device_Type>().HasData(
               new Device_Type()
               {
                   Id = 1,
                   Name = "测试类型1",
                   CreatorId = 2,
                   Script = "",
                   UserTreeId = 2,
               }
                , new Device_Type()
                {
                    Id = 2,
                    Name = "测试类型2",
                    CreatorId = 2,
                    ThingModels = new List<ThingModel>(),
                    Script = "",
                    UserTreeId = 2,
                }

                );
            modelBuilder.Entity<User_Device_Group>().HasData(
              new User_Device_Group()
              {
                  Id = 1,
                  Name = "测试分组1",
                  CreatorId = 2,
                  UserTreeId = 1,
              },
               new User_Device_Group()
               {
                   Id = 2,
                   Name = "测试分组2",
                   CreatorId = 3,
                   UserTreeId = 1,
               }
              );
            modelBuilder.Entity<Device>().HasData(
                new Device()
                {
                    Id = 1,
                    Name = "设备1",
                    CreatorId = 2,
                    DeviceTypeId = 1,
                    LatestData = "{1:59}",
                    LocationStr = "",
                    Status = 2,
                    UserTreeId = 2,
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
                    UserTreeId = 2,
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
                    UserTreeId = 2,
                }
            );
            modelBuilder.Entity<User_Device>().HasData(
                new User_Device()
                {
                    Id = 1,
                    UserId = 2,
                    DeviceId = 1,
                    _Authority = UserDeviceAuthority.Every,
                    User_Device_GroupId = 1,
                    UserTreeId = 2,
                }
                , new User_Device()
                {
                    Id = 2,
                    UserId = 2,
                    DeviceId = 2,
                    _Authority = UserDeviceAuthority.Every,
                    User_Device_GroupId = 0,
                    UserTreeId = 2,
                }
                , new User_Device()
                {
                    Id = 3,
                    UserId = 2,
                    DeviceId = 3,
                    _Authority = UserDeviceAuthority.Every,
                    User_Device_GroupId = 0,
                    UserTreeId = 2,
                }
                , new User_Device()
                {
                    Id = 4,
                    UserId = 3,
                    DeviceId = 1,
                    _Authority = UserDeviceAuthority.Every,
                    User_Device_GroupId = 2,
                    UserTreeId = 2,
                }
                , new User_Device()
                {
                    Id = 5,
                    UserId = 3,
                    DeviceId = 2,
                    _Authority = UserDeviceAuthority.Every,
                    User_Device_GroupId = 0,
                    UserTreeId = 2,
                }
                , new User_Device()
                {
                    Id = 6,
                    UserId = 4,
                    DeviceId = 1,
                    _Authority = UserDeviceAuthority.Every,
                    User_Device_GroupId = 0,
                    UserTreeId = 2,
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
                         DeviceId = 1,
                         StreamId = 1,
                         Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8 + 1)) / 1000,
                         Value = 91,
                     },
                      new Device_DataPoint()
                      {
                          Id = 3,
                          DeviceId = 1,
                          StreamId = 1,
                          Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8 + 2)) / 1000,
                          Value = 95,
                      },
                       new Device_DataPoint()
                       {
                           Id = 4,
                           DeviceId = 1,
                           StreamId = 1,
                           Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8 + 3)) / 1000,
                           Value = 96,
                       }, new Device_DataPoint()
                       {
                           Id = 5,
                           DeviceId = 1,
                           StreamId = 1,
                           Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8 + 15)) / 1000,
                           Value = 96,
                       },
                       new Device_DataPoint()
                       {
                           Id = 6,
                           DeviceId = 1,
                           StreamId = 1,
                           Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8 + 18)) / 1000,
                           Value = 96,
                       },
                       new Device_DataPoint()
                       {
                           Id = 7,
                           DeviceId = 1,
                           StreamId = 1,
                           Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8 + 30)) / 1000,
                           Value = 96,
                       },
                     new Device_DataPoint()
                     {
                         Id = 8,
                         DeviceId = 2,
                         StreamId = 1,
                         Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8 + 4)) / 1000,
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
                        DiscoveryTime = time.GetTicket(new System.DateTime(2023, 1, 2)),
                        CompletionTime = time.GetTicket(new System.DateTime(2023, 1, 5)),
                        Context = "测试维修1",
                        CreatorId = 2,
                        UserTreeId = 2,
                    },
                     new Device_Repair()
                     {
                         Id = 2,
                         DeviceId = 2,
                         DiscoveryTime = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8)) / 1000,
                         CompletionTime = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8)) / 1000,
                         Context = "测试维修2",
                         CreatorId = 2,
                         UserTreeId = 2,
                     }
                );
            #endregion

            #region 站内信
            modelBuilder.Entity<Internal_Mail>().HasData(
                    new Internal_Mail()
                    {
                        Id = 1,
                        LastEMailTime = 0,
                        Title = "测试1",
                        Context = "测试1",
                        Readed = false,
                        ReceiverId = 2,
                        SenderId = 3,
                        Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8)) / 1000
                        ,
                        UserTreeId = 2,
                    },
                      new Internal_Mail()
                      {
                          Id = 2,
                          LastEMailTime = 0,
                          Title = "测试2",
                          Context = "测试2",
                          Readed = false,
                          ReceiverId = 3,
                          SenderId = 2,
                          Time = DateTimeUtilities.DateTimeToUnixMs(System.DateTime.Now.AddHours(-8)) / 1000
                            ,
                          UserTreeId = 2,
                      }
                );
            for (int i = 0; i < 20; i++)
            {
                modelBuilder.Entity<Internal_Mail>().HasData(
                  new Internal_Mail()
                  {
                      Id = 3 + i,
                      LastEMailTime = 0,
                      Title = "测试2-" + i,
                      Context = "测试2-" + i,
                      Readed = false,
                      ReceiverId = 2,
                      SenderId = 3,
                      Time = i + DateTimeUtilities.DateTimeToUnixMs(System.DateTime.UtcNow) / 1000
                        ,
                      UserTreeId = 2,
                  }
              );
            }
            #endregion

        }
    }
    public class ColdDataBuilder : IDBValueBuilder
    {
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            var time = new MyUtility.TimeUtility();
            int t = 10000;
            for (int i = t + 1; i <= t + 1000; i++)
            {
                modelBuilder.Entity<Device_DataPoint>().HasData(
                new Device_DataPoint
                {
                    DeviceId = 1,
                    StreamId = 1,
                    Time = time.GetTicket((new DateTime(2023, 1, 1)).AddHours(i - t)),
                    Id = i,
                    Value = Random.Shared.Next(0, 100)
                });
            }

            modelBuilder.Entity<Device_DataPoint_Cold>().HasData(new Device_DataPoint_Cold
            {
                Count = 1,
                CreatTime = time.GetTicket(),
                DeviceId = 1,
                StreamId = 1,
                EndTime = time.GetTicket(),
                StartTime = time.GetTicket(DateTime.Now.AddDays(-2)),
                Id = 1,
                ManagerName = "InFile",
                Pars = "",
                status = 0,
                TreeId = 2,
            });
            modelBuilder.Entity<DeviceColdDataSettings>().HasData(new DeviceColdDataSettings
            {
                ColdDownTime = 10,
                ManagerName = "InFile",
                TreeId = 2,
                MinCount = 100,
                Open = false,
            });

        }
    }

}