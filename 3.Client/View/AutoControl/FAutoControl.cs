using System;
using System.Linq;
using System.Windows.Forms; 
using System.Collections.Generic;
using XNYAPI.Model.AutoControl;
using XNYAPI.Request.AutoControl;
using XNYAPI.Model;
using XNYAPI.Request.Device;

namespace MyClient.View.AutoControl
{
    //TODO 错误处理
    public partial class FAutoControl : Form, IView
    {
        FCreatOrUpdate f = new FCreatOrUpdate();
        ScheduleInfo SheduleInfo;
        AutoControlSettings Settings;
        List<ValueTuple<uint, string>> IDs;
        bool Changed;


        public FAutoControl()
        {
            InitializeComponent();
        }


        public void PrePare(params object[] par)
        {
            Changed = false;
            if (par.Count() == 1)
            {
                IDs = par[0] as List<ValueTuple<uint, string>>;
                if (IDs.Count == 0)
                    throw new Exception("设备个数不能是0");
                else if (IDs.Count == 1) {
                    try
                    {
                        var res = Global.client.Exec(new GetAutoControlSettingRequest(
                                              new List<uint>() { IDs[0].Item1 }
                                           ));
                        if (res.IsError || res.Data == null )
                            throw new Exception();
                        if (res.Data.Count != 0)
                            Settings = res.Data[0];
                        else
                            Settings = default;

                        var res2 = Global.client.Exec(new GetAutoControlScheduleDataRequest(
                                            new List<uint>() { IDs[0].Item1 }
                                         ));
                        if (res2.IsError || res2.Data == null )
                            throw new Exception();
                        if (res2.Data.Count != 0)
                            SheduleInfo = res2.Data[0];
                        else 
                            SheduleInfo =new ScheduleInfo( ServiceType.DeviceLEDControl);


                       RefreshView();
           
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("信息获取失败","错误");
                    }
                    linfo.Text = "设备[" + Utility.Utility.BuildLongString(IDs.Select(it => it.Item2), 40) + "]";
                    Changed = false;
                    //todo load
                    //throw new Exception("todo");
                } 
                else {
                   
                    SheduleInfo = new ScheduleInfo( ServiceType.DeviceLEDControl);
                    Settings = new AutoControlSettings(0,0,false,false,false, ServiceType.DeviceLEDControl);
                    linfo.Text = "设备[" + Utility.Utility.BuildLongString(IDs.Select(it => it.Item2), 40) + "]";
                    RefreshView();
                    Changed = false;
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

        void RefreshView() { 
            Func<int, string> valtostring = (v) =>
            {
                return v == 0 ? "无控制" :
                (int)v == 1 ? "关" :
                (int)v == 2 ? "开" :
                (int)v == 3 ? "高级自动" :
                "异常";
            };
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
            Func<ScheduleItem, string> tigtimetostring = (v) =>
            {
                string s;
                switch (v.TriggerType)
                {
                    case TimeTriggerType.ALL:
                        return "总是";
                    case TimeTriggerType.Once:
                        return  v.GetTimeStart() .ToString() + " - " + v.GetTimeEnd().ToString();
                    case TimeTriggerType.EveryWeek:
                        s = v.GetTimeStart().ToString("HH-mm-ss") + " - " + v.GetTimeEnd().ToString("HH-mm-ss");
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
            if (SheduleInfo!=null)
            {
                var ls = new List<string>();
                foreach (var it in SheduleInfo.Data)
                {
                    string s = "";
                    s += $"任务事件:{ valtostring(it.GetValue())}\t";
                    s += $"触发方式:{tiggertostring(it.TriggerType) }\t";
                    s += $"触发时间:{tigtimetostring(it)}";
                    ls.Add(s);

                }
                datalist.DataSource = ls;
            }

            //更新配置信息
           // if (Settings.OwnerID!=0)
            //{
                ctimeplanopen.Checked = Settings.TimeScheduleEnabled;
                cadvancedcontrol.Checked = Settings.AdvancedControlEnabled;
           // }
           

        }
        void OnDataChanged()
        {
            Changed = true; 
            RefreshView();
        }
        private void bdelete_Click(object sender, EventArgs e)
        {
            if (listselect < 0)
                return;
            SheduleInfo.Data.RemoveAt(datalist.SelectedIndex);
            OnDataChanged();
        }

        private void bcreat_Click(object sender, EventArgs e)
        {
            f.InitFor((sh) =>
           {
               SheduleInfo.Data.Add(sh);
           });
            f.ShowDialog();
            OnDataChanged();
            datalist.SelectedIndex = datalist.Items.Count - 1;
        }

        private void bupdate_Click(object sender, EventArgs e)
        {
           var sel=datalist.SelectedIndex;
            if (sel < 0)
                return;
            var s = SheduleInfo.Data[sel];
            f.InitFor(s, (sh) =>
            {
                SheduleInfo.Data[sel] = sh;
            });
            f.ShowDialog();
            OnDataChanged();
            datalist.SelectedIndex = sel;
        }

        private void bpriorityup_Click(object sender, EventArgs e)
        {
            var sel = datalist.SelectedIndex;
            if (sel <= 0)
                return;
         
            var t = SheduleInfo.Data[sel];
            SheduleInfo.Data[sel] = SheduleInfo.Data[sel- 1];
            SheduleInfo.Data[sel- 1] = t;
            OnDataChanged();
            datalist.SelectedIndex = sel-1;
        }

        private void bprioritydown_Click(object sender, EventArgs e)
        {
            var sel = datalist.SelectedIndex;
            if (sel < 0 || sel >= datalist.Items.Count - 1)
                return; 
            var t = SheduleInfo.Data[sel];
            SheduleInfo.Data[sel] = SheduleInfo.Data[sel + 1];
            SheduleInfo.Data[sel + 1] = t;
            OnDataChanged();
            datalist.SelectedIndex = sel+1;
        }



        private void ctimeplanopen_CheckedChanged(object sender, EventArgs e)
        {
            Settings.TimeScheduleEnabled = ctimeplanopen.Checked;
            OnDataChanged();
        }

        private void cadvancedcontrol_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AdvancedControlEnabled = cadvancedcontrol.Checked;
            OnDataChanged();
        }
        private void bok_Click(object sender, EventArgs e)
        {
            if (Changed)
            {
                
                try
                { 
                    var res=  Global.client.Exec(new SetAutoControlScheduleDataRequest(
                            IDs.Select(it=>it.Item1).ToList(),
                            SheduleInfo
                     ));
                    if (res.IsError)
                    {
                        throw new Exception();
                       
                    }
                    var res2 = Global.client.Exec(new SetAutoControlSettingRequest(
                            IDs.Select(it => it.Item1).ToList(),
                            Settings.TimeScheduleEnabled,Settings.AdvancedControlEnabled,
                            Settings.GroupID
                     ));
                    if (res.IsError)
                    {
                        throw new Exception();

                    }
                    MessageBox.Show("成功", "提示");
                    Changed = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("失败", "提示");
                }
              
                //todo 提交
            }
        }
        private void btest_Click(object sender, EventArgs e)
        {
            Func<int, string> valtostring = (v) =>
            {
                return v == 0 ? "无控制" :
  (int)v == 1 ? "关" :
  (int)v == 2 ? "开" :
  (int)v == 3 ? "高级自动" :
  "异常";
            };

            MessageBox.Show(valtostring(SheduleInfo.GetValue(DateTime.Now, 4)));
        }




        //private async void timer1_TickAsync(object sender, EventArgs e)
        //{
        //    if (!Visible)
        //    {
        //        return;
        //    }
        //    bool hasdvcmdsended = false;
        //    string info = "已向设备: \n ";
        //    bool exed = await SigleExecute.ExecuteAsync(GetType().FullName + "tic", () =>
        //    {
        //        try
        //        {
        //            foreach (var key in new List<string>(needsend.Keys))
        //            {
        //                if (NetWork.CommonNetUtility.Device.IsCmdSended(needsend[key].Item2))
        //                {
        //                    needsend.Remove(key);
        //                    info += key + "\n";
        //                    hasdvcmdsended = true;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }

        //    });
        //    if (exed)
        //    {
        //        if (needsend.Count == 0)
        //        {
        //            timer1.Stop();
        //        }
        //        if (hasdvcmdsended)
        //        {
        //            MessageBox.Show(info + "发送命令");
        //        }
        //    }

        //}



        int listselect => datalist.SelectedIndex; 


        public Control View => this;
        public void SetViewHolder(IViewHolder viewholder)
        {

        }

        public void OnEvent(string name, params object[] pars)
        {
            if (name=="Exit")
            {//退出界面
                if (Changed)
                {//询问是否退出
                    if (MessageBox.Show("尚未保存,是否离开", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        (pars[0] as FormExitEventArg).Cancel = true;
                    }
                }
            }
        }

        public void OnTick()
        {
           
        }

        private void bopen_Click(object sender, EventArgs e)
        {
            if (IDs == null || IDs.Count == 0)
            {
                MessageBox.Show("未知错误","异常");
                return;
            }
            string s = "";
            for (int i = 0; i < IDs.Count; i++)
            {
                s += IDs[i].Item2 + ",";
                if (s.Length > 40)
                {
                    break;
                }
            }
            if (s.Length > 40)
                s = s.Substring(0, 40) + "...";
            else
                s = s.Substring(0, s.Length - 1);
            try
            {
                var res = Global.client.Exec(new SendCMDRequest(IDs.Select(it => it.Item1).ToList(), "status:2"));
                if (res.IsError)
                    throw new Exception();
                MessageBox.Show("向设备[" + s + "]发送成功", "提示");
            }
            catch (Exception)
            {
                MessageBox.Show("向设备[" + s + "]发送失败", "提示");
            }
          
        }

        private void bclose_Click(object sender, EventArgs e)
        {
            if (IDs == null || IDs.Count == 0)
            {
                MessageBox.Show("未知错误", "异常");
                return;
            }
            string s = "";
            for (int i = 0; i < IDs.Count; i++)
            {
                s += IDs[i].Item2 + ",";
                if (s.Length > 40)
                {
                    break;
                }
            }
            if (s.Length > 40)
                s = s.Substring(0, 40) + "...";
            else
                s = s.Substring(0, s.Length - 1);
            try
            {
                var res = Global.client.Exec(new SendCMDRequest(IDs.Select(it => it.Item1).ToList(), "status:3"));
                if (res.IsError)
                    throw new Exception();
                MessageBox.Show("向设备[" + s + "]发送成功", "提示");
            }
            catch (Exception)
            {
                MessageBox.Show("向设备[" + s + "]发送失败", "提示");
            }
        }
    }
}
