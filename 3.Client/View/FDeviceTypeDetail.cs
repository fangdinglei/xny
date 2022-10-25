using FdlWindows.View;
using GrpcMain.DeviceType;
using MyClient.Grpc;
using System.ComponentModel;

namespace MyClient.View
{
    [AutoDetectView("FDeviceTypeDetail", "", "", false)]
    public partial class FDeviceTypeDetail : Form, IView
    {

        DeviceTypeService.DeviceTypeServiceClient _typeServiceClient;
        IViewHolder _viewholder;
        bool isCreat;
        TypeInfo typeinfo;
        /// <summary>
        /// 对物模型的操作应当先操作此 再合并到typeinfo
        /// </summary>
        BindingList<ThingModel> thingModels;
        LocalDataBase _localData;
        public FDeviceTypeDetail(DeviceTypeService.DeviceTypeServiceClient typeServiceClient, LocalDataBase localData)
        {
            InitializeComponent();
            _typeServiceClient = typeServiceClient;
            _localData = localData;
        }

        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {

        }

        public async void PrePare(params object[] par)
        {
            isCreat = (bool)par[0];
            if (isCreat)
            {
                text_id.Text = "创建";
                list_thingmodels.DataSource = null;
            }
            else
            {
                var typeid = (long)par[1];
                _viewholder.ShowLoading(this,
                    async () =>
                    {
                        var req = new DTODefine.Types.Request_GetTypeInfos { };
                        req.Ids.Add(typeid);
                        var rsp = await _typeServiceClient.GetTypeInfosAsync(req);
                        if (rsp.TypeInfos.Count == 0)
                        {
                            throw new Exception();
                        }
                        typeinfo = rsp.TypeInfos[0];
                        return true;
                    },
                    okcall: () =>
                    {
                        text_id.Text = typeinfo.Id + "";
                        text_name.Text = typeinfo.Name;
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
            if (list_thingmodels.SelectedIndex < 0)
            {
                text_thingmodel_id.Text = "";
                text_thingmodel_name.Text = "";
                text_thingmodel_type.Text = "";
                text_thingmodel_unit.Text = "";
                text_thingmodel_max.Text = "";
                text_thingmodel_min.Text = "";
                text_thingmodel_remark.Text = "";
                check_thingmodel_abandonted.Checked = false;
            }
            var thingModel = thingModels[list_thingmodels.SelectedIndex];
            text_thingmodel_id.Text = thingModel.Id + "";
            text_thingmodel_name.Text = thingModel.Name;
            text_thingmodel_type.Text = thingModel.ValueType.ToString();
            text_thingmodel_unit.Text = thingModel.Unit;
            text_thingmodel_max.Text = thingModel.MaxValue + "";
            text_thingmodel_min.Text = thingModel.MinValue + "";
            text_thingmodel_remark.Text = thingModel.Remark;
            check_thingmodel_abandonted.Checked = thingModel.Abandonted;

        }
        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }

        public void OnTick()
        {

        }

        private void btn_thingmodel_update_Click(object sender, EventArgs e)
        {
            if (list_thingmodels.SelectedIndex < 0)
                return;
            var thingModel = thingModels[list_thingmodels.SelectedIndex];
            //text_thingmodel_id.Text = thingModel.Id + "";
            thingModel.Name = text_thingmodel_name.Text;
            thingModel.ValueType = 1;
            thingModel.Unit = text_thingmodel_unit.Text;
            thingModel.MaxValue = float.Parse(text_thingmodel_max.Text);
            thingModel.MinValue = float.Parse(text_thingmodel_min.Text);
            thingModel.Remark = text_thingmodel_remark.Text;
            thingModel.Abandonted = check_thingmodel_abandonted.Checked;

        }

        private void btn_thingmodel_creat_Click(object sender, EventArgs e)
        {
            var thingModel = new ThingModel();
            thingModel.Id = 0;
            thingModel.Name = text_thingmodel_name.Text;
            thingModel.ValueType = 1;
            thingModel.Unit = text_thingmodel_unit.Text;
            thingModel.MaxValue = float.Parse(text_thingmodel_max.Text);
            thingModel.MinValue = float.Parse(text_thingmodel_min.Text);
            thingModel.Remark = text_thingmodel_remark.Text;
            thingModel.Abandonted = check_thingmodel_abandonted.Checked;
            thingModels.Add(thingModel);
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {//TODO 提交变化
            try
            { 
                if (isCreat)
                {//创建
                    var res=_typeServiceClient.AddTypeInfo(new Request_AddTypeInfo()
                    {
                        Info = typeinfo
                    });
                    _localData.Save(res.Info);
                }
                else
                {
                    var res=_typeServiceClient.UpdateTypeInfo(new DTODefine.Types.Request_UpdateTypeInfo { 
                        Info = typeinfo
                    });
                    res.ThrowIfNotSuccess();
                    _localData.Save(typeinfo);
                }
                MessageBox.Show("提交成功", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message, "错误");
                return;
            }
           
        }
    }
}
