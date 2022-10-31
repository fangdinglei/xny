using FdlWindows.View;
using GrpcMain.Device;
using MyClient.Grpc;
using MyDBContext.Main;
using System.Data;

namespace MyClient.View
{
    [AutoDetectView("FSendCMD", "", "", false)]
    public partial class FSendCMD : Form, IView
    {
        List<ValueTuple<long, string>>? dvs;
        DeviceService.DeviceServiceClient deviceServiceClient;
        LocalDataBase _localData;
        public FSendCMD(DeviceService.DeviceServiceClient deviceServiceClient, LocalDataBase localData)
        {
            InitializeComponent();
            this.deviceServiceClient = deviceServiceClient;
            _localData = localData;
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
                var notoklist = new List<(long,string)>();
                req.Dvids.AddRange(dvs.Where(it => {
                    var a=_localData.GetUser_Device(it.Item1);
                    if (a==null|| !((UserDeviceAuthority)a.Authority).HasFlag(UserDeviceAuthority.Control_Cmd))
                    {
                        notoklist.Add(it);
                        return false;
                    }
                    return true;
                }).Select(it=>it.Item1));
                if (notoklist.Count!=0)
                {
                    var str=notoklist.Aggregate("", (old, it) => {
                        return old+","+it.Item2;
                    });
                    MessageBox.Show("不具有设备:\r\n" + str.Trim(',')+ "\r\n的命令权限,已忽略", "提示");
                }

                req.Cmd = tcmd.Text;
                var rsp = deviceServiceClient.SendCMD(req);
                rsp.ThrowIfNotSuccess();
                MessageBox.Show("发送成功", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show("失败:" + ex.Message, "错误");
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
