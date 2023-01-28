using FdlWindows.View;
using GrpcMain.Device.AutoControl;
using MyClient.Grpc;
using MyUtility;
using System.ComponentModel;
using XNYAPI.Model.AutoControl;
using static GrpcMain.Device.AutoControl.DeviceAutoControlService;

namespace MyClient.View.AutoControl
{
    [AutoDetectView(nameof(FAutoControl), "自动控制配置", "", false)]
    public partial class FAutoControl : Form, IView
    {
        FCreatOrUpdate f = new FCreatOrUpdate();
        DeviceAutoControlServiceClient _client;
        Dictionary<string, List<DeviceAutoControlSetting>> groupedsettings = new();
        BindingList<string> names = new BindingList<string>();
        List<ValueTuple<long, string>> IDs;
        TimeUtility tu = new TimeUtility();
        bool Changed;
        IViewHolder _viewholder;

        public FAutoControl(DeviceAutoControlServiceClient client)
        {
            InitializeComponent();
            _client = client;
        }


        public void PrePare(params object[] par)
        {
            Changed = false;
            if (par.Count() == 1)
            {
                IDs = par[0] as List<ValueTuple<long, string>>;
                if (IDs.Count == 0)
                    throw new Exception("设备个数不能是0");
                else if (IDs.Count == 1)
                {
                    try
                    {
                        _viewholder.ShowLoading(this, async () =>
                        {
                            var r = await _client.GetDeviceSettingAsync(new Request_GetDeviceSetting()
                            {
                                Dvids = IDs[0].Item1
                            });
                            var settings = r.Setting.OrderBy(it => it.Name).ThenBy(it => it.Order);
                            names = new BindingList<string>(settings.Select(it => it.Name).Distinct().ToList());
                            groupedsettings = new Dictionary<string, List<DeviceAutoControlSetting>>();
                            names.ToList().ForEach(it =>
                            {
                                groupedsettings.Add(it, new List<DeviceAutoControlSetting>());
                            });
                            settings.ToList().ForEach(it =>
                            {
                                groupedsettings[it.Name].Add(it);
                            });

                            list_names.DataSource = names;
                            if (names.Count != 0)
                            {
                                list_names.SelectedIndex = 0;
                            }
                            else
                            {
                                list_names.SelectedIndex = -1;
                            }
                            return true;
                        });
                        RefreshView();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("信息获取失败", "错误");
                    }
                    linfo.Text = "设备[" + Utility.Utility.BuildLongString(IDs.Select(it => it.Item2), 40) + "]";
                    Changed = false;
                    //todo load
                    //throw new Exception("todo");
                }
                else
                {
                    throw new Exception("暂时只支持一个设备");
                }

            }
            else
            {
                throw new Exception("无法直接访问");
            }
        }

        //  void LoadData()
        //  {
        //      UIByCode.CodeEnter();
        //      try
        //      {
        //          if (StartByGroup)
        //          {
        //              Settings = CommonNetUtility.AutoControl.GetAutoControlSetting(GroupID, true);
        //              SheduleInfo = CommonNetUtility.AutoControl.GetAutoControlSheduleData(GroupID, true);
        //              linfo.Text = $"分组{GroupID}";
        //              bgotogroup.Enabled = false;
        //          }
        //          else
        //          {
        //              Settings = CommonNetUtility.AutoControl.GetAutoControlSetting(DeviceID);
        //              if (Settings.GroupID == "")
        //              {
        //                  SheduleInfo = CommonNetUtility.AutoControl.GetAutoControlSheduleData(DeviceID);
        //                  linfo.Text = $"设备{DeviceID}";
        //              }
        //              else
        //              {
        //                  GroupID = Settings.GroupID;
        //                  Settings = CommonNetUtility.AutoControl.GetAutoControlSetting(GroupID, true);
        //                  SheduleInfo = CommonNetUtility.AutoControl.GetAutoControlSheduleData(GroupID, true);
        //                  linfo.Text = $"设备{DeviceID},分组{GroupID}";
        //                  OpGroup = true;
        //              }
        //              bgotogroup.Enabled = true;
        //          }
        //          datalist.Items.Clear();
        //          Func<int, string> valtostring = (v) =>
        //          {
        //              return v == 0 ? "无控制" :
        //(int)v == 1 ? "关" :
        //(int)v == 2 ? "开" :
        //(int)v == 3 ? "高级自动" :
        //"异常";
        //          };
        //          Func<TimeTriggerType, string> tiggertostring = (v) =>
        //          {
        //              switch (v)
        //              {
        //                  case TimeTriggerType.ALL:
        //                      return "总是";
        //                  case TimeTriggerType.Once:
        //                      return "时间段";
        //                  case TimeTriggerType.EveryWeek:
        //                      return "周定时";
        //                  default:
        //                      return "异常";
        //              }

        //          };
        //          Func<ScheduleItem, string> tigtimetostring = (v) =>
        //          {
        //              string s;
        //              switch (v.TriggerType)
        //              {
        //                  case TimeTriggerType.ALL:
        //                      return "总是";
        //                  case TimeTriggerType.Once:
        //                      return (new DateTime(v.TimeStart)).ToString() + " - " + (new DateTime(v.TimeEnd)).ToString();
        //                  case TimeTriggerType.EveryWeek:
        //                      s = (new DateTime(v.TimeStart)).ToString("HH-mm-ss") + " - " + (new DateTime(v.TimeEnd)).ToString("HH-mm-ss");
        //                      s += " 周";
        //                      var vl = v.Week;
        //                      for (int i = 0; i < 7; i++)
        //                      {
        //                          if (vl % 2 == 1)
        //                          {
        //                              s += "日一二三四五六"[i];
        //                          }
        //                          vl /= 2;
        //                      }
        //                      return s;
        //                  default:
        //                      return "异常";
        //              }
        //              return "";
        //          };
        //          foreach (var it in SheduleInfo.Data)
        //          {
        //              string s = "";
        //              s += $"ID:" + it.ID + "\t";
        //              s += $"任务事件:{ valtostring(it.GetValue())}\t";
        //              s += $"触发方式:{tiggertostring(it.TriggerType) }\t";
        //              s += $"触发时间:{tigtimetostring(it)}";
        //              datalist.Items.Add(s);
        //          }


        //          //更新配置信息
        //          ctimeplanopen.Checked = Settings.TimeScheduleEnabled;
        //          cadvancedcontrol.Checked = Settings.AdvancedControlEnabled;

        //      }
        //      catch (Exception)
        //      {

        //          throw;
        //      }
        //      finally
        //      {
        //          UIByCode.CodeExit();
        //      }


        //  }

        void RefreshView()
        {

            var sel = list_names.SelectedIndex;
            if (sel < 0 || sel > names.Count - 1)
                return;
            var list = groupedsettings[list_names.SelectedItem as string];

            Func<TimeTriggerType, string> tiggertostring = (v) =>
            {
                switch (v)
                {
                    case TimeTriggerType.ALL:
                        return "总是";
                    case TimeTriggerType.Once:
                        return "时间段";
                    case TimeTriggerType.EveryWeek:
                        return "周定时";
                    default:
                        return "异常";
                }

            };
            Func<DeviceAutoControlSetting, string> tigtimetostring = (v) =>
            {
                string s;
                switch ((TimeTriggerType)v.TriggerType)
                {
                    case TimeTriggerType.ALL:
                        return "总是";
                    case TimeTriggerType.Once:
                        return tu.GetDateTime(v.TimeStart).ToString() + " - " + tu.GetDateTime(v.TimeEnd).ToString();
                    case TimeTriggerType.EveryWeek:
                        s = tu.GetDateTime(tu.GetTicket(DateTime.Now.Date) + v.TimeStart).ToString("HH-mm-ss") + " - "
                        + tu.GetDateTime(tu.GetTicket(DateTime.Now.Date) + v.TimeEnd).ToString("HH-mm-ss");
                        s += " 周";
                        var vl = v.Week;
                        for (int i = 0; i < 7; i++)
                        {
                            if (vl % 2 == 1)
                            {
                                s += "日一二三四五六"[i];
                            }
                            vl /= 2;
                        }
                        return s;
                    default:
                        return "异常";
                }
            };


            var ls = new List<string>();
            foreach (var it in list)
            {
                string s = "";
                s += $"任务事件:{it.Cmd}\t";
                s += $"触发方式:{tiggertostring((TimeTriggerType)it.TriggerType)}\t";
                s += $"触发时间:{tigtimetostring(it)}";
                ls.Add(s);
            }
            datalist.DataSource = ls;
        }
        /// <summary>
        /// 设置数据变更并更新界面 <see cref="RefreshView"/>
        /// </summary>
        void OnDataChanged()
        {
            Changed = true;
            RefreshView();
        }
        private void bdelete_Click(object sender, EventArgs e)
        {
            var sel = list_names.SelectedIndex;
            if (sel < 0 || sel > names.Count - 1)
                return;
            var ls = groupedsettings[list_names.SelectedItem as string];
            ls.RemoveAt(datalist.SelectedIndex);
            OnDataChanged();
        }

        private void bcreat_Click(object sender, EventArgs e)
        {
            var sel = list_names.SelectedIndex;
            if (sel < 0 || sel > names.Count - 1)
                return;
            f.InitFor((sh) =>
           {
               sh.Name = list_names.SelectedItem.ToString();
               groupedsettings[list_names.SelectedItem as string].Add(sh);
           });
            f.ShowDialog();
            OnDataChanged();
            datalist.SelectedIndex = datalist.Items.Count - 1;
        }

        private void bupdate_Click(object sender, EventArgs e)
        {
            var sel = list_names.SelectedIndex;
            if (sel < 0 || sel > names.Count - 1)
                return;
            sel = datalist.SelectedIndex;
            if (sel < 0 || sel > groupedsettings[list_names.SelectedItem as string].Count - 1)
                return;
            var s = groupedsettings[list_names.SelectedItem as string][datalist.SelectedIndex];
            f.InitFor(s, (sh) =>
            {
                sh.Name = list_names.SelectedItem.ToString();
                groupedsettings[list_names.SelectedItem as string][datalist.SelectedIndex] = sh;
            });
            f.ShowDialog();
            OnDataChanged();
            datalist.SelectedIndex = sel;
        }

        private void bpriorityup_Click(object sender, EventArgs e)
        {
            var sel = list_names.SelectedIndex;
            if (sel < 0 || sel > names.Count - 1)
                return;
            sel = datalist.SelectedIndex;
            if (sel <= 0)
                return;

            var ls = groupedsettings[list_names.SelectedItem as string];
            var t = ls[sel];
            ls[sel] = ls[sel - 1];
            ls[sel - 1] = t;
            OnDataChanged();
            datalist.SelectedIndex = sel - 1;
        }

        private void bprioritydown_Click(object sender, EventArgs e)
        {
            var sel = list_names.SelectedIndex;
            if (sel < 0 || sel > names.Count - 1)
                return;
            sel = datalist.SelectedIndex;
            if (sel >= groupedsettings[list_names.SelectedItem as string].Count - 1)
                return;

            var ls = groupedsettings[list_names.SelectedItem as string];
            var t = ls[sel];
            ls[sel] = ls[sel + 1];
            ls[sel + 1] = t;
            OnDataChanged();
            datalist.SelectedIndex = sel + 1;
        }



        private void ctimeplanopen_CheckedChanged(object sender, EventArgs e)
        {
            //TODO

            //OnDataChanged();
        }

        private async void bok_Click(object sender, EventArgs e)
        {
            if (Changed)
            {

                try
                {
                    var req = new Request_SetDeviceSetting()
                    {
                    };
                    req.Dvids.Add(IDs[0].Item1);
                    groupedsettings.Values.ToList().ForEach(it =>
                    {
                        req.Setting.AddRange(it);
                    });
                    var r = await _client.SetDeviceSettingAsync(req);
                    r.ThrowIfNotSuccess();
                    MessageBox.Show("成功", "提示");
                    Changed = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("失败", "提示");
                }
            }
        }
        private void btest_Click(object sender, EventArgs e)
        {
            var sel = list_names.SelectedIndex;
            if (sel < 0 || sel > names.Count - 1)
                return;
            var cmd = groupedsettings[list_names.SelectedItem as string].GetCmd(DateTime.Now);
            MessageBox.Show(string.IsNullOrWhiteSpace(cmd) ? "无" : cmd);
        }

        public Control View => this;
        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }

        public void OnEvent(string name, params object[] pars)
        {
            if (name == "Exit")
            {//退出界面
                if (Changed)
                {//询问是否退出
                    if (MessageBox.Show("尚未保存,是否离开", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        (pars[0] as FormExitEventArg).Cancel = true;
                    }
                }
            }
        }

        public void OnTick()
        {

        }


        private void btn_addname_Click(object sender, EventArgs e)
        {
            string name;
            var dr = InputBox.GetString("创建", "请输入自动控制项目名称", out name);
            if (dr != DialogResult.OK)
                return;
            var ls = names;
            if (ls.Contains(name))
            {
                MessageBox.Show("已经存在", "提示");
                return;
            }
            ls.Add(name);
            groupedsettings[name] = new List<DeviceAutoControlSetting>();
        }

        private void list_names_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_names.SelectedIndex < 0)
                return;
            RefreshView();
        }
    }


    static public class DeviceAutoControlUtility
    {
        static TimeUtility tu = new TimeUtility();

        ///  <summary>
        ///   创建一个星期定时任务
        ///  </summary>
        ///  <param name="week">[周日,周一,...]是否生效</param>
        static public DeviceAutoControlSetting Creat
            (string name, long ownerID, string cmd, long start, long end, bool[] week)
        {
            var re = new DeviceAutoControlSetting()
            {
                Name = name,
                TriggerType = (int)TimeTriggerType.EveryWeek,
                TimeStart = start,
                TimeEnd = end,
                OwnerID = ownerID,
                Cmd = cmd,
            };
            re.Week = 0;
            for (int i = 0; i < week.Length; i++)
            {
                if (week[i])
                    re.Week |= (byte)(1 << i);
            }
            return re;
        }

        /// <summary>
        /// 创建一个时间任务
        /// </summary>
        static public DeviceAutoControlSetting Creat
            (string name, long ownerID, string cmd, long start, long end)
        {
            return new DeviceAutoControlSetting()
            {
                Name = name,
                TriggerType = (int)TimeTriggerType.Once,
                TimeStart = start,
                TimeEnd = end,
                OwnerID = ownerID,
                Cmd = cmd,
            };
        }

        /// <summary>
        /// 创建一个一直生效的任务 
        /// </summary>
        static public DeviceAutoControlSetting Creat
            (string name, long ownerID, string cmd)
        {
            return new DeviceAutoControlSetting()
            {
                Name = name,
                TriggerType = (int)TimeTriggerType.ALL,
                OwnerID = ownerID,
                Cmd = cmd,
            };
        }

        /// <summary>
        /// 给定时间是否在此时间内
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        static public bool IsTimeIn(this DeviceAutoControlSetting item, DateTime time)
        {
            switch ((TimeTriggerType)item.TriggerType)
            {
                case TimeTriggerType.ALL:
                    return true;
                case TimeTriggerType.Once:
                    return (time.Ticks >= item.TimeStart) && (time.Ticks <= item.TimeEnd);
                case TimeTriggerType.EveryWeek:
                    //time = time.AddHours(8);
                    long t = tu.GetTicket(time) - tu.GetTicket(time.Date);
                    return (t >= item.TimeStart) && (t <= item.TimeEnd) && (item.Week & (1 << (int)time.DayOfWeek)) > 0;
                default:
                    return false;
            }
        }

        static public string? GetCmd(this List<DeviceAutoControlSetting> item, DateTime time)
        {
            item = item.OrderBy(it => it.Order).ToList();
            for (int i = item.Count - 1; i >= 0; i--)
            {
                if (item[i].IsTimeIn(time))
                {
                    return item[i].Cmd;
                }
            }
            return null;
        }



        const long TicketADay = 24L * 60 * 60 * 1000;
        /// <summary>
        /// 校验时间信息是否合法
        /// </summary>
        /// <returns></returns>
        static public bool Check(this DeviceAutoControlSetting item)
        {
            switch ((TimeTriggerType)item.TriggerType)
            {
                case TimeTriggerType.ALL:
                    return true;
                case TimeTriggerType.Once:
                    return true;
                case TimeTriggerType.EveryWeek:

                    return (item.TimeStart >= 0 && item.TimeStart < TicketADay) && (item.TimeEnd >= 0 && item.TimeEnd < TicketADay);
                default:
                    return true;
            }
        }


    }
}
