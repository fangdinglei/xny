using FdlWindows.View;
using GrpcMain.Device;
using GrpcMain.DeviceType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyClient.View
{
    [AutoDetectView("FDeviceTypeDetail", "", "", false)]
    public partial class FDeviceTypeDetail : Form,IView
    {

        DeviceTypeService.DeviceTypeServiceClient _typeServiceClient;
        IViewHolder _viewholder;
        bool isCreat;
        GrpcMain.DeviceType.TypeInfo typeinfo;
        /// <summary>
        /// 对物模型的操作应当先操作此 再合并到typeinfo
        /// </summary>
        BindingList<ThingModel> thingModels;

        public FDeviceTypeDetail(DeviceTypeService.DeviceTypeServiceClient typeServiceClient)
        {
            InitializeComponent();
            _typeServiceClient = typeServiceClient;
        }

        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {

        }

        public async void PrePare(params object[] par)
        {
            isCreat = (bool) par[0] ;
            if (isCreat)
            {
                text_id.Text = "创建";
                list_thingmodels.DataSource = null;
            }
            else
            {
                var typeid=(long)par[1];
                _viewholder.ShowLoading(this,
                    async () => {
                        var req = new GrpcMain.DeviceType.DTODefine.Types.Request_GetTypeInfos { };
                        req.Ids.Add(typeid);
                        var rsp = await _typeServiceClient.GetTypeInfosAsync(req);
                        if (rsp.TypeInfos.Count==0)
                        {
                            throw new Exception();
                        }
                        typeinfo= rsp.TypeInfos[0];
                        return true;
                    },
                    okcall: () =>
                    { 
                        text_id.Text =typeinfo.Id+"";
                        text_name.Text=typeinfo.Name; 
                        thingModels = new BindingList<ThingModel>(typeinfo.ThingModels);
                        list_thingmodels.DataSource = typeinfo.ThingModels;
                        list_thingmodels.DisplayMember = "Name";
                    },
                    exitcall: () =>
                    {
                        _viewholder.Back(); 
                    });
              
            }

        }
        private void list_thingmodels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_thingmodels.SelectedIndex < 0) {
                text_thingmodel_id.Text = "";
                text_thingmodel_name.Text = "";
                text_thingmodel_type.Text = "";
                text_thingmodel_unit.Text = "";
                text_thingmodel_max.Text = "";
                text_thingmodel_min.Text = "";
                text_thingmodel_remark.Text = "";
                check_thingmodel_abandonted.Checked = false;
            } 
            var thingModel= thingModels[list_thingmodels.SelectedIndex];
            text_thingmodel_id.Text = thingModel.Id+"";
            text_thingmodel_name.Text = thingModel.Name;
            text_thingmodel_type.Text = thingModel.ValueType.ToString();
            text_thingmodel_unit.Text = thingModel.Unit;
            text_thingmodel_max.Text = thingModel.MaxValue + "";
            text_thingmodel_min.Text = thingModel.MinValue+"";
            text_thingmodel_remark.Text = thingModel.Remark;
            check_thingmodel_abandonted.Checked = thingModel.Abandonted;

        }
        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder=viewholder;
        }

        public void OnTick()
        {

        }

       
    }
}
