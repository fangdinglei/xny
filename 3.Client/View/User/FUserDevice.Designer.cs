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
            this.components = new System.ComponentModel.Container();
            this.list_devicegroup = new System.Windows.Forms.ListBox();
            this.list_devices = new System.Windows.Forms.ListBox();
            this.check_read_data = new System.Windows.Forms.CheckBox();
            this.check_control_cmd = new System.Windows.Forms.CheckBox();
            this.check_write_baseinfo = new System.Windows.Forms.CheckBox();
            this.check_control_timesetting = new System.Windows.Forms.CheckBox();
            this.check_read_repair = new System.Windows.Forms.CheckBox();
            this.check_write_data = new System.Windows.Forms.CheckBox();
            this.check_read_status = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lay_read = new System.Windows.Forms.TableLayoutPanel();
            this.check_read_timesetting = new System.Windows.Forms.CheckBox();
            this.check_read_cmd = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lay_write = new System.Windows.Forms.TableLayoutPanel();
            this.check_write_device = new System.Windows.Forms.CheckBox();
            this.check_write_repair = new System.Windows.Forms.CheckBox();
            this.check_write_type = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lay_control = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.check_delegate = new System.Windows.Forms.CheckBox();
            this.btn_sel_all = new System.Windows.Forms.Button();
            this.btn_sel_read = new System.Windows.Forms.Button();
            this.btn_sel_rw = new System.Windows.Forms.Button();
            this.btn_sel_rwc = new System.Windows.Forms.Button();
            this.btn_submit = new System.Windows.Forms.Button();
            this.check_quickop = new System.Windows.Forms.CheckBox();
            this.check_read_baseinfo = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.lay_read.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.lay_write.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.lay_control.SuspendLayout();
            this.SuspendLayout();
            // 
            // list_devicegroup
            // 
            this.list_devicegroup.FormattingEnabled = true;
            this.list_devicegroup.ItemHeight = 24;
            this.list_devicegroup.Location = new System.Drawing.Point(12, 38);
            this.list_devicegroup.Name = "list_devicegroup";
            this.list_devicegroup.Size = new System.Drawing.Size(180, 580);
            this.list_devicegroup.TabIndex = 0;
            // 
            // list_devices
            // 
            this.list_devices.FormattingEnabled = true;
            this.list_devices.ItemHeight = 24;
            this.list_devices.Location = new System.Drawing.Point(198, 41);
            this.list_devices.Name = "list_devices";
            this.list_devices.Size = new System.Drawing.Size(180, 580);
            this.list_devices.TabIndex = 1;
            // 
            // check_read_data
            // 
            this.check_read_data.AutoSize = true;
            this.check_read_data.Location = new System.Drawing.Point(3, 114);
            this.check_read_data.Name = "check_read_data";
            this.check_read_data.Size = new System.Drawing.Size(90, 28);
            this.check_read_data.TabIndex = 2;
            this.check_read_data.Text = "读数据";
            this.check_read_data.UseVisualStyleBackColor = true;
            // 
            // check_control_cmd
            // 
            this.check_control_cmd.AutoSize = true;
            this.check_control_cmd.Location = new System.Drawing.Point(3, 3);
            this.check_control_cmd.Name = "check_control_cmd";
            this.check_control_cmd.Size = new System.Drawing.Size(72, 28);
            this.check_control_cmd.TabIndex = 3;
            this.check_control_cmd.Text = "命令";
            this.check_control_cmd.UseVisualStyleBackColor = true;
            // 
            // check_write_baseinfo
            // 
            this.check_write_baseinfo.AutoSize = true;
            this.check_write_baseinfo.Location = new System.Drawing.Point(150, 3);
            this.check_write_baseinfo.Name = "check_write_baseinfo";
            this.check_write_baseinfo.Size = new System.Drawing.Size(126, 28);
            this.check_write_baseinfo.TabIndex = 4;
            this.check_write_baseinfo.Text = "改基础信息";
            this.check_write_baseinfo.UseVisualStyleBackColor = true;
            // 
            // check_control_timesetting
            // 
            this.check_control_timesetting.AutoSize = true;
            this.check_control_timesetting.Location = new System.Drawing.Point(3, 63);
            this.check_control_timesetting.Name = "check_control_timesetting";
            this.check_control_timesetting.Size = new System.Drawing.Size(108, 28);
            this.check_control_timesetting.TabIndex = 5;
            this.check_control_timesetting.Text = "定时配置";
            this.check_control_timesetting.UseVisualStyleBackColor = true;
            // 
            // check_read_repair
            // 
            this.check_read_repair.AutoSize = true;
            this.check_read_repair.Location = new System.Drawing.Point(150, 3);
            this.check_read_repair.Name = "check_read_repair";
            this.check_read_repair.Size = new System.Drawing.Size(126, 28);
            this.check_read_repair.TabIndex = 6;
            this.check_read_repair.Text = "读维修记录";
            this.check_read_repair.UseVisualStyleBackColor = true;
            // 
            // check_write_data
            // 
            this.check_write_data.AutoSize = true;
            this.check_write_data.Location = new System.Drawing.Point(3, 3);
            this.check_write_data.Name = "check_write_data";
            this.check_write_data.Size = new System.Drawing.Size(90, 28);
            this.check_write_data.TabIndex = 7;
            this.check_write_data.Text = "删数据";
            this.check_write_data.UseVisualStyleBackColor = true;
            // 
            // check_read_status
            // 
            this.check_read_status.AutoSize = true;
            this.check_read_status.Location = new System.Drawing.Point(3, 77);
            this.check_read_status.Name = "check_read_status";
            this.check_read_status.Size = new System.Drawing.Size(90, 28);
            this.check_read_status.TabIndex = 8;
            this.check_read_status.Text = "读状态";
            this.check_read_status.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lay_read);
            this.groupBox1.Location = new System.Drawing.Point(411, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 180);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "读";
            // 
            // lay_read
            // 
            this.lay_read.ColumnCount = 2;
            this.lay_read.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.lay_read.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.lay_read.Controls.Add(this.check_read_baseinfo, 0, 0);
            this.lay_read.Controls.Add(this.check_read_timesetting, 1, 1);
            this.lay_read.Controls.Add(this.check_read_status, 0, 2);
            this.lay_read.Controls.Add(this.check_read_cmd, 0, 1);
            this.lay_read.Controls.Add(this.check_read_repair, 1, 0);
            this.lay_read.Controls.Add(this.check_read_data, 0, 3);
            this.lay_read.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lay_read.Location = new System.Drawing.Point(3, 26);
            this.lay_read.Name = "lay_read";
            this.lay_read.RowCount = 4;
            this.lay_read.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.lay_read.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.lay_read.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.lay_read.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.lay_read.Size = new System.Drawing.Size(294, 151);
            this.lay_read.TabIndex = 12;
            // 
            // check_read_timesetting
            // 
            this.check_read_timesetting.AutoSize = true;
            this.check_read_timesetting.Location = new System.Drawing.Point(150, 40);
            this.check_read_timesetting.Name = "check_read_timesetting";
            this.check_read_timesetting.Size = new System.Drawing.Size(126, 28);
            this.check_read_timesetting.TabIndex = 6;
            this.check_read_timesetting.Text = "读定时配置";
            this.check_read_timesetting.UseVisualStyleBackColor = true;
            // 
            // check_read_cmd
            // 
            this.check_read_cmd.AutoSize = true;
            this.check_read_cmd.Location = new System.Drawing.Point(3, 40);
            this.check_read_cmd.Name = "check_read_cmd";
            this.check_read_cmd.Size = new System.Drawing.Size(126, 28);
            this.check_read_cmd.TabIndex = 6;
            this.check_read_cmd.Text = "读历史命令";
            this.check_read_cmd.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lay_write);
            this.groupBox2.Location = new System.Drawing.Point(411, 198);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 150);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "写";
            // 
            // lay_write
            // 
            this.lay_write.ColumnCount = 2;
            this.lay_write.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.lay_write.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.lay_write.Controls.Add(this.check_write_data, 0, 0);
            this.lay_write.Controls.Add(this.check_write_baseinfo, 1, 0);
            this.lay_write.Controls.Add(this.check_write_device, 0, 1);
            this.lay_write.Controls.Add(this.check_write_repair, 1, 1);
            this.lay_write.Controls.Add(this.check_write_type, 1, 2);
            this.lay_write.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lay_write.Location = new System.Drawing.Point(3, 26);
            this.lay_write.Name = "lay_write";
            this.lay_write.RowCount = 3;
            this.lay_write.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.lay_write.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.lay_write.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.lay_write.Size = new System.Drawing.Size(294, 121);
            this.lay_write.TabIndex = 12;
            // 
            // check_write_device
            // 
            this.check_write_device.AutoSize = true;
            this.check_write_device.Location = new System.Drawing.Point(3, 39);
            this.check_write_device.Name = "check_write_device";
            this.check_write_device.Size = new System.Drawing.Size(90, 28);
            this.check_write_device.TabIndex = 8;
            this.check_write_device.Text = "删设备";
            this.check_write_device.UseVisualStyleBackColor = true;
            // 
            // check_write_repair
            // 
            this.check_write_repair.AutoSize = true;
            this.check_write_repair.Location = new System.Drawing.Point(150, 39);
            this.check_write_repair.Name = "check_write_repair";
            this.check_write_repair.Size = new System.Drawing.Size(126, 28);
            this.check_write_repair.TabIndex = 7;
            this.check_write_repair.Text = "改维修记录";
            this.check_write_repair.UseVisualStyleBackColor = true;
            // 
            // check_write_type
            // 
            this.check_write_type.AutoSize = true;
            this.check_write_type.Location = new System.Drawing.Point(150, 75);
            this.check_write_type.Name = "check_write_type";
            this.check_write_type.Size = new System.Drawing.Size(126, 28);
            this.check_write_type.TabIndex = 9;
            this.check_write_type.Text = "改设备类型";
            this.check_write_type.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lay_control);
            this.groupBox3.Location = new System.Drawing.Point(411, 354);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(300, 150);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "控制";
            // 
            // lay_control
            // 
            this.lay_control.ColumnCount = 2;
            this.lay_control.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.lay_control.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.lay_control.Controls.Add(this.check_control_cmd, 0, 0);
            this.lay_control.Controls.Add(this.check_control_timesetting, 0, 1);
            this.lay_control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lay_control.Location = new System.Drawing.Point(3, 26);
            this.lay_control.Name = "lay_control";
            this.lay_control.RowCount = 2;
            this.lay_control.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.lay_control.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.lay_control.Size = new System.Drawing.Size(294, 121);
            this.lay_control.TabIndex = 12;
            // 
            // check_delegate
            // 
            this.check_delegate.AutoSize = true;
            this.check_delegate.Location = new System.Drawing.Point(417, 524);
            this.check_delegate.Name = "check_delegate";
            this.check_delegate.Size = new System.Drawing.Size(72, 28);
            this.check_delegate.TabIndex = 9;
            this.check_delegate.Text = "转授";
            this.check_delegate.UseVisualStyleBackColor = true;
            // 
            // btn_sel_all
            // 
            this.btn_sel_all.Location = new System.Drawing.Point(730, 89);
            this.btn_sel_all.Name = "btn_sel_all";
            this.btn_sel_all.Size = new System.Drawing.Size(112, 34);
            this.btn_sel_all.TabIndex = 12;
            this.btn_sel_all.Text = "全选";
            this.btn_sel_all.UseVisualStyleBackColor = true;
            // 
            // btn_sel_read
            // 
            this.btn_sel_read.Location = new System.Drawing.Point(730, 128);
            this.btn_sel_read.Name = "btn_sel_read";
            this.btn_sel_read.Size = new System.Drawing.Size(112, 34);
            this.btn_sel_read.TabIndex = 13;
            this.btn_sel_read.Text = "读";
            this.btn_sel_read.UseVisualStyleBackColor = true;
            this.btn_sel_read.Click += new System.EventHandler(this.btn_sel_read_Click);
            // 
            // btn_sel_rw
            // 
            this.btn_sel_rw.Location = new System.Drawing.Point(730, 167);
            this.btn_sel_rw.Name = "btn_sel_rw";
            this.btn_sel_rw.Size = new System.Drawing.Size(112, 34);
            this.btn_sel_rw.TabIndex = 14;
            this.btn_sel_rw.Text = "读写";
            this.btn_sel_rw.UseVisualStyleBackColor = true;
            this.btn_sel_rw.Click += new System.EventHandler(this.btn_sel_rw_Click);
            // 
            // btn_sel_rwc
            // 
            this.btn_sel_rwc.Location = new System.Drawing.Point(730, 206);
            this.btn_sel_rwc.Name = "btn_sel_rwc";
            this.btn_sel_rwc.Size = new System.Drawing.Size(112, 34);
            this.btn_sel_rwc.TabIndex = 15;
            this.btn_sel_rwc.Text = "读写控";
            this.btn_sel_rwc.UseVisualStyleBackColor = true;
            this.btn_sel_rwc.Click += new System.EventHandler(this.btn_sel_rwc_Click);
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(730, 245);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(112, 34);
            this.btn_submit.TabIndex = 16;
            this.btn_submit.Text = "确定";
            this.btn_submit.UseVisualStyleBackColor = true;
            // 
            // check_quickop
            // 
            this.check_quickop.AutoSize = true;
            this.check_quickop.Location = new System.Drawing.Point(734, 285);
            this.check_quickop.Name = "check_quickop";
            this.check_quickop.Size = new System.Drawing.Size(108, 28);
            this.check_quickop.TabIndex = 17;
            this.check_quickop.Text = "快捷操作";
            this.check_quickop.UseVisualStyleBackColor = true;
            // 
            // check_read_baseinfo
            // 
            this.check_read_baseinfo.AutoSize = true;
            this.check_read_baseinfo.Location = new System.Drawing.Point(3, 3);
            this.check_read_baseinfo.Name = "check_read_baseinfo";
            this.check_read_baseinfo.Size = new System.Drawing.Size(126, 28);
            this.check_read_baseinfo.TabIndex = 9;
            this.check_read_baseinfo.Text = "读基础信息";
            this.check_read_baseinfo.UseVisualStyleBackColor = true;
            // 
            // FUserDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 639);
            this.Controls.Add(this.check_quickop);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.btn_sel_rwc);
            this.Controls.Add(this.btn_sel_rw);
            this.Controls.Add(this.btn_sel_read);
            this.Controls.Add(this.btn_sel_all);
            this.Controls.Add(this.check_delegate);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.list_devices);
            this.Controls.Add(this.list_devicegroup);
            this.Name = "FUserDevice";
            this.Text = "FUserDevice";
            this.groupBox1.ResumeLayout(false);
            this.lay_read.ResumeLayout(false);
            this.lay_read.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.lay_write.ResumeLayout(false);
            this.lay_write.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.lay_control.ResumeLayout(false);
            this.lay_control.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox list_devicegroup;
        private ListBox list_devices;
        private CheckBox check_read_data;
        private CheckBox check_control_cmd;
        private CheckBox check_write_baseinfo;
        private CheckBox check_control_timesetting;
        private CheckBox check_read_repair;
        private CheckBox check_write_data;
        private CheckBox check_read_status;
        private GroupBox groupBox1;
        private TableLayoutPanel lay_read;
        private CheckBox check_read_timesetting;
        private CheckBox check_read_cmd;
        private GroupBox groupBox2;
        private TableLayoutPanel lay_write;
        private CheckBox check_write_device;
        private CheckBox check_write_repair;
        private GroupBox groupBox3;
        private TableLayoutPanel lay_control;
        private ToolTip toolTip1;
        private CheckBox check_write_type;
        private CheckBox check_delegate;
        private Button btn_sel_all;
        private Button btn_sel_read;
        private Button btn_sel_rw;
        private Button btn_sel_rwc;
        private Button btn_submit;
        private CheckBox check_quickop;
        private CheckBox check_read_baseinfo;
    }
}