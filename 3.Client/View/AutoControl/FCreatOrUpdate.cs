 
using System;
using System.Windows.Forms;
using XNYAPI.Model;
using XNYAPI.Model.AutoControl;

namespace MyClient.View.AutoControl
{
    public partial class FCreatOrUpdate : Form
    {
        //g0 总是 panel g1周定时 panel  g2时间段panel
        ScheduleItem Org;
        Action<ScheduleItem> CallBack;

        public FCreatOrUpdate()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 创建一个任务
        /// </summary>
        /// <param name="deviceid"></param>
        /// <param name="callback"></param>
        public void InitFor(  Action<ScheduleItem> callback)
        {
            CallBack = callback;
           
            Org = null;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
           
        }
        /// <summary>
        /// 修改任务 TODO内容适应原有的值
        /// </summary>
        public void InitFor( ScheduleItem org, Action<ScheduleItem> callback)
        {
            CallBack = callback;
            Org = org;
            comboBox1.SelectedIndex = (int)org.TriggerType;
            comboBox2.SelectedIndex = org.GetValue();
            switch (org.TriggerType)
            {
                case TimeTriggerType.ALL:
                    break;
                case TimeTriggerType.Once:
                    g2_startdatepicker.Value = org.GetTimeStart() ;
                    g2_starttimepicker.Value = org.GetTimeStart();
                    g2_enddatepicker.Value = org.GetTimeEnd();
                    g2_endtimepicker.Value = org.GetTimeEnd();
                    break;
                case TimeTriggerType.EveryWeek:
                    g1_starttimepicker.Value = org.GetTimeStart().AddHours(-8);  
                    g1_endtimepicker.Value = org.GetTimeEnd().AddHours(-8);
                    for (int i = 0; i < 7; i++)
                    {
                        if ((org.Week & (1 << i)) > 0)
                        {
                            g1_week.SetItemChecked(i, true);
                        }
                        else
                        {
                            g1_week.SetItemChecked(i, false);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private int GetSelectedSheduleValue()
        {
            //忽略 关闭 启动 智能
            return comboBox2.SelectedIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)comboBox1.SelectedItem == "总 是")
            {
                g1panel.Visible = false;
                g2panel.Visible = false;
            }
            else if ((string)comboBox1.SelectedItem == "时间段")
            {
                g1panel.Visible = false;
                g2panel.Visible = true;
            }
            else if ((string)comboBox1.SelectedItem == "周定时")
            {
                g2panel.Visible = false;
                g1panel.Visible = true;
            }
            else
            {
                MessageBox.Show("未知触发条件，请联系管理员");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void bok_Click(object sender, EventArgs e)
        {
            var v = GetSelectedSheduleValue();
            ScheduleItem sh = null;
            if ((string)comboBox1.SelectedItem == "总 是")
            {
                //g0
                sh = ScheduleItem.Creat(ServiceType.DeviceLEDControl,0,v,0);
            }
            else if ((string)comboBox1.SelectedItem == "时间段")
            {
                //g2
                var datestart = new DateTime(
                    g2_startdatepicker.Value.Year,
                    g2_startdatepicker.Value.Month,
                    g2_startdatepicker.Value.Day,
                    g2_starttimepicker.Value.Hour,
                    g2_starttimepicker.Value.Minute,
                    g2_starttimepicker.Value.Second
                    );
                var dateend = new DateTime(
                g2_enddatepicker.Value.Year,
                g2_enddatepicker.Value.Month,
                g2_enddatepicker.Value.Day,
                g2_endtimepicker.Value.Hour,
                g2_endtimepicker.Value.Minute,
                g2_endtimepicker.Value.Second);
                if (datestart >= dateend)
                {
                    MessageBox.Show("结束时间不能在开始时间之前", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                sh = ScheduleItem.Creat(ServiceType.DeviceLEDControl, 0, v, datestart.BeijingTimeToJavaTicket(), dateend.BeijingTimeToJavaTicket(), 0);
            }
            else if ((string)comboBox1.SelectedItem == "周定时")
            {
                //g1
                var datestart = new DateTime(
                  1970, 1, 1,
                  g1_starttimepicker.Value.Hour,
                  g1_starttimepicker.Value.Minute,
                  g1_starttimepicker.Value.Second
                  ) .AddHours(8);
                var dateend = new DateTime(
                 1970, 1, 1,
                  g1_endtimepicker.Value.Hour,
                  g1_endtimepicker.Value.Minute,
                  g1_endtimepicker.Value.Second
                  ).AddHours(8);
                if (datestart >= dateend)
                {
                    MessageBox.Show("结束时间不能在开始时间之前", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var week = new bool[7] { false, false, false, false, false, false, false };
                bool has = false;
                for (int i = 0; i < 7; i++)
                {
                    week[i] = g1_week.GetItemChecked(i);
                    if (week[i])
                        has = true;
                }
                if (!has)
                {
                    MessageBox.Show("请选择至少一天", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                sh = ScheduleItem.Creat(ServiceType.DeviceLEDControl, 0, v, datestart.BeijingTimeToJavaTicket(), dateend.BeijingTimeToJavaTicket(), week,0);
            }
            else
            {
                MessageBox.Show("未知触发条件，请联系管理员");
                return;
            }
         
            CallBack(sh);
            Hide();
        }

        private void bcancel_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
