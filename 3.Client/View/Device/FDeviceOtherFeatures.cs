using FdlWindows.View;
using MyClient.View.AutoControl;

namespace MyClient.View
{
    [AutoDetectView("FDeviceOtherFeatures", "", "", false)]
    public partial class FDeviceOtherFeatures : Form, IView
    {
        long _dvid;

        IViewHolder _viewholder;
        LocalDataBase _localDataBase;
        public FDeviceOtherFeatures(LocalDataBase localDataBase)
        {
            InitializeComponent();
            _localDataBase = localDataBase;
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
            _dvid = (long)par[0];
        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }

        private void btn_repair_Click(object sender, EventArgs e)
        {
            _viewholder.SwitchTo("FDeviceRepair", false, _dvid);
        }

        private void btn_type_Click(object sender, EventArgs e)
        {
            var dv = _localDataBase.GetDevice(_dvid);
            if (dv == null)
            {
                MessageBox.Show("请求超时或设备不存在", "错误");
                return;
            }
            _viewholder.SwitchTo("FDeviceTypeDetail", false, false, dv.DeviceTypeId);
        }

        private void btn_timeplan_Click(object sender, EventArgs e)
        {
            var dv = _localDataBase.GetDevice(_dvid);
            if (dv == null)
            {
                MessageBox.Show("请求超时或设备不存在", "错误");
                return;
            }
            _viewholder.SwitchTo(nameof(FAutoControl), false, new List<(long, string)> { (dv.Id, dv.Name) });
        }
    }
}
