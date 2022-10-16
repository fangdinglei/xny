using FdlWindows.View;
using GrpcMain.UserDevice;
using MyClient.Grpc;
using System;
using System.Collections.Generic; 
using System.Data; 
using System.Linq; 
using System.Windows.Forms;
using static GrpcMain.UserDevice.DTODefine.Types;
namespace MyClient.View
{
    [AutoDetectView("FDeviceGroupManager", "设备分组管理","",true)]
    public partial class FDeviceGroupManager : Form,IView
    {
        UserDeviceService.UserDeviceServiceClient userDeviceServiceClient;
        public FDeviceGroupManager(UserDeviceService.UserDeviceServiceClient userDeviceServiceClient)
        {
            InitializeComponent();
            this.userDeviceServiceClient = userDeviceServiceClient;
        }

        List<User_Device_Group> groupinfo;
        void RefreshGroup() {
            var rsp = userDeviceServiceClient.GetGroupInfos(new Google.Protobuf.WellKnownTypes.Empty());
            groupinfo = rsp.Groups.ToList();
            var dl = groupinfo.Select(it => it.Name).ToList();
            list_Group.DataSource = dl;
        }

        public Control View =>this; 
        public void OnEvent(string name, params object[] pars)
        {
            if (name == "Exit")
            {
                 }
        } 
        public void PrePare(params object[] par)
        {
            RefreshGroup();
        }
        public void OnTick() {
            if (!Visible)
                return; 
        }
        public void SetViewHolder(IViewHolder viewholder)
        {
             
        }


       long GetSelectedGroupID() {
            if (list_Group.SelectedIndex >= 0
                   && groupinfo != null
                   && list_Group.SelectedIndex < groupinfo.Count)
            {
                return groupinfo[list_Group.SelectedIndex].Id;
            }
            return long.MaxValue;
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            try
            {
                var gid = GetSelectedGroupID();
                if (gid == long.MaxValue)
                    return;
                var req = new Request_UpdateGroupInfos();
                req.Groups.Add(new User_Device_Group() { 
                    Delet=true,
                    Id=gid,
                });
                var res =userDeviceServiceClient.UpdateGroupInfos(req) ;
                res.ThrowIfNotSuccess();

                MessageBox.Show("删除成功", "提示");
                RefreshGroup(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败:"+ex.Message, "错误");
            }
        }

        private void btn_creat_Click(object sender, EventArgs e)
        {
            try
            {
                var req = new Request_NewGroup();
                tname.Text = tname.Text.Trim();
                req.Name = tname.Text;
                if (string.IsNullOrEmpty(tname.Text) )
                {
                    tname.Focus();
                    MessageBox.Show("名称不能为空"); 
                    return;
                }
                if (groupinfo != null&&groupinfo.Find(it=>it.Name==tname.Text)!=null)
                {
                    tname.Focus();
                    MessageBox.Show("名称不能重复");
                    return;
                }
                var res = userDeviceServiceClient.NewGroup(req);
                res.ThrowIfNotSuccess(); 
                MessageBox.Show("创建成功", "错误");
                RefreshGroup();
                int idx = 0;
                if ((idx = (list_Group.DataSource as List<string>).FindIndex(it => it == tname.Text)) >0)
                { 
                    list_Group.SelectedIndex = idx;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建失败:" + ex.Message, "错误");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                var gid = GetSelectedGroupID();
                if (gid == long.MaxValue)
                    return;
                tname.Text = tname.Text.Trim(); 
                if (string.IsNullOrEmpty(tname.Text))
                {
                    tname.Focus();
                    MessageBox.Show("名称不能为空");
                    return;
                }
                if (groupinfo != null && groupinfo.Find(it => it.Name == tname.Text) != null)
                {
                    tname.Focus();
                    MessageBox.Show("名称不能重复");
                    return;
                }
                var req = new Request_UpdateGroupInfos();
                req.Groups.Add(new User_Device_Group()
                { 
                    Id = gid,
                    Name = tname.Text,
                });
                var res = userDeviceServiceClient.UpdateGroupInfos(req);
                res.ThrowIfNotSuccess();

                MessageBox.Show("修改成功", "提示");
                RefreshGroup();
                int idx = 0;
                if ((idx = (list_Group.DataSource as List<string>).FindIndex(it => it == tname.Text)) > 0)
                {
                    list_Group.SelectedIndex = idx;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改失败:" + ex.Message, "错误");
            }
        }

        private void list_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GetSelectedGroupID()==0|| GetSelectedGroupID()==uint.MaxValue)
            {
               /* btn_creat.Enabled = */btn_del.Enabled = btn_update.Enabled = false;
            }
            else
            {
              /*  btn_creat.Enabled = */btn_del.Enabled = btn_update.Enabled = true;
            }
        }
    }
}
