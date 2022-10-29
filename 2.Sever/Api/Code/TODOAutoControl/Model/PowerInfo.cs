using System;

namespace XNYAPI.Model.AutoControl
{
    public class PowerInfo
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public uint DeviceId;
        /// <summary>
        ///最近一次更新时间
        /// </summary>
        public DateTime LastChargePUpdate;
        /// <summary>
        ///最近一次更新时间
        /// </summary>
        public DateTime LastDisChargePUpdate;
        /// <summary>
        /// 最近一次充电功率
        /// </summary>
        public double LastChargeP;
        /// <summary>
        /// 最近一次放电功率
        /// </summary>
        public double LastDischargeP;
        /// <summary>
        /// 最近一次电量
        /// </summary>
        public double SOC;
        /// <summary>
        /// 电量最大值
        /// </summary>
        public double MaxSOC;
        public double SocRate
        {
            get
            {
                if (MaxSOC <= 0 || SOC < 0)
                {
                    return 0;
                }
                else
                {
                    return SOC / MaxSOC;
                }
            }
        }

        public PowerInfo(uint deviceId, DateTime lastChargePUpdate, DateTime lastDischargePUpdate, double maxSOC, double soc = -1)
        {
            DeviceId = deviceId;
            LastChargePUpdate = lastChargePUpdate;
            LastDisChargePUpdate = lastDischargePUpdate;
            LastChargeP = 0; LastDischargeP = 0;
            MaxSOC = maxSOC;
            SOC = soc < 0 ? maxSOC : soc;
        }

    }
}

