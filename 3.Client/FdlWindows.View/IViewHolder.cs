namespace FdlWindows.View
{
    /// <summary>
    /// 所有方法都是线程安全的
    /// </summary>
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
        /// <summary>
        /// 判断一个控件是否是当前容器的直接子控件  
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        bool IsParentOfView(IView it);
        /// <summary>
        ///  
        /// </summary>
        void Back();
        /// <summary>
        /// 如果传入界面是第一个界面则弹出 并返回是否能成功  
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        bool Back(IView it);
        ///// <summary>
        ///// 不太级联等待 应当在okcall 或exitcall中等待否则将会出现错误  
        ///// </summary>
        ///// <param name="view"></param>
        ///// <param name="load"></param>
        ///// <param name="retry"></param>
        ///// <param name="okcall"></param>
        ///// <param name="exitcall"></param>
        //void ShowLoading(IView view, Func<Task<bool>> load, Func<Task<bool>>? retry = null
        //    , Action okcall = null, Action exitcall = null);
        ///// <summary>
        /////  
        ///// </summary>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //bool IsLoading(IView view);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="call"></param>
        void ShowDatePicker(Action<DateTime, DateTime> call);
    }

}
