using FdlWindows.View;
using GrpcMain.Account;
using MyClient.View.User;

namespace MyClient.View
{
    [AutoDetectView("FSystemInfo", "系统", "", true)]
    public partial class FSystemInfo : Form, IView
    {

        GrpcMain.System.SystemService.SystemServiceClient _client;
        AccountService.AccountServiceClient accountServiceClient;
        IViewHolder _viewholder;
        public FSystemInfo(GrpcMain.System.SystemService.SystemServiceClient client, AccountService.AccountServiceClient accountServiceClient)
        {
            InitializeComponent();
            _client = client;
            this.accountServiceClient = accountServiceClient;
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

        private async void btnCreatUser_Click(object sender, EventArgs e)
        {
            try
            {
                FCreatUser f = new FCreatUser(accountServiceClient, true);
                f.ShowDialog();

                var r2 = await _client.GetStaticsAsync(new GrpcMain.System.Request_GetStatics());
                grid_userstatics.DataSource = r2.Data.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"错误");
            }
        }
    }
}
