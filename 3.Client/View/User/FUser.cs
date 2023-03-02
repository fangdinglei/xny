using FdlWindows.View;
using GrpcMain.Account;
using GrpcMain.AccountHistory;
using GrpcMain.Device;
using GrpcMain.UserDevice;
using MyUtility;
using System.ComponentModel;
using static GrpcMain.Account.DTODefine.Types;

namespace MyClient.View.User
{
    [AutoDetectView("FUser", "用户管理", "", true)]
    public partial class FUser : Form, IView
    {
        FUserInfo _FUserInfo;
        FUserPriority _FUserPriority;
        FUserLoginHistory _FUserLoginHistory;
        FUserDevice _FUserDevice;

        BaseManager[] Managers => new BaseManager[] {
            _FUserInfo.BaseManager,
            _FUserPriority.BaseManager,
            _FUserDevice.BaseManager,
            _FUserLoginHistory.BaseManager,
        };

        AccountService.AccountServiceClient _accountServiceClient;
        UserDeviceService.UserDeviceServiceClient _userDeviceServiceClient;
        DeviceService.DeviceServiceClient _deviceServiceClient;
        AccountHistoryService.AccountHistoryServiceClient _AccountHistoryServiceClient;
        private ITimeUtility _timeUtility;
        IViewHolder _viewholder;
        public FUser(AccountService.AccountServiceClient accountServiceClient, UserDeviceService.UserDeviceServiceClient userDeviceServiceClient, DeviceService.DeviceServiceClient deviceServiceClient, AccountHistoryService.AccountHistoryServiceClient AccountHistoryServiceClient, ITimeUtility timeUtility)
        {
            InitializeComponent();
            this._userDeviceServiceClient = userDeviceServiceClient;
            this._accountServiceClient = accountServiceClient;
            this._deviceServiceClient = deviceServiceClient;
            _AccountHistoryServiceClient = AccountHistoryServiceClient;
            _timeUtility = timeUtility;
            _FUserInfo = new FUserInfo(accountServiceClient);
            _FUserPriority = new FUserPriority(accountServiceClient);
            _FUserLoginHistory = new FUserLoginHistory(AccountHistoryServiceClient, _timeUtility, this);
            _FUserDevice = new FUserDevice(_userDeviceServiceClient);

            for (int i = 0; i < Managers.Length; i++)
            {
                var item = Managers[i];
                item.Init(tabControl1, i, list_user, () => _userInfos);
                (item.View as Form).TopLevel = false;
                (item.View as Form).FormBorderStyle = FormBorderStyle.None;
                item.View.Dock = DockStyle.Fill;
                item.View.Visible = true;
                tabControl1.TabPages[i].Controls.Add(item.View);
            }
        }

        public Control View => this;

        BindingList<UserInfo>? _userInfos;
        List<DeviceWithUserDeviceInfo>? _devicewithuserdeviceinfos;
        public void PrePare(params object[] par)
        {
            list_user.DataSource = null;
            list_user.Items.Clear();
            list_user.ShowLoading( async () =>
            {
                _userInfos = null;
                var r1 = await _accountServiceClient.GetUserInfoAsync(new GrpcMain.Account.DTODefine.Types.Request_GetUserInfo()
                {
                    SubUser = true,
                });
                _userInfos = new BindingList<UserInfo>(r1.UserInfo);
                return true;
            },
            okcall: () =>
            {
                //用户选择列表 
                //arr[0] = _userInfos[0].UserName + "(自己)";
                //for (int i = 1; i < _userInfos.Count; i++)
                //    arr[i] = _userInfos[i].UserName; 
                list_user.DataSource = _userInfos;
                list_user.DisplayMember = "UserName";
                //group_userdevice_list_device.Items.Clear();
                //group_userdevice_list_device.Items.AddRange(devicewithuserdeviceinfos.Select(it => it.Device.Name).ToArray());
                //var sel = list_user.SelectedIndex;
                //if (userinfos != null && sel >= 0 && sel < userinfos.Count)
                //{
                //    var user = userinfos[sel];
                //    if (deviceIds.Item1 == user.ID)
                //    {//是当前选中的用户的数据
                //        for (int i = 0; i < deviceInfos.Count; i++)
                //        {
                //            var dv = deviceInfos[i];
                //            group_userdevice_list_device.SetItemChecked(i, deviceIds.Item2.Contains(dv.ID));
                //        }
                //    }
                //}
            });

            list_user.DataSource = null;
            list_user.Items.Clear();
            list_user.SelectedIndex = -1;
        }
        public void OnEvent(string name, params object[] pars)
        {
            if (name == "Exit")
            {
            }
        }

        public void OnTick()
        {
            if (!Visible)
                return;
        }
        public void SetViewHolder(IViewHolder viewholder)
        {
            this._viewholder = viewholder;
            foreach (var item in Managers)
            {
                item.SetViewHolder(viewholder);
            }
        }

        /// <summary>
        /// 获取选中的用户
        /// </summary>
        /// <param name="self">是否是自己</param>
        /// <returns></returns>
        UserInfo? GetSelectedUser(out bool self)
        {
            var sel = list_user.SelectedIndex;
            if (_userInfos != null && sel >= 0 && sel < _userInfos.Count)
            {
                self = list_user.SelectedIndex == 0;
                return _userInfos[sel];
            }
            self = false;
            return null;
        }
        private void list_user_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        //private async void btn_passupdate_Click(object sender, EventArgs e)
        //{
        //    bool self;
        //    var user = GetSelectedUser(out self);
        //    if (user == null)
        //    {
        //        MessageBox.Show("没有选中用户", "警告");
        //        return;
        //    }
        //    //TODO 密码格式校验
        //    if (string.IsNullOrWhiteSpace(tpass.Text))
        //    {
        //        MessageBox.Show("密码不能为空", "警告");
        //        return;
        //    }


        //    try
        //    {
        //        var rsp = await _accountServiceClient.ChangePassWordAsync(self switch
        //        {
        //            true => new GrpcMain.Account.DTODefine.Types.Request_ChangePassWord()
        //            {//是自己

        //            },
        //            false => new GrpcMain.Account.DTODefine.Types.Request_ChangePassWord()
        //            {//是子用户

        //            }
        //        });
        //        rsp.ThrowIfNotSuccess();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("修改密码失败:" + ex.Message, "警告");
        //    }
        //}

        private void btn_delet_Click(object sender, EventArgs e)
        {
            //if (userinfos == null || userinfos.Count == 0)
            //{
            //    MessageBox.Show("未知异常", "警告");
            //    return;
            //}
            //if (list_user.SelectedIndex < 0 || list_user.SelectedIndex > userinfos.Count)
            //{
            //    MessageBox.Show("请选择要删除的用户", "警告");
            //    return;
            //}
            //if (list_user.SelectedIndex == 0)
            //{
            //    MessageBox.Show("不能删除自己哦", "警告");
            //    return;
            //}


            //try
            //{
            //    var res = Global.client.Exec(new DeletUserRequest(userinfos[list_user.SelectedIndex].ID));
            //    if (!res.IsError)
            //    {
            //        MessageBox.Show("删除成功哦", "提示");
            //        GetNewUserInfoFromNet();
            //    }
            //    else
            //    {
            //        MessageBox.Show("删除失败:" + res.Error, "警告");
            //    }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("未知异常", "警告");
            //}

            //var user = infos[list_user.SelectedIndex];
            //tuname.Text = user.UserName;
            //tuid.Text = user.ID + "";
            //tpass.Text = "****";
            //tphone.Text = user.Phone;
        }

        private void bok_Click(object sender, EventArgs e)
        {
            //var sel = list_user.SelectedIndex;
            //if (userinfos != null && sel >= 0 && sel < userinfos.Count)
            //{
            //    var user = userinfos[sel];
            //    List<uint> add = new List<uint>();
            //    List<uint> del = new List<uint>();
            //    for (int i = 0; i < deviceInfos.Count; i++)
            //    {
            //        if (group_userdevice_list_device.GetItemChecked(i))
            //        {
            //            if (!deviceIds.Item2.Contains(deviceInfos[i].ID))
            //            {
            //                add.Add(deviceInfos[i].ID);
            //            }
            //        }
            //        else
            //        {
            //            if (deviceIds.Item2.Contains(deviceInfos[i].ID))
            //            {
            //                del.Add(deviceInfos[i].ID);
            //            }
            //        }
            //    }

            //    try
            //    {
            //        XNYResponseBase res = null, res2 = null;
            //        if (add.Count > 0)
            //            res = Global.client.Exec(new AddUserDeviceRequest(add, user.ID));
            //        if (del.Count > 0)
            //            res2 = Global.client.Exec(new DelUserDeviceRequest(del, user.ID));
            //        if (res != null && res.IsError || res2 != null && res2.IsError)
            //        {
            //            MessageBox.Show("操作失败", "提示");
            //        }
            //        Task.Run(() =>
            //        {
            //            Global.client.ExecAsync(new GetUserAllDeviceIDRequest(user.ID));
            //        });
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("操作失败", "提示");
            //    }


            //}
        }


    }
}
