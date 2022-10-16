using FdlWindows.View;
using GrpcMain.Account;
using GrpcMain.Device;
using GrpcMain.UserDevice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GrpcMain.UserDevice.DTODefine.Types;

namespace MyClient.View
{
    [AutoDetectView("FUserInfo", "用户信息", "", true)]
    public partial class FUserInfo : Form, IView
    {
        AccountService.AccountServiceClient accountServiceClient;
        UserDeviceService.UserDeviceServiceClient userDeviceServiceClient;
        DeviceService.DeviceServiceClient deviceServiceClient;
        IViewHolder viewholder;
        public FUserInfo(AccountService.AccountServiceClient accountServiceClient, UserDeviceService.UserDeviceServiceClient userDeviceServiceClient, DeviceService.DeviceServiceClient deviceServiceClient)
        {
            InitializeComponent();
            this.userDeviceServiceClient = userDeviceServiceClient;
            this.accountServiceClient = accountServiceClient;
            this.deviceServiceClient = deviceServiceClient;
        }

        public Control View => this;

        List<GrpcMain.Account.DTODefine.Types.UserInfo>? userInfos;
       List<DeviceWithUserDeviceInfo>? devicewithuserdeviceinfos; 
        public void PrePare(params object[] par)
        {
            list_user.DataSource = null;
            list_user.Items.Clear();
            viewholder.ShowLoading(this, async () => {
                userInfos = null;
                var r1 =await accountServiceClient.GetUserInfoAsync(new GrpcMain.Account.DTODefine.Types.Request_GetUserInfo()
                {
                    SubUser=true,
                });
                userInfos = r1.UserInfo.ToList();

                devicewithuserdeviceinfos = null;
                var r2 = await userDeviceServiceClient.GetDevicesAsync(new GrpcMain.UserDevice.DTODefine.Types.Request_GetDevices
                {

                });
                devicewithuserdeviceinfos = r2.Info.ToList();

                return true;
            },
            okcall: () => {
                //用户选择列表
                string[] arr = new string[userInfos.Count];
                arr[0] = userInfos[0].UserName + "(自己)";
                for (int i = 1; i < userInfos.Count; i++)
                    arr[i] = userInfos[i].UserName;
                list_user.DataSource = arr;


                list_device.Items.Clear();
                list_device.Items.AddRange(devicewithuserdeviceinfos.Select(it => it.Device.Name).ToArray());
                var sel = list_user.SelectedIndex;
                if (userinfos != null && sel >= 0 && sel < userinfos.Count)
                {
                    var user = userinfos[sel];
                    if (deviceIds.Item1 == user.ID)
                    {//是当前选中的用户的数据
                        for (int i = 0; i < deviceInfos.Count; i++)
                        {
                            var dv = deviceInfos[i];
                            list_device.SetItemChecked(i, deviceIds.Item2.Contains(dv.ID));
                        }
                    }
                }
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
        this.viewholder = viewholder;
        }

        private void list_user_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sel = list_user.SelectedIndex;
            if (userinfos != null && sel >= 0 && sel < userinfos.Count)
            {
                var user = userinfos[sel];
                tuname.Text = user.UserName;
                tuid.Text = user.ID + "";
                tpass.Text = "****";
                tphone.Text = user.Phone;
                btn_passupdate.Enabled = sel == 0;
                tpass.Enabled = sel == 0;
                list_device.Enabled = sel != 0;
                btn_delet.Enabled = sel != 0;
                for (int i = 0; i < list_device.Items.Count; i++)
                {
                    list_device.SetItemChecked(i, false);
                }

                bok.Enabled = sel != 0;

                if (sel != 0)
                    Task.Run(() =>
                    {
                        Global.client.ExecAsync(new GetUserAllDeviceIDRequest(user.ID));
                    });

            }
        }

        private void btn_passupdate_Click(object sender, EventArgs e)
        {
            //TODO 密码格式校验
            if (string.IsNullOrWhiteSpace(tpass.Text))
            {
                MessageBox.Show("密码不能为空", "警告");
                return;
            }
            if (!tpass.Text.IsSqlSafeString())
            {
                MessageBox.Show("密码格式不对", "警告");
                return;
            }


            try
            {
                var res = Global.client.Exec(new ChangePassWordRequest(Global.client.pass, tpass.Text));
                if (!res.IsError)
                {
                    MessageBox.Show("修改成功", "提示");
                    GetNewUserInfoFromNet();
                }
                else
                {
                    MessageBox.Show("修改失败:" + res.Error, "警告");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("未知异常", "警告");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            MessageBox.Show("敬请期待", "提示");
        }

        private void btn_delet_Click(object sender, EventArgs e)
        {
            if (userinfos == null || userinfos.Count == 0)
            {
                MessageBox.Show("未知异常", "警告");
                return;
            }
            if (list_user.SelectedIndex < 0 || list_user.SelectedIndex > userinfos.Count)
            {
                MessageBox.Show("请选择要删除的用户", "警告");
                return;
            }
            if (list_user.SelectedIndex == 0)
            {
                MessageBox.Show("不能删除自己哦", "警告");
                return;
            }


            try
            {
                var res = Global.client.Exec(new DeletUserRequest(userinfos[list_user.SelectedIndex].ID));
                if (!res.IsError)
                {
                    MessageBox.Show("删除成功哦", "提示");
                    GetNewUserInfoFromNet();
                }
                else
                {
                    MessageBox.Show("删除失败:" + res.Error, "警告");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("未知异常", "警告");
            }

            //var user = infos[list_user.SelectedIndex];
            //tuname.Text = user.UserName;
            //tuid.Text = user.ID + "";
            //tpass.Text = "****";
            //tphone.Text = user.Phone;
        }

        private void bok_Click(object sender, EventArgs e)
        {
            var sel = list_user.SelectedIndex;
            if (userinfos != null && sel >= 0 && sel < userinfos.Count)
            {
                var user = userinfos[sel];
                List<uint> add = new List<uint>();
                List<uint> del = new List<uint>();
                for (int i = 0; i < deviceInfos.Count; i++)
                {
                    if (list_device.GetItemChecked(i))
                    {
                        if (!deviceIds.Item2.Contains(deviceInfos[i].ID))
                        {
                            add.Add(deviceInfos[i].ID);
                        }
                    }
                    else
                    {
                        if (deviceIds.Item2.Contains(deviceInfos[i].ID))
                        {
                            del.Add(deviceInfos[i].ID);
                        }
                    }
                }

                try
                {
                    XNYResponseBase res = null, res2 = null;
                    if (add.Count > 0)
                        res = Global.client.Exec(new AddUserDeviceRequest(add, user.ID));
                    if (del.Count > 0)
                        res2 = Global.client.Exec(new DelUserDeviceRequest(del, user.ID));
                    if (res != null && res.IsError || res2 != null && res2.IsError)
                    {
                        MessageBox.Show("操作失败", "提示");
                    }
                    Task.Run(() =>
                    {
                        Global.client.ExecAsync(new GetUserAllDeviceIDRequest(user.ID));
                    });
                }
                catch (Exception)
                {
                    MessageBox.Show("操作失败", "提示");
                }


            }
        }
    }
}
