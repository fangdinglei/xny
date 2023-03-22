using FdlWindows.View;
using GrpcMain.UserDevice;
using System.Data;

namespace MyClient.View.Ext
{
    /// <summary>
    /// Action<List<long>>
    /// mode 0不选调用空 1单选 2多选
    /// mode0用于方便级联
    /// </summary>
    [AutoDetectView(nameof(FDeviceSelector), "设备选择器", "", false)]
    public partial class FDeviceSelector : Form, IView
    {
        UserDeviceService.UserDeviceServiceClient userDeviceServiceClient;
        List<DeviceWithUserDeviceInfo>? devices;
        List<User_Device_Group>? groups;
        Action<List<long>>? call;
        int mode = 0;
        IViewHolder _viewholder;
        public FDeviceSelector(UserDeviceService.UserDeviceServiceClient userDeviceServiceClient)
        {
            InitializeComponent();
            this.userDeviceServiceClient = userDeviceServiceClient;
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
            call = (par[0] as Action<List<long>>);
            mode = (int)par[1];
            if (mode == 0)
            {
                call?.Invoke(null);
                _viewholder.Back();
                return;
            }
            list_devicegroup.ShowLoading(async () =>
            {
                list_devices.DataSource = null;
                list_devices.Visible = false;
                var res1 = await userDeviceServiceClient.GetGroupInfosAsync(new Google.Protobuf.WellKnownTypes.Empty());
                groups = res1.Groups.ToList();
                groups.Insert(0, new User_Device_Group()
                {
                    Id = 0,
                    Name = "默认",
                });
                list_devicegroup.DisplayMember = "Name";
                list_devicegroup.DataSource = groups;
                return true;
            });
        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }

        private void list_devicegroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groups == null
                || list_devicegroup.SelectedIndex < 0
                || list_devicegroup.SelectedIndex >= groups.Count)
            {
                list_devices.DataSource = null;
                list_devices.Visible = false;
                return;
            }
            list_devices.Visible = true;
            list_devices.ShowLoading(async () =>
            {
                list_devices.DataSource = null;
                var res1 = await userDeviceServiceClient.GetDevicesAsync(
                    new Request_GetDevices()
                    {
                        GroupId = groups[list_devicegroup.SelectedIndex].Id
                    });
                devices = res1.Info.ToList();
                list_devices.DisplayMember = "Name";
                list_devices.DataSource = devices.Select(it => it.Device).ToList();
                return true;
            });
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (list_devices.Items.Count > 0 && list_devices.SelectedIndex >= 0)
            {
                var id = (list_devices.SelectedItem as GrpcMain.Device.Device).Id;
                call?.Invoke(new List<long> { id });
                _viewholder.Back();
            }
        }
    }
}
