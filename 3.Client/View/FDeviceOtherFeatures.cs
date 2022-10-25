using FdlWindows.View;
using GrpcMain.UserDevice;

namespace MyClient.View
{
    [AutoDetectView("FDeviceOtherFeatures", "", "", false)]
    public partial class FDeviceOtherFeatures : Form, IView
    {
        DeviceWithUserDeviceInfo _dev;

        IViewHolder _viewholder;
        public FDeviceOtherFeatures()
        {
            InitializeComponent();
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
            _dev = par[0] as DeviceWithUserDeviceInfo;
        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }

        private void btn_repair_Click(object sender, EventArgs e)
        {
            _viewholder.SwitchTo("FDeviceRepair", false, _dev);
        }
    }
}
