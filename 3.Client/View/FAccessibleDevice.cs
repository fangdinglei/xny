
using FdlWindows.View;
using GrpcMain.Device;
using GrpcMain.DeviceType;
using GrpcMain.UserDevice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using static GrpcMain.Device.DTODefine.Types;
using static GrpcMain.DeviceType.DTODefine.Types;
using static GrpcMain.UserDevice.DTODefine.Types;
namespace MyClient.View
{
    [AutoDetectView("全部设备", "全部设备","",true )]
    public partial class FAccessibleDevice : Form, IView
    {
        public Control View => this;
        DataTable? table;

        DeviceService.DeviceServiceClient deviceServiceClient ;
        UserDeviceService.UserDeviceServiceClient userDeviceServiceClient;
        DeviceTypeService.DeviceTypeServiceClient deviceTypeServiceClient ;
        public FAccessibleDevice(DeviceService.DeviceServiceClient deviceServiceClient, UserDeviceService.UserDeviceServiceClient userDeviceServiceClient, DeviceTypeService.DeviceTypeServiceClient deviceTypeServiceClient)
        {
            InitializeComponent();
            InitDataTable();
            this.deviceServiceClient = deviceServiceClient;
            this.userDeviceServiceClient = userDeviceServiceClient;
            this.deviceTypeServiceClient = deviceTypeServiceClient;
        }
        void InitDataTable()
        {
            table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");
            table.Columns.Add("Status");
            table.Columns.Add("Type");
            table.Columns.Add("OP1");
            table.Columns.Add("OP2");
        }

     
 
     

        private  void brefresh_Click(object sender, EventArgs e)
        {
            RefreshDatas();
            RefreshDeviceInTab();
        }
        List<User_Device_Group>? groups;
        List<User_Device>? user_Devices;
        List<TypeInfo>? typeInfos;
        List<Device>? dvinfos;
        void RefreshDatas() {
            var res1 = userDeviceServiceClient.GetGroupInfos(new Google.Protobuf.WellKnownTypes.Empty());
            groups = res1.Groups.ToList();
            var res2 = userDeviceServiceClient.GetUserDevices(new Request_GetUserDevices());
            user_Devices = res2.UserDevices.ToList();
            var res3 = deviceTypeServiceClient.GetTypeInfos(new Request_GetTypeInfos());
            typeInfos = res3.TypeInfos.ToList();
            var res4 = userDeviceServiceClient.GetDevices(new Request_GetDevices());
            dvinfos = res4.Devices.ToList();
            RefreshDeviceInTab( );
        }

        void RefreshDeviceInTab(  )
        {
            if (groups != null && dvinfos != null
               && list_Group.SelectedIndex >= 0
               && list_Group.SelectedIndex < list_Group.Items.Count
               )
            {
                var dt = table.Clone();
                foreach (var device in dvinfos)
                { 
                    //如果所选分组和当前分组不一致 则跳过
                    if (device.GroupId == 0 && list_Group.SelectedIndex != 0
                        || device.GroupId != 0 &&groups[list_Group.SelectedIndex].Id!=  device.GroupId )
                        continue;
                    DataRow dr = dt.NewRow();
                    dr["Name"] = device.Name;
                    dr["ID"] = device.Id;
                    dr["Status"] = "未知";
                    var tinfo = typeInfos.Find(it => it.Id == device.DeviceTypeId);
                    if (tinfo!=null)
                    {
                        dr["Type"] = tinfo.Name;
                    }
                    else
                    {
                        dr["Type"] = "未知类型";
                    }
                    dr["OP1"] = "测试中";
                    dr["OP2"] = "测试中";
                    dt.Rows.Add(dr);
                }
                dataGridView1.DataSource = dt;
            }
        }


        #region 必须选择 在线和离线中的一个
        private void CB_ShowOnline_CheckedChanged(object sender, EventArgs e)
        {
            if (!CB_ShowOnline.Checked && !CB_ShowNotOnline.Checked)
                CB_ShowNotOnline.Checked = true;
            brefresh_Click(null, null);
        }

        private void CB_ShowNotOnline_CheckedChanged(object sender, EventArgs e)
        {
            if (!CB_ShowOnline.Checked && !CB_ShowNotOnline.Checked)
                CB_ShowOnline.Checked = true;
            brefresh_Click(null, null);
        }
        #endregion 
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex<0||e.RowIndex == dataGridView1.Rows.Count - 1)
                return;
            string id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            //if (e.ColumnIndex == 4)
            //{
            //    FMain.Instance.SwitchTo("数据统计", false, id);
            //}
            //else if (e.ColumnIndex == 5)
            //{
            //    FMain.Instance.SwitchTo("自控配置管理", false,"device" ,id);
            //}

        }


     
        public void OnTick()
        { 
        }

        public void PrePare(params object[] par)
        {
            brefresh_Click(null,null);
        } 
        public void OnEvent(string name, params object[] pars)
        {
            if (name=="Exit")
            {
               
            }
        }

        IViewHolder ViewHolder;
        public void SetViewHolder(IViewHolder viewholder)
        {
            ViewHolder = viewholder; 
        }

        private void list_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDeviceInTab(); 
        }

        private void btn_groupmgr_Click(object sender, EventArgs e)
        {
            ViewHolder.SwitchTo("设备分组管理",false);
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            //int id = -1;
            //for (int i = 0; i < list_Group.Items.Count; i++)
            //{
            //    if (dataGridView1.GetRowDisplayRectangle(i, true).Contains(e.X, e.Y))
            //    {
            //        id = i;
            //        break;
            //    }
            //}
            //if (id == -1)
            //    return;
            //var dvid = uint.Parse((string)dataGridView1.Rows[id].Cells[1].Value);
            //DragDropEffects dde1 = this.DoDragDrop(dvid, DragDropEffects.Move);
            //if (dde1 == DragDropEffects.Move)//如果移动成功
            //{
            //    // brefresh_ClickAsync(null, null);
            //    list_Group_SelectedIndexChanged(null, null);
            //}

        }

     

        private void b_sendcmd_Click(object sender, EventArgs e)
        {
            var tb = new List<ValueTuple<uint, string>>();
            foreach (DataGridViewRow row in dataGridView1.Rows )
            {
                if ((bool)row.Cells[0].EditedFormattedValue)
                {
                    tb.Add((uint.Parse((string)row.Cells[1].EditedFormattedValue) , (string)row.Cells[2].EditedFormattedValue));
                }    
            }
            if (tb.Count == 0)
                return;
            ViewHolder.SwitchTo("向设备发送命令",false, tb);
        }

        [DllImport("user32.dll")]
        private static extern int SetCursorPos(int x, int y); 
        private void bgroupmove_MouseDown(object sender, MouseEventArgs e)
        {
            var tb = new List<long>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue)
                {
                    tb.Add(uint.Parse((string)row.Cells[1].EditedFormattedValue));
                }
            }
            if (tb.Count == 0)
                return;
            var pt = list_Group.PointToScreen(new System.Drawing.Point(0,0));
            SetCursorPos(pt.X, pt.Y);
            DragDropEffects dde1 = this.DoDragDrop(tb, DragDropEffects.Move);
            if (dde1 == DragDropEffects.Move)//如果移动成功
            {
                // brefresh_ClickAsync(null, null);
                list_Group_SelectedIndexChanged(null, null);
            }

        }
        private void list_Group_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

    
        private void list_Group_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            var pt = list_Group.PointToClient(new System.Drawing.Point(e.X, e.Y));

            int id = list_Group.IndexFromPoint(pt);
            if (id == -1)
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                if (id == list_Group.SelectedIndex)
                    return;

                try
                {
                    var tb =(List<long>) e.Data .GetData( typeof(List<long>));
                   var req= new Request_SetDeviceGroup()  ;
                    req.GroupId = groups[id].Id;
                    req.Dvids.AddRange(tb); 
                   var res= userDeviceServiceClient.SetDeviceGroup(req);
                    if (res.Success == false)
                        throw new Exception(res.Message);
                    foreach (var sucid in tb)
                    {
                        var dv =dvinfos.Find(it => it.Id == sucid);
                        if (dv != null)
                        {
                            dv.GroupId = groups[id].Id;
                        }
                    }
                  
                    e.Effect = DragDropEffects.Move;
                }
                catch (Exception ex)
                {
                    e.Effect = DragDropEffects.None;
                }

                //设置设备分组
                //移动
                // MessageBox.Show();
            }

        }

        private void bselectall_Click(object sender, EventArgs e)
        {
            //如果有没有选中的则选中,如果都选中则取消全选
            bool hasnotchecked = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue==false)
                {
                    hasnotchecked = true;
                    break;
                }
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[0].Value = hasnotchecked;
            }
        }

        private void bsetting_Click(object sender, EventArgs e)
        {
            var tb = new List<ValueTuple<uint, string>>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue)
                {
                    tb.Add((uint.Parse((string)row.Cells[1].EditedFormattedValue), (string)row.Cells[2].EditedFormattedValue));
                }
            }
            if (tb.Count == 0)
                return;
            ViewHolder.SwitchTo("自动控制配置", false, tb); 
        }
 
    }
}
