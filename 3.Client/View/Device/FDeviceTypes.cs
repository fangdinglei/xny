using FdlWindows.View;
using GrpcMain.DeviceType;
using System.ComponentModel;
using System.Data;

namespace MyClient.View.Device
{
    [AutoDetectView("FDeviceTypes", "设备类型", "", true)]
    public partial class FDeviceTypes : Form, IView
    {
        BindingList<ToStringHelper<TypeInfo>> _types = new BindingList<ToStringHelper<TypeInfo>>();
        Func<TypeInfo, string> tostringfunction = (it) => it.Name;

        DeviceTypeService.DeviceTypeServiceClient _client;
        LocalDataBase _db;
        FDeviceTypeDetail _deviceTypeDetail;
        IViewHolder _viewholder;
        public FDeviceTypes(DeviceTypeService.DeviceTypeServiceClient client, LocalDataBase db, FDeviceTypeDetail deviceTypeDetail)
        {
            InitializeComponent();
            _client = client;
            _db = db;
            _deviceTypeDetail = deviceTypeDetail;
            _deviceTypeDetail.TopLevel = false;
            _deviceTypeDetail.FormBorderStyle = FormBorderStyle.None;
            _deviceTypeDetail.Parent = this;
            _deviceTypeDetail.Visible = true;
            _deviceTypeDetail.Location = location_lab.Location;


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
            _viewholder.ShowLoading(this, async () =>
            {
                var rsp = await _client.GetTypeInfosAsync(new DTODefine.Types.Request_GetTypeInfos()
                {

                });
                _types = new BindingList<ToStringHelper<TypeInfo>>(
                    rsp.TypeInfos.Select(it => new ToStringHelper<TypeInfo>(it, tostringfunction)).ToList()
                    );
                return true;
            }, okcall: () =>
            {
                list_types.DataSource = _types;
            }, exitcall: () =>
            {
                list_types.DataSource = null;
                _viewholder.Back();
            });
        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
            _deviceTypeDetail.SetViewHolder(viewholder);
        }

        private void list_types_SelectedIndexChanged(object sender, EventArgs e)
        {
            _deviceTypeDetail.Visible = false;
            if (list_types.SelectedIndex < 0)
                return;
            _deviceTypeDetail.Visible = true;
            _deviceTypeDetail.PrePare(false, _types[list_types.SelectedIndex].Value.Id);
        }

        private void btn_creat_Click(object sender, EventArgs e)
        {
            _deviceTypeDetail.Visible = false;
            list_types.SelectedIndex = -1;
            _deviceTypeDetail.Visible = true;
            _deviceTypeDetail.PrePare(true, (TypeInfo it) =>
            {
                //TODO
            });

        }
    }
}
