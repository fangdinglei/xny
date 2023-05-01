using FdlWindows.View;
using GrpcMain.Device;
using GrpcMain.History;
using GrpcMain.UserDevice;
using MyClient.Grpc;
using MyDBContext.Main;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyClient.View
{
    [AutoDetectView(nameof(FDeviceCmdHistory), "", "", false)]
    public partial class FDeviceCmdHistory : Form, IView
    {
        record DeviceCmdHistoryPar(string cmd, byte sendertype, long senderid);
        record DeviceCmdHistory(long id, long deviceId, string deviceName, string cmd, long time);

        DeviceHistoryService.DeviceHistoryServiceClient deviceHistoryServiceClient;
        UserDeviceService.UserDeviceServiceClient userDeviceServiceClient;
        public FDeviceCmdHistory(DeviceHistoryService.DeviceHistoryServiceClient deviceHistoryServiceClient, UserDeviceService.UserDeviceServiceClient userDeviceServiceClient)
        {
            InitializeComponent();
            this.deviceHistoryServiceClient = deviceHistoryServiceClient;
            this.userDeviceServiceClient = userDeviceServiceClient;
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
            if (par.Count() != 1 || par[0] is not long)
            {
                throw new Exception(nameof(FDeviceCmdHistory) + " 只能传入一个参数");
            }
            long deviceId = (long)par[0];

            this.ShowLoading(async () =>
            {
                var historyRsp = await deviceHistoryServiceClient.GetHistoryAsync(new Request_GetHistory()
                {
                    DeviceId = deviceId
                });
                historyRsp.Status.ThrowIfNotSuccess();
                var dvsRsp = await userDeviceServiceClient.GetDevicesAsync(new Request_GetDevices()
                {

                });
                var dvs = dvsRsp.Info.ToDictionary(it => it.Device.Id, it => it.Device);
                var ls = historyRsp.Historys.ToList()
                .Where(it=>it.Type==(byte)DeviceHistoryType.Command)
                .Select(it =>
                {
                    var deviceName = dvs[it.DeviceId].Name;
                    DeviceCmdHistoryPar par = JsonConvert.DeserializeObject<DeviceCmdHistoryPar>(it.Data);
                    return new DeviceCmdHistory(it.Id, it.DeviceId, deviceName, par.cmd, it.Time);
                })
                .Select(it => new ToStringHelper<DeviceCmdHistory>(it, (a) =>
                {
                    return a.deviceName + ":" + a.cmd;
                })).ToList();
                listHistory.DataSource = ls;
                return true;
            });
        }

        public void SetViewHolder(IViewHolder viewholder)
        {

        }

        private void listHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listHistory.SelectedIndex < 0 || listHistory.SelectedIndex >= listHistory.Items.Count)
                return;
            var item = (listHistory.SelectedItem as ToStringHelper<DeviceCmdHistory>).Value;

            textBox1.Text = $"设备ID:{item.deviceId} 设备名称:{item.deviceName} 命令:{item.cmd}"; 
        }
    }
}
