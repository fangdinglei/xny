using FdlWindows.View;
using GrpcMain.Device;

namespace MyClient.View.Device
{
    [AutoDetectView("FCreatDevice", "创建设备", "", false)]
    public partial class FCreatDevice : Form, IView
    {
        DeviceService.DeviceServiceClient client;
        IViewHolder _viewholder;
        long typeId;
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

        /// <summary>
        /// par[0] typeid
        /// </summary>
        /// <param name="par"></param>
        public void PrePare(params object[] par)
        {
            if (par.Length!=1||par[0] is not long)
            {
                throw new Exception("创建设备必须传入一个参数");
            }
            typeId = (long)par[0];
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
                        DeviceTypeId=typeId,
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
