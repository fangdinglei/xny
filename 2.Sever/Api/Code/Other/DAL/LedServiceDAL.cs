using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using XNYAPI.Model;
using XNYAPI.Model.AutoControl;
using XNYAPI.Utility;

namespace XNYAPI.DAL
{
    public class LedServiceDAL
    {
       

        /// <summary>
        /// 获取设备定时任务
        /// </summary> 
        /// <returns></returns>
        /// <exception cref="Exception"/>
        static public ScheduleInfo GetAutoControlScheduleData(uint ownerid,bool isgroup,  ServiceType type)
        {
            //TODO 加锁
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText =
                    $"SELECT ID,TriggerType,TimeStart,TimeEnd," +
                    $"Week,OwnerID,Value,Priority," +
                    $"UpdateTime,OwnerUser " +
                    $"FROM   time_schedule WHERE OwnerID={ownerid } AND Type={(int)type} AND IsGroup = {isgroup}" +
                    $" ORDER BY Priority";
                var rd = cmd.ExecuteReader();
                var re = new ScheduleInfo()
                {
                    OwnerID = ownerid,
                    Data = new List<ScheduleItem>(),
                    Type = ServiceType.DeviceLEDControl,
                    IsGroup = isgroup
                };
                while (rd.Read())
                {
                    re.Data.Add(new ScheduleItem(
                        ServiceType.DeviceLEDControl, rd.GetUInt32(0), (TimeTriggerType)rd.GetByte(1), rd.GetInt64(2), rd.GetInt64(3),
                        rd.GetByte(4), rd.GetUInt32(5), rd.GetInt32(6), rd.GetByte(7),
                       rd.GetInt64(8).JavaTicketToBeijingTime(), rd.GetUInt32(9)
                        ));
                }
                return re;
            }
        }
        /// <summary>
        ///设置设备定时任务
        /// </summary> 
        /// <returns></returns>
        /// <exception cref="Exception"/>
        static public void SetAutoControlScheduleData(ScheduleInfo schedule, uint updater, MySqlCommand cmd)
        {
            //TODO 加锁
            cmd.CommandText = $"DELETE FROM  time_schedule WHERE OwnerID={schedule.OwnerID } AND IsGroup = {schedule.IsGroup}";
            cmd.ExecuteNonQuery();
            StringBuilder sb = new StringBuilder();
            schedule.Data.RunByBatch(10, (ls, s, e) =>
            {
                sb.Clear();
                sb.Append("INSERT INTO  time_schedule ");
                sb.Append("(Type,TriggerType,TimeStart,TimeEnd,Week,OwnerID,Value,");
                sb.Append("OtherInfo,UpdateTime,OwnerUser,Priority,IsGroup) VALUES");
                for (int i = s; i < e; i++)
                {
                    sb.Append("(");
                    sb.Append((int)schedule.Type);
                    sb.Append(",");
                    sb.Append((int)ls[i].TriggerType);
                    sb.Append(",");
                    sb.Append((long)ls[i].TimeStart);
                    sb.Append(",");
                    sb.Append((long)ls[i].TimeEnd);
                    sb.Append(",");
                    sb.Append((int)ls[i].Week);
                    sb.Append(",");
                    sb.Append(schedule.OwnerID);
                    sb.Append(",");
                    sb.Append(ls[i].Value);
                    sb.Append(",'',");//otherinfo
                    sb.Append(DateTime.Now.BeijingTimeToJavaTicket());
                    sb.Append(",");
                    sb.Append(updater);
                    sb.Append(",");
                    sb.Append(i + 1);
                    sb.Append(",");
                    sb.Append(schedule.IsGroup);
                    sb.Append("),");
                }
                sb.Remove(sb.Length - 1, 1);
                cmd.CommandText = sb.ToString();
                cmd.ExecuteNonQuery();
            });
        }

        /// <summary>
        /// 获取 自控配置信息
        /// </summary>
        /// <param name="ownerid"></param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        static public AutoControlSettings GetAutoControlSetting(uint ownerid,bool isgroup,  ServiceType type= ServiceType.DeviceLEDControl)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = $"SELECT GroupID,TimeSheduleEnabled,AdvancedControlEnabled" +
                    $" FROM  autocontrol_settings WHERE OwnerID= {ownerid} AND Type={(ushort)type} AND  IsGroup={isgroup}  ";
                var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    return new AutoControlSettings()
                    {
                        OwnerID = ownerid,
                        AdvancedControlEnabled = rd.GetBoolean(2),
                        TimeScheduleEnabled = rd.GetBoolean(1),
                        GroupID = rd.GetUInt32(0),
                        IsGroup = isgroup,
                        Type = type
                    };
                }
                else
                {
                    rd.Close();
                    return new AutoControlSettings()
                    {
                        OwnerID = ownerid,
                        AdvancedControlEnabled = false,
                        TimeScheduleEnabled = false,
                        GroupID = 0,
                         Type = type
                    };
                }
            }
        }
        /// <summary>
        /// 更新自控配置 如果没有则创建
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        static public void SetAutoControlSetting(List<uint> dvs, AutoControlSettings info,bool isgroup, MySqlCommand cmd, ServiceType type = ServiceType.DeviceLEDControl)
        {

            foreach (var dv in dvs)
            {

                cmd.CommandText = $"SELECT 1 " +
                    $"FROM autocontrol_settings " +
                    $"WHERE OwnerID={dv}  AND Type={(ushort)type} LIMIT 1";
                if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                {
                    cmd.CommandText = $"INSERT INTO  autocontrol_settings " +
                       $"( OwnerID,TimeSheduleEnabled,AdvancedControlEnabled,GroupID,Type,IsGroup) " +
                       $"VALUES(" +
                       $" {dv} ,{(info.TimeScheduleEnabled ? 1 : 0)},{(info.AdvancedControlEnabled ? 1 : 0)}" +
                       $",{info.GroupID},{(ushort)type},{isgroup}) ";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText = $"UPDATE autocontrol_settings  " +
                        $"SET  TimeSheduleEnabled={(info.TimeScheduleEnabled ? 1 : 0)}," +
                        $"AdvancedControlEnabled ={(info.AdvancedControlEnabled ? 1 : 0)} ," +
                        $"GroupID={info.GroupID} " +
                        $"WHERE OwnerID={dv} AND Type={(ushort)type}";
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }


}
