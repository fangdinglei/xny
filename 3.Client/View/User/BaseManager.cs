using FdlWindows.View;
using System.Collections.ObjectModel;
using static GrpcMain.Account.DTODefine.Types;

namespace MyClient.View.User
{
    public class BaseManager
    {
        protected ListBox _list_user;
        private TabControl _tabControl1;
        private int _tabindex;
        public Func<Collection<UserInfo>?> GetUserInfos;
        public IViewHolder _viewHolder;
        public Control View;
        public BaseManager(Control control)
        {
            View = control;
        }


        public bool Active
        {
            get => _tabControl1.SelectedIndex == _tabindex;
        }
        public UserInfo SelectedUser { get; private set; }
        public UserInfo FatherInfo { get; private set; }
        public bool Self => SelectedUser == FatherInfo;

        public Action<BaseManager> RefreshHandle;
        public Action<BaseManager> SetUserInfoHandle;

        private void TabControl1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            RefreshHandle?.Invoke(this);
        }
        private void _list_user_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var infos = GetUserInfos();
            if (infos != null && _list_user.SelectedIndex >= 0 && _list_user.SelectedIndex < infos.Count)
            {
                SetUserInfo(infos[_list_user.SelectedIndex], infos[0]);
            }
            else
            {
                SetUserInfo(null, null);
            }
        }


        public void SetUserInfo(UserInfo? sonInfo, UserInfo? fatherinfo)
        {
            SelectedUser = sonInfo;
            FatherInfo = fatherinfo;
            SetUserInfoHandle?.Invoke(this);
        }

        public void SetViewHolder(IViewHolder viewHolder)
        {
            _viewHolder = viewHolder;
        }
        public void Init(TabControl tabControl1, int tabindex, ListBox list_user, Func<Collection<UserInfo>?> getUserInfos)
        {
            _list_user = list_user;
            _tabControl1 = tabControl1;
            _tabindex = tabindex;
            GetUserInfos = getUserInfos;
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
            _list_user.SelectedIndexChanged += _list_user_SelectedIndexChanged;
        }


    }
}
