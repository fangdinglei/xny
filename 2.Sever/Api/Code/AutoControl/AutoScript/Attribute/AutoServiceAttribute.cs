using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XNYAPI.AutoControl.Script.Model;

namespace XNYAPI.AutoControl.Script
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =true)]
    public class AutoServiceAttribute : Attribute
    {
        /// <summary>
        /// script处理函数
        /// </summary>
        public string OnScript { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 当所有script开始时触发
        /// </summary>
        public string OnStart { get; set; }
        /// <summary>
        /// 当所有script完成时触发
        /// </summary>
        public string OnEnd { get; set; }

        public Action<ScriptContext, PageItem > ScriptCall;
        /// <summary>
        /// par step
        /// </summary>
        public Action< int> StartCall;
        /// <summary>
        /// par step
        /// </summary>
        public Action<int> EndCall;

        /// <summary>
        /// 获取所有的服务
        /// </summary>
        /// <returns>回调集合 ScriptContext, PageItem,作用对象,step step用于区分刷新的时间，销毁过期缓存</returns>
        public static List<AutoServiceAttribute> GetServices()
        {
            var res = new List<AutoServiceAttribute>() ;
            var types = Assembly.GetExecutingAssembly().ExportedTypes.Where(t => Attribute.IsDefined(t, typeof(AutoServiceAttribute))).ToList();
            foreach (var t in types)
            {
                try
                {
                    var atts = (AutoServiceAttribute[])Attribute.GetCustomAttributes(t, typeof(AutoServiceAttribute));
                    foreach (var att in atts)
                    {
                        if (att == null)
                            continue;
                        GetService(att, t);
                        res.Add(att);
                    }
                }
                catch (Exception ex)
                {
                    throw  ;
                }
            }
            return res;
        }
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="att"></param>
        /// <param name="t"></param>
        /// <exception cref="Exception">
        static void GetService(AutoServiceAttribute att, Type t)
        { 
            object obj = null;
            if (string.IsNullOrWhiteSpace(att.Name))
            {
                throw new Exception($"{att.Name} 特性 Name不能是空");
            }
            if (!string.IsNullOrWhiteSpace(att.OnScript))
            {
                MethodInfo method = t.GetMethod(att.OnScript);
                if (method == null)
                    throw new Exception($"{att.Name}未找到OnScript函数，请检查名称是否正确");
                
                if (method.IsStatic) {
                    att.ScriptCall = (context, item ) =>
                    {
                        method.Invoke(null, new object[] { context, item });
                    };
                }
                else
                {
                    if (obj==null)
                        obj = Activator.CreateInstance(t);
                    var o = obj;
                    att.ScriptCall = (context, item ) =>
                    {
                        method.Invoke(o, new object[] { context, item  });
                    }; 
                }
                     
             
            }
            if (!string.IsNullOrWhiteSpace(att.OnStart))
            {
                MethodInfo method = t.GetMethod(att.OnStart);
                if (method == null)
                    throw new Exception($"{att.Name}未找到到OnStart函数，请检查名称是否正确");
                if (method.IsStatic)
                {
                    att.StartCall = (step) =>
                    {
                        method.Invoke(null, new object[] { step });
                    };
                }
                else
                {
                    if (obj == null)
                        obj = Activator.CreateInstance(t);
                    var o = obj;
                    att.StartCall = (step) =>
                    {
                        method.Invoke(o, new object[] { step });
                    };
                }
                 
             
            }
            if (!string.IsNullOrWhiteSpace(att.OnEnd))
            {
                MethodInfo method = t.GetMethod(att.OnEnd);
                if (method == null)
                    throw new Exception($"{att.Name}未找到OnEnd函数，请检查名称是否正确");
               
                if (method.IsStatic)
                {
                    att.EndCall = (step) =>
                    {
                        method.Invoke(null, new object[] { step });
                    };
                }
                else
                {
                    if (obj == null)
                        obj = Activator.CreateInstance(t);
                    var o = obj;
                    att.EndCall = (step) =>
                    {
                        method.Invoke(o, new object[] { step });
                    };
                }
               
            } 
        }
    }
}



