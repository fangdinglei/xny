using GrpcMain.Device.AutoControl;
using MyUtility;
using XNYAPI.Model.AutoControl;

namespace MyClient.View.AutoControl
{
    public partial class FCreatOrUpdate : Form
    {
        TimeUtility tu = new TimeUtility();
        //g0 总是 panel g1周定时 panel  g2时间段panel
        DeviceAutoControlSetting Org;
        Action<DeviceAutoControlSetting> CallBack;

        public FCreatOrUpdate()
        {
            InitializeComponent();
            cbTimeZone.SelectedIndex = 12 + TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).Hours;
        }
        /// <summary>
        /// 创建一个任务
        /// </summary>
        /// <param name="callback"></param>
        public void InitFor(Action<DeviceAutoControlSetting> callback)
        {
            CallBack = callback;
            Org = null;
            comboBox1.SelectedIndex = 0;
            t_cmd.Text = "";

        }
        /// <summary>
        /// 修改任务 TODO内容适应原有的值
        /// </summary>
        public void InitFor(DeviceAutoControlSetting org, Action<DeviceAutoControlSetting> callback)
        {
            CallBack = callback;
            Org = org;
            comboBox1.SelectedIndex = org.TriggerType;
            t_cmd.Text = org.Cmd;
            cbTimeZone.SelectedIndex = org.TimeZone + 12;
            switch ((TimeTriggerType)org.TriggerType)
            {
                case TimeTriggerType.ALL:
                    break;
                case TimeTriggerType.Once:
                    g2_startdatepicker.Value = tu.GetDateTime(org.TimeStart);
                    g2_starttimepicker.Value = tu.GetDateTime(org.TimeStart);
                    g2_enddatepicker.Value = tu.GetDateTime(org.TimeEnd);
                    g2_endtimepicker.Value = tu.GetDateTime(org.TimeEnd);
                    break;
                case TimeTriggerType.EveryWeek:
                    g1_starttimepicker.Value = tu.GetDateTime(tu.GetTicket(DateTime.Now.Date) + org.TimeStart);
                    g1_endtimepicker.Value = tu.GetDateTime(tu.GetTicket(DateTime.Now.Date) + org.TimeEnd);
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


        private void bok_Click(object sender, EventArgs e)
        {
            var v = t_cmd.Text;
            DeviceAutoControlSetting sh;
            if ((string)comboBox1.SelectedItem == "总 是")
            {
                //g0
                sh = DeviceAutoControlUtility.Creat("", 0, v);
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
                    g2_starttimepicker.Value.Second, DateTimeKind.Utc
                    );
                var dateend = new DateTime(
                g2_enddatepicker.Value.Year,
                g2_enddatepicker.Value.Month,
                g2_enddatepicker.Value.Day,
                g2_endtimepicker.Value.Hour,
                g2_endtimepicker.Value.Minute,
                g2_endtimepicker.Value.Second, DateTimeKind.Utc);
                if (datestart >= dateend)
                {
                    MessageBox.Show("结束时间不能在开始时间之前", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                sh = DeviceAutoControlUtility.Creat("", 0, v,
                    tu.GetTicket(datestart),
                    tu.GetTicket(dateend),
                    int.Parse(cbTimeZone.SelectedItem.ToString()));
            }
            else if ((string)comboBox1.SelectedItem == "周定时")
            {
                //g1
                var datestart = new DateTime(
                  1970, 1, 1,
                  g1_starttimepicker.Value.Hour,
                  g1_starttimepicker.Value.Minute,
                  g1_starttimepicker.Value.Second
                  );
                var dateend = new DateTime(
                 1970, 1, 1,
                  g1_endtimepicker.Value.Hour,
                  g1_endtimepicker.Value.Minute,
                  g1_endtimepicker.Value.Second
                  );
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
                sh = DeviceAutoControlUtility.Creat("", 0, v, tu.GetTickDiffer(datestart, datestart.Date), tu.GetTickDiffer(dateend, dateend.Date), week, int.Parse(cbTimeZone.SelectedItem.ToString()));
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
