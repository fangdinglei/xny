using CefSharp.DevTools.IO;
using FdlWindows.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static GrpcMain.DeviceData.Cold.ColdDataService;
using MyClient.Grpc;

namespace MyClient.View
{
    [AutoDetectView(nameof(FColdDatas), "冷数据管理", "", true)]
    public partial class FColdDatas : Form, IView
    {
        ColdDataServiceClient _client;
        IViewHolder _viewholder;
        public FColdDatas(ColdDataServiceClient client)
        {
            InitializeComponent();
            _client = client;
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
            btn_search_Click(null, null);
        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            _viewholder.ShowLoading(this, async () =>
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
                var res = await _client.GetInfosAsync(new GrpcMain.DeviceData.Cold.Request_GetInfos());
                res.Info.ToList().ForEach(it =>
                {
                    var row = dt.NewRow();
                    row["Id"] = it.Id;
                    row["Device"] = it.DeviceId;
                    row["Stream"] = it.StreamId;
                    row["CreatTime"] = it.CreatTime;
                    row["StartTime"] = it.StreamId;
                    row["EndTime"] = it.EndTime;
                    row["Count"] = it.Count;
                    row["Status"] = it.Status;
                    row["ManagerName"] = it.ManagerName;
                    row["OP1"] = "删除";
                    dt.Rows.Add(row);
                });
                dataGridView1.DataSource = dt;
                return true;
            }, exitcall: () =>
            {
                _viewholder.Back();
            });
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var id = (long)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                var res = await _client.DeletAsync(new GrpcMain.DeviceData.Cold.Request_Delet()
                {
                    Id = id
                });
                res.ThrowIfNotSuccess();
                MessageBox.Show("成功", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"错误");
            }
          


        }
    }
}
