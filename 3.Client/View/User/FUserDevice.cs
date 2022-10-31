using GrpcMain.Account;
using System.Collections.ObjectModel;
using System.Data;
using static GrpcMain.Account.DTODefine.Types;

namespace MyClient.View.User
{
    public partial class FUserDevice : Form
    {
        public BaseManager BaseManager;
        UserInfo SelectedUser => BaseManager.SelectedUser;
        bool Active => BaseManager.Active;
        Func<Collection<UserInfo>?> GetUserInfos => BaseManager.GetUserInfos;



        AccountService.AccountServiceClient client;
        public FUserDevice()
        {
            BaseManager = new BaseManager(this);
            InitializeComponent();
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
                A.ForEach(it => it.Checked = true);
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
    }
}
