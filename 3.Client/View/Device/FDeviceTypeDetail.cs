using FdlWindows.View;
using GrpcMain.Device;
using GrpcMain.DeviceType;
using MyClient.Grpc;
using MyClient.View.Device;
using MyDBContext.Main;
using System.ComponentModel;
using System.Reflection;
using TypeInfo = GrpcMain.DeviceType.TypeInfo;

namespace MyClient.View
{
    [AutoDetectView("FDeviceTypeDetail", "", "", false)]
    public partial class FDeviceTypeDetail : Form, IView
    {

        DeviceTypeService.DeviceTypeServiceClient _typeServiceClient;
        Action<TypeInfo>? OnCreatCall;
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
            text_thingmodel_type.SelectedIndex = 0;
            _typeServiceClient = typeServiceClient;
            _localData = localData;
            thingModels=new BindingList<ThingModel>();
            list_thingmodels.DataSource = thingModels;
        }













        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {

        }

        /// <summary>
        ///  [0]isCreat=true [1]action<type> 
        ///  [0]isCreat=false [1]typeid
        /// </summary>
        /// <param name="par"></param>
        /// <exception cref="Exception"></exception>
        public async void PrePare(params object[] par)
        {
            isCreat = (bool)par[0];
            if (isCreat)
            {
                OnCreatCall = (Action<TypeInfo>)par[1];
                text_id.Text = "创建";
                thingModels.Clear();
                typeinfo = new TypeInfo
                {
                     Id=0,
                     Name="创建分组",
                     Script="",
                };
            }
            else
            {
                var typeid = (long)par[1];
                this.ShowLoading(
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
                        thingModels.Clear();
                        return true;
                    },
                    okcall: () =>
                    {
                        thingModels.Clear();
                        text_id.Text = typeinfo.Id + "";
                        text_name.Text = typeinfo.Name;
                        typeinfo.ThingModels.ToList().ForEach(thingModels.Add);
                        list_thingmodels.DisplayMember = "Name";
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
                text_alterlow.Text = "";
                text_alterhigh.Text = "";
                text_thingmodel_remark.Text = "";
                check_thingmodel_abandonted.Checked = false;
                text_thingmodel_type.SelectedIndex = -1;
                return;
            }
            var thingModel = thingModels[list_thingmodels.SelectedIndex];
            text_thingmodel_id.Text = thingModel.Id + "";
            text_thingmodel_name.Text = thingModel.Name;
            text_thingmodel_type.SelectedIndex = text_thingmodel_type.Items.IndexOf(((ThingModelValueType)thingModel.ValueType).ToString());
            text_thingmodel_unit.Text = thingModel.Unit;
            text_thingmodel_max.Text = thingModel.MaxValue + "";
            text_thingmodel_min.Text = thingModel.MinValue + "";
            text_alterhigh.Text = thingModel.AlertHighValue + "";
            text_alterlow.Text = thingModel.AlertLowValue + "";
            text_alterlow.Text = thingModel.AlertLowValue + "";
            text_alterhigh.Text = thingModel.AlertHighValue + "";
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
            thingModel.ValueType = (int)Enum.Parse<ThingModelValueType>(text_thingmodel_type.Text);
            thingModel.Unit = text_thingmodel_unit.Text;
            thingModel.MaxValue = float.Parse(text_thingmodel_max.Text);
            thingModel.MinValue = float.Parse(text_thingmodel_min.Text);
            thingModel.Remark = text_thingmodel_remark.Text;
            thingModel.Abandonted = check_thingmodel_abandonted.Checked;
            thingModel.AlertLowValue = float.Parse(text_alterlow.Text);
            thingModel.AlertHighValue = float.Parse(text_alterhigh.Text);
            thingModels[list_thingmodels.SelectedIndex] = thingModel;

        }

        private void btn_thingmodel_creat_Click(object sender, EventArgs e)
        {
            if (thingModels.FirstOrDefault(it => it.Name == text_thingmodel_name.Text) != null)
            {
                MessageBox.Show("该名称的物模型已经存在", "提示");
                return;
            }
            try
            {
                var thingModel = new ThingModel();
                thingModel.Id = 0;
                thingModel.Name = text_thingmodel_name.Text;
                thingModel.ValueType = (int)Enum.Parse<ThingModelValueType>(text_thingmodel_type.Text); ;
                thingModel.Unit = text_thingmodel_unit.Text;
                thingModel.MaxValue = float.Parse(text_thingmodel_max.Text);
                thingModel.MinValue = float.Parse(text_thingmodel_min.Text);
                thingModel.Remark = text_thingmodel_remark.Text;
                thingModel.Abandonted = check_thingmodel_abandonted.Checked;
                thingModel.AlertLowValue = float.Parse(text_alterlow.Text);
                thingModel.AlertHighValue = float.Parse(text_alterhigh.Text);
                thingModels.Add(thingModel);
            }
            catch (Exception)
            {
                MessageBox.Show("请输入合法的数据","错误");
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {//TODO 提交变化
            try
            {
                typeinfo.ThingModels.Clear();
                typeinfo.ThingModels.AddRange(thingModels);
                if (isCreat)
                {//创建
                    typeinfo.Name = text_name.Text;
                    var res = _typeServiceClient.AddTypeInfo(new Request_AddTypeInfo()
                    {
                        Info = typeinfo,
                    });
                    res.Status.ThrowIfNotSuccess();
                    OnCreatCall?.Invoke(typeinfo);
                }
                else
                {
                    var res = _typeServiceClient.UpdateTypeInfo(new DTODefine.Types.Request_UpdateTypeInfo
                    {
                        Info = typeinfo
                    });
                    res.ThrowIfNotSuccess();
                }
                MessageBox.Show("提交成功", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message, "错误");
                return;
            }

        }

        private void text_thingmodel_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (text_thingmodel_type.SelectedItem != null && text_thingmodel_type.SelectedItem.ToString() == "Bool")
            {
                text_thingmodel_max.Enabled = false;
                text_thingmodel_min.Enabled = false;
                text_thingmodel_max.Text = "0";
                text_thingmodel_min.Text = "1";
            }
            else
            {
                text_thingmodel_max.Enabled = true;
                text_thingmodel_min.Enabled = true;
            }

        }

        private void btn_creatdevice_Click(object sender, EventArgs e)
        {
            if (long.TryParse(text_id.Text,out var id))
            {
                _viewholder.SwitchTo(nameof(FCreatDevice),false, id);
            }
            else
            {
                MessageBox.Show("请先创建类型","错误");
            }
           
        }
    }
}
