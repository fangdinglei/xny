﻿using CefSharp;
using FDL.Program;
using FdlWindows.View;
using GrpcMain.Device;
using MyClient.Grpc;
using MyClient.View.AutoControl;
using MyDBContext.Main;
using System.Collections.Generic;

namespace MyClient.View.Device
{
    [AutoDetectView("FDeviceDetail", "设备详情", "", false)]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]//标记对com可见
    public partial class FDeviceDetail : Form, IView
    {
        record LastData(long thingModelId, long time, float value, long alertSeconds);


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

        DateTime lastupdate = new DateTime(1970, 1, 1);
        public void OnTick()
        {
            if ((DateTime.Now - lastupdate).TotalMinutes == 1)
            {
                lastupdate = DateTime.Now;
            }
            else
            {
                return;
            }
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            SigleExecute.ExecuteAsync(nameof(FDeviceDetail) + nameof(Rresh2Async), () => Rresh2Async());
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
        }
        public async Task Rresh2Async(bool force = false)
        {
            if (!Visible && !force)
            {
                return;
            }
            var ls = await Task.Run(() =>
            {
                var res1 = _deviceServiceClient.GetDeviceStatusAndLatestData(new Request_GetDeviceStatusAndLatestData
                {
                    Dvids = { device.Id }
                });
                if (res1.LatestData.Count == 0 || string.IsNullOrWhiteSpace(res1.LatestData[0]))
                {
                    return new List<Dictionary<string, object>>();
                }
                List<LastData> dt;
                try
                {
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LastData>>(res1.LatestData[0]);
                }
                catch (Exception)
                {
                    dt = new List<LastData>();
                }


                var ls = new List<Dictionary<string, object>>();
                foreach (var lastData in dt)
                {
                    var thingmodel = typeinfo.ThingModels.FirstOrDefault(it => it.Id == lastData.thingModelId);
                    if (thingmodel == null)
                        continue;
                    //Name,Unit,MinValue,MaxValue ,AlertLowValue,AlertHighValue,Value,ValueType
                    Dictionary<string, object> data = new Dictionary<string, object> {
                    {"Name",thingmodel.Name },{"Unit",thingmodel.Unit } ,
                     {"MinValue",thingmodel.MinValue },{"MaxValue",thingmodel.MaxValue } ,
                      {"AlertLowValue",thingmodel.AlertLowValue },{"AlertHighValue",thingmodel.AlertHighValue } ,
                       {"Value",lastData.value },{"ValueType",thingmodel.ValueType.ToString() } ,
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
            chromiumWebBrowser1.ShowLoading(async () =>
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
            }, okcall: () =>
            {
                text_id.Text = device.Id + "";
                text_name.Text = device.Name;
                if (device.HasAlertEmail)
                {
                    text_AlertEmail.Text = device.AlertEmail;
                }
                else
                {
                    text_AlertEmail.Text = "";
                }
                
                text_status.Text = device.Status switch
                { //1未激活 2离线 3在线 4};
                    1 => "未激活",
                    2 => "离线",

                    3 => "在线",
                    _ => "未知",
                };
                text_type.Text = typeinfo.Id + ":" + typeinfo.Name;
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
            if (!_localdata.TestDeviceAuthorityWithMessageBox(device.Id, UserDeviceAuthority.Write_DeletDevice, "删除"))
            {
                return;
            }
            try
            {
                var res = _deviceServiceClient.DeletDevice(new Request_DeletDevice { Dvid = device.Id, Reason = "1" });
                res.ThrowIfNotSuccess();
                MessageBox.Show("删除成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message, "错误");
            }

        }

        private void btn_commit_Click(object sender, EventArgs e)
        {
            if (!_localdata.TestDeviceAuthorityWithMessageBox(device.Id, UserDeviceAuthority.Write_BaseInfo, "修改"))
            {
                return;
            }

            try
            {
                var dv = device.Clone();
                dv.Name = text_name.Text;
                dv.AlertEmail = text_AlertEmail.Text;
                var res = _deviceServiceClient.UpdateDeviceBaseInfo(new Request_UpdateDeviceBaseInfo
                {
                    Device = dv,
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
            if (!_localdata.TestDeviceAuthorityWithMessageBox(device.Id, UserDeviceAuthority.Control_Cmd, "命令"))
            {
                return;
            }
            _viewholder.SwitchTo("FSendCMD", false, new List<(long, string)> { (device.Id, device.Name) });
        }

        private void btn_repair_Click(object sender, EventArgs e)
        {
            if (!_localdata.TestDeviceAuthorityWithMessageBox(device.Id, UserDeviceAuthority.Read_Repair, "维修信息"))
            {
                return;
            }
            _viewholder.SwitchTo("FDeviceRepair", false, device.Id);
        }

        private void btn_timesetting_Click(object sender, EventArgs e)
        {
            if (!_localdata.TestDeviceAuthorityWithMessageBox(device.Id, UserDeviceAuthority.Control_TimeSetting, "定时控制"))
            {
                return;
            }
            var tb = new List<ValueTuple<long, string>>();
            tb.Add((device.Id, device.Name));
            _viewholder.SwitchTo(nameof(FAutoControl), false, tb);
        }

        private void btnCmdHistory_Click(object sender, EventArgs e)
        {
            _viewholder.SwitchTo(nameof(FDeviceCmdHistory), false, device.Id);
        }
    }
}
