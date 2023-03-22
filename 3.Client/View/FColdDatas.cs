using FdlWindows.View;
using MyClient.Grpc;
using MyClient.View.Ext;
using MyUtility;
using System.Data;
using static GrpcMain.DeviceData.Cold.ColdDataService;

namespace MyClient.View
{
    [AutoDetectView(nameof(FColdDatas), "冷数据管理", "", true)]
    public partial class FColdDatas : Form, IView
    {
        ColdDataServiceClient _client;
        ITimeUtility _tu;
        IViewHolder _viewholder;
        public FColdDatas(ColdDataServiceClient client, ITimeUtility tu)
        {
            InitializeComponent();
            _client = client;
            int[] colddownlist = new int[] { 10, 30, 90 };
            cb_colddown.DataSource = colddownlist.Select(it => it + "天").ToList();
            cb_colddown.SelectedIndex = 1;
            int[] minlist = new int[] { 10, 100, 1000, 10000 };
            cb_mincount.DataSource = minlist.Select(it => it + "个").ToList();
            cb_mincount.SelectedIndex = 2;
            _tu = tu;
        }

        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {

        }

        public void OnTick()
        {

        }

        public void PrePare(params object[] par)
        {
            c_timesearch.Checked = false;
            c_device.Checked=false; 
            btn_search_Click(null, null);
            pcoldsetting.ShowLoading( async () =>
            {
                var r = await _client.GetSettingAsync(new GrpcMain.DeviceData.Cold.Request_GetSetting() { });
                cb_coldmanager.DataSource = r.Data.Managers.ToList()/*.Union(new List<string>() { "测试TODO"}).ToList()*/;
                cb_colddown.SelectedIndex = (cb_colddown.DataSource as List<string>).FindIndex(it => it == r.Data.ColdDownTime + "天");
                cb_mincount.SelectedIndex = (cb_mincount.DataSource as List<string>).FindIndex(it => it == r.Data.MinCount + "个");
                cb_coldmanager.SelectedIndex = (cb_coldmanager.DataSource as List<string>).FindIndex(it => it == r.Data.ManagerName);
                c_opencolddata.Checked = r.Data.Open;
                btn_savesetting.Enabled = false;
                return true;
            }, okcall: () => {
                pcoldsetting.Visible = true;
            });

        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Device");
            dt.Columns.Add("Stream");
            dt.Columns.Add("CreatTime");
            dt.Columns.Add("StartTime");
            dt.Columns.Add("EndTime");
            dt.Columns.Add("Count");
            dt.Columns.Add("Status");
            dt.Columns.Add("ManagerName");
            dt.Columns.Add("OP1");


            Action<GrpcMain.DeviceData.Cold.Request_GetInfos> loadcall = (r) => {
                dataGridView1.ShowLoading(async () => {
                    var res = await _client.GetInfosAsync(r);
                    res.Info.ToList().ForEach(it =>
                    {
                        var row = dt.NewRow();
                        row["Id"] = it.Id;
                        row["Device"] = $"{it.DeviceId}:{it.DeviceName}";
                        row["Stream"] = it.StreamName;
                        row["CreatTime"] = _tu.GetDateTime(it.CreatTime);
                        row["StartTime"] = _tu.GetDateTime(it.StartTime);
                        row["EndTime"] = _tu.GetDateTime(it.EndTime);
                        row["Count"] = it.Count;
                        //0未知 1在线 2离线 3删除中 4已删除 5 创建中
                        row["Status"] = it.Status switch {
                            0 => "未知",
                            1 => "在线",
                            2 => "离线",
                            3 => "删除中",
                            4 => "已删除",
                            5 => "创建中",
                            _=>"未知",
                        };
                        row["ManagerName"] = it.ManagerName;
                        row["OP1"] = "删除";
                        dt.Rows.Add(row);
                    });
                    dataGridView1.DataSource = dt;
                    return true;
                });
            };
            
            //选择时间搜索
            if (c_timesearch.Checked)
            {
                _viewholder.ShowDatePicker((s, e) => {
                    var req = new GrpcMain.DeviceData.Cold.Request_GetInfos()
                    {
                        Starttime =_tu.GetTicket(s),
                        Endtime=_tu.GetTicket(e),
                    };
                    if (c_device.Checked)
                    {
                        Action<List<long>> call2 = (ls) => {
                            if (ls!=null&&ls.Count==1)
                                req.DeviceId = ls[0];
                            loadcall(req);
                        };
                        _viewholder.SwitchTo(nameof(FDeviceSelector), false, call2);
                    }
                    else
                    {
                        loadcall(req);
                    }
                  
                });
                return;
            }
            //不使用时间搜索
            var req = new GrpcMain.DeviceData.Cold.Request_GetInfos();
            if (c_device.Checked)
            {
                Action<List<long>> call2 = (ls) => {
                    if (ls != null && ls.Count == 1)
                        req.DeviceId = ls[0];
                    loadcall(req);
                };
                _viewholder.SwitchTo(nameof(FDeviceSelector), false, call2);
            }
            else
            {
                loadcall(req);
            }
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Columns[e.ColumnIndex].HeaderText=="操作1" )
                {
                    var id = long.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    var res = await _client.DeletAsync(new GrpcMain.DeviceData.Cold.Request_Delet()
                    {
                        Id = id
                    });
                    res.ThrowIfNotSuccess();
                    MessageBox.Show("成功", "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }



        }

        private void c_opencolddata_CheckedChanged(object sender, EventArgs e)
        {
            btn_savesetting.Enabled = true;
        }

        private void cb_colddown_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_savesetting.Enabled = true;
        }

        private void cb_mincount_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_savesetting.Enabled = true;
        }

        private void c_opencolddata_CheckStateChanged(object sender, EventArgs e)
        {
            btn_savesetting.Enabled = true;
        }

        private void cb_colddown_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            btn_savesetting.Enabled = true;
        }

        private async void btn_savesetting_Click(object sender, EventArgs e)
        {
            try
            {
                if (cb_colddown.SelectedIndex < 0 || cb_mincount.SelectedIndex < 0 || cb_coldmanager.SelectedIndex < 0)
                {
                    throw new Exception("请选择");
                }
                var r = await _client.SetSettingAsync(new GrpcMain.DeviceData.Cold.Request_SetSetting()
                {
                    Data = new GrpcMain.DeviceData.Cold.ColdDataSetting
                    {
                        Open = c_opencolddata.Checked,
                        ColdDownTime = int.Parse(cb_colddown.SelectedItem.ToString().Replace("天", "")),
                        MinCount = int.Parse(cb_mincount.SelectedItem.ToString().Replace("个", "")),
                        ManagerName = cb_coldmanager.SelectedItem.ToString()
                    }
                });
                r.ThrowIfNotSuccess();
                MessageBox.Show("成功", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }
    }
}
