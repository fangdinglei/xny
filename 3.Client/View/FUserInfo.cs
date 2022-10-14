//using FdlWindows.View;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace MyClient.View
//{
//    [AutoDetectView("FUserInfo", "用户信息", "", true)]
//    public partial class FUserInfo : Form, IView
//    {
//        public FUserInfo()
//        {
//            InitializeComponent();
//        }

//        public Control View => this;

//        List<UserInfo> NewNetUserInfo;
//        List<DeviceInfo> NewNetDeviceInfo;
//        Tuple<uint, List<uint>> NewNetDeviceID;
//        List<UserInfo> userinfos;
//        List<DeviceInfo> deviceInfos;
//        Tuple<uint, List<uint>> deviceIds;
//        void GetNewUserInfoFromNet()
//        {
//            userinfos = null;
//            list_user.DataSource = null;
//            list_user.Items.Clear();
//            Task.Run(() =>
//            {
//                Global.client.ExecAsync(new GetUserInfoRequest(true));
//            });
//        }
//        void GetNewDeviceInfoFromNet()
//        {
//            deviceInfos = null;

//            Task.Run(() =>
//            {
//                Global.client.ExecAsync(new GetUserAllDeviceInfoRequest());
//            });
//        }
//        void OnUserInfos(object data)
//        {
//            DataListResponse<UserInfo> dt = data as DataListResponse<UserInfo>;
//            if (dt == null || dt.IsError || dt.Data.Count == 0)
//                return;
//            NewNetUserInfo = dt.Data;
//        }
//        void OnNetDeviceData(object data)
//        {
//            DataListResponse<DeviceInfo> dt = data as DataListResponse<DeviceInfo>;
//            if (dt == null || dt.IsError)
//                return;
//            NewNetDeviceInfo = dt.Data;
//        }
//        void OnNetDeviceID(object data)
//        {
//            DataResponse<Tuple<uint, List<uint>>> dt = data as DataResponse<Tuple<uint, List<uint>>>;
//            if (dt == null || dt.IsError)
//                return;
//            NewNetDeviceID = dt.Data;
//        }
//        public void PrePare(params object[] par)
//        {
//            Global.client.AddListener(typeof(DataListResponse<UserInfo>), OnUserInfos);
//            Global.client.AddListener(typeof(DataListResponse<DeviceInfo>), OnNetDeviceData);
//            Global.client.AddListener(typeof(DataResponse<Tuple<uint, List<uint>>>), OnNetDeviceID);
//            list_user.DataSource = null;
//            list_user.Items.Clear();
//            list_user.SelectedIndex = -1;
//            GetNewUserInfoFromNet();
//            GetNewDeviceInfoFromNet();
//        }
//        public void OnEvent(string name, params object[] pars)
//        {
//            if (name == "Exit")
//            {
//                Global.client.RemoveListener(typeof(DataListResponse<UserInfo>), OnUserInfos);
//                Global.client.RemoveListener(typeof(DataListResponse<DeviceInfo>), OnNetDeviceData);
//                Global.client.RemoveListener(typeof(DataResponse<Tuple<uint, List<uint>>>), OnNetDeviceID);
//            }
//        }

//        public void OnTick()
//        {
//            if (!Visible)
//                return;
//            if (NewNetUserInfo != null)
//            {
//                userinfos = NewNetUserInfo;
//                NewNetUserInfo = null;
//                string[] arr = new string[userinfos.Count];
//                arr[0] = userinfos[0].UserName + "(自己)";
//                for (int i = 1; i < userinfos.Count; i++)
//                    arr[i] = userinfos[i].UserName;
//                list_user.DataSource = arr;
//            }
//            if (NewNetDeviceInfo != null)
//            {
//                deviceInfos = NewNetDeviceInfo;
//                NewNetDeviceInfo = null;
//                list_device.Items.Clear();
//                list_device.Items.AddRange(deviceInfos.Select(it => it.Name).ToArray());
//            }
//            if (NewNetDeviceID != null)
//            {
//                deviceIds = NewNetDeviceID;
//                NewNetDeviceID = null;
//                var sel = list_user.SelectedIndex;
//                if (userinfos != null && sel >= 0 && sel < userinfos.Count)
//                {
//                    var user = userinfos[sel];
//                    if (deviceIds.Item1 == user.ID)
//                    {//是当前选中的用户的数据
//                        for (int i = 0; i < deviceInfos.Count; i++)
//                        {
//                            var dv = deviceInfos[i];
//                            list_device.SetItemChecked(i, deviceIds.Item2.Contains(dv.ID));
//                        }
//                    }
//                }
//            }
//        }
//        public void SetViewHolder(IViewHolder viewholder) { }

//        private void list_user_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            var sel = list_user.SelectedIndex;
//            if (userinfos != null && sel >= 0 && sel < userinfos.Count)
//            {
//                var user = userinfos[sel];
//                tuname.Text = user.UserName;
//                tuid.Text = user.ID + "";
//                tpass.Text = "****";
//                tphone.Text = user.Phone;
//                btn_passupdate.Enabled = sel == 0;
//                tpass.Enabled = sel == 0;
//                list_device.Enabled = sel != 0;
//                btn_delet.Enabled = sel != 0;
//                for (int i = 0; i < list_device.Items.Count; i++)
//                {
//                    list_device.SetItemChecked(i, false);
//                }

//                bok.Enabled = sel != 0;

//                if (sel != 0)
//                    Task.Run(() =>
//                    {
//                        Global.client.ExecAsync(new GetUserAllDeviceIDRequest(user.ID));
//                    });

//            }
//        }

//        private void btn_passupdate_Click(object sender, EventArgs e)
//        {
//            //TODO 密码格式校验
//            if (string.IsNullOrWhiteSpace(tpass.Text))
//            {
//                MessageBox.Show("密码不能为空", "警告");
//                return;
//            }
//            if (!tpass.Text.IsSqlSafeString())
//            {
//                MessageBox.Show("密码格式不对", "警告");
//                return;
//            }


//            try
//            {
//                var res = Global.client.Exec(new ChangePassWordRequest(Global.client.pass, tpass.Text));
//                if (!res.IsError)
//                {
//                    MessageBox.Show("修改成功", "提示");
//                    GetNewUserInfoFromNet();
//                }
//                else
//                {
//                    MessageBox.Show("修改失败:" + res.Error, "警告");
//                }
//            }
//            catch (Exception)
//            {
//                MessageBox.Show("未知异常", "警告");
//            }
//        }

//        private void btn_update_Click(object sender, EventArgs e)
//        {
//            MessageBox.Show("敬请期待", "提示");
//        }

//        private void btn_delet_Click(object sender, EventArgs e)
//        {
//            if (userinfos == null || userinfos.Count == 0)
//            {
//                MessageBox.Show("未知异常", "警告");
//                return;
//            }
//            if (list_user.SelectedIndex < 0 || list_user.SelectedIndex > userinfos.Count)
//            {
//                MessageBox.Show("请选择要删除的用户", "警告");
//                return;
//            }
//            if (list_user.SelectedIndex == 0)
//            {
//                MessageBox.Show("不能删除自己哦", "警告");
//                return;
//            }


//            try
//            {
//                var res = Global.client.Exec(new DeletUserRequest(userinfos[list_user.SelectedIndex].ID));
//                if (!res.IsError)
//                {
//                    MessageBox.Show("删除成功哦", "提示");
//                    GetNewUserInfoFromNet();
//                }
//                else
//                {
//                    MessageBox.Show("删除失败:" + res.Error, "警告");
//                }
//            }
//            catch (Exception)
//            {
//                MessageBox.Show("未知异常", "警告");
//            }

//            //var user = infos[list_user.SelectedIndex];
//            //tuname.Text = user.UserName;
//            //tuid.Text = user.ID + "";
//            //tpass.Text = "****";
//            //tphone.Text = user.Phone;
//        }

//        private void bok_Click(object sender, EventArgs e)
//        {
//            var sel = list_user.SelectedIndex;
//            if (userinfos != null && sel >= 0 && sel < userinfos.Count)
//            {
//                var user = userinfos[sel];
//                List<uint> add = new List<uint>();
//                List<uint> del = new List<uint>();
//                for (int i = 0; i < deviceInfos.Count; i++)
//                {
//                    if (list_device.GetItemChecked(i))
//                    {
//                        if (!deviceIds.Item2.Contains(deviceInfos[i].ID))
//                        {
//                            add.Add(deviceInfos[i].ID);
//                        }
//                    }
//                    else
//                    {
//                        if (deviceIds.Item2.Contains(deviceInfos[i].ID))
//                        {
//                            del.Add(deviceInfos[i].ID);
//                        }
//                    }
//                }

//                try
//                {
//                    XNYResponseBase res = null, res2 = null;
//                    if (add.Count > 0)
//                        res = Global.client.Exec(new AddUserDeviceRequest(add, user.ID));
//                    if (del.Count > 0)
//                        res2 = Global.client.Exec(new DelUserDeviceRequest(del, user.ID));
//                    if (res != null && res.IsError || res2 != null && res2.IsError)
//                    {
//                        MessageBox.Show("操作失败", "提示");
//                    }
//                    Task.Run(() =>
//                    {
//                        Global.client.ExecAsync(new GetUserAllDeviceIDRequest(user.ID));
//                    });
//                }
//                catch (Exception)
//                {
//                    MessageBox.Show("操作失败", "提示");
//                }


//            }
//        }
//    }
//}
