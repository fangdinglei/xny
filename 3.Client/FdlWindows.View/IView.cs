namespace FdlWindows.View
{
    public interface IView
    {
        Control View { get; }
        /// <summary>
        /// 向界面传输数据
        /// </summary>
        /// <param name="par">数据</param>
        void PrePare(params object[] par);
        /// <summary>
        /// 设置其容器
        /// </summary>
        /// <param name="container"></param>
        void SetViewHolder(IViewHolder viewholder);
        /// <summary>
        /// 响应事件
        ///"Exit", FormExitEventArg;    退出界面
        ///"SizeChanged" null 窗体大小改变
        ///"Covered"  被其他界面覆盖
        ///"UnCovered" 覆盖解除
        /// </summary>
        /// <param name="name"> 
        /// </param>
        /// <param name="pars"></param>
        void OnEvent(string name, params object[] pars);
        /// <summary>
        /// 每次界面需要刷新时触发
        /// </summary>
        void OnTick();
    }

}
