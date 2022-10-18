using FdlWindows.View;
using GrpcMain.Device;

namespace MyClient.View
{
    [AutoDetectView("FInternalMail", "站内信", "", true)]
    public partial class FInternalMail : Form, IView
    {
        List<ValueTuple<long, string>>? dvs;
        DeviceService.DeviceServiceClient deviceServiceClient;
        public FInternalMail(DeviceService.DeviceServiceClient deviceServiceClient)
        {
            InitializeComponent();
            this.deviceServiceClient = deviceServiceClient;
        }



        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {

        }

        public void PrePare(params object[] par)
        {

        }

        public void SetViewHolder(IViewHolder viewholder)
        {

        }

        public void OnTick()
        {

        }
    }
}
