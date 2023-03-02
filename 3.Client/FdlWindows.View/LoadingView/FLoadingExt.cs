using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyClient.View;
using System.Linq;
using System.Runtime.CompilerServices;
using static Grpc.Core.Metadata;
using System.Threading.Tasks;

namespace FdlWindows.View
{
    static public class FLoadingExt
    {
        static public void UseFLoading(this IServiceCollection serviceCollection, FLoadingOption option)
        {
            serviceCollection.TryAddTransient<FLoading>();
            serviceCollection.TryAddSingleton(option);
        }

        static string LOADING_NAME = "___loading___";
        //control -> child control ->visible enabled 也是此功能的锁
        static ConditionalWeakTable<Control, ConditionalWeakTable<Control, Tuple<bool, bool>>> datas
            = new();
        //control ->FLoading
        static ConditionalWeakTable<Control, FLoading> loadingview
         = new();
        /// <summary>
        /// 扩展控件加载中功能
        /// </summary>
        /// <param name="form">控件</param>
        /// <param name="task">任务</param>
        /// <param name="retry">失败重试</param>
        /// <param name="okcall">成功回调</param>
        static public void ShowLoading(this Control form, Func<Task<bool>> task, Func<Task<bool>>? retry = null, Action okcall = null)
        {
            FLoading? loading = null;
            ConditionalWeakTable<Control, Tuple<bool, bool>>? dt = null;
            lock (datas)
            {
                loading = loadingview.GetOrCreateValue(form);
                loading.Visible = false;
                loading.TopLevel = false;
                loading.Parent = form;
                loading.Dock = DockStyle.Fill;
                loading.FormBorderStyle = FormBorderStyle.None;
                loading.BringToFront();
                dt = datas.GetOrCreateValue(form);
            }
            var tid = loading.GetTaskId();
            //此处可能堆积请求
            form.CancelLoading();
            if (!loading.CheckTaskId(tid))
                return;
            lock (form)
            {
                form.Visible = true;
                //记录加载前控件的状态
                foreach (Control ctl in form.Controls)
                {
                    if (ctl is FLoading)
                        continue;
                    Tuple<bool, bool> ctldt = new Tuple<bool, bool>(ctl.Visible, ctl.Enabled); ;
                    ctl.Visible = false;
                    ctl.Enabled = false;
                    dt.AddOrUpdate(ctl, ctldt);
                }
                loading.Visible = true;
            }
            loading.ShowLoading2(tid, task, retry, () =>
            {
               lock (datas)  {
                    ConditionalWeakTable<Control, Tuple<bool, bool>>? dt
                        = datas.GetOrCreateValue(form);
                    foreach (Control ctl in form.Controls)
                    {
                        if (ctl is FLoading)
                            continue;
                        Tuple<bool, bool> ctldt = dt.GetOrCreateValue(ctl);
                        ctl.Visible = ctl.Visible||ctldt.Item1;
                        ctl.Enabled = ctl.Enabled||ctldt.Item2;
                    }
                    datas.Remove(form);
                }
                loading.Visible = false;
                okcall?.Invoke();
            });
        }
        /// <summary>
        /// 扩展控件加载中功能是否正在加载中
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        static public bool IsLoading(this Control form)
        {
            lock (datas)
            {
                FLoading? loading = null;
                return loadingview.TryGetValue(form, out loading) && loading.LoadingX;
            }
        }
        /// <summary>
        /// 取消加载 如果在加载则等到结束
        /// </summary>
        /// <param name="form"></param>
        /// <returns>获取true不保证一定处于未加载状态</returns>
        static public bool CancelLoading(this Control form)
        {
            FLoading? loading = null;
            lock (datas)
            {
                if (!loadingview.TryGetValue(form, out loading) || !loading.LoadingX)
                {
                    return true;
                }
            }
            //等待结束
            if (!loading.CancelLoad())
                return false;
            //恢复状态
            lock (datas)
            {
                ConditionalWeakTable<Control, Tuple<bool, bool>>? dt
                           = datas.GetOrCreateValue(form);
                foreach (Control ctl in form.Controls)
                {
                    if (ctl.Name == LOADING_NAME && ctl is FLoading a)
                        continue;
                    Tuple<bool, bool> ctldt = dt.GetOrCreateValue(ctl);
                    ctl.Visible = ctldt.Item1;
                    ctl.Enabled = ctldt.Item2;
                    dt.AddOrUpdate(ctl, ctldt);
                }
                datas.Remove(form);
                loading.Visible = false;
            }
            return true;
        }

    }

}
