using FdlWindows.View;

namespace MyClient.View
{
    [AutoDetectView("FSystemInfo", "系统", "", true)]
    public partial class FSystemInfo : Form, IView
    {

        GrpcMain.System.SystemService.SystemServiceClient _client;
        IViewHolder _viewholder;
        public FSystemInfo(GrpcMain.System.SystemService.SystemServiceClient client)
        {
            InitializeComponent();
            _client = client;
        }

        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {

        }

        public void OnTick()
        {

        }

        public void PrePare(params object[] par)
        {
            this.ShowLoading(async () =>
             {
                 var res = await _client.GetSystemBaseInfoAsync(new GrpcMain.System.Request_GetSystemBaseInfo { });
                 text_id.Text = res.SeverId + "";
                 text_os.Text = res.OsVersionName;
                 text_pagesize.Text = res.PageSize / 1024 + "KB";
                 text_totalmemory.Text = res.PhisicalMemory / 1024 + "GB";
                 text_cpucount.Text = res.ProcesserCount + "个";
                 text_runtime.Text = res.SystemTime / 1000 + "s";

                 var r2 = await _client.GetStaticsAsync(new GrpcMain.System.Request_GetStatics());
                 grid_userstatics.DataSource = r2.Data.ToList();
                 //TODO
                 return true;
             });

        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }
    }
}
