namespace MyClient.View
{
    partial class FSystemInfo
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
            text_totalmemory = new TextBox();
            label3 = new Label();
            text_runtime = new TextBox();
            label4 = new Label();
            text_os = new TextBox();
            label2 = new Label();
            text_cpucount = new TextBox();
            label8 = new Label();
            text_pagesize = new TextBox();
            label7 = new Label();
            grid_userstatics = new DataGridView();
            btnCreatUser = new Button();
            col_id = new DataGridViewTextBoxColumn();
            col_subuser = new DataGridViewTextBoxColumn();
            col_totaldevice = new DataGridViewTextBoxColumn();
            col_devicetype = new DataGridViewTextBoxColumn();
            col_datapoints = new DataGridViewTextBoxColumn();
            col_topuser = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)grid_userstatics).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 18);
            label1.Name = "label1";
            label1.Size = new Size(46, 24);
            label1.TabIndex = 0;
            label1.Text = "编号";
            // 
            // text_id
            // 
            text_id.Location = new Point(102, 18);
            text_id.Name = "text_id";
            text_id.ReadOnly = true;
            text_id.Size = new Size(150, 30);
            text_id.TabIndex = 1;
            // 
            // text_totalmemory
            // 
            text_totalmemory.Location = new Point(102, 90);
            text_totalmemory.Name = "text_totalmemory";
            text_totalmemory.ReadOnly = true;
            text_totalmemory.Size = new Size(150, 30);
            text_totalmemory.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 90);
            label3.Name = "label3";
            label3.Size = new Size(46, 24);
            label3.TabIndex = 4;
            label3.Text = "内存";
            // 
            // text_runtime
            // 
            text_runtime.Location = new Point(102, 126);
            text_runtime.Name = "text_runtime";
            text_runtime.ReadOnly = true;
            text_runtime.Size = new Size(150, 30);
            text_runtime.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 126);
            label4.Name = "label4";
            label4.Size = new Size(82, 24);
            label4.TabIndex = 6;
            label4.Text = "运行时长";
            // 
            // text_os
            // 
            text_os.Location = new Point(102, 57);
            text_os.Name = "text_os";
            text_os.ReadOnly = true;
            text_os.Size = new Size(150, 30);
            text_os.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 57);
            label2.Name = "label2";
            label2.Size = new Size(46, 24);
            label2.TabIndex = 13;
            label2.Text = "系统";
            // 
            // text_cpucount
            // 
            text_cpucount.Location = new Point(102, 162);
            text_cpucount.Name = "text_cpucount";
            text_cpucount.ReadOnly = true;
            text_cpucount.Size = new Size(150, 30);
            text_cpucount.TabIndex = 16;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(14, 162);
            label8.Name = "label8";
            label8.Size = new Size(82, 24);
            label8.TabIndex = 15;
            label8.Text = "逻辑核心";
            // 
            // text_pagesize
            // 
            text_pagesize.Location = new Point(102, 198);
            text_pagesize.Name = "text_pagesize";
            text_pagesize.ReadOnly = true;
            text_pagesize.Size = new Size(150, 30);
            text_pagesize.TabIndex = 18;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 198);
            label7.Name = "label7";
            label7.Size = new Size(64, 24);
            label7.TabIndex = 17;
            label7.Text = "页大小";
            // 
            // grid_userstatics
            // 
            grid_userstatics.AllowUserToAddRows = false;
            grid_userstatics.AllowUserToDeleteRows = false;
            grid_userstatics.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid_userstatics.Columns.AddRange(new DataGridViewColumn[] { col_id, col_subuser, col_totaldevice, col_devicetype, col_datapoints, col_topuser });
            grid_userstatics.Location = new Point(258, 18);
            grid_userstatics.Name = "grid_userstatics";
            grid_userstatics.ReadOnly = true;
            grid_userstatics.RowHeadersWidth = 62;
            grid_userstatics.RowTemplate.Height = 32;
            grid_userstatics.Size = new Size(1047, 647);
            grid_userstatics.TabIndex = 19;
            // 
            // btnCreatUser
            // 
            btnCreatUser.Location = new Point(59, 267);
            btnCreatUser.Name = "btnCreatUser";
            btnCreatUser.Size = new Size(112, 34);
            btnCreatUser.TabIndex = 20;
            btnCreatUser.Text = "创建租户";
            btnCreatUser.UseVisualStyleBackColor = true;
            btnCreatUser.Click += btnCreatUser_Click;
            // 
            // col_id
            // 
            col_id.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_id.DataPropertyName = "TreeId";
            col_id.HeaderText = "ID";
            col_id.MinimumWidth = 8;
            col_id.Name = "col_id";
            col_id.ReadOnly = true;
            // 
            // col_subuser
            // 
            col_subuser.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_subuser.DataPropertyName = "SubUserCount";
            col_subuser.HeaderText = "子用户数量";
            col_subuser.MinimumWidth = 8;
            col_subuser.Name = "col_subuser";
            col_subuser.ReadOnly = true;
            // 
            // col_totaldevice
            // 
            col_totaldevice.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_totaldevice.DataPropertyName = "TotalDevice";
            col_totaldevice.HeaderText = "设备数量";
            col_totaldevice.MinimumWidth = 8;
            col_totaldevice.Name = "col_totaldevice";
            col_totaldevice.ReadOnly = true;
            // 
            // col_devicetype
            // 
            col_devicetype.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            col_devicetype.DataPropertyName = "TotalDeviceType";
            col_devicetype.HeaderText = "设备类型数量";
            col_devicetype.MinimumWidth = 8;
            col_devicetype.Name = "col_devicetype";
            col_devicetype.ReadOnly = true;
            col_devicetype.Width = 154;
            // 
            // col_datapoints
            // 
            col_datapoints.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_datapoints.DataPropertyName = "TotalDataPoint";
            col_datapoints.HeaderText = "数据数量";
            col_datapoints.MinimumWidth = 8;
            col_datapoints.Name = "col_datapoints";
            col_datapoints.ReadOnly = true;
            // 
            // col_topuser
            // 
            col_topuser.DataPropertyName = "TopUserId";
            col_topuser.HeaderText = "顶级用户";
            col_topuser.MinimumWidth = 8;
            col_topuser.Name = "col_topuser";
            col_topuser.ReadOnly = true;
            col_topuser.Width = 150;
            // 
            // FSystemInfo
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1317, 703);
            Controls.Add(btnCreatUser);
            Controls.Add(grid_userstatics);
            Controls.Add(text_pagesize);
            Controls.Add(label7);
            Controls.Add(text_cpucount);
            Controls.Add(label8);
            Controls.Add(text_os);
            Controls.Add(label2);
            Controls.Add(text_runtime);
            Controls.Add(label4);
            Controls.Add(text_totalmemory);
            Controls.Add(label3);
            Controls.Add(text_id);
            Controls.Add(label1);
            Name = "FSystemInfo";
            Text = "FSystemInfo";
            ((System.ComponentModel.ISupportInitialize)grid_userstatics).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox text_id;
        private TextBox text_totalmemory;
        private Label label3;
        private TextBox text_runtime;
        private Label label4;
        private TextBox text_os;
        private Label label2;
        private TextBox text_cpucount;
        private Label label8;
        private TextBox text_pagesize;
        private Label label7;
        private DataGridView grid_userstatics;
        private Button btnCreatUser;
        private DataGridViewTextBoxColumn col_id;
        private DataGridViewTextBoxColumn col_subuser;
        private DataGridViewTextBoxColumn col_totaldevice;
        private DataGridViewTextBoxColumn col_devicetype;
        private DataGridViewTextBoxColumn col_datapoints;
        private DataGridViewTextBoxColumn col_topuser;
    }
}