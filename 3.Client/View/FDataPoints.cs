//#define TEST

using CefSharp;
using FdlWindows.View;
using GrpcMain.Device;
using GrpcMain.DeviceData;
using GrpcMain.DeviceType;
using GrpcMain.UserDevice;
using MyDBContext.Main;
using MyUtility;
using System.ComponentModel;
using static GrpcMain.DeviceType.DTODefine.Types;

namespace MyClient.View
{
    [AutoDetectView("FDataPoints", "数据", "", true)]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]//标记对com可见
    public partial class FDataPoints : Form, IView
    {

        public Control View => this;
        IViewHolder _viewHolder;
        DeviceDataService.DeviceDataServiceClient _deviceDataServiceClient;
        DeviceTypeService.DeviceTypeServiceClient _deviceTypeServiceClient;
        DeviceService.DeviceServiceClient _deviceServiceClient;
        UserDeviceService.UserDeviceServiceClient _userDeviceServiceClient;
        ITimeUtility _timeUtility;
        public FDataPoints(DeviceDataService.DeviceDataServiceClient deviceDataServiceClient, DeviceTypeService.DeviceTypeServiceClient deviceTypeServiceClient, DeviceService.DeviceServiceClient deviceServiceClient, UserDeviceService.UserDeviceServiceClient userDeviceServiceClient, ITimeUtility timeUtility)
        {
            InitializeComponent();
            string curDir = Directory.GetCurrentDirectory();
            chromiumWebBrowser1.Load(String.Format("file:///{0}/ECHART/devicedata/index.html", curDir));
            _deviceDataServiceClient = deviceDataServiceClient;
            _deviceTypeServiceClient = deviceTypeServiceClient;
            _deviceServiceClient = deviceServiceClient;
            _userDeviceServiceClient = userDeviceServiceClient;
            _timeUtility = timeUtility;
        }

        BindingList<ToStringHelper<DeviceWithUserDeviceInfo>> devices;
        List<TypeInfo> types;
        List<ThingModel> thingModels;
        async Task PreGetData()
        {
            var res3 = await _deviceTypeServiceClient.GetTypeInfosAsync(new Request_GetTypeInfos { });
            types = res3.TypeInfos.ToList();
            list_Type.DisplayMember = "Name";
            list_Type.DataSource = types;


            //var res2 = await _deviceTypeServiceClient.GetTypeInfosAsync(new GrpcMain.DeviceType.DTODefine.Types.Request_GetTypeInfos()
            //{

            //});
            //types = res2.TypeInfos.ToList();



        }



        public void SetContainer(Control container)
        {

        }

        public void OnEvent(string name, params object[] pars)
        {
        }


        void ClearChart()
        {
            chromiumWebBrowser1.ExecuteScriptAsync("clear_fromcs()");

        }
        async void RefreshChart_1()
        {
            if (CDevice.CheckedIndices.Count == 0)
            {
                MessageBox.Show("请选择至少一个设备", "提示");
                return;
            }
            if (CStreamName.SelectedIndex < 0)
            {
                MessageBox.Show("请选择合适的数据名称", "提示");
                return;
            }

            while (!chromiumWebBrowser1.IsBrowserInitialized || chromiumWebBrowser1.IsLoading)
            {
                Application.DoEvents();
            }
            var ds = new List<DeviceWithUserDeviceInfo>();
            foreach (int item in CDevice.CheckedIndices)
            {
                if (!((UserDeviceAuthority)devices[item].Value.UserDevice.Authority).HasFlag(UserDeviceAuthority.Read_Data))
                {
                    MessageBox.Show("没有该设备数据读取权限" + devices[item].Value.Device.Name, "提示");
                    return;
                }
                ds.Add(devices[item].Value);
            }
            try
            {
                (string data, var s, var e) = await GetDataStr_1(thingModels[CStreamName.SelectedIndex], ds);
                var ts = dateTimePicker1.Value;
                ts = new DateTime(ts.Year, ts.Month, ts.Day);
                var te = dateTimePicker2.Value;
                te = (new DateTime(te.Year, te.Month, te.Day)).AddDays(1);
                chromiumWebBrowser1.ExecuteScriptAsync("showdata_fromcs",
                    Newtonsoft.Json.JsonConvert.SerializeObject(ds.Select(it => it.Device.Name).ToList())
                    , data, s, e);
            }
            catch (Exception ex)
            {
                ClearChart();
                MessageBox.Show("错误:" + ex.Message, "错误");
            }


        }

        async Task<(string, long, long)> GetDataStr_1(ThingModel model, List<DeviceWithUserDeviceInfo> devinfo)
        {
            DateTime ds = dateTimePicker1.Value, de = dateTimePicker2.Value;
            ds = new DateTime(ds.Year, ds.Month, ds.Day);
            de = new DateTime(de.Year, de.Month, de.Day).AddDays(1);
            long timeStampStart = long.MaxValue;
            long timeStampEnd = int.MinValue;
            List<Dictionary<string, object>> obj = new List<Dictionary<string, object>>();
            foreach (var dev in devinfo)
            {
                var res = await _deviceDataServiceClient.GetDataPointsAsync(new Request_GetDataPoints()
                {
                    Starttime = _timeUtility.GetTicket(ds),
                    Endtime = _timeUtility.GetTicket(de),
                    StreamId = model.Id,
                    Dvid = dev.Device.Id,
                    ColdData = cbUseCold.Checked,
                });
                List<object[]> dps = res.Stream.Points.Select(it => new object[] { it.Time * 1000, it.Value }).ToList();
                dps.ForEach(it =>
                {
                    timeStampStart = Math.Min(timeStampStart, (long)(it[0]));
                    timeStampEnd = Math.Max(timeStampEnd, (long)(it[0]));
                });
                obj.Add(new Dictionary<string, object>() {
                    { "Name",dev.Device.Name},
                    { "Data", dps },
                });
            }
            return (Newtonsoft.Json.JsonConvert.SerializeObject(obj), timeStampStart, timeStampEnd);
        }

        async void RefreshChart_2()
        {
            if (CDevice.CheckedIndices.Count != 1)
            {
                MessageBox.Show("请选择一个设备", "提示");
                return;
            }
            if (CStreamName.SelectedIndex < 0)
            {
                MessageBox.Show("请选择合适的数据名称", "提示");
                return;
            }

            while (!chromiumWebBrowser1.IsBrowserInitialized || chromiumWebBrowser1.IsLoading)
            {
                Application.DoEvents();
            }
            var ds = devices[CDevice.CheckedIndices[0]].Value;
            if (!((UserDeviceAuthority)ds.UserDevice.Authority).HasFlag(UserDeviceAuthority.Read_Data))
            {
                MessageBox.Show("没有该设备数据读取权限" + ds.Device.Name, "提示");
                return;
            }
            try
            {
                var data = await GetDataStr_2(thingModels[CStreamName.SelectedIndex], ds);
                chromiumWebBrowser1.ExecuteScriptAsync("showdata_onedeviceofmanydays_fromcs",
                    data.Item1, data.Item2, data.Item3); ;
            }
            catch (Exception ex)
            {
                ClearChart();
                MessageBox.Show("错误:" + ex.Message, "错误");
            }


        }
        /// <summary>
        /// 系列
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dev"></param>
        /// <returns>系列,数据,时间偏移</returns>
        async Task<(string, string, string)> GetDataStr_2(ThingModel model, DeviceWithUserDeviceInfo dev)
        {
            DateTime ds = dateTimePicker1.Value, de = dateTimePicker2.Value;
            ds = new DateTime(ds.Year, ds.Month, ds.Day);
            de = new DateTime(de.Year, de.Month, de.Day).AddDays(1);
            var timeoffset = (long)((_timeUtility.GetDateTime(0) - new DateTime(1970, 1, 1)).TotalSeconds);
            List<Dictionary<string, object>> obj = new List<Dictionary<string, object>>();
            List<string> legend = new List<string>();
            var res = await _deviceDataServiceClient.GetDataPointsAsync(new Request_GetDataPoints()
            {
                Starttime = _timeUtility.GetTicket(ds),
                Endtime = _timeUtility.GetTicket(de),
                StreamId = model.Id,
                Dvid = dev.Device.Id,
                ColdData = cbUseCold.Checked,
            });
            Dictionary<long, List<DataPoinet>> dps = new();
            foreach (var item in res.Stream.Points)
            {
                var day = (item.Time + timeoffset) / (24 * 60 * 60);
                if (dps.ContainsKey(day))
                {
                    dps[day].Add(item);
                }
                else
                {
                    dps[day] = new List<DataPoinet>() { item };
                }
            }
            foreach (var item in dps.Values)
            {
                var dt = _timeUtility.GetDateTime(item[0].Time).ToString("yy-MM-dd");
                legend.Add(dt);

                obj.Add(new Dictionary<string, object>() {
                    { "Name",dt+""},
                    { "Data",item.Select(it=>{
                        var t=(it.Time )%(24 * 60 * 60);
                        //这里对utc进行映射 映射到 -8+16小时 而取模得到0,24 将16,24的部分重映射到-8,-0
                        t=t>(-timeoffset+24*60*60)?t-24*60*60:t;
                       return new object[]{
                            t*1000,
                            it.Value,
                            it.Time*1000
                       };
                    }) },
                });
            }

            return (Newtonsoft.Json.JsonConvert.SerializeObject(legend), Newtonsoft.Json.JsonConvert.SerializeObject(obj), (timeoffset * 1000).ToString());
        }



        void LoadAllStreamName(TypeInfo type)
        {
            thingModels = type.ThingModels.ToList();
            CStreamName.DataSource =
            thingModels.Select(it => it.Name).ToList();
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ClearChart();
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            ClearChart();
        }

        private async void list_Type_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            if (types == null && (sender as ListBox).SelectedIndex >= 0)
            {
                (sender as ListBox).SelectedIndex = -1;
                return;
            }
            CDevice.ClearSelected();
            CStreamName.DataSource = null;

            var type = types[(sender as ListBox).SelectedIndex];
            LoadAllStreamName(type);
            CDevice.ShowLoading(async () =>
            {
                var res = await _userDeviceServiceClient.GetDevicesAsync(new Request_GetDevices
                { TypeId = type.Id });
                var lsx = res.Info.Where(it => ((UserDeviceAuthority)it.UserDevice.Authority).HasFlag(UserDeviceAuthority.Read_Data))
                    .Select(it => new ToStringHelper<DeviceWithUserDeviceInfo>
                (it, (it) => it.Device.Id + ":" + it.Device.Name)).ToList();
                devices = new BindingList<ToStringHelper<DeviceWithUserDeviceInfo>>(lsx);
                CDevice.DataSource = devices;
                return true;
            });

        }
        private void CDevice_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (devices == null || CDevice.SelectedIndices.Count == 0 || CDevice.SelectedIndex >= devices.Count)
                return;
            ClearChart();
        }
        private void CStreamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (devices == null || CDevice.SelectedIndex < 0 || CDevice.SelectedIndex >= devices.Count || CStreamName.SelectedIndex < 0)
                return;
            ClearChart();
        }



        public void PrePare(params object[] par)
        {
            if (par.Count() == 1)
            {
                string id = par[0] as string;
                for (int i = 0; i < CDevice.Items.Count; i++)
                {
                    var item = CDevice.Items[i];
                    if (item.ToString() == id)
                    {
                        CDevice.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                list_Type.ShowLoading(async () => { await PreGetData(); return true; });
            }

        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewHolder = viewholder;
        }

        public void OnTick()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshChart_1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshChart_2();
        }
    }
}
