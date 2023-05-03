namespace DeviceSimulator
{
    public partial class FMQTTMock : Form
    {
        long DeviceID = 1;
        bool running = false;

        SynchronizationContext SyncContext = null;
        public FMQTTMock()
        {
            InitializeComponent();
            textBox1.Text = DeviceID + "";
            SyncContext = SynchronizationContext.Current;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "开始")
            {
                if (!long.TryParse(textBox1.Text, out DeviceID))
                {
                    MessageBox.Show("请输入数字设备ID");
                    return;
                }
                timer1.Start();
                textBox1.Enabled = false;
                button1.Text = "结束";
            }
            else
            {
                timer1.Stop();
                textBox1.Enabled = true;
                button1.Text = "开始";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (running)
            {
                return;
            }
            try
            {
                running = true;
                var dic = new Dictionary<long, float>();
                float Temperature, Humidity, Lumination, PowerRate;
                Temperature = bar1.Value / 10f;
                //Humidity = bar2.Value / 10f;
                //Lumination = bar3.Value / 10f;
                //PowerRate = bar4.Value / 10f;
                dic.Add(1, Temperature);
                //dic.Add(nameof(Humidity), Humidity);
                //dic.Add(nameof(Lumination), Lumination);
                //dic.Add(nameof(PowerRate), PowerRate);
                Mock.MQTT.MQTTManager.SendData(DeviceID, dic);
            }
            catch (Exception)
            {
            }
            finally
            {
                running = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            running = true;
            var dic = new Dictionary<long, float>();
            float Temperature, Humidity, Lumination, PowerRate;
            Temperature = bar1.Value / 10f;
            Humidity = bar2.Value / 10f;
            //Lumination = bar3.Value / 10f;
            //PowerRate = bar4.Value / 10f;
            dic.Add(1, Temperature);
            dic.Add(2, Humidity);
            //dic.Add(nameof(Lumination), Lumination);
            //dic.Add(nameof(PowerRate), PowerRate);
            Mock.MQTT.MQTTManager.SendData(DeviceID, dic);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _ = Mock.MQTT.MQTTManager.ListenCmdAsync(DeviceID, (s) =>
            {
                SyncContext.Post((s) =>
                {
                    listBox1.Items.Insert(0, s);
                }, s);
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}