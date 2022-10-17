using FdlWindows.View;
using GrpcMain.Device;
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
