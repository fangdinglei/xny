using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            Debuger.Assert(action != null, "SigleExecuteaction不能为空");
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
        static public async Task<bool> ExecuteAsync(string name, Action action)
        {
            Debuger.Assert(action != null, "SigleExecuteaction不能为空");
            lock (acts)
            {
                if (acts.Contains(name))
                    return false;
                else
                    acts.Add(name);
            }
            try
            {
                await Task.Run(()=> {
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
        static public async Task<bool> ExecuteAsync(string name, Func<Task> action)
        {
            Debuger.Assert(action != null, "SigleExecutea ction不能为空");
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
    }
}