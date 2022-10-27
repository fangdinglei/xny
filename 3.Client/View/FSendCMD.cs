using FdlWindows.View;
using GrpcMain.Device;
using System.Data;

namespace MyClient.View
{
    [AutoDetectView("FSendCMD", "", "", false)]
    public partial class FSendCMD : Form, IView
    {
        List<ValueTuple<long, string>>? dvs;
        DeviceService.DeviceServiceClient deviceServiceClient;
        public FSendCMD(DeviceService.DeviceServiceClient deviceServiceClient)
        {
            InitializeComponent();
            this.deviceServiceClient = deviceServiceClient;
        }


        void RefreshLab()
        {
            if (dvs == null || dvs.Count == 0)
            {
                label1.Text = "异常";
                return;
            }
            string s = "";
            for (int i = 0; i < dvs.Count; i++)
            {
                s += dvs[i].Item2 + ",";
                if (s.Length > 40)
                {
                    break;
                }
            }
            if (s.Length > 40)
                s = s.Substring(0, 40) + "...";
            else
                s = s.Substring(0, s.Length - 1);
            label1.Text = "向设备[" + s + "]发送命令";
        }
        private void btnok_Click(object sender, EventArgs e)
        {
            if (dvs == null || dvs.Count == 0)
            {
                MessageBox.Show("没有选择设备", "提示");
                return;
            }
            if (tcmd.Text == "")
            {
                MessageBox.Show("请输入命令", "提示");
                return;
            }
            try
            {
                var req = new Request_SendCMD();
                req.Dvids.AddRange(dvs.Select(it => it.Item1));
                req.Cmd = tcmd.Text;
                var rsp = deviceServiceClient.SendCMD(req);
                if (!rsp.Success)
                    throw new Exception(rsp.Message);
                MessageBox.Show("发送成功", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送设备:" + ex.Message, "提示");
            }

        }

        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {

        }

        public void PrePare(params object[] par)
        {
            if (par.Count() == 1 && par[0] != null && (par[0] is List<ValueTuple<long, string>>))
            {
                dvs = (List<ValueTuple<long, string>>)par[0];
                RefreshLab();
            }
            else
            {
                throw new Exception("该界面需要参数");
            }

        }

        public void SetViewHolder(IViewHolder viewholder)
        {

        }

        public void OnTick()
        {

        }
    }
}
