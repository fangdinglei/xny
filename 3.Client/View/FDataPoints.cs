//#define TEST

using FdlWindows.View;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using CefSharp;
using System.Text;
using GrpcMain.DeviceData;
using GrpcMain.DeviceType;
using GrpcMain.Device;
using GrpcMain.UserDevice;
using static GrpcMain.UserDevice.DTODefine.Types;
using System.ComponentModel;
using static GrpcMain.DeviceType.DTODefine.Types;
using MyDBContext;
using MyUtility;

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
            chromiumWebBrowser1.Load(String.Format("file:///{0}/ECHART/index.html", curDir));
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
            list_Type.DataSource = types;
            list_Type.DisplayMember = "Name";

            var res2 = await _deviceTypeServiceClient.GetTypeInfosAsync(new GrpcMain.DeviceType.DTODefine.Types.Request_GetTypeInfos()
            {

            });
            types = res2.TypeInfos.ToList();



        }



        public void SetContainer(Control container)
        {

        }

        public void OnEvent(string name, params object[] pars)
        {
        }


        void ClearChart()
        {

        }
        async void RefreshChart()
        {
            if (CDevice.CheckedIndices.Count ==0)
            {
                MessageBox.Show("请选择至少一个设备", "提示");
                return;
            }
            if (CStreamName.SelectedIndex<0)
            {
                MessageBox.Show("请选择合适的数据名称","提示");
                return;
            }

            while (!chromiumWebBrowser1.IsBrowserInitialized || chromiumWebBrowser1.IsLoading)
            {
                Application.DoEvents();
            }
            var ds = new List<DeviceWithUserDeviceInfo>();
            foreach (int item in CDevice.SelectedIndices)
            {
                ds.Add(devices[item].Value);
            }

            string data = await GetDataStr(thingModels[CStreamName.SelectedIndex], ds);
            chromiumWebBrowser1.ExecuteScriptAsync("showdata_fromcs",
                Newtonsoft.Json.JsonConvert.SerializeObject(ds.Select(it=>it.Device.Name).ToList())
                , data); ;

        }

        async Task<string> GetDataStr(ThingModel model, List<DeviceWithUserDeviceInfo> devinfo)
        {
            DateTime ds = dateTimePicker1.Value, de = dateTimePicker2.Value;
            ds = new DateTime(ds.Year, ds.Month, ds.Day);
            de = new DateTime(de.Year, de.Month, de.Day).AddDays(1);

            List<Dictionary<string, object>> obj = new List<Dictionary<string, object>>();
            foreach (var dev in devinfo)
            {
                var res = await _deviceDataServiceClient.GetDataPointsAsync(new Request_GetDataPoints()
                {
                    Starttime = _timeUtility.GetTicket(ds),
                    Endtime = _timeUtility.GetTicket(de),
                    StreamId = model.Id,
                    Dvid = dev.Device.Id,
                    ColdData = false,
                });
                List<object[]> dps = res.Stream.Points.Select(it => new object[] { it.Time, it.Value }).ToList();
                obj.Add(new Dictionary<string, object>() {
                    { "Name",dev.Device.Name},
                    { "Data", dps },
                });
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        void LoadAllStreamName(TypeInfo type)
        {
            thingModels = null;
            if (type.DataPoints.TryDeserializeObject(out thingModels))
            {
                CStreamName.DataSource =
             thingModels.Select(it => it.Name).ToList();
            }
            else
            {

            }

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
            if (types == null)
            {
                (sender as ListBox).SelectedIndex = -1;
                return;
            }
            CDevice.ClearSelected();
            CStreamName.DataSource = null;

            var type=types[(sender as ListBox).SelectedIndex] ;

            var res = await _userDeviceServiceClient.GetDevicesAsync(new GrpcMain.UserDevice.DTODefine.Types.Request_GetDevices
            {TypeId= type.Id });
            var lsx = res.Info.Select(it => new ToStringHelper<DeviceWithUserDeviceInfo>
            (it, (it) => it.Device.Id + ":" + it.Device.Name)).ToList();
            devices = new BindingList<ToStringHelper<DeviceWithUserDeviceInfo>>(lsx);
            CDevice.DataSource = devices;
            LoadAllStreamName(type);
        }
        private void CDevice_SelectedValueChanged(object sender, EventArgs e)
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
                _viewHolder.ShowLoading(this, async () => { await PreGetData(); return true; },
                    okcall: () =>
                    {

                    },
                    exitcall: () =>
                    {
                        _viewHolder.Back();
                    });
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
            RefreshChart();
        }
    }
}
