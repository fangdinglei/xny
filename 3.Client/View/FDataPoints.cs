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
            var res2 = await _deviceTypeServiceClient.GetTypeInfosAsync(new GrpcMain.DeviceType.DTODefine.Types.Request_GetTypeInfos()
            {

            });
            types = res2.TypeInfos.ToList();


            var res = await _userDeviceServiceClient.GetDevicesAsync(new GrpcMain.UserDevice.DTODefine.Types.Request_GetDevices
            { });
            var lsx = res.Info.Select(it => new ToStringHelper<DeviceWithUserDeviceInfo>
            (it, (it) => it.Device.Id + ":" + it.Device.Name)).ToList();
            devices = new BindingList<ToStringHelper<DeviceWithUserDeviceInfo>>(lsx);
            CDevice.DataSource = devices;

            CDevice.SelectedIndex = -1;
            CDevice.SelectedIndex = 0;
        }



        public void SetContainer(Control container)
        {

        }

        public void OnEvent(string name, params object[] pars)
        {
        }


        async void RefreshChart()
        {
            while (!chromiumWebBrowser1.IsBrowserInitialized || chromiumWebBrowser1.IsLoading)
            {
                Application.DoEvents();
            }
            string htmlstr = await GetDataStr(thingModels[CStreamName.SelectedIndex], devices[CDevice.SelectedIndex].Value);
            chromiumWebBrowser1.ExecuteScriptAsync("showdata_fromcs", htmlstr);
        }

        async Task<string> GetDataStr(ThingModel model, DeviceWithUserDeviceInfo devinfo)
        {
            DateTime ds = dateTimePicker.Value, de = dateTimePicker.Value;
            ds = new DateTime(ds.Year, ds.Month, ds.Day);
            de = ds.AddDays(1);
            var res = await _deviceDataServiceClient.GetDataPointsAsync(new Request_GetDataPoints()
            {
                Starttime = _timeUtility.GetTicket(ds),
                Endtime = _timeUtility.GetTicket(de),
                StreamId = model.Id,
                Dvid = devinfo.Device.Id,
                ColdData = false,
            });
            var points = res.Stream.Points.ToList();
            if (points.Count == 0)
                return "[];";
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (var point in points)
            {
                sb.Append("{");
                sb.Append("\'name\':");
                sb.Append(point.Time);
                sb.Append(",");
                sb.Append("\'value\':[");
                sb.Append(point.Time);
                sb.Append(",");
                sb.Append(point.Value);
                sb.Append("]},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");

            return sb.ToString();
        }
        void LoadAllStreamName(DeviceWithUserDeviceInfo dev)
        {
            var type = types.Where(it => it.Id == dev.Device.DeviceTypeId).FirstOrDefault();
            if (type == null)
            {
                CStreamName.DataSource = null;
            }
            else
            {
                type.DataPoints.TryDeserializeObject(out thingModels);
                if (thingModels != null)
                {
                    CStreamName.DataSource = thingModels.Select(it => it.Name).ToList();
                }
            }
        }
        //todo 异常校验
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
             if (CStreamName.SelectedIndex < 0)
                 return;
             RefreshChart();
        }
        private void CDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (devices == null || CDevice.SelectedIndex < 0 || CDevice.SelectedIndex >= devices.Count)
                return;

            LoadAllStreamName(devices[CDevice.SelectedIndex].Value); 
        }
        private void CStreamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (devices == null || CDevice.SelectedIndex < 0 || CDevice.SelectedIndex >= devices.Count || CStreamName.SelectedIndex < 0)
                return;
            RefreshChart();
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
    }
}
