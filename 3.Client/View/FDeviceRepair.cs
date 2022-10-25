using FDL.Program;
using FdlWindows.View;
using GrpcMain.Device;
using GrpcMain.UserDevice;
using MyUtility;
using System.ComponentModel;
using System.Data; 

namespace MyClient.View
{
    [AutoDetectView("FDeviceRepair", "设备维修", "", true)]
    public partial class FDeviceRepair : Form, IView
    {
        bool _isSingleMode;
        /// <summary>
        /// 是否是单个设备模式 此模式只获取单个设备维修信息
        /// </summary>
        bool IsSingleMode
        {
            set
            {
                btn_creat.Visible = value;
                //list_infos.Visible = !value;
                //btn_search.Visible = !value;
                _device = null;
                _isSingleMode = value;
            }
            get
            {
                return _isSingleMode;
            }
        }

        DeviceWithUserDeviceInfo _device;
        BindingList<ToStringHelper<RepairInfo>> _repairInfos = new BindingList<ToStringHelper<RepairInfo>>();
        IViewHolder _viewHolder;
        ITimeUtility _timeUtility;
        RepairService.RepairServiceClient _repairServiceClient;
        LocalDataBase _db;
        public FDeviceRepair(RepairService.RepairServiceClient repairServiceClient, ITimeUtility timeUtility, LocalDataBase db)
        {
            InitializeComponent();
            _repairServiceClient = repairServiceClient;
            _timeUtility = timeUtility;
            _db = db;
        }

        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {
            if (name=="Exit")
            {
                _repairInfos.Clear();
            }
        }

        public void PrePare(params object[] par)
        {
            if (par.Count() == 0)
            {//查询模式
                IsSingleMode = false;
            }
            else if (par.Count() == 1)
            {//新增模式
                IsSingleMode = true;
                _device = (DeviceWithUserDeviceInfo)par[0];

            }
        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewHolder = viewholder;
        }

        public void OnTick()
        {

        }

        private void btn_update_Click(object sender, EventArgs e)
        { 
            SigleExecute.Execute(nameof(FDeviceRepair), () =>
            {
                if (_repairInfos.Count == 0)
                {
                    return;
                }
                try
                {
                    var info = new RepairInfo
                    {
                        Id = _repairInfos[list_infos.SelectedIndex].Value.Id,
                        DiscoveryTime = _timeUtility.GetTicket(time_DiscoveryTime.Value),
                        CompletionTime = _timeUtility.GetTicket(time_CompletionTime.Value),
                        Context = text_context.Text,
                        DeviceId = _repairInfos[list_infos.SelectedIndex].Value.DeviceId,
                    };
                    var r = _repairServiceClient.UpdateRepairInfo(new Request_UpdateRepairInfo
                    {
                        Info = info
                    });
                    _repairInfos[list_infos.SelectedIndex] = new ToStringHelper<RepairInfo>(info, it => it.Id + "");
                    MessageBox.Show("更新完成", "提示");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误:" + ex.Message, "错误");
                    return;
                }
            });

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            var smode = IsSingleMode;
            SigleExecute.Execute(nameof(FDeviceRepair), unlock =>
            {
                _viewHolder.ShowDatePicker((ds, de) =>
                {
                    _viewHolder.ShowLoading(this, async () =>
                    {
                        var req1 = new Request_GetRepairInfos
                        {
                            Cursor = 0,
                            StartTime = _timeUtility.GetTicket(ds),
                            EndTime = _timeUtility.GetTicket(de),
                        };
                        if (smode)
                        {
                            req1.DeviceId = _device.Device.Id;
                        }
                        var res1 = await _repairServiceClient.GetRepairInfosAsync(req1);
                        _repairInfos = new BindingList<ToStringHelper<RepairInfo>>(
                            res1.Info.Select(it => new ToStringHelper<RepairInfo>(it, it => it.Id + "")).ToList());
                        return true;
                    }, okcall: () =>
                    {
                        list_infos.DataSource = _repairInfos;
                        unlock();
                    }, exitcall: () =>
                    {
                        unlock();
                    });
                });
            });

        }

        private void btn_creat_Click(object sender, EventArgs e)
        {
            SigleExecute.Execute(nameof(FDeviceRepair), () =>
            {
                if (!IsSingleMode)
                    return;
                try
                {
                    var r = _repairServiceClient.AddRepairInfo(new Request_AddRepairInfo
                    {
                        Info = new RepairInfo
                        {
                            Id = 0,
                            DiscoveryTime = _timeUtility.GetTicket(time_DiscoveryTime.Value),
                            CompletionTime = _timeUtility.GetTicket(time_CompletionTime.Value),
                            Context = text_context.Text,
                            DeviceId = _device.Device.Id,
                        }
                    });
                    _repairInfos.Add(new ToStringHelper<RepairInfo>(r.Info, it => it.Id + ""));
                    MessageBox.Show("添加完成", "提示");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误:" + ex.Message, "错误");
                    return;
                }
            });
        }

        private void list_infos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_infos.SelectedIndex<0)
            { 
                text_dvname.Text = "";
                text_context.Text = "";
                return;
            }
            var v = _repairInfos[list_infos.SelectedIndex].Value;
            text_context.Text= v.Context;
            var dv=_db.GetDevice(v.DeviceId);
            if (dv==null)
            {
                text_dvname.Text = "Id:" + v.DeviceId;
            }
            else
            {
                text_dvname.Text = "Id:" + dv.Id + ",Name:" + dv.Name;
            }
           
            time_CompletionTime.Value = _timeUtility.GetDateTime(v.CompletionTime);
            time_DiscoveryTime.Value = _timeUtility.GetDateTime(v.DiscoveryTime);
        }
    }
}