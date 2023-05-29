namespace MyClient.View.Device
{
    partial class FDeviceDetail
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
            label1 = new Label();
            text_id = new TextBox();
            text_name = new TextBox();
            label2 = new Label();
            text_status = new TextBox();
            label3 = new Label();
            label4 = new Label();
            text_type = new TextBox();
            btn_sendcmd = new Button();
            button2 = new Button();
            btn_typeinfo = new Button();
            btn_deletdevice = new Button();
            btn_commit = new Button();
            chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            btn_repair = new Button();
            btn_timesetting = new Button();
            btnCmdHistory = new Button();
            text_AlertEmail = new TextBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(46, 24);
            label1.TabIndex = 0;
            label1.Text = "编号";
            // 
            // text_id
            // 
            text_id.Location = new Point(112, 12);
            text_id.Name = "text_id";
            text_id.ReadOnly = true;
            text_id.Size = new Size(150, 30);
            text_id.TabIndex = 1;
            // 
            // text_name
            // 
            text_name.Location = new Point(112, 48);
            text_name.Name = "text_name";
            text_name.Size = new Size(150, 30);
            text_name.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 51);
            label2.Name = "label2";
            label2.Size = new Size(46, 24);
            label2.TabIndex = 2;
            label2.Text = "名称";
            // 
            // text_status
            // 
            text_status.Location = new Point(112, 84);
            text_status.Name = "text_status";
            text_status.Size = new Size(150, 30);
            text_status.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 87);
            label3.Name = "label3";
            label3.Size = new Size(46, 24);
            label3.TabIndex = 4;
            label3.Text = "状态";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 156);
            label4.Name = "label4";
            label4.Size = new Size(82, 24);
            label4.TabIndex = 7;
            label4.Text = "设备类型";
            // 
            // text_type
            // 
            text_type.Location = new Point(112, 156);
            text_type.Name = "text_type";
            text_type.Size = new Size(150, 30);
            text_type.TabIndex = 8;
            // 
            // btn_sendcmd
            // 
            btn_sendcmd.Location = new Point(12, 281);
            btn_sendcmd.Name = "btn_sendcmd";
            btn_sendcmd.Size = new Size(112, 34);
            btn_sendcmd.TabIndex = 9;
            btn_sendcmd.Text = "发送命令";
            btn_sendcmd.UseVisualStyleBackColor = true;
            btn_sendcmd.Click += btn_sendcmd_Click;
            // 
            // button2
            // 
            button2.Location = new Point(130, 361);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 10;
            button2.Text = "更多设置";
            button2.UseVisualStyleBackColor = true;
            // 
            // btn_typeinfo
            // 
            btn_typeinfo.Location = new Point(130, 281);
            btn_typeinfo.Name = "btn_typeinfo";
            btn_typeinfo.Size = new Size(112, 34);
            btn_typeinfo.TabIndex = 11;
            btn_typeinfo.Text = "设备类型";
            btn_typeinfo.UseVisualStyleBackColor = true;
            btn_typeinfo.Click += btn_typeinfo_Click;
            // 
            // btn_deletdevice
            // 
            btn_deletdevice.Location = new Point(130, 321);
            btn_deletdevice.Name = "btn_deletdevice";
            btn_deletdevice.Size = new Size(112, 34);
            btn_deletdevice.TabIndex = 12;
            btn_deletdevice.Text = "删除设备";
            btn_deletdevice.UseVisualStyleBackColor = true;
            btn_deletdevice.Click += btn_deletdevice_Click;
            // 
            // btn_commit
            // 
            btn_commit.Location = new Point(55, 203);
            btn_commit.Name = "btn_commit";
            btn_commit.Size = new Size(112, 34);
            btn_commit.TabIndex = 13;
            btn_commit.Text = "提交更新";
            btn_commit.UseVisualStyleBackColor = true;
            btn_commit.Click += btn_commit_Click;
            // 
            // chromiumWebBrowser1
            // 
            chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            chromiumWebBrowser1.Location = new Point(289, 12);
            chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            chromiumWebBrowser1.Size = new Size(972, 634);
            chromiumWebBrowser1.TabIndex = 14;
            chromiumWebBrowser1.Text = "chromiumWebBrowser1";
            // 
            // btn_repair
            // 
            btn_repair.Location = new Point(12, 321);
            btn_repair.Name = "btn_repair";
            btn_repair.Size = new Size(112, 34);
            btn_repair.TabIndex = 15;
            btn_repair.Text = "维修记录";
            btn_repair.UseVisualStyleBackColor = true;
            btn_repair.Click += btn_repair_Click;
            // 
            // btn_timesetting
            // 
            btn_timesetting.Location = new Point(12, 361);
            btn_timesetting.Name = "btn_timesetting";
            btn_timesetting.Size = new Size(112, 34);
            btn_timesetting.TabIndex = 16;
            btn_timesetting.Text = "定时任务";
            btn_timesetting.UseVisualStyleBackColor = true;
            btn_timesetting.Click += btn_timesetting_Click;
            // 
            // btnCmdHistory
            // 
            btnCmdHistory.Location = new Point(12, 401);
            btnCmdHistory.Name = "btnCmdHistory";
            btnCmdHistory.Size = new Size(230, 34);
            btnCmdHistory.TabIndex = 17;
            btnCmdHistory.Text = "设备命令历史记录";
            btnCmdHistory.UseVisualStyleBackColor = true;
            btnCmdHistory.Click += btnCmdHistory_Click;
            // 
            // text_AlertEmail
            // 
            text_AlertEmail.Location = new Point(112, 120);
            text_AlertEmail.Name = "text_AlertEmail";
            text_AlertEmail.Size = new Size(150, 30);
            text_AlertEmail.TabIndex = 19;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 123);
            label5.Name = "label5";
            label5.Size = new Size(82, 24);
            label5.TabIndex = 18;
            label5.Text = "预警邮箱";
            // 
            // FDeviceDetail
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1273, 671);
            Controls.Add(text_AlertEmail);
            Controls.Add(label5);
            Controls.Add(btnCmdHistory);
            Controls.Add(btn_timesetting);
            Controls.Add(btn_repair);
            Controls.Add(chromiumWebBrowser1);
            Controls.Add(btn_commit);
            Controls.Add(btn_deletdevice);
            Controls.Add(btn_typeinfo);
            Controls.Add(button2);
            Controls.Add(btn_sendcmd);
            Controls.Add(text_type);
            Controls.Add(label4);
            Controls.Add(text_status);
            Controls.Add(label3);
            Controls.Add(text_name);
            Controls.Add(label2);
            Controls.Add(text_id);
            Controls.Add(label1);
            Name = "FDeviceDetail";
            Text = "FDeviceDetail";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox text_id;
        private TextBox text_name;
        private Label label2;
        private TextBox text_status;
        private Label label3;
        private Label label4;
        private TextBox text_type;
        private Button btn_sendcmd;
        private Button button2;
        private Button btn_typeinfo;
        private Button btn_deletdevice;
        private Button btn_commit;
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
        private Button btn_repair;
        private Button btn_timesetting;
        private Button btnCmdHistory;
        private TextBox text_AlertEmail;
        private Label label5;
    }
}