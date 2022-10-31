using CefSharp;
using FDL.Program;
using FdlWindows.View;
using GrpcMain.Device;
using GrpcMain.DeviceType;
using MyClient.Grpc;
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
    [AutoDetectView("FDeviceDetail", "设备详情", "",false)]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]//标记对com可见
    public partial class FDeviceDetail : Form, IView
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

        DateTime lastupdate = new DateTime(1970,1,1);
        public void OnTick()
        {
            if ((DateTime.Now - lastupdate).TotalMinutes==1)
            {
                lastupdate = DateTime.Now;
            }
            else
            {
                return;
            }
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            SigleExecute.ExecuteAsync(nameof(FDeviceDetail)+nameof(Rresh2Async),()=> Rresh2Async());
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
        }
        public async Task Rresh2Async(bool force=false)
        {
            if (!Visible&&!force)
            {
                return;
            }
            var ls= await Task.Run(() =>
            {
                var res1 = _deviceServiceClient.GetDeviceStatusAndLatestData(new Request_GetDeviceStatusAndLatestData
                {
                    Dvids = { device.Id }
                });
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<long, float>>(res1.LatestData[0]);
                var ls = new List<Dictionary<string, object>>();
                foreach (var kv in dt)
                {
                    var thingmodel = typeinfo.ThingModels.FirstOrDefault(it => it.Id == kv.Key);
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
                }
                return ls;
            });

            chromiumWebBrowser1.ExecuteScriptAsync("fromcs_ShowStatus",
               Newtonsoft.Json.JsonConvert.SerializeObject(ls));

        }
        public void PrePare(params object[] par)
        {
            _viewholder.ShowLoading(this, async () =>
            {

                while (!chromiumWebBrowser1.IsBrowserInitialized || chromiumWebBrowser1.IsLoading)
                {
                    //Application.DoEvents();
                    await Task.Delay(10);
                }
                var dvid = (long)par[0];
                device = _localdata.GetDevice(dvid, true);
                typeinfo = _localdata.GetTypeInfo(device.DeviceTypeId, true);
                await Rresh2Async();
                return true;
            }, okcall:() =>
            {
                text_id.Text = device.Id+"";
                text_name.Text = device.Name;
                text_status.Text = device.Status switch
                { //1未激活 2离线 3在线 4};
                    1=>"未激活",
                    2=>"离线",
                  
                    3 => "在线",
                    _ => "未知",
                };
                text_type.Text = typeinfo.Id + ":" + typeinfo.Name;
            },
            exitcall: () =>
            {
                _viewholder.Back();
            });
        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }

        private void btn_typeinfo_Click(object sender, EventArgs e)
        {
            _viewholder.SwitchTo("FDeviceTypeDetail", false, false, device.DeviceTypeId);
        }

        private void btn_deletdevice_Click(object sender, EventArgs e)
        {
            try
            {
                var res =_deviceServiceClient.DeletDevice(new Request_DeletDevice { Dvid = device.Id, Reason = "1" });
                res.ThrowIfNotSuccess();
                MessageBox.Show("删除成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:"+ex.Message,"错误");
            }
           
        }

        private void btn_commit_Click(object sender, EventArgs e)
        {
            try
            {
                var dv=device.Clone();
                dv.Name=text_name.Text;
                var res = _deviceServiceClient.UpdateDeviceBaseInfo(new Request_UpdateDeviceBaseInfo { 
                    Device= dv,
                });
                MessageBox.Show("更新成功");
                device = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message, "错误");
            }
        }

        private void btn_sendcmd_Click(object sender, EventArgs e)
        {
            _viewholder.SwitchTo("FSendCMD", false,new List<(long,string)>{ (device.Id, device.Name) });
        }

        private void btn_repair_Click(object sender, EventArgs e)
        {
            _viewholder.SwitchTo("FDeviceRepair", false, device.Id);
        }
    }
}
