using GrpcMain.Account;
using MyClient.Grpc;
using System.Collections.ObjectModel;
using static GrpcMain.Account.DTODefine.Types;

namespace MyClient.View.User
{
    public partial class FUserInfo : Form
    {
        public BaseManager BaseManager;
        UserInfo SelectedUser => BaseManager.SelectedUser;
        bool Active => BaseManager.Active;
        Func<Collection<UserInfo>?> GetUserInfos => BaseManager.GetUserInfos;

        AccountService.AccountServiceClient client;
        public FUserInfo(AccountService.AccountServiceClient client)
        {
            BaseManager = new BaseManager(this);
            InitializeComponent();
            BaseManager.RefreshHandle = Refresh;
            BaseManager.SetUserInfoHandle = SetUserInfo;
            btn_update.Click += btn_update_Click;
            this.client = client;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void btn_update_Click(object? sender, EventArgs e)
        {
            try
            {
                if (SelectedUser == null)
                {
                    MessageBox.Show("没有选中用户", "提示");
                    return;
                }
                Request_UpdateUserInfo req = new Request_UpdateUserInfo()
                {
                    UserInfo = new UserInfo()
                    {
                        ID = BaseManager.SelectedUser.ID,
                    }
                };
                UserInfo uclone = SelectedUser.Clone();
                if (tuname.Text != SelectedUser.UserName)
                {
                    req.UserInfo.UserName = tuname.Text;
                    uclone.UserName = tuname.Text;
                }
                if (tphone.Text != SelectedUser.Phone)
                {
                    req.UserInfo.Phone = tphone.Text;
                    uclone.Phone = tphone.Text;
                }
                if (temail.Text != SelectedUser.Email)
                {
                    uclone.Email = req.UserInfo.Email = temail.Text;
                }

                var rsp = await client.UpdateUserInfoAsync(req);
                rsp.ThrowIfNotSuccess();
                MessageBox.Show("修改成功", "提示");
                var idx = -1;
                foreach (var item in GetUserInfos())
                {
                    idx++;
                    if (item.ID == uclone.ID)
                    {
                        break;
                    }
                }
                GetUserInfos()[idx] = uclone;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message, "错误");
            }
        }
        void Refresh(BaseManager mgr)
        {
            if (!Active)
                return;
            if (SelectedUser != null)
            {
                tuname.Text = SelectedUser.UserName;
                tuid.Text = SelectedUser.ID + "";
                tpass.Text = "****";
                tphone.Text = SelectedUser.Phone;
                temail.Text = SelectedUser.Email;
                List<string> obj; var s___baseinfo_authoritys = "";
                SelectedUser.Authoritys.TryDeserializeObject(out obj);
                if (obj != null)
                {
                    foreach (var item in obj)
                    {
                        s___baseinfo_authoritys += item + "\r\n";
                    }
                }
                baseinfo_authoritys.Text = s___baseinfo_authoritys;
            }
        }
        void SetUserInfo(BaseManager mgr)
        {
            mgr.RefreshHandle?.Invoke(mgr);
        }

    }
}
