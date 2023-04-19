namespace MyClient.View.User
{
    partial class FUserDevice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            list_devicegroup = new ListBox();
            list_devices = new ListBox();
            check_read_data = new CheckBox();
            check_control_cmd = new CheckBox();
            check_write_baseinfo = new CheckBox();
            check_control_timesetting = new CheckBox();
            check_read_repair = new CheckBox();
            check_write_deletdata = new CheckBox();
            check_read_status = new CheckBox();
            groupBox1 = new GroupBox();
            lay_read = new TableLayoutPanel();
            check_read_baseinfo = new CheckBox();
            check_read_timesetting = new CheckBox();
            check_read_cmd = new CheckBox();
            groupBox2 = new GroupBox();
            lay_write = new TableLayoutPanel();
            check_write_deletdevice = new CheckBox();
            check_write_repair = new CheckBox();
            check_write_type = new CheckBox();
            groupBox3 = new GroupBox();
            lay_control = new TableLayoutPanel();
            toolTip1 = new ToolTip(components);
            check_delegate = new CheckBox();
            btn_sel_read = new Button();
            btn_sel_rw = new Button();
            btn_sel_rwc = new Button();
            btn_submit = new Button();
            op_panel = new Panel();
            groupBox1.SuspendLayout();
            lay_read.SuspendLayout();
            groupBox2.SuspendLayout();
            lay_write.SuspendLayout();
            groupBox3.SuspendLayout();
            lay_control.SuspendLayout();
            op_panel.SuspendLayout();
            SuspendLayout();
            // 
            // list_devicegroup
            // 
            list_devicegroup.FormattingEnabled = true;
            list_devicegroup.ItemHeight = 24;
            list_devicegroup.Location = new Point(12, 38);
            list_devicegroup.Name = "list_devicegroup";
            list_devicegroup.Size = new Size(180, 580);
            list_devicegroup.TabIndex = 0;
            list_devicegroup.SelectedIndexChanged += list_devicegroup_SelectedIndexChanged;
            // 
            // list_devices
            // 
            list_devices.FormattingEnabled = true;
            list_devices.ItemHeight = 24;
            list_devices.Location = new Point(198, 38);
            list_devices.Name = "list_devices";
            list_devices.Size = new Size(180, 580);
            list_devices.TabIndex = 1;
            list_devices.SelectedIndexChanged += list_devices_SelectedIndexChanged;
            // 
            // check_read_data
            // 
            check_read_data.AutoSize = true;
            check_read_data.Location = new Point(3, 114);
            check_read_data.Name = "check_read_data";
            check_read_data.Size = new Size(90, 28);
            check_read_data.TabIndex = 2;
            check_read_data.Text = "读数据";
            check_read_data.UseVisualStyleBackColor = true;
            // 
            // check_control_cmd
            // 
            check_control_cmd.AutoSize = true;
            check_control_cmd.Location = new Point(3, 3);
            check_control_cmd.Name = "check_control_cmd";
            check_control_cmd.Size = new Size(72, 28);
            check_control_cmd.TabIndex = 3;
            check_control_cmd.Text = "命令";
            check_control_cmd.UseVisualStyleBackColor = true;
            // 
            // check_write_baseinfo
            // 
            check_write_baseinfo.AutoSize = true;
            check_write_baseinfo.Location = new Point(150, 3);
            check_write_baseinfo.Name = "check_write_baseinfo";
            check_write_baseinfo.Size = new Size(126, 28);
            check_write_baseinfo.TabIndex = 4;
            check_write_baseinfo.Text = "改基础信息";
            check_write_baseinfo.UseVisualStyleBackColor = true;
            // 
            // check_control_timesetting
            // 
            check_control_timesetting.AutoSize = true;
            check_control_timesetting.Location = new Point(3, 63);
            check_control_timesetting.Name = "check_control_timesetting";
            check_control_timesetting.Size = new Size(108, 28);
            check_control_timesetting.TabIndex = 5;
            check_control_timesetting.Text = "定时配置";
            check_control_timesetting.UseVisualStyleBackColor = true;
            // 
            // check_read_repair
            // 
            check_read_repair.AutoSize = true;
            check_read_repair.Location = new Point(150, 3);
            check_read_repair.Name = "check_read_repair";
            check_read_repair.Size = new Size(126, 28);
            check_read_repair.TabIndex = 6;
            check_read_repair.Text = "读维修记录";
            check_read_repair.UseVisualStyleBackColor = true;
            // 
            // check_write_deletdata
            // 
            check_write_deletdata.AutoSize = true;
            check_write_deletdata.Location = new Point(3, 3);
            check_write_deletdata.Name = "check_write_deletdata";
            check_write_deletdata.Size = new Size(90, 28);
            check_write_deletdata.TabIndex = 7;
            check_write_deletdata.Text = "删数据";
            check_write_deletdata.UseVisualStyleBackColor = true;
            // 
            // check_read_status
            // 
            check_read_status.AutoSize = true;
            check_read_status.Location = new Point(3, 77);
            check_read_status.Name = "check_read_status";
            check_read_status.Size = new Size(90, 28);
            check_read_status.TabIndex = 8;
            check_read_status.Text = "读状态";
            check_read_status.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lay_read);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(300, 180);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "读";
            // 
            // lay_read
            // 
            lay_read.ColumnCount = 2;
            lay_read.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            lay_read.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            lay_read.Controls.Add(check_read_baseinfo, 0, 0);
            lay_read.Controls.Add(check_read_timesetting, 1, 1);
            lay_read.Controls.Add(check_read_status, 0, 2);
            lay_read.Controls.Add(check_read_cmd, 0, 1);
            lay_read.Controls.Add(check_read_repair, 1, 0);
            lay_read.Controls.Add(check_read_data, 0, 3);
            lay_read.Dock = DockStyle.Fill;
            lay_read.Location = new Point(3, 26);
            lay_read.Name = "lay_read";
            lay_read.RowCount = 4;
            lay_read.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            lay_read.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            lay_read.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            lay_read.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            lay_read.Size = new Size(294, 151);
            lay_read.TabIndex = 12;
            // 
            // check_read_baseinfo
            // 
            check_read_baseinfo.AutoSize = true;
            check_read_baseinfo.Location = new Point(3, 3);
            check_read_baseinfo.Name = "check_read_baseinfo";
            check_read_baseinfo.Size = new Size(126, 28);
            check_read_baseinfo.TabIndex = 9;
            check_read_baseinfo.Text = "读基础信息";
            check_read_baseinfo.UseVisualStyleBackColor = true;
            // 
            // check_read_timesetting
            // 
            check_read_timesetting.AutoSize = true;
            check_read_timesetting.Location = new Point(150, 40);
            check_read_timesetting.Name = "check_read_timesetting";
            check_read_timesetting.Size = new Size(126, 28);
            check_read_timesetting.TabIndex = 6;
            check_read_timesetting.Text = "读定时配置";
            check_read_timesetting.UseVisualStyleBackColor = true;
            // 
            // check_read_cmd
            // 
            check_read_cmd.AutoSize = true;
            check_read_cmd.Location = new Point(3, 40);
            check_read_cmd.Name = "check_read_cmd";
            check_read_cmd.Size = new Size(126, 28);
            check_read_cmd.TabIndex = 6;
            check_read_cmd.Text = "读历史命令";
            check_read_cmd.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lay_write);
            groupBox2.Location = new Point(3, 189);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(300, 150);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "写";
            // 
            // lay_write
            // 
            lay_write.ColumnCount = 2;
            lay_write.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            lay_write.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            lay_write.Controls.Add(check_write_deletdata, 0, 0);
            lay_write.Controls.Add(check_write_baseinfo, 1, 0);
            lay_write.Controls.Add(check_write_deletdevice, 0, 1);
            lay_write.Controls.Add(check_write_repair, 1, 1);
            lay_write.Controls.Add(check_write_type, 1, 2);
            lay_write.Dock = DockStyle.Fill;
            lay_write.Location = new Point(3, 26);
            lay_write.Name = "lay_write";
            lay_write.RowCount = 3;
            lay_write.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            lay_write.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            lay_write.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
            lay_write.Size = new Size(294, 121);
            lay_write.TabIndex = 12;
            // 
            // check_write_deletdevice
            // 
            check_write_deletdevice.AutoSize = true;
            check_write_deletdevice.Location = new Point(3, 39);
            check_write_deletdevice.Name = "check_write_deletdevice";
            check_write_deletdevice.Size = new Size(90, 28);
            check_write_deletdevice.TabIndex = 8;
            check_write_deletdevice.Text = "删设备";
            check_write_deletdevice.UseVisualStyleBackColor = true;
            // 
            // check_write_repair
            // 
            check_write_repair.AutoSize = true;
            check_write_repair.Location = new Point(150, 39);
            check_write_repair.Name = "check_write_repair";
            check_write_repair.Size = new Size(126, 28);
            check_write_repair.TabIndex = 7;
            check_write_repair.Text = "改维修记录";
            check_write_repair.UseVisualStyleBackColor = true;
            // 
            // check_write_type
            // 
            check_write_type.AutoSize = true;
            check_write_type.Location = new Point(150, 75);
            check_write_type.Name = "check_write_type";
            check_write_type.Size = new Size(126, 28);
            check_write_type.TabIndex = 9;
            check_write_type.Text = "改设备类型";
            check_write_type.UseVisualStyleBackColor = true;
            check_write_type.Visible = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(lay_control);
            groupBox3.Location = new Point(3, 345);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(300, 150);
            groupBox3.TabIndex = 11;
            groupBox3.TabStop = false;
            groupBox3.Text = "控制";
            // 
            // lay_control
            // 
            lay_control.ColumnCount = 2;
            lay_control.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            lay_control.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            lay_control.Controls.Add(check_control_cmd, 0, 0);
            lay_control.Controls.Add(check_control_timesetting, 0, 1);
            lay_control.Dock = DockStyle.Fill;
            lay_control.Location = new Point(3, 26);
            lay_control.Name = "lay_control";
            lay_control.RowCount = 2;
            lay_control.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            lay_control.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            lay_control.Size = new Size(294, 121);
            lay_control.TabIndex = 12;
            // 
            // check_delegate
            // 
            check_delegate.AutoSize = true;
            check_delegate.Location = new Point(9, 501);
            check_delegate.Name = "check_delegate";
            check_delegate.Size = new Size(72, 28);
            check_delegate.TabIndex = 9;
            check_delegate.Text = "转授";
            check_delegate.UseVisualStyleBackColor = true;
            // 
            // btn_sel_read
            // 
            btn_sel_read.Location = new Point(322, 119);
            btn_sel_read.Name = "btn_sel_read";
            btn_sel_read.Size = new Size(112, 34);
            btn_sel_read.TabIndex = 13;
            btn_sel_read.Text = "读";
            btn_sel_read.UseVisualStyleBackColor = true;
            btn_sel_read.Click += btn_sel_read_Click;
            // 
            // btn_sel_rw
            // 
            btn_sel_rw.Location = new Point(322, 158);
            btn_sel_rw.Name = "btn_sel_rw";
            btn_sel_rw.Size = new Size(112, 34);
            btn_sel_rw.TabIndex = 14;
            btn_sel_rw.Text = "读写";
            btn_sel_rw.UseVisualStyleBackColor = true;
            btn_sel_rw.Click += btn_sel_rw_Click;
            // 
            // btn_sel_rwc
            // 
            btn_sel_rwc.Location = new Point(322, 197);
            btn_sel_rwc.Name = "btn_sel_rwc";
            btn_sel_rwc.Size = new Size(112, 34);
            btn_sel_rwc.TabIndex = 15;
            btn_sel_rwc.Text = "读写控";
            btn_sel_rwc.UseVisualStyleBackColor = true;
            btn_sel_rwc.Click += btn_sel_rwc_Click;
            // 
            // btn_submit
            // 
            btn_submit.Location = new Point(322, 236);
            btn_submit.Name = "btn_submit";
            btn_submit.Size = new Size(112, 34);
            btn_submit.TabIndex = 16;
            btn_submit.Text = "确定";
            btn_submit.UseVisualStyleBackColor = true;
            btn_submit.Click += btn_submit_Click;
            // 
            // op_panel
            // 
            op_panel.BackColor = Color.FromArgb(192, 255, 255);
            op_panel.Controls.Add(groupBox1);
            op_panel.Controls.Add(check_delegate);
            op_panel.Controls.Add(groupBox2);
            op_panel.Controls.Add(btn_submit);
            op_panel.Controls.Add(groupBox3);
            op_panel.Controls.Add(btn_sel_rwc);
            op_panel.Controls.Add(btn_sel_rw);
            op_panel.Controls.Add(btn_sel_read);
            op_panel.Location = new Point(384, 38);
            op_panel.Name = "op_panel";
            op_panel.Size = new Size(468, 568);
            op_panel.TabIndex = 18;
            // 
            // FUserDevice
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(925, 639);
            Controls.Add(op_panel);
            Controls.Add(list_devices);
            Controls.Add(list_devicegroup);
            Name = "FUserDevice";
            Text = "FUserDevice";
            groupBox1.ResumeLayout(false);
            lay_read.ResumeLayout(false);
            lay_read.PerformLayout();
            groupBox2.ResumeLayout(false);
            lay_write.ResumeLayout(false);
            lay_write.PerformLayout();
            groupBox3.ResumeLayout(false);
            lay_control.ResumeLayout(false);
            lay_control.PerformLayout();
            op_panel.ResumeLayout(false);
            op_panel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListBox list_devicegroup;
        private ListBox list_devices;
        private CheckBox check_read_data;
        private CheckBox check_control_cmd;
        private CheckBox check_write_baseinfo;
        private CheckBox check_control_timesetting;
        private CheckBox check_read_repair;
        private CheckBox check_write_deletdata;
        private CheckBox check_read_status;
        private GroupBox groupBox1;
        private TableLayoutPanel lay_read;
        private CheckBox check_read_timesetting;
        private CheckBox check_read_cmd;
        private GroupBox groupBox2;
        private TableLayoutPanel lay_write;
        private CheckBox check_write_deletdevice;
        private CheckBox check_write_repair;
        private GroupBox groupBox3;
        private TableLayoutPanel lay_control;
        private ToolTip toolTip1;
        private CheckBox check_write_type;
        private CheckBox check_delegate;
        private Button btn_sel_read;
        private Button btn_sel_rw;
        private Button btn_sel_rwc;
        private Button btn_submit;
        private CheckBox check_read_baseinfo;
        private Panel op_panel;
    }
}