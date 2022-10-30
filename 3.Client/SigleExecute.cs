using MyClient.Utility;

namespace FDL.Program
{
    /// <summary>
    /// 只能在同一时间执行一次
    /// </summary>
    public class SigleExecute
    {
        static HashSet<string> acts = new HashSet<string>();
        static public bool Execute(string name, Action action)
        {
            Debuger.Assert(action != null, "action不能为空");
            lock (acts)
            {
                if (acts.Contains(name))
                    return false;
                else
                    acts.Add(name);
            }
            try
            {
                action();
                lock (acts)
                    acts.Remove(name);
            }
            catch (Exception)
            {
                lock (acts)
                    acts.Remove(name);
                throw;
            }
            return true;
        }
        /// <summary>
        /// 通过回调释放锁
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        static public bool Execute(string name, Action<Action> action)
        {
            Debuger.Assert(action != null, "action不能为空");
            lock (acts)
            {
                if (acts.Contains(name))
                    return false;
                else
                    acts.Add(name);
            }
            try
            {
                action(() =>
                {
                    lock (acts)
                        acts.Remove(name);
                });
            }
            catch (Exception)
            {
                lock (acts)
                    acts.Remove(name);
                throw;
            }
            return true;
        }

        /// <summary>
        /// 开启新的task并在结束后释放锁
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        static public async Task<bool> ExecuteAsync(string name, Action action)
        {
            Debuger.Assert(action != null, "action不能为空");
            lock (acts)
            {
                if (acts.Contains(name))
                    return false;
                else
                    acts.Add(name);
            }
            try
            {
                await Task.Run(() =>
                {
                    action();
                });
                lock (acts)
                    acts.Remove(name);
            }
            catch (Exception)
            {
                lock (acts)
                    acts.Remove(name);
                throw;
            }
            return true;
        }
        /// <summary>
        /// 等待执行结束后释放锁
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        static public async Task<bool> ExecuteAsync(string name, Func<Task> action)
        {
            Debuger.Assert(action != null, "action不能为空");
            lock (acts)
            {
                if (acts.Contains(name))
                    return false;
                else
                    acts.Add(name);
            }
            try
            {
                await action();
                lock (acts)
                    acts.Remove(name);
            }
            catch (Exception)
            {
                lock (acts)
                    acts.Remove(name);
                throw;
            }
            return true;
        }

        internal static Task ExecuteAsync(Task task)
        {
            throw new NotImplementedException();
        }
    }
}