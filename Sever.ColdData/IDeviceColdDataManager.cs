using MyDBContext.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sever.ColdData
{
    internal interface IDeviceColdDataManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colddata"></param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        Task DoStore(Device_DataPoint_Cold colddata,Dictionary<string,object>?pars);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colddata"></param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        Task<byte[]> DoLoad(Device_DataPoint_Cold colddata, Dictionary<string, object>? pars);
    }
}
