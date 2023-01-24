//using MyDBContext.Main;
//using Microsoft.EntityFrameworkCore;

//namespace Sever.ColdData.Imp
//{
//    public class ColdDataInDBManager : ColdDataManagerBase
//    {
//        public override string Name => "InDB";

//        public override Task<bool> Delet(Device_DataPoint_Cold colddata)
//        {
//            throw new NotImplementedException();
//        }

//        public override Task<byte> GetStatus(Device_DataPoint_Cold colddata)
//        {
//            throw new NotImplementedException();
//        }

//        public override Task<byte[]> Load(Device_DataPoint_Cold colddata)
//        {
//            return Task.FromResult(colddata.Data);
//        }

//        public override Task Store(Device_DataPoint_Cold colddata, byte[] bytes)
//        {
//            throw new NotImplementedException();
//        }
//    }

//}
