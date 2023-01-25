using FdlWindows.View;
using System.Diagnostics;

namespace MyClient.View
{
    [AutoDetectView("Loading", "", "", false)]
    public partial class FLoading : Form, IView
    {
        bool loading = false; bool backfailed;
        FLoadingOption? Option;
        public FLoading()
        {
            InitializeComponent();
        }
        public FLoading(FLoadingOption option)
        {
            InitializeComponent();
            Option = option;
        }
        Func<Task<bool>>? retry;
        /// <summary>
        /// 向界面管理器汇报是否在加载中 加载中的界面将不再被复用
        /// </summary>
        Action<bool> setisloading;
        Action? okcall;
        Action? exitcall;
        public Control View => this;
        IViewHolder _viewholder;

        ToolTip tipui = new ToolTip();
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
            tipui.RemoveAll();
            backfailed = !_viewholder.Back(this);

        }
        void OnFailure(Exception ex)
        {
            setisloading(false);
            loading = false;
            var str = Option?.Convertor(ex) ?? ex.Message;
            label1.Text = str.Length > 7 ? str.Substring(0, 7) + ".." : str;
            tipui.SetToolTip(label1, str);
            button1.Visible = true;
        }
        void OnStartLoad()
        {
            tipui.RemoveAll();
            loading = true; setisloading(true);
            button1.Visible = false;
            label1.Text = "加载中";
        }

        async Task<bool> ShowLoading(Func<Task<bool>> task, Func<Task<bool>>? retry, Action okcall = null, Action exitcall = null, Action<bool> setisloading = null)
        {
            Debug.Assert(loading == false, "不能重复进入加载界面");
            backfailed = false;
            this.retry = retry ?? task;
            this.setisloading = setisloading;
            this.okcall = okcall;
            this.exitcall = exitcall;
            try
            {
                OnStartLoad();
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
            catch (Exception ex)
            {
                OnFailure(ex);
                return false;
            }
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {


            try
            {
                OnStartLoad();
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
            catch (Exception ex)
            {
                OnFailure(ex);
            }
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
                    if (loading)
                    {
                        loading = false;
                        setisloading(false);
                    }
                    exitcall?.Invoke();
                }
                catch (Exception)
                {
                    Debug.Assert(false, "回调失败");
                }

                //FormExitEventArg arg = pars[0] as FormExitEventArg;
                //arg.Cancel = !arg.IsForNewWindow; 
            }
            else if (name == "UnCovered")
            {
                if (backfailed)
                {
                    backfailed = !_viewholder.Back(this);
                }
            }
        }

        public void OnTick()
        {
        }
    }

}
