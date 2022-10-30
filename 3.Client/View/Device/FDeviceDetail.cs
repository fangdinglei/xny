using CefSharp;
using FdlWindows.View;
using GrpcMain.Device;
using GrpcMain.DeviceType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyClient.View.Device
{

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]//标记对com可见
    public partial class FDeviceDetail : Form,IView
    {
        GrpcMain.Device.Device device;
        GrpcMain.DeviceType.TypeInfo typeinfo;

        IViewHolder _viewholder;
        LocalDataBase _localdata;
        DeviceService.DeviceServiceClient _deviceServiceClient;
        public FDeviceDetail(LocalDataBase localdata,
DeviceService.DeviceServiceClient deviceServiceClient)
        {
            InitializeComponent();
            _localdata = localdata;
            _deviceServiceClient = deviceServiceClient;
            string curDir = Directory.GetCurrentDirectory();
            chromiumWebBrowser1.Load(String.Format("file:///{0}/ECHART/devicestatus/index.html", curDir));
           
            
        }

        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {
             
        }

        public void OnTick()
        {
            //if (Visible)
            //{
            //    Rresh2();
            //}
        }
        public void Rresh2() {
            var res1=  _deviceServiceClient.GetDeviceStatusAndLatestData (new Request_GetDeviceStatusAndLatestData
            {
                Dvids = {device.Id}
            });
            var dt=Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<long, float>>(res1.LatestData[0]);
            var ls = new List<Dictionary<string, object>>();
            foreach (var kv in dt)
            {
                var thingmodel=typeinfo.ThingModels.FirstOrDefault(it => it.Id == kv.Key);
                if (thingmodel == null)
                    continue;
                //Name,Unit,MinValue,MaxValue ,AlertLowValue,AlertHighValue,Value,ValueType
                Dictionary<string, object> data = new Dictionary<string, object> { 
                    {"Name",thingmodel.Name },{"Unit",thingmodel.Unit } ,
                     {"MinValue",thingmodel.MinValue },{"MaxValue",thingmodel.MaxValue } ,
                      {"AlertLowValue",thingmodel.AlertLowValue },{"AlertHighValue",thingmodel.AlertHighValue } ,
                       {"Value",kv.Value },{"ValueType",thingmodel.ValueType.ToString() } ,
                };
                ls.Add(data);
                chromiumWebBrowser1.ExecuteScriptAsync("fromcs_ShowStatus",
                   Newtonsoft.Json.JsonConvert.SerializeObject(ls));
            }
        
        }
        public void PrePare(params object[] par)
        {
            _viewholder.ShowLoading(this, async () => {
               
                while (!chromiumWebBrowser1.IsBrowserInitialized || chromiumWebBrowser1.IsLoading)
                {
                    //Application.DoEvents();
                    await Task.Delay(10);
                }
                var dvid = (long)par[0];
                device = _localdata.GetDevice(dvid, true);
                typeinfo = _localdata.GetTypeInfo(device.DeviceTypeId, true);
                return true;
            }, okcall: () => { 
            }, 
            exitcall: () => { 
                
            });
        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }



    }
}
