namespace GrpcMain
{
    public interface  IGrpcCursorUtility {
        /// <summary>
        /// 更新cursor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="maxcount"></param>
        /// <param name="onlast">null则为0否则更新为指定ID</param>
        /// <returns></returns>
        IEnumerable<T> Run<T>(IEnumerable<T> list,   int maxcount,Action<T >onlast );
    }







    public class GrpcCursorUtilityImp : IGrpcCursorUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="maxcount">实际获取为maxcount-1个</param>
        /// <param name="onlast"></param>
        /// <returns></returns>
        public IEnumerable<T> Run<T>(IEnumerable<T> list, int maxcount, Action<T > onlast)
        {
            if (list.Count()==maxcount)
            { 
                return list.Take(maxcount-1);
            }
            else
            { 
                return list;
            } 
        }
    }

}
