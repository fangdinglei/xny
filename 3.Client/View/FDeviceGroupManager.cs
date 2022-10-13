using System;
using System.Collections.Generic; 
using System.Data; 
using System.Linq; 
using System.Windows.Forms;
using XNYAPI.Model.UserDevice;
using XNYAPI.Request.UserDevice;
using XNYAPI.Response;

namespace MyClient.View
{
    public partial class FDeviceGroupManager : Form,IView
    {
        public FDeviceGroupManager()
        {
            InitializeComponent();
            RefreshGroup();
        }

        List<DeviceGroup> groupinfo;
        void RefreshGroup() {
            Global.client.ExecAsync(new GetGroupInfoRequest()); 
        }

        public Control View =>this;

        List<DeviceGroup> NewNetGroupInfo; 
        void OnNetGroupData(object data)
        {
            DataListResponse<DeviceGroup> dt = data as DataListResponse<DeviceGroup>;
            if (dt == null || dt.IsError)
                return;
            NewNetGroupInfo = dt.Data;
        }
        public void OnEvent(string name, params object[] pars)
        {
            if (name == "Exit")
            {
                Global.client.RemoveListener(typeof(DataListResponse<DeviceGroup>), OnNetGroupData); 
            }
        } 
        public void PrePare(params object[] par)
        {
            Global.client.AddListener(typeof(DataListResponse<DeviceGroup>), OnNetGroupData);
            RefreshGroup();
        }
        public void OnTick() {
            if (!Visible)
                return;
            if (NewNetGroupInfo != null)
            {
                groupinfo = NewNetGroupInfo;
                NewNetGroupInfo = null;
                var dl = groupinfo.Select(it => it.Name).ToList(); 
                list_Group.DataSource = dl; 
            }
        }
        public void SetViewHolder(IViewHolder viewholder)
        {
             
        }


        uint GetSelectedGroupID() {
            if (list_Group.SelectedIndex >= 0
                   && groupinfo != null
                   && list_Group.SelectedIndex < groupinfo.Count)
            {
                return groupinfo[list_Group.SelectedIndex].GroupID;
            }
            return uint.MaxValue;
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            try
            {
                var gid = GetSelectedGroupID();
                if (gid == uint.MaxValue)
                    return;
                var res =Global.client.Exec(new UpdateGroupInfoRequest(gid,"",true));
                if (res.IsError) {
                    MessageBox.Show(res.Error, "错误");
                    return;
                }
                 
                if (res.Suc)
                {
                    MessageBox.Show("删除成功", "错误");
                    RefreshGroup();
                }
                else
                {
                    MessageBox.Show(res.Data, "错误");
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败", "错误");
            }
        }

        private void btn_creat_Click(object sender, EventArgs e)
        {
            try
            {
                var res = Global.client.Exec(new CreatGroupRequest(tname.Text));
                if (res.IsError)
                {
                    MessageBox.Show(res.Error, "错误");
                    return;
                }

                if (res.Suc)
                {
                    MessageBox.Show("创建成功", "错误");
                    RefreshGroup();
                }
                else
                {
                    MessageBox.Show(res.Data, "错误");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("创建失败", "错误");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                var gid = GetSelectedGroupID();
                if (gid == uint.MaxValue)
                    return; 
                var res = Global.client.Exec(new UpdateGroupInfoRequest(gid, tname.Text, false));
                if (res.IsError)
                {
                    MessageBox.Show(res.Error, "错误");
                    return;
                }
                if (res.Suc)
                {
                    MessageBox.Show("修改成功", "错误");
                    RefreshGroup();
                }
                else
                {
                    MessageBox.Show(res.Data, "错误");
                }
               
               
            }
            catch (Exception)
            {
                MessageBox.Show("修改失败", "错误");
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
