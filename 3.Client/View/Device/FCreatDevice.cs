using FdlWindows.View;
using GrpcMain.Device;

namespace MyClient.View.Device
{
    [AutoDetectView("FCreatDevice", "创建设备", "", false)]
    public partial class FCreatDevice : Form, IView
    {
        DeviceService.DeviceServiceClient client;
        IViewHolder _viewholder;
        public FCreatDevice(DeviceService.DeviceServiceClient client)
        {
            InitializeComponent();
            this.client = client;
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

        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                var res = client.AddDevice(new Request_AddDevice()
                {
                    Device = new GrpcMain.Device.Device
                    {
                        Name = text_Name.Text,
                    }
                });
                MessageBox.Show("添加成功", "提示");
                _viewholder.Back();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }

        }
    }
}
