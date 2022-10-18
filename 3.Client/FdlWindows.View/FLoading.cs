using FdlWindows.View;
using System.Diagnostics;

namespace MyClient.View
{
    [AutoDetectView("Loading", "", "", false)]
    public partial class FLoading : Form, IView
    {
        bool loading = false;
        public FLoading()
        {
            InitializeComponent();
        }
        Func<Task<bool>> retry;
        /// <summary>
        /// 向界面管理器汇报是否在加载中 加载中的界面将不再被复用
        /// </summary>
        Action<bool> setisloading;
        Action? okcall;
        Action? exitcall;
        public Control View => this;
        IViewHolder _viewholder;

        void OnSuccess()
        {
            setisloading(false);
            loading = false;
            try
            {
                okcall?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.Assert(false, "回调失败" + ex.Message);
            }

            okcall = null;
            exitcall = null;
            _viewholder.Back();
        }
        void OnFailure()
        {
            setisloading(false);
            loading = false;
            label1.Text = "加载失败";
            button1.Visible = true;
            button1.Enabled = true;
        }
        async Task<bool> ShowLoading(Func<Task<bool>> task, Func<Task<bool>>? retry, Action okcall = null, Action exitcall = null, Action<bool> setisloading = null)
        {
            Debug.Assert(loading == false, "不能重复进入加载界面");
            loading = true; setisloading(true);
            button1.Visible = false;
            label1.Text = "加载中";
            this.retry = retry ?? task;
            this.setisloading = setisloading;
            this.okcall = okcall;
            this.exitcall = exitcall;
            try
            {
                if (await task())
                {
                    OnSuccess();
                    return true;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                OnFailure();
                return false;
            }
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            button1.Enabled = false;
            loading = true;
            setisloading(true);
            try
            {
                if (await retry())
                {
                    OnSuccess();
                    return;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                OnFailure();
            }
            button1.Enabled = true;
        }

        public async void PrePare(params object[] par)
        {
            await ShowLoading(par[0] as Func<Task<bool>>, par[1] as Func<Task<bool>>, par[2] as Action, par[3] as Action, par[4] as Action<bool>);
        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }

        public void OnEvent(string name, params object[] pars)
        {
            if (name == "Exit")
            {
                try
                {
                    exitcall?.Invoke();
                }
                catch (Exception)
                {
                    Debug.Assert(false, "回调失败");
                }

                //FormExitEventArg arg = pars[0] as FormExitEventArg;
                //arg.Cancel = !arg.IsForNewWindow; 
            }
        }

        public void OnTick()
        {
        }
    }
}
