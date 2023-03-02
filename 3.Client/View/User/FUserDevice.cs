using FdlWindows.View;
using GrpcMain.Account;
using GrpcMain.UserDevice;
using MyClient.Grpc;
using MyDBContext.Main;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;
using static GrpcMain.Account.DTODefine.Types;

namespace MyClient.View.User
{
    public partial class FUserDevice : Form
    {
        public BaseManager BaseManager;
        UserInfo SelectedUser => BaseManager.SelectedUser;
        bool Active => BaseManager.Active;
        Func<Collection<UserInfo>?> GetUserInfos => BaseManager.GetUserInfos;

        UserDeviceService.UserDeviceServiceClient userDeviceServiceClient;

        List<User_Device_Group> groups;
        List<DeviceWithUserDeviceInfo> devices_self;
        List<User_Device> devices_selecteduser;

        AccountService.AccountServiceClient client;
        public FUserDevice(UserDeviceService.UserDeviceServiceClient userDeviceServiceClient)
        {
            BaseManager = new BaseManager(this);
            BaseManager.RefreshHandle += Refresh;
            BaseManager.SetUserInfoHandle += OnSetUser;
            InitializeComponent();
            this.userDeviceServiceClient = userDeviceServiceClient;
            list_devicegroup.ShowLoading(async () =>
            {
                list_devices.DataSource = null;
                list_devices.Visible = false;
                op_panel.Visible = false;
                var res1 = await userDeviceServiceClient.GetGroupInfosAsync(new Google.Protobuf.WellKnownTypes.Empty());
                groups = res1.Groups.ToList();
                list_devicegroup.DisplayMember = "Name";
                list_devicegroup.DataSource = groups;
                return true;
            });

        }

        void Refresh(BaseManager bm)
        {
            if (!Active)
                return;
            if (SelectedUser == null || SelectedUser == bm.FatherInfo|| devices_self==null)
                return;//TODO 未加载完时异常进入
            op_panel.Enabled = SelectedUser != bm.FatherInfo;
            var req = new Request_GetUserDevices()
            {
                UserId = SelectedUser.ID,
            };
            req.DeviceIds.Add(devices_self.Select(it => it.Device.Id));
            op_panel.ShowLoading(async () =>
            {
                var res2 = await userDeviceServiceClient.GetUserDevicesAsync(
                 req
                );
                devices_selecteduser = res2.UserDevices.ToList();
                list_devices_SelectedIndexChanged(null, null);
                return true;
            });
            return;
        }
        void OnSetUser(BaseManager bm)
        {
            if (!Active)
                return;
            Refresh(bm);
        }
        void QuickCheck(int level)
        {
            List<TableLayoutPanel> checkgroups = new List<TableLayoutPanel>() { lay_read, lay_write, lay_control };
            List<CheckBox> A = new List<CheckBox>();
            List<CheckBox> B = new List<CheckBox>();
            for (int i = 0; i < level; i++)
            {
                foreach (var item in checkgroups[i].Controls)
                    if (item is CheckBox check)
                        A.Add(check);
            }
            for (int i = level; i < checkgroups.Count; i++)
            {
                foreach (var item in checkgroups[i].Controls)
                    if (item is CheckBox check)
                        B.Add(check);
            }
            if (A.Where(it => it.Checked).Count() != A.Count)
                A.ForEach(it => it.Checked = it.Enabled && true);
            else
                A.ForEach(it => it.Checked = false);
            B.ForEach(it => it.Checked = false);

        }
        private void btn_sel_read_Click(object sender, EventArgs e)
        {
            QuickCheck(1);
        }

        private void btn_sel_rw_Click(object sender, EventArgs e)
        {
            QuickCheck(2);
        }

        private void btn_sel_rwc_Click(object sender, EventArgs e)
        {
            QuickCheck(3);
        }


        private void list_devicegroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groups == null
                || list_devicegroup.SelectedIndex < 0
                || list_devicegroup.SelectedIndex >= groups.Count)
            {
                list_devices.DataSource = null;
                op_panel.Visible = false;
                list_devices.Visible = false;
                return;
            }
            list_devices.Visible = true;
            list_devices.ShowLoading(async () =>
            {
                list_devices.DataSource = null;
                op_panel.Visible = false;
                var res1 = await userDeviceServiceClient.GetDevicesAsync(
                    new Request_GetDevices()
                    {
                        GroupId = groups[list_devicegroup.SelectedIndex].Id
                    });
                devices_self = res1.Info.ToList();
                list_devices.DataSource = devices_self.Select(it => it.Device.Name).ToList();
                return true;
            });
        }
        private void list_devices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (devices_self == null || devices_selecteduser == null
               || list_devices.SelectedIndex < 0
               || list_devices.SelectedIndex >= devices_self.Count)
            {
                op_panel.Visible = false;
                return;
            }
            op_panel.Visible = true;

            var userdevice = devices_self[list_devices.SelectedIndex].UserDevice;
            var authority = (UserDeviceAuthority)devices_self[list_devices.SelectedIndex].UserDevice.Authority;
            var bool_0 = check_read_baseinfo.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate) && authority.HasFlag(UserDeviceAuthority.Read_BaseInfo);
            var bool_1 = check_read_repair.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate) && authority.HasFlag(UserDeviceAuthority.Read_Repair);
            var bool_2 = check_read_cmd.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate) && authority.HasFlag(UserDeviceAuthority.Read_Cmd);
            var bool_3 = check_read_timesetting.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate) && authority.HasFlag(UserDeviceAuthority.Read_TimeSetting);
            var bool_4 = check_read_status.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate) && authority.HasFlag(UserDeviceAuthority.Read_Status);
            var bool_5 = check_read_data.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate) && authority.HasFlag(UserDeviceAuthority.Read_Data);
            var bool_6 = check_write_deletdata.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate) && authority.HasFlag(UserDeviceAuthority.Write_DeletData);
            var bool_7 = check_write_deletdevice.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate) && authority.HasFlag(UserDeviceAuthority.Write_DeletDevice);
            var bool_8 = check_write_baseinfo.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate) && authority.HasFlag(UserDeviceAuthority.Write_BaseInfo);
            var bool_9 = check_write_repair.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate) && authority.HasFlag(UserDeviceAuthority.Write_Repair);
            var bool_10 = check_control_cmd.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate) && authority.HasFlag(UserDeviceAuthority.Control_Cmd);
            var bool_11 = check_control_timesetting.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate) && authority.HasFlag(UserDeviceAuthority.Control_TimeSetting);
            var bool_12 = check_delegate.Enabled = authority.HasFlag(UserDeviceAuthority.Delegate);

            var userdevice2 = devices_selecteduser.Where(it => it.Dvid == userdevice.Dvid).FirstOrDefault();
            var authority2 = (UserDeviceAuthority)userdevice2.Authority;
            if (userdevice2 == null)
            {
                check_read_baseinfo.Checked = false;
                check_read_repair.Checked = false;
                check_read_cmd.Checked = false;
                check_read_timesetting.Checked = false;
                check_read_status.Checked = false;
                check_read_data.Checked = false;
                check_write_deletdata.Checked = false;
                check_write_deletdevice.Checked = false;
                check_write_baseinfo.Checked = false;
                check_write_repair.Checked = false;
                check_control_cmd.Checked = false;
                check_control_timesetting.Checked = false;
                check_delegate.Checked = false;
            }
            else
            {
                check_read_baseinfo.Checked = bool_0 && authority.HasFlag(UserDeviceAuthority.Read_BaseInfo);
                check_read_repair.Checked = bool_1 && authority.HasFlag(UserDeviceAuthority.Read_Repair);
                check_read_cmd.Checked = bool_2 && authority.HasFlag(UserDeviceAuthority.Read_Cmd);
                check_read_timesetting.Checked = bool_3 && authority.HasFlag(UserDeviceAuthority.Read_TimeSetting);
                check_read_status.Checked = bool_4 && authority.HasFlag(UserDeviceAuthority.Read_Status);
                check_read_data.Checked = bool_5 && authority.HasFlag(UserDeviceAuthority.Read_Data);
                check_write_deletdata.Checked = bool_6 && authority.HasFlag(UserDeviceAuthority.Write_DeletData);
                check_write_deletdevice.Checked = bool_7 && authority.HasFlag(UserDeviceAuthority.Write_DeletDevice);
                check_write_baseinfo.Checked = bool_8 && authority.HasFlag(UserDeviceAuthority.Write_BaseInfo);
                check_write_repair.Checked = bool_9 && authority.HasFlag(UserDeviceAuthority.Write_Repair);
                check_control_cmd.Checked = bool_10 && authority.HasFlag(UserDeviceAuthority.Control_Cmd);
                check_control_timesetting.Checked = bool_11 && authority.HasFlag(UserDeviceAuthority.Control_TimeSetting);
                check_delegate.Checked = bool_12 && authority.HasFlag(UserDeviceAuthority.Delegate);
            }


        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedUser==null|| devices_self==null||list_devices.SelectedIndex<0
                    || list_devices.SelectedIndex > devices_self.Count)
                {
                    MessageBox.Show("请选择合适的用户和设备" , "提示");
                    return;
                }
                var req = new Request_UpdateUserDeviceAuthority();
                req.Dvids.Add(devices_self[list_devices.SelectedIndex].UserDevice.Dvid);
                req.UserDevice = new User_Device();
                req.UserDevice.UserId = SelectedUser.ID;
                #region 设置权限
                req.UserDevice.Authority = 0;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Read_BaseInfo;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Read_Repair;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Read_Cmd;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Read_TimeSetting;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Read_Status;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Read_Data;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Write_DeletData;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Write_DeletDevice;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Write_BaseInfo;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Write_Repair;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Control_Cmd;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Control_TimeSetting;
                req.UserDevice.Authority |= (int)UserDeviceAuthority.Delegate;
                #endregion
                var rsp= userDeviceServiceClient.UpdateUserDeviceAuthority(req);
                rsp.ThrowIfNotSuccess();
                MessageBox.Show(String.IsNullOrWhiteSpace(rsp.Message)?"成功":rsp.Message,"提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.IsNullOrWhiteSpace(ex.Message) ? "未知错误" : ex.Message, "提示");
            }
        }
    }
}
