
using GrpcMain.Account;
using MyClient.Grpc;
using System.Collections.ObjectModel;
using static GrpcMain.Account.DTODefine.Types;

namespace MyClient.View.User
{
    public partial class FUserPriority : Form
    {
        UserInfo SelectedUser => BaseManager.SelectedUser;
        bool Active => BaseManager.Active;
        bool Self => BaseManager.Self;
        Func<Collection<UserInfo>?> GetUserInfos => BaseManager.GetUserInfos;

        public BaseManager BaseManager;
        public List<string> FatherPrioritys { get; private set; }
        public List<string> SelectedUserPriority { get; private set; }



        AccountService.AccountServiceClient client;
        public FUserPriority(AccountService.AccountServiceClient client)
        {
            BaseManager = new BaseManager(this);
            InitializeComponent();
            group_priority_list.ItemCheck += group_priority_list_ItemCheck;
            group_priority_btn_ok.Click += (a, b) => Submit();
            this.client = client;
            BaseManager.SetUserInfoHandle = SetUserInfo;
            BaseManager.RefreshHandle = Refresh;
        }






        void SetUserInfo(BaseManager mgr)
        {
            var sonInfo = mgr.SelectedUser;
            var fatherinfo = mgr.FatherInfo;
            if (sonInfo == null || fatherinfo == null)
            {
                return;
            }
            List<string> fatherlist, sonlist;
            fatherinfo.Authoritys.TryDeserializeObject(out fatherlist);
            if (fatherlist == null)
            {
                group_priority_list.DataSource = null;
                FatherPrioritys = new List<string>();
                SelectedUserPriority = new List<string>();
                return;
            }
            sonInfo.Authoritys.TryDeserializeObject(out sonlist);
            FatherPrioritys = fatherlist;
            SelectedUserPriority = sonlist ?? new List<string>();
            group_priority_list.DataSource = fatherlist;
            mgr.RefreshHandle?.Invoke(mgr);
        }

        bool lock_group_priority_list_SelectedValueChanged = false;
        private void group_priority_list_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            if (lock_group_priority_list_SelectedValueChanged)
            {
                return;
            }
            lock_group_priority_list_SelectedValueChanged = true;

            var list = group_priority_list.DataSource as List<string>;
            //原本具有的
            var has = SelectedUserPriority.Contains(FatherPrioritys[e.Index]);
            //现在的 lock_group_priority_list_SelectedValueChanged = false;
            var has2 = e.NewValue == CheckState.Checked;
            if (has && !has2)
            {
                list[e.Index] = " - " + FatherPrioritys[e.Index];
            }
            else if (!has && has2)
            {
                list[e.Index] = " + " + FatherPrioritys[e.Index];
            }
            else
            {
                list[e.Index] = FatherPrioritys[e.Index];
            }
            var selid = group_priority_list.SelectedIndex;
            var idxs = new List<int>();
            foreach (int item in group_priority_list.CheckedIndices)
            {
                idxs.Add(item);
            }
            group_priority_list.DataSource = null;
            group_priority_list.DataSource = list;
            foreach (int item in idxs)
            {
                group_priority_list.SetItemChecked(item, true);
            }
            group_priority_list.SelectedIndex = selid;
            lock_group_priority_list_SelectedValueChanged = false;

        }

        public void Refresh(BaseManager mgr)
        {
            if (!Active)
                return;
            group_priority_list.Enabled = !Self;
            if (SelectedUser == null)
            {
                group_priority_list.DataSource = null;
            }
            else
            {
                lock_group_priority_list_SelectedValueChanged = true;
                group_priority_list.DataSource = null;
                var ls = new List<string>(FatherPrioritys);
                for (int i = 0; i < FatherPrioritys.Count; i++)
                { /*ls[i] = "   " + ls[i]; */
                }
                group_priority_list.DataSource = ls;
                for (int i = 0; i < FatherPrioritys.Count; i++)
                {
                    var has = SelectedUserPriority != null && SelectedUserPriority.Contains(FatherPrioritys[i]);
                    group_priority_list.SetItemChecked(i, has);
                }

                lock_group_priority_list_SelectedValueChanged = false;
            }
        }

        /// <summary>
        /// 提交变更
        /// </summary>
        public async void Submit()
        {
            try
            {
                List<string> prioritys = new List<string>();
                for (int i = 0; i < FatherPrioritys.Count; i++)
                {
                    if (group_priority_list.GetItemChecked(i))
                    {
                        prioritys.Add(FatherPrioritys[i]);
                    }
                }
                var r = await client.UpdateUserInfoAsync(new Request_UpdateUserInfo()
                {
                    UserInfo = new UserInfo()
                    {
                        ID = SelectedUser.ID,
                        Authoritys = Newtonsoft.Json.JsonConvert.SerializeObject(prioritys)
                    }
                });
                r.ThrowIfNotSuccess();
                SelectedUser.Authoritys = Newtonsoft.Json.JsonConvert.SerializeObject(prioritys);
                MessageBox.Show("更新成功", "提示");
                BaseManager.SetUserInfo(BaseManager.SelectedUser, BaseManager.FatherInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message, "错误");
            }

        }

    }
}
