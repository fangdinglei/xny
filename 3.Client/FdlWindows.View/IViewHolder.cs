namespace FdlWindows.View
{
    public interface IViewHolder
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        int Uid { get; }
        /// <summary>
        /// 容器
        /// </summary>
        Control Holder { get; }
        /// <summary>
        /// 打开页面
        /// </summary>
        /// <param name="name">页面名称</param>
        /// <param name="newwindow">关闭当前页面还是附加在当前页面上</param>
        /// <param name="par">参数</param>
        /// <return >true if opened</return>>
        bool SwitchTo(string name, bool newwindow, params object[] par);
        /// <summary>
        /// 当前控件是否显示在界面最顶层
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        bool IsTopView(IView it);
        void Back();
        void ShowLoading(IView view, Func<Task<bool>> load, Func<Task<bool>>? retry = null
            , Action okcall = null, Action exitcall = null);
        bool IsLoading(IView view);

        void ShowDatePicker(Action<DateTime, DateTime> call);
    }

}
