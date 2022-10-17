using FdlWindows.View;
using GrpcMain.Account;
using GrpcMain.Device;
using GrpcMain.UserDevice;
using MyClient.Grpc;
using MyClient ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GrpcMain.Account.DTODefine.Types;
using static GrpcMain.UserDevice.DTODefine.Types;

namespace MyClient.View
{
    [AutoDetectView("FUserInfo", "用户管理", "", true)]
    public partial class FUserInfo : Form, IView
    {
        UserBaseInfoManager _userBaseInfoManager;

        AccountService.AccountServiceClient _accountServiceClient;
        UserDeviceService.UserDeviceServiceClient _userDeviceServiceClient;
        DeviceService.DeviceServiceClient _deviceServiceClient;
        IViewHolder _viewholder;
        public FUserInfo(AccountService.AccountServiceClient accountServiceClient, UserDeviceService.UserDeviceServiceClient userDeviceServiceClient, DeviceService.DeviceServiceClient deviceServiceClient)
        {
            InitializeComponent();
            this._userDeviceServiceClient = userDeviceServiceClient;
            this._accountServiceClient = accountServiceClient;
            this._deviceServiceClient = deviceServiceClient;
            _userBaseInfoManager = new UserBaseInfoManager(
                tuname,tuid,tpass,tphone,temail,baseinfo_authoritys
                ,btn_passupdate,btn_update,list_user
                ,()=>_userInfos,accountServiceClient);
        }

        public Control View => this;

        List< UserInfo>? _userInfos;
       List<DeviceWithUserDeviceInfo>? _devicewithuserdeviceinfos; 
        public void PrePare(params object[] par)
        {
            list_user.DataSource = null;
            list_user.Items.Clear();
            _viewholder.ShowLoading(this, async () => {
                _userInfos = null;
                var r1 =await _accountServiceClient.GetUserInfoAsync(new GrpcMain.Account.DTODefine.Types.Request_GetUserInfo()
                {
                    SubUser=true,
                });
                _userInfos = r1.UserInfo.ToList();

                //devicewithuserdeviceinfos = null;
                //var r2 = await userDeviceServiceClient.GetDevicesAsync(new GrpcMain.UserDevice.DTODefine.Types.Request_GetDevices
                //{

                //});
                //devicewithuserdeviceinfos = r2.Info.ToList();

                return true;
            },
            okcall: () => {
                //用户选择列表
                string[] arr = new string[_userInfos.Count];
                arr[0] = _userInfos[0].UserName + "(自己)";
                for (int i = 1; i < _userInfos.Count; i++)
                    arr[i] = _userInfos[i].UserName;
                list_user.DataSource = arr;


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
        public void SetViewHolder(IViewHolder viewholder) { 
        this._viewholder = viewholder;
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

        private async void btn_passupdate_Click(object sender, EventArgs e)
        {
            bool self;
            var user = GetSelectedUser(out self);
            if (user==null)
            {
                MessageBox.Show("没有选中用户", "警告");
                return;
            }
            //TODO 密码格式校验
            if (string.IsNullOrWhiteSpace(tpass.Text))
            {
                MessageBox.Show("密码不能为空", "警告");
                return;
            }


            try
            {  
                var rsp=await _accountServiceClient.ChangePassWordAsync(self switch {
                    true => new GrpcMain.Account.DTODefine.Types.Request_ChangePassWord()
                    {//是自己

                    },
                    false => new GrpcMain.Account.DTODefine.Types.Request_ChangePassWord()
                    {//是子用户

                    }
                });
                rsp.ThrowIfNotSuccess();
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改密码失败:" +ex.Message, "警告");
            }
        }
         
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



        #region 管理器定义
        public class UserBaseInfoManager
        {
            private TextBox _tuname;
            private TextBox _tuid;
            private TextBox _tpass;
            private TextBox _tphone;
            private TextBox _temail; 
            private TextBox _baseinfo_authoritys;
            private Button _btn_passupdate;
            private Button _btn_update;
            private ListBox _list_user;
            private Func<List<UserInfo>?> _getUserInfos;
            private AccountService.AccountServiceClient _client;

            public UserInfo? User { get; private set; }
            public bool IsSelf { get; private set; }

            public UserBaseInfoManager(TextBox tuname, TextBox tuid,
                TextBox tpass, TextBox tphone, TextBox temail, TextBox baseinfo_authoritys,
                Button btn_passupdate, Button btn_update,
                ListBox list_user, Func<List<UserInfo>?> getUserInfos,
                AccountService.AccountServiceClient client)
            {
                _tuname = tuname;
                _tuid = tuid;
                _tpass = tpass;
                _tphone = tphone;
                _btn_passupdate = btn_passupdate;
                _btn_update = btn_update;
                _list_user = list_user;
                _getUserInfos = getUserInfos;
                _client = client;
                _temail = temail;

                _list_user.SelectedIndexChanged += _list_user_SelectedIndexChanged;
                _btn_update.Click += _btn_update_Click;
                _baseinfo_authoritys = baseinfo_authoritys;
            }


            void SetUserInfo(UserInfo? userInfo, bool isself)
            {
                User = userInfo;
                IsSelf = isself; 
                if (userInfo!=null)
                { 
                    _tuname.Text = userInfo.UserName;
                    _tuid.Text = userInfo.ID + "";
                    _tpass.Text = "****";
                    _tphone.Text = userInfo.Phone;
                    _temail.Text = userInfo.Email;
                    List<string> obj;var s___baseinfo_authoritys = "";
                    userInfo.Authoritys.TryDeserializeObject(out obj);
                    if (obj!=null)
                    {
                        foreach (var item in obj)
                        {
                            s___baseinfo_authoritys += item+"\r\n";
                        } 
                    }
                    _baseinfo_authoritys.Text = s___baseinfo_authoritys;
                }
            }
            private void _list_user_SelectedIndexChanged(object? sender, EventArgs e)
            {
                ListBox? list = sender as ListBox;
                Debug.Assert(list != null);
                var infos = _getUserInfos();
                if (infos != null && list.SelectedIndex >= 0 && list.SelectedIndex < infos.Count)
                {
                    SetUserInfo(infos[list.SelectedIndex], list.SelectedIndex == 0);
                }
                else
                {
                    SetUserInfo(null, false);
                }
            }
            /// <summary>
            /// 修改用户信息
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            /// <exception cref="NotImplementedException"></exception>
            private async void _btn_update_Click(object? sender, EventArgs e)
            {
                try
                {
                    if (User == null)
                    {
                        MessageBox.Show("没有选中用户", "提示");
                        return;
                    }
                    Request_UpdateUserInfo req = new Request_UpdateUserInfo() { 
                        UserInfo=new UserInfo() { 
                            ID = User.ID,
                        }
                    };
                    if (_tuname.Text != User.UserName)
                    {
                        req.UserInfo.UserName = _tuname.Text;
                    }
                    if (_tphone.Text != User.Phone)
                    {
                        req.UserInfo.Phone = _tphone.Text;
                    }
                    if (_temail.Text != User.Email)
                    {
                        req.UserInfo.Email = _temail.Text;
                    }

                    var rsp =await _client.UpdateUserInfoAsync(req);
                    rsp.ThrowIfNotSuccess(); 
                    MessageBox.Show("修改成功" , "提示");

                    Request_GetUserInfo reqx = new Request_GetUserInfo()
                    {
                        UserId=User.ID,
                        SubUser=false,
                    };
                    var rspx=await _client.GetUserInfoAsync(reqx);
                    var idx=_getUserInfos().FindIndex(it=>it.ID==User.ID);
                    _getUserInfos()[idx] = rspx.UserInfo[0];
                    idx = _list_user.SelectedIndex;
                    _list_user.SelectedIndex = -1;
                    _list_user.SelectedIndex = idx;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误:" + ex.Message, "错误");
                }
            }



        }
        #endregion

    } 
}
