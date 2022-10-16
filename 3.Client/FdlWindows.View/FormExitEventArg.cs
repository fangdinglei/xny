namespace FdlWindows.View
{
    public class FormExitEventArg
    {
        /// <summary>
        /// 是退出直到展开新的界面
        /// </summary>
        public bool IsForNewWindow { get; internal set; }
        public bool Cancel { get; internal set; }
    }

}
