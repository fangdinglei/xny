using System;
using System.Collections.Generic;
using XNYAPI.AutoControl.Script.Model;
using XNYAPI.Model.Device;

namespace XNYAPI.AutoControl.Script
{
    public class AutoControlScriptManager
    {
        static public bool Inited { get; private set; }
        static Dictionary<string, AutoServiceAttribute> Services;
        /// <summary>
        /// 注册所有服务
        /// </summary> 
        /// <returns></returns>
        /// <exception cref="Exception">
        static public void RegistServices()
        {
            Services = new Dictionary<string, AutoServiceAttribute>();
            foreach (var item in AutoServiceAttribute.GetServices())
            {
                Services.Add(item.Name, item);

            }
            Inited = true;
        }
        /// <summary>
        /// 解析脚本
        /// </summary>
        /// <param name="scriptstr"></param>
        /// <returns></returns>
        /// <exception cref="Exception">
        static public AutoScript Prase(string scriptstr)
        {
            if (!Inited)
                throw new Exception("请先初始化");
            var pages = new List<ScriptPage>();
            var items = new List<PageItem>();
            var pagestrs = scriptstr.Split("###\n", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < pagestrs.Length; i++)
            {
                var itemsstr = pagestrs[i].Split("\n", StringSplitOptions.RemoveEmptyEntries);
                items.Clear();
                //获取step
                int step;
                if (itemsstr.Length < 2)
                    throw new Exception("格式异常：页最少为两行");
                if (!int.TryParse(itemsstr[0], out step))
                    throw new Exception("格式异常：无法获取step");
                if (step <= 0 || step > 100000)
                    throw new Exception("格式异常：step范围为[1,100000]");
                for (int j = 1; j < itemsstr.Length; j++)
                {
                    var itemstr = itemsstr[j];
                    var arr = itemstr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length == 0)
                        continue;
                    else if (arr.Length == 1)
                    {
                        if (!Services.ContainsKey(arr[0]))
                            throw new Exception($"没有合适的服务：{arr[0]}");
                        items.Add(new PageItem() { Name = arr[0], Parm = null });
                    }
                    else if (arr.Length == 2)
                    {
                        if (!Services.ContainsKey(arr[0]))
                            throw new Exception($"没有合适的服务：{arr[0]}");
                        var par = arr[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
                        items.Add(new PageItem() { Name = arr[0], Parm = par });
                    }
                }
                pages.Add(new ScriptPage() { Items = items.ToArray(), Step = step });
            }
            return new AutoScript() { Pages = pages.ToArray() };
        }


        static public void OnStart(int step)
        {
            foreach (var sv in Services.Values)
            {
                sv.StartCall?.Invoke(step);
            }
        }
        static public void OnEnd(int step)
        {
            foreach (var sv in Services.Values)
            {
                sv.EndCall?.Invoke(step);
            }
        }
        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="script"></param>
        /// <param name="ownerid">执行对象的id</param>
        /// <param name="step">时间s</param>
        /// <exception cref="Exception">
        static public void RunScript(AutoScript script, uint ownerid, string realid, DeviceTypeInfo typeInfo, int step)
        {
            if (!Inited)
                throw new Exception("请先初始化");
            foreach (var page in script.Pages)
            {
                if (step % page.Step != 0)
                    continue;
                ScriptContext context = new ScriptContext(ownerid, realid, step);
                context.Type = typeInfo;
                foreach (var item in page.Items)
                {
                    if (!Services.ContainsKey(item.Name))
                        throw new Exception("没有合适的服务");
                    Services[item.Name].ScriptCall.Invoke(context, item);
                }
            }
        }
    }

}

