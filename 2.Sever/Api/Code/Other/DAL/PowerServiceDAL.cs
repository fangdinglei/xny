 
using System;
using System.Collections.Generic;
using XNYAPI.Model.AutoControl; 
using XNYAPI.Utility;

namespace XNYAPI.DAL
{
    public class PowerServiceDAL
    {
        /// <summary>
        /// 获取用户所有设备的电量信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception" />
        static public List<PowerInfo> LoadAllPowerInfos()
        {
            List<PowerInfo> res = new List<PowerInfo>();
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT DeviceId,LastChargeUpdate,LastDisChargeUpdate" +
                    ",LastChargeP,Soc,LastDisChargeP,MaxSOC FROM powerservice_info";
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    res.Add(new PowerInfo(rd.GetUInt32(0), new DateTime(rd.GetInt64(1)), new DateTime(rd.GetInt64(2))
                        , rd.GetDouble(6), rd.GetDouble(4))
                    { LastChargeP = rd.GetDouble(3), LastDischargeP = rd.GetDouble(5) });
                }
            }
            return res;
        }
        /// <summary>
        /// 获取用户所有设备的剩余电量
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception" />
        static public List<Response.Power.PowerRate> GetPowerRates(uint uid)
        {
            List< Response.Power.PowerRate> res = new List<Response.Power.PowerRate>();
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT powerservice_info.DeviceId,Soc,MaxSOC FROM powerservice_info inner join userdevice ON powerservice_info.DeviceId = userdevice.DeviceId " +
                    $"WHERE UserID={uid}";
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    double soc = rd.GetDouble(1);
                    double max= rd.GetDouble(2);
                    float rt;
                    if (max == 0 || soc <= 0 || soc > max)
                        rt = 0;
                    else
                        rt =(float)(soc / max) ;
                    res.Add(new  Response.Power.PowerRate(rd.GetUInt32(0),rt));
                }
            }
            return res;
        }
        /// <summary>
        /// 获取设备的剩余电量
        /// </summary>
        /// <returns>null if not find</returns>
        /// <exception cref="Exception" />
        static public Response.Power.PowerRate GetPowerRate(uint dvid)
        { 
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT Soc,MaxSOC FROM powerservice_info  " +
                    $"WHERE DeviceId={dvid}";
                var rd = cmd.ExecuteReader();
                if (!rd.Read())
                    return null;
                double soc = rd.GetDouble(1);
                double max = rd.GetDouble(2);
                float rt;
                if (max == 0 || soc <= 0 || soc > max)
                    rt = 0;
                else
                    rt = (float)(soc / max);
                return new Response.Power.PowerRate(rd.GetUInt32(0), rt); 
            } 
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>null if dont have</returns>
        /// <exception cref="Exception" />
        static public PowerInfo LoadPowerInfo(uint id)
        {

            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = $"SELECT  LastChargeUpdate,LastDisChargeUpdate" +
                   $",LastChargeP,Soc,LastDisChargeP,MaxSOC FROM powerservice_info WHERE DeviceId={id}";
                var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    return new PowerInfo(id, new DateTime(rd.GetInt64(0)), new DateTime(rd.GetInt64(1))
                        , rd.GetDouble(5), rd.GetDouble(3))
                    { LastChargeP = rd.GetDouble(2), LastDischargeP = rd.GetDouble(4) };
                }
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception" />
        static public void SetPowerInfos(List<PowerInfo> infos)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var tran = cnn.BeginTransaction();
                var cmd = cnn.CreateCommand();
                cmd.Transaction = tran;
                cmd.CommandText = "LOCK tables powerservice_info write";
                cmd.ExecuteNonQuery();
                foreach (var info in infos)
                {
                    if (info == null)
                        continue;
                    cmd.CommandText = $"SELECT 1 FROM powerservice_info WHERE DeviceId='{info.DeviceId}' LIMIT 1";
                    if (cmd.ExecuteScalar() == null)
                    {//数据不存在
                        cmd.CommandText = $"INSERT INTO  powerservice_info(DeviceId,LastChargeUpdate,LastDisChargeUpdate,MaxSOC,Soc,LastChargeP,LastDisChargeP) " +
                            $"VALUES('{info.DeviceId}',{info.LastChargePUpdate.Ticks},{info.LastDisChargePUpdate.Ticks},{info.MaxSOC},{info.SOC},{info.LastChargeP},{info.LastDischargeP})";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {//更新soc
                        cmd.CommandText = $"UPDATE powerservice_info " +
                            $"SET Soc={info.SOC},LastChargeP={info.LastChargeP},LastChargeUpdate={info.LastChargePUpdate.Ticks}," +
                            $" LastDisChargeUpdate= {info.LastDisChargePUpdate.Ticks} ,LastDisChargeP= {info.LastDischargeP}" +
                            $" WHERE DeviceId='{info.DeviceId}'";
                        cmd.ExecuteNonQuery();
                    }
                }
                cmd.CommandText = "Unlock tables";
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
        }
        static public void SetPowerInfo(PowerInfo info)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                var tran = cnn.BeginTransaction();
                cmd.Transaction = tran; 
                cmd.CommandText = $"SELECT 1 FROM powerservice_info WHERE DeviceId='{info.DeviceId}' LIMIT 1";
                if (cmd.ExecuteScalar() == null)
                {//数据不存在
                    cmd.CommandText = $"INSERT INTO powerservice_info(DeviceId,LastChargeUpdate,LastDisChargeUpdate,MaxSOC,Soc,LastChargeP,LastDisChargeP) " +
                        $"VALUES('{info.DeviceId}',{info.LastChargePUpdate.Ticks},{info.LastDisChargePUpdate.Ticks},{info.MaxSOC},{info.SOC},{info.LastChargeP},{info.LastDischargeP})";
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                else
                {//更新soc
                    cmd.CommandText = $"UPDATE powerservice_info " +
                        $"SET Soc={info.SOC},LastChargeP={info.LastChargeP},LastChargeUpdate={info.LastChargePUpdate.Ticks}," +
                        $" LastDisChargeUpdate= {info.LastDisChargePUpdate.Ticks} ,LastDisChargeP= {info.LastDischargeP}" +
                        $" WHERE DeviceId='{info.DeviceId}'";
                    cmd.ExecuteNonQuery(); 
                    tran.Commit();
                }
            }
        }
    }
}
