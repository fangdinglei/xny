using FdlWindows.View;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FdlWindows.View
{
    public partial class FLoading : Form
    {
        //用于跳过任务直接执行最新的任务
        int taskid = 0;
        public bool LoadingX = false; 
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
        Action? okcall;
        Task<bool>? tsk;


       ToolTip tipui = new ToolTip();
        void OnSuccess()
        {
            try
            {
                okcall?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.Assert(false, "回调失败" + ex.Message);
            }
            okcall = null;
            this.tsk = null;
            tipui.RemoveAll();
            LoadingX = false;

        }
        void OnFailure(Exception ex)
        {
            var str = Option?.Convertor(ex) ?? ex.Message;
            label1.Text = str.Length > 7 ? str.Substring(0, 7) + ".." : str;
            tipui.SetToolTip(label1, str);
            button1.Visible = true;
            this.tsk = null;
            LoadingX = false;
        }
        void OnStartLoad()
        {
            tipui.RemoveAll();
            LoadingX = true; 
            button1.Visible = false;
            label1.Text = "加载中";
        }
        /// <summary>
        /// 内置取消加载
        /// </summary>
        /// <returns></returns>
        public bool CancelLoad() {
            Task<bool>? tsk=null;
            lock (this)
            {
                if (LoadingX)
                {
                    this.Visible = false;
                    retry = null;
                    okcall = null;
                }
                tsk = this.tsk;
            }
            if (tsk != null)
                Task.WaitAll(tsk);
            lock (this)
            {
                if (tsk==this.tsk)
                {
                    this.tsk = null;
                }
            }
            return true;
        }

        public int GetTaskId() {
            lock (this)
            {

                int res = taskid ;
                taskid = (taskid + 1) & 0xFFFFFF;
                return res;
            }
        }
        public bool CheckTaskId(int taskid) {
            lock (this)
            {
                return ((taskid + 1) & 0xFFFFFF) ==this.taskid;
            }
        }

        public async void ShowLoading2(int taskid,Func<Task<bool>> task, Func<Task<bool>>? retry, Action okcall = null)
        {
            Debug.Assert(!LoadingX,"请等待加载结束后在加载");
            lock (this)
            {
                if (!CheckTaskId(taskid))
                {
                    return;
                }
            }
            this.retry = retry ?? task;
            this.okcall = okcall;
            try
            {
                OnStartLoad();
                tsk = task(); 
                if (await tsk)
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
                return;
            }
        }
        private async void button1_ClickAsync(object sender, EventArgs e)
        {

            try
            {
                OnStartLoad();
                tsk = retry(); 
                if (await tsk)
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

      
    }

}
