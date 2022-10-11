using System;
using System.Collections.Generic;

namespace XNYAPI.Model.AutoControl
{
    /// <summary>
    /// 计划集合，在数组越后面优先级越大
    /// 例如:
    /// 1.永久 值0
    /// 2.定时y年m月d日 20点到21点 值1
    /// 则优先检查2是否满足要求 如果满足则不在考虑1
    /// 0忽略 1关闭 2启动 3智能
    /// 0ALL 1Once 2EveryWeek
    /// </summary>
    public class ScheduleInfo
    {
        public ServiceType Type;
        public uint OwnerID;
        public bool IsGroup;
        public List<ScheduleItem> Data;
        public int GetValue(DateTime time, int defaultvalue)
        {
            for (int i = Data.Count - 1; i >= 0; i--)
            {
                if (Data[i].IsTimeIn(time))
                {
                    return Data[i].GetValue();
                }
            }
            return defaultvalue;
        }
        public ScheduleInfo()
        {
                
        }
        public ScheduleInfo(ServiceType Type)
        {
            this.Type = Type;
            Data = new List<ScheduleItem>();
        }
        public bool Validate_Json() {
            if (!Enum.IsDefined(typeof(ServiceType), Type))
                return false;
            foreach (var item in Data)
            {
                if (!item.Validate_Json())
                {
                    return false;
                }  
            }
            return true;
        }
    }
}

