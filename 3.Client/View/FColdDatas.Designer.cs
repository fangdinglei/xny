namespace MyClient.View
{
    partial class FColdDatas
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_device = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_stream = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_creattime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_starttime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_endtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_manager = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_delet = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.c_timesearch = new System.Windows.Forms.CheckBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.cb_colddown = new System.Windows.Forms.ComboBox();
            this.cb_mincount = new System.Windows.Forms.ComboBox();
            this.c_opencolddata = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_savesetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_coldmanager = new System.Windows.Forms.ComboBox();
            this.pcoldsetting = new System.Windows.Forms.Panel();
            this.c_device = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pcoldsetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_id,
            this.col_device,
            this.col_stream,
            this.col_creattime,
            this.col_starttime,
            this.col_endtime,
            this.col_count,
            this.col_manager,
            this.col_status,
            this.col_delet});
            this.dataGridView1.Location = new System.Drawing.Point(130, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.Size = new System.Drawing.Size(1103, 492);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // col_id
            // 
            this.col_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_id.DataPropertyName = "Id";
            this.col_id.HeaderText = "ID";
            this.col_id.MinimumWidth = 8;
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.Width = 65;
            // 
            // col_device
            // 
            this.col_device.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_device.DataPropertyName = "Device";
            this.col_device.HeaderText = "设备";
            this.col_device.MinimumWidth = 8;
            this.col_device.Name = "col_device";
            this.col_device.ReadOnly = true;
            this.col_device.Width = 82;
            // 
            // col_stream
            // 
            this.col_stream.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_stream.DataPropertyName = "Stream";
            this.col_stream.HeaderText = "数据名称";
            this.col_stream.MinimumWidth = 8;
            this.col_stream.Name = "col_stream";
            this.col_stream.ReadOnly = true;
            this.col_stream.Width = 118;
            // 
            // col_creattime
            // 
            this.col_creattime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_creattime.DataPropertyName = "CreatTime";
            this.col_creattime.HeaderText = "创建时间";
            this.col_creattime.MinimumWidth = 8;
            this.col_creattime.Name = "col_creattime";
            this.col_creattime.ReadOnly = true;
            this.col_creattime.Width = 118;
            // 
            // col_starttime
            // 
            this.col_starttime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_starttime.DataPropertyName = "StartTime";
            this.col_starttime.HeaderText = "数据开始";
            this.col_starttime.MinimumWidth = 8;
            this.col_starttime.Name = "col_starttime";
            this.col_starttime.ReadOnly = true;
            this.col_starttime.ToolTipText = "第一个数据的时间";
            this.col_starttime.Width = 118;
            // 
            // col_endtime
            // 
            this.col_endtime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_endtime.DataPropertyName = "EndTime";
            this.col_endtime.HeaderText = "数据结束";
            this.col_endtime.MinimumWidth = 8;
            this.col_endtime.Name = "col_endtime";
            this.col_endtime.ReadOnly = true;
            this.col_endtime.ToolTipText = "最后一个数据的时间";
            this.col_endtime.Width = 118;
            // 
            // col_count
            // 
            this.col_count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_count.DataPropertyName = "Count";
            this.col_count.HeaderText = "数量";
            this.col_count.MinimumWidth = 8;
            this.col_count.Name = "col_count";
            this.col_count.ReadOnly = true;
            this.col_count.Width = 82;
            // 
            // col_manager
            // 
            this.col_manager.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_manager.DataPropertyName = "ManagerName";
            this.col_manager.HeaderText = "管理器";
            this.col_manager.MinimumWidth = 8;
            this.col_manager.Name = "col_manager";
            this.col_manager.ReadOnly = true;
            this.col_manager.ToolTipText = "冷数据管理器名称";
            // 
            // col_status
            // 
            this.col_status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_status.DataPropertyName = "Status";
            this.col_status.HeaderText = "状态";
            this.col_status.MinimumWidth = 8;
            this.col_status.Name = "col_status";
            this.col_status.ReadOnly = true;
            this.col_status.Width = 82;
            // 
            // col_delet
            // 
            this.col_delet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_delet.DataPropertyName = "OP1";
            this.col_delet.HeaderText = "操作1";
            this.col_delet.MinimumWidth = 8;
            this.col_delet.Name = "col_delet";
            this.col_delet.ReadOnly = true;
            this.col_delet.Width = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "时间筛选器";
            // 
            // c_timesearch
            // 
            this.c_timesearch.AutoSize = true;
            this.c_timesearch.Location = new System.Drawing.Point(12, 39);
            this.c_timesearch.Name = "c_timesearch";
            this.c_timesearch.Size = new System.Drawing.Size(72, 28);
            this.c_timesearch.TabIndex = 2;
            this.c_timesearch.Text = "启用";
            this.c_timesearch.UseVisualStyleBackColor = true;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(12, 142);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(112, 34);
            this.btn_search.TabIndex = 3;
            this.btn_search.Text = "搜索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // cb_colddown
            // 
            this.cb_colddown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_colddown.FormattingEnabled = true;
            this.cb_colddown.Items.AddRange(new object[] {
            "10天",
            "30天",
            "90天"});
            this.cb_colddown.Location = new System.Drawing.Point(3, 79);
            this.cb_colddown.Name = "cb_colddown";
            this.cb_colddown.Size = new System.Drawing.Size(182, 32);
            this.cb_colddown.TabIndex = 4;
            this.cb_colddown.SelectedIndexChanged += new System.EventHandler(this.cb_colddown_SelectedIndexChanged_1);
            // 
            // cb_mincount
            // 
            this.cb_mincount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mincount.FormattingEnabled = true;
            this.cb_mincount.Location = new System.Drawing.Point(3, 146);
            this.cb_mincount.Name = "cb_mincount";
            this.cb_mincount.Size = new System.Drawing.Size(182, 32);
            this.cb_mincount.TabIndex = 5;
            this.cb_mincount.SelectedIndexChanged += new System.EventHandler(this.cb_colddown_SelectedIndexChanged_1);
            // 
            // c_opencolddata
            // 
            this.c_opencolddata.AutoSize = true;
            this.c_opencolddata.Location = new System.Drawing.Point(3, 12);
            this.c_opencolddata.Name = "c_opencolddata";
            this.c_opencolddata.Size = new System.Drawing.Size(72, 28);
            this.c_opencolddata.TabIndex = 6;
            this.c_opencolddata.Text = "启用";
            this.c_opencolddata.UseVisualStyleBackColor = true;
            this.c_opencolddata.CheckStateChanged += new System.EventHandler(this.c_opencolddata_CheckStateChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "冷却时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "最小数据量";
            // 
            // btn_savesetting
            // 
            this.btn_savesetting.Enabled = false;
            this.btn_savesetting.Location = new System.Drawing.Point(49, 254);
            this.btn_savesetting.Name = "btn_savesetting";
            this.btn_savesetting.Size = new System.Drawing.Size(98, 40);
            this.btn_savesetting.TabIndex = 10;
            this.btn_savesetting.Text = "保存";
            this.btn_savesetting.UseVisualStyleBackColor = true;
            this.btn_savesetting.Click += new System.EventHandler(this.btn_savesetting_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "冷数据管理器";
            // 
            // cb_coldmanager
            // 
            this.cb_coldmanager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_coldmanager.FormattingEnabled = true;
            this.cb_coldmanager.Location = new System.Drawing.Point(3, 216);
            this.cb_coldmanager.Name = "cb_coldmanager";
            this.cb_coldmanager.Size = new System.Drawing.Size(182, 32);
            this.cb_coldmanager.TabIndex = 11;
            this.cb_coldmanager.SelectedIndexChanged += new System.EventHandler(this.cb_colddown_SelectedIndexChanged_1);
            // 
            // pcoldsetting
            // 
            this.pcoldsetting.Controls.Add(this.cb_colddown);
            this.pcoldsetting.Controls.Add(this.label3);
            this.pcoldsetting.Controls.Add(this.cb_mincount);
            this.pcoldsetting.Controls.Add(this.cb_coldmanager);
            this.pcoldsetting.Controls.Add(this.c_opencolddata);
            this.pcoldsetting.Controls.Add(this.btn_savesetting);
            this.pcoldsetting.Controls.Add(this.label2);
            this.pcoldsetting.Controls.Add(this.label4);
            this.pcoldsetting.Location = new System.Drawing.Point(1249, 12);
            this.pcoldsetting.Name = "pcoldsetting";
            this.pcoldsetting.Size = new System.Drawing.Size(193, 304);
            this.pcoldsetting.TabIndex = 13;
            // 
            // c_device
            // 
            this.c_device.AutoSize = true;
            this.c_device.Location = new System.Drawing.Point(12, 97);
            this.c_device.Name = "c_device";
            this.c_device.Size = new System.Drawing.Size(72, 28);
            this.c_device.TabIndex = 15;
            this.c_device.Text = "启用";
            this.c_device.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 24);
            this.label5.TabIndex = 14;
            this.label5.Text = "设备筛选器";
            // 
            // FColdDatas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1515, 560);
            this.Controls.Add(this.c_device);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pcoldsetting);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.c_timesearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FColdDatas";
            this.Text = "FColdDatas";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pcoldsetting.ResumeLayout(false);
            this.pcoldsetting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private CheckBox c_timesearch;
        private Button btn_search;
        private ComboBox cb_colddown;
        private ComboBox cb_mincount;
        private CheckBox c_opencolddata;
        private Label label2;
        private Label label4;
        private Button btn_savesetting;
        private Label label3;
        private ComboBox cb_coldmanager;
        private Panel pcoldsetting;
        private DataGridViewTextBoxColumn col_id;
        private DataGridViewTextBoxColumn col_device;
        private DataGridViewTextBoxColumn col_stream;
        private DataGridViewTextBoxColumn col_creattime;
        private DataGridViewTextBoxColumn col_starttime;
        private DataGridViewTextBoxColumn col_endtime;
        private DataGridViewTextBoxColumn col_count;
        private DataGridViewTextBoxColumn col_manager;
        private DataGridViewTextBoxColumn col_status;
        private DataGridViewButtonColumn col_delet;
        private CheckBox c_device;
        private Label label5;
    }
}