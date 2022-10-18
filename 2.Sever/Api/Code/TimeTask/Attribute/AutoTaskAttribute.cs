
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using XNYAPI.Utility;

namespace TimerMvcWeb.Filters
{
    /// <summary>
    /// Author:BigLiang(lmw)
    /// Date:2016-12-29
    /// Modifier:FDL
    /// Date:2016-12-19
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]//表示此Attribute仅可以施加到类元素上
    public class AutoTaskAttribute : Attribute
    {
        public string Name { get; set; }
        /// <summary>
        /// 当程序运行时触发
        /// </summary>
        public string OnLoadCall { get; set; }
        /// <summary>
        /// 当定时时间到时触发
        /// </summary>
        public string OnTimeCall { get; set; }
        /// <summary>
        /// 执行间隔秒数（未设置或0 则只执行一次）
        /// </summary>
        public int IntervalSeconds { get; set; }
        /// <summary>
        /// 当程序退出时触发
        /// </summary>
        public string OnExitCall { get; set; }

        //保留对Timer 的引用，避免回收
        private static Dictionary<AutoTaskAttribute, Timer> timers = new Dictionary<AutoTaskAttribute, Timer>();
        private static List<Action> OnExit = new List<Action>();
        private static List<Action> OnLoad = new List<Action>();

        /// <summary>
        /// Global.asax.cs 中调用
        /// </summary>
        public static void RegisterTask()
        {
            StartAutoTask();
            foreach (var a in OnLoad)
            {
                a.Invoke();
            }
            OnLoad.Clear();
            ////异步执行该方法
            //new Task(() => StartAutoTask()).Start();
        }
        public static void OnExitAPP()
        {
            foreach (var a in OnExit)
            {
                a.Invoke();
            }
            OnExit.Clear();
        }


        /// <summary>
        /// 反射获取自动任务信息
        /// </summary>
        private static void StartAutoTask()
        {
            var types = Assembly.GetExecutingAssembly().ExportedTypes.Where(t => Attribute.IsDefined(t, typeof(AutoTaskAttribute))).ToList();
            foreach (var t in types)
            {
                try
                {
                    var atts = (AutoTaskAttribute[])Attribute.GetCustomAttributes(t, typeof(AutoTaskAttribute));
                    foreach (var att in atts)
                    {
                        if (att == null)
                            continue;
                        RegisterTask(att, t);
                    }
                }
                catch (Exception ex)
                {
                    //LogHelper.Error(t.FullName + " 任务启动失败", ex);
                    //Debug.WriteLine(t.FullName + " 任务启动失败", ex);
                    Logger.Log(Logger.LOGICERROR, "定时任务启动失败");
                    Logger.Log(ex);
                }
            }
        }
        /// <summary>
        /// 注册一个自动任务
        /// </summary>
        /// <param name="att"></param>
        /// <param name="t"></param>
        static void RegisterTask(AutoTaskAttribute att, Type t)
        {
            MethodInfo method_onload = null, method_time = null, method_exit = null;
            if (string.IsNullOrWhiteSpace(att.Name))
            {
                throw new Exception("Name不能是空");
            }
            if (!string.IsNullOrWhiteSpace(att.OnLoadCall))
            {
                method_onload = t.GetMethod(att.OnLoadCall);
                if (method_onload == null)
                    throw new Exception("未找到OnLoadCall函数，请检查名称是否正确");
                if (!method_onload.IsStatic)
                    throw new Exception("OnLoadCall函数必须是静态函数");
            }
            if (!string.IsNullOrWhiteSpace(att.OnTimeCall))
            {
                method_time = t.GetMethod(att.OnTimeCall);
                if (method_time == null)
                    throw new Exception("未找到OnTimeCall函数，请检查名称是否正确");
                if (!method_time.IsStatic)
                    throw new Exception("OnTimeCall函数必须是静态函数");
            }
            if (!string.IsNullOrWhiteSpace(att.OnExitCall))
            {
                method_exit = t.GetMethod(att.OnExitCall);
                if (method_exit == null)
                    throw new Exception("未找到OnExitCall函数，请检查名称是否正确");
                if (!method_exit.IsStatic)
                    throw new Exception("OnExitCall函数必须是静态函数");
            }
            if (method_time != null && att.IntervalSeconds <= 0)
            {
                throw new Exception("IntervalSeconds必须是正数");
            }

            if (method_onload != null)
            {
                OnLoad.Add(() =>
                {
                    try
                    {
                        method_onload.Invoke(null, null);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(Logger.ERROR, att.Name + " 自动任务 OnLoad失败");
                        Logger.Log(ex);
                    }
                });
            }
            if (method_time != null)
            {
                timers.Add(att, new Timer((o) =>
                {
                    timers[att].Change(-1, -1);
                    try
                    {
                        method_time.Invoke(null, null);

                    }
                    catch (Exception e)
                    {
                        Logger.Log(Logger.ERROR, att.Name + " 自动任务 OnTime失败");
                        Logger.Log(e);
                    }

                    timers[att].Change(att.IntervalSeconds * 1000, att.IntervalSeconds * 1000);
                }, null, 0, att.IntervalSeconds * 1000));
            }
            if (method_exit != null)
            {
                OnExit.Add(() =>
                {
                    try
                    {
                        method_exit.Invoke(null, null);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(Logger.ERROR, att.Name + " 自动任务 OnExit失败");
                        Logger.Log(ex);
                    }
                });
            }


        }
    }
}

