using System.Diagnostics;

namespace GrpcMain
{
    public interface IGrpcCursorUtility
    {
        /// <summary>
        /// 更新cursor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="maxcount"></param>
        /// <param name="onlast">如果还有后续值 传入最后一个实体 否则传入null</param>
        /// <returns></returns>
        IEnumerable<T> Run<T>(IEnumerable<T> list, int maxcount, Action<T> onlast) where T : class;
    }







    public class GrpcCursorUtilityImp : IGrpcCursorUtility
    {
        public IEnumerable<T> Run<T>(IEnumerable<T> list, int maxcount, Action<T> onlast)
            where T : class
        {
            Debug.Assert(list != null && maxcount > 0);
            if (list.Count() == maxcount)
            {
                onlast?.Invoke(list.Last());
                return list.Take(maxcount - 1);
            }
            else
            {
                onlast?.Invoke(null);
                return list;
            }
        }
    }

}
