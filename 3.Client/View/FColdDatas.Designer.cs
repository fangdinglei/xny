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
            this.col_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_delet = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.cb_colddown = new System.Windows.Forms.ComboBox();
            this.cb_mincount = new System.Windows.Forms.ComboBox();
            this.c_opencolddata = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_savesetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_coldmanager = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.col_count,
            this.col_status,
            this.col_delet});
            this.dataGridView1.Location = new System.Drawing.Point(209, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.Size = new System.Drawing.Size(1024, 492);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // col_id
            // 
            this.col_id.DataPropertyName = "Id";
            this.col_id.HeaderText = "ID";
            this.col_id.MinimumWidth = 8;
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.Width = 150;
            // 
            // col_device
            // 
            this.col_device.DataPropertyName = "Device";
            this.col_device.HeaderText = "设备";
            this.col_device.MinimumWidth = 8;
            this.col_device.Name = "col_device";
            this.col_device.ReadOnly = true;
            this.col_device.Width = 150;
            // 
            // col_stream
            // 
            this.col_stream.DataPropertyName = "Stream";
            this.col_stream.HeaderText = "数据名称";
            this.col_stream.MinimumWidth = 8;
            this.col_stream.Name = "col_stream";
            this.col_stream.ReadOnly = true;
            this.col_stream.Width = 150;
            // 
            // col_count
            // 
            this.col_count.DataPropertyName = "Count";
            this.col_count.HeaderText = "数量";
            this.col_count.MinimumWidth = 8;
            this.col_count.Name = "col_count";
            this.col_count.ReadOnly = true;
            this.col_count.Width = 150;
            // 
            // col_status
            // 
            this.col_status.DataPropertyName = "Status";
            this.col_status.HeaderText = "状态";
            this.col_status.MinimumWidth = 8;
            this.col_status.Name = "col_status";
            this.col_status.ReadOnly = true;
            this.col_status.Width = 150;
            // 
            // col_delet
            // 
            this.col_delet.DataPropertyName = "OP1";
            this.col_delet.HeaderText = "操作1";
            this.col_delet.MinimumWidth = 8;
            this.col_delet.Name = "col_delet";
            this.col_delet.ReadOnly = true;
            this.col_delet.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "时间***";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 39);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 28);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "启用";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(12, 111);
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
            this.cb_colddown.Location = new System.Drawing.Point(1269, 89);
            this.cb_colddown.Name = "cb_colddown";
            this.cb_colddown.Size = new System.Drawing.Size(182, 32);
            this.cb_colddown.TabIndex = 4;
            this.cb_colddown.SelectedIndexChanged += new System.EventHandler(this.cb_colddown_SelectedIndexChanged_1);
            // 
            // cb_mincount
            // 
            this.cb_mincount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mincount.FormattingEnabled = true;
            this.cb_mincount.Location = new System.Drawing.Point(1269, 156);
            this.cb_mincount.Name = "cb_mincount";
            this.cb_mincount.Size = new System.Drawing.Size(182, 32);
            this.cb_mincount.TabIndex = 5;
            this.cb_mincount.SelectedIndexChanged += new System.EventHandler(this.cb_colddown_SelectedIndexChanged_1);
            // 
            // c_opencolddata
            // 
            this.c_opencolddata.AutoSize = true;
            this.c_opencolddata.Location = new System.Drawing.Point(1269, 22);
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
            this.label2.Location = new System.Drawing.Point(1269, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "冷却时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1269, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "最小数据量";
            // 
            // btn_savesetting
            // 
            this.btn_savesetting.Enabled = false;
            this.btn_savesetting.Location = new System.Drawing.Point(1281, 264);
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
            this.label3.Location = new System.Drawing.Point(1269, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "冷数据管理器";
            // 
            // cb_coldmanager
            // 
            this.cb_coldmanager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_coldmanager.FormattingEnabled = true;
            this.cb_coldmanager.Location = new System.Drawing.Point(1269, 226);
            this.cb_coldmanager.Name = "cb_coldmanager";
            this.cb_coldmanager.Size = new System.Drawing.Size(182, 32);
            this.cb_coldmanager.TabIndex = 11;
            this.cb_coldmanager.SelectedIndexChanged += new System.EventHandler(this.cb_colddown_SelectedIndexChanged_1);
            // 
            // FColdDatas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1515, 560);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_coldmanager);
            this.Controls.Add(this.btn_savesetting);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.c_opencolddata);
            this.Controls.Add(this.cb_mincount);
            this.Controls.Add(this.cb_colddown);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FColdDatas";
            this.Text = "FColdDatas";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private CheckBox checkBox1;
        private DataGridViewTextBoxColumn col_id;
        private DataGridViewTextBoxColumn col_device;
        private DataGridViewTextBoxColumn col_stream;
        private DataGridViewTextBoxColumn col_count;
        private DataGridViewTextBoxColumn col_status;
        private DataGridViewButtonColumn col_delet;
        private Button btn_search;
        private ComboBox cb_colddown;
        private ComboBox cb_mincount;
        private CheckBox c_opencolddata;
        private Label label2;
        private Label label4;
        private Button btn_savesetting;
        private Label label3;
        private ComboBox cb_coldmanager;
    }
}