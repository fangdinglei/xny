using FdlWindows.View;
using GrpcMain.AccountHistory;
using MyClient.Grpc;
using MyDBContext.Main;
using MyUtility;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using static GrpcMain.Account.DTODefine.Types;

namespace MyClient.View.User
{
    public partial class FUserLoginHistory : Form
    {
        UserInfo SelectedUser => BaseManager.SelectedUser;
        bool Active => BaseManager.Active;
        bool Self => BaseManager.Self;
        Func<Collection<UserInfo>?> GetUserInfos => BaseManager.GetUserInfos;
        private BindingList<ToStringHelper<AccountHistory>> _histories;

        public BaseManager BaseManager;

        private AccountHistoryService.AccountHistoryServiceClient _AccountHistoryServiceClient;
        private ITimeUtility _timeUtility;
        private IView _father;
        public FUserLoginHistory(AccountHistoryService.AccountHistoryServiceClient AccountHistoryServiceClient, ITimeUtility timeUtility, IView father)
        {
            BaseManager = new BaseManager(this);
            InitializeComponent();

            BaseManager.SetUserInfoHandle += SetUserInfo;
            group_loginhistory_maxcount.SelectedIndex = 0;
            group_loginhistory_list.SelectedIndexChanged +=
               (box, _) => OnSelectChanged(box as ListBox, group_loginhistory_text);
            group_loginhistory_getinfos.Click +=
               (_, _) => OnSearch(group_loginhistory_maxcount, group_loginhistory_usetimes);
            group_loginhistory_delet.Click +=
               (_, _) => OnDelet(group_loginhistory_list);
            group_loginhistory_multidelet.Click +=
               (_, _) => OnMultiDelet();
            _AccountHistoryServiceClient = AccountHistoryServiceClient;
            _timeUtility = timeUtility;
            _father = father;
        }

        /// <summary>
        /// 包含开始时间 到当前选择日期的23.59.59
        /// </summary>
        /// <param name="maxcount"></param>
        /// <param name="usertimes"></param>
        void OnSearch(ComboBox maxcount, CheckBox usertimes)
        {
            var vs = group_loginhistory_datepicker1.Value;
            var ve = group_loginhistory_datepicker2.Value;
            vs = new DateTime(vs.Year, vs.Month, vs.Day);
            ve = new DateTime(ve.Year, ve.Month, ve.Day, 23, 59, 59);

            long st = _timeUtility.GetTicket(vs);
            long ed = _timeUtility.GetTicket(ve);
            group_loginhistory_list.DataSource = null;
            BaseManager._viewHolder.ShowLoading(_father, async () =>
            {
                try
                {
                    var req = new GrpcMain.AccountHistory.Request_GetHistory()
                    {
                        UserId = SelectedUser.ID,
                        Type = (int)AccountHistoryType.Login,
                    };
                    if (usertimes.Checked)
                    {//TODO 使用时间
                        req.StartTime = st;
                        req.EndTime = ed;
                    }
                    req.MaxCount = int.Parse(maxcount.Text);
                    var rsp = await _AccountHistoryServiceClient.GetHistoryAsync(req);
                    _histories = new BindingList<ToStringHelper<AccountHistory>>(
                        rsp.Historys.Select(it => new ToStringHelper<AccountHistory>(it, (it) =>
                        {
                            return _timeUtility.GetDateTime(it.Time).ToString();
                        })).ToList());
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误:" + ex.Message, "错误");
                    return false;
                }
            }, okcall: () =>
            {
                group_loginhistory_list.DataSource = _histories;
                group_loginhistory_list.DisplayMember = "Time";
            }, exitcall: () =>
            {
            });



        }
        void OnSelectChanged(ListBox box, TextBox text)
        {
            if (_histories != null && box.SelectedIndex >= 0 && box.SelectedIndex < _histories.Count)
            {
                text.Text = _timeUtility.GetDateTime(_histories[box.SelectedIndex].Value.Time)
                    .ToString();
            }
            else
            {
                text.Text = "";
            }
        }
        /// <summary>
        /// 删除单个记录
        /// </summary>
        /// <param name="list"></param>
        void OnDelet(ListBox list)
        {
            try
            {
                if (_histories != null && list.SelectedIndex >= 0 && list.SelectedIndex < _histories.Count)
                {
                    var rsp = _AccountHistoryServiceClient.DeletHistory(new GrpcMain.AccountHistory.Request_DeletHistory
                    {
                        Id = _histories[list.SelectedIndex].Value.Id
                    });
                    rsp.ThrowIfNotSuccess();
                }
                else
                {
                    MessageBox.Show("选择了错误的项目", "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message, "错误");
            }
        }
        void OnMultiDelet()
        {
            BaseManager._viewHolder.ShowDatePicker((s, e) =>
            {
                try
                {
                    var rsp = _AccountHistoryServiceClient.DeletHistorys(new GrpcMain.AccountHistory.Request_DeletHistorys()
                    {
                        StartTime = _timeUtility.GetTicket(s),
                        EndTime = _timeUtility.GetTicket(e),
                        Type = (int)AccountHistoryType.Login,
                        UserId = SelectedUser.ID,
                    });
                    rsp.ThrowIfNotSuccess();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误:" + ex.Message, "错误");
                }
            });
        }

        void SetUserInfo(BaseManager mgr)
        {
            group_loginhistory_usetimes.Checked = true;
            group_loginhistory_list.DataSource = _histories = null;
            mgr.RefreshHandle?.Invoke(mgr);
        }





    }
}
