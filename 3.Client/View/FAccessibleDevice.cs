
using FdlWindows.View;
using GrpcMain.Device;
using GrpcMain.DeviceType;
using GrpcMain.UserDevice;
using System.Data;
using System.Runtime.InteropServices;
using static GrpcMain.DeviceType.DTODefine.Types; 

namespace MyClient.View
{
    [AutoDetectView("全部设备", "全部设备", "", true)]
    public partial class FAccessibleDevice : Form, IView
    {
        public Control View => this;
        DataTable? table;

        DeviceService.DeviceServiceClient deviceServiceClient;
        UserDeviceService.UserDeviceServiceClient userDeviceServiceClient;
        DeviceTypeService.DeviceTypeServiceClient deviceTypeServiceClient;
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

        List<User_Device_Group>? groups;
        //List<User_Device>? user_Devices;
        List<TypeInfo>? typeInfos;
        List<DeviceWithUserDeviceInfo>? dvinfos;
        List<DeviceWithUserDeviceInfo> currentshow = new List<DeviceWithUserDeviceInfo>();
        private void brefresh_Click(object sender, EventArgs e)
        {
            if (!brefresh.Enabled)
                return;
            brefresh.Enabled = false;
            ViewHolder.ShowLoading(this,
                 async () =>
                 {
                     //await Task.Delay(10000);
                     //throw new Exception();
                     if (await RefreshDatas())
                     {
                         return true;
                     }
                     else
                     {
                         return false;
                     }
                 },
                 okcall: () =>
                 {
                     RefreshGroupList();
                     RefreshDeviceInTab();
                     brefresh.Enabled = true;
                 },
                 exitcall: () =>
                 {
                     brefresh.Enabled = true;
                 }
            );
        }

        /// <summary>
        /// 刷新设备分组列表
        /// </summary>
        void RefreshGroupList()
        {
            list_Group.DataSource = null;
            var ginfo = new List<User_Device_Group>();
            ginfo.Add(new User_Device_Group()
            {
                Id = 0,
                Name = "全部",
            });
            ginfo.Add(new User_Device_Group()
            {
                Id = 0,
                Name = "默认分组",
            });
            ginfo.AddRange(groups);
            list_Group.DataSource = ginfo.Select(it => it.Name).ToList();
        }
        async Task<bool> RefreshDatas()
        {
            try
            {
                var res1 = await userDeviceServiceClient.GetGroupInfosAsync(new Google.Protobuf.WellKnownTypes.Empty());
                groups = res1.Groups.ToList();
                //var res2 = await userDeviceServiceClient.GetUserDevicesAsync(new Request_GetUserDevices());
                //user_Devices = res2.UserDevices.ToList();
                var res3 = await deviceTypeServiceClient.GetTypeInfosAsync(new Request_GetTypeInfos());
                typeInfos = res3.TypeInfos.ToList();
                var res4 = await userDeviceServiceClient.GetDevicesAsync(new Request_GetDevices());
                dvinfos = res4.Info.ToList();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        void RefreshDeviceInTab()
        {
            var idx =
                list_Group.SelectedIndex == -1 ? 0 : list_Group.SelectedIndex;
            if (groups != null && dvinfos != null
               && idx >= 0
               && idx < list_Group.Items.Count
               )
            {
                currentshow.Clear();
                foreach (var device in dvinfos)
                {
                    //选择全部
                    var c0 = idx == 0;
                    //默认分组且符合
                    var c1 = c0 || idx == 1 && device.UserDevice.UserDeviceGroup == 0;
                    //或 不是默认分组且符合
                    var c2 = c1 || idx > 1 && groups[idx - 2].Id == device.UserDevice.UserDeviceGroup;
                    //或 是默认分组和设备分组信息不存在 防止出现不一致导致无法访问设备
                    var c3 = c2 ||
                        idx == 0 && groups.Find(it => it.Id == device.UserDevice.UserDeviceGroup) == null;
                    if (!c3)
                        continue;
                    currentshow.Add(device);
                }
                var dt = table.Clone();
                foreach (var device in currentshow)
                {
                    DataRow dr = dt.NewRow();
                    dr["Name"] = device.Device.Name;
                    dr["ID"] = device.Device.Id;
                    dr["Status"] = device.Device.Status switch
                    {
                        1 => "未激活",
                        2 => "离线",
                        3 => "在线",
                        _ => "未知"
                    };
                    var tinfo = typeInfos.Find(it => it.Id == device.Device.DeviceTypeId);
                    if (tinfo != null)
                    {
                        dr["Type"] = tinfo.Name;
                    }
                    else
                    {
                        dr["Type"] = "未知类型";
                    }
                    dr["OP1"] = "测试中";
                    dr["OP2"] = "更多功能";
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
            if (e.RowIndex < 0 || e.RowIndex == dataGridView1.Rows.Count)
                return;
            var dev = currentshow[e.RowIndex];
            if (e.ColumnIndex == 5)
            {

            }
            else if (e.ColumnIndex==6)
            {
                ViewHolder.SwitchTo("FDeviceOtherFeatures", false, dev);
            }

        }



        public void OnTick()
        {
        }

        public void PrePare(params object[] par)
        {
            brefresh_Click(null, null);
        }
        public void OnEvent(string name, params object[] pars)
        {
            if (name == "Exit")
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
            ViewHolder.SwitchTo("FDeviceGroupManager", false);
        }



        private void b_sendcmd_Click(object sender, EventArgs e)
        {
            var tb = new List<ValueTuple<long, string>>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue)
                {
                    tb.Add((long.Parse((string)row.Cells[1].EditedFormattedValue), (string)row.Cells[2].EditedFormattedValue));
                }
            }
            if (tb.Count == 0)
                return;
            ViewHolder.SwitchTo("FSendCMD", false, tb);
        }

        #region 分组移动
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
            var pt = list_Group.PointToScreen(new System.Drawing.Point(0, 0));
            SetCursorPos(pt.X, pt.Y);
            DragDropEffects dde1 = this.DoDragDrop(tb, DragDropEffects.Move);
            if (dde1 == DragDropEffects.Move)//如果移动成功
            {
                RefreshDeviceInTab();
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
            if (id == -1 || id == 0)
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                if (id == list_Group.SelectedIndex)
                    return;

                try
                {
                    var tb = (List<long>)e.Data.GetData(typeof(List<long>));
                    var req = new Request_SetDeviceGroup();
                    if (id == 1)
                        req.GroupId = 0;
                    else
                        req.GroupId = groups[id - 2].Id;

                    req.Dvids.AddRange(tb);
                    var res = userDeviceServiceClient.SetDeviceGroup(req);
                    if (res.Success == false)
                        throw new Exception(res.Message);
                    foreach (var sucid in tb)
                    {
                        var dv = dvinfos.Find(it => it.Device.Id == sucid);
                        if (dv != null)
                        {
                            dv.UserDevice.UserDeviceGroup = id switch
                            {
                                1 => 0,
                                _ => groups[id - 2].Id
                            };
                        }
                    }

                    e.Effect = DragDropEffects.Move;
                    RefreshDeviceInTab();
                }
                catch (Exception ex)
                {
                    e.Effect = DragDropEffects.None;
                    MessageBox.Show("移动分组出错" + ex.Message, "错误");
                }
            }

        }
        #endregion  

        private void bselectall_Click(object sender, EventArgs e)
        {
            //如果有没有选中的则选中,如果都选中则取消全选
            bool hasnotchecked = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue == false)
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
