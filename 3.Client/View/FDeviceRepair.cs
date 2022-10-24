using FdlWindows.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GrpcMain.Device.DTODefine.Types;
using static GrpcMain.UserDevice.DTODefine.Types;

namespace MyClient.View
{
    [AutoDetectView("FDeviceRepair", "设备维修", "", true)]
    public partial class FDeviceRepair : Form, IView
    {
        bool _isSingleMode;
        /// <summary>
        /// 是否是单个设备模式 此模式只获取单个设备维修信息
        /// </summary>
        bool IsSingleMode {
            set {
                //list_infos.Visible = !value;
                //btn_search.Visible = !value;
                _device = null;
            }
            get { 
                return _isSingleMode;
            }
        }

        DeviceWithUserDeviceInfo _device;
        //BindingList<ToStringHelper<Device>> devices;


        public FDeviceRepair()
        {
            InitializeComponent();
        }

        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {

        }

        public void PrePare(params object[] par)
        {
            if (par.Count()==0)
            {//查询模式
                IsSingleMode = false;
                text_dvname.Text = "";
            }
            else if(par.Count() == 1)
            {//新增模式
                IsSingleMode = true;
                _device = (DeviceWithUserDeviceInfo)par[0];
                text_dvname.Text = _device.Device.Id+":"+ _device.Device.Name;

            }
        }

        public void SetViewHolder(IViewHolder viewholder)
        {

        }

        public void OnTick()
        {

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {

        }
    }
} 