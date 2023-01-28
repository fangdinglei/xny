using FdlWindows.View;
using GrpcMain.Account.Audit;

namespace MyClient.View
{
    public partial class FAudit : Form
    {
        const int PerPage = 15;
        AuditService.AuditServiceClient _client;
        public FAudit(AuditService.AuditServiceClient client)
        {
            InitializeComponent();
            cb_mode.SelectedIndex = 0;
            _client = client; 
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void btn_search_Click(object sender, EventArgs e)
        {
            //TODO 总数量
            var r=await  _client.GetAsync(new Request_Get() { 
                 Count=PerPage,
                 Cursor=0,
                 Mode= cb_mode.SelectedIndex,
            });
            pageController1.OnPageChanged-= PageController1_OnPageChanged;
            pageController1.PageSize = PerPage;
            pageController1.RecordCount=r.Infos.Count;
            pageController1.OnPageChanged += PageController1_OnPageChanged;
        }

        private async void PageController1_OnPageChanged()
        {
            var r = await _client.GetAsync(new Request_Get()
            {
                Count = PerPage,
                Cursor = (pageController1.Page - 1) *PerPage,
                Mode = cb_mode.SelectedIndex,
            });
            pageController1.OnPageChanged -= PageController1_OnPageChanged;
            pageController1.PageSize = PerPage;
            pageController1.RecordCount = r.Infos.Count;
            pageController1.OnPageChanged += PageController1_OnPageChanged;
        }
    }
}
