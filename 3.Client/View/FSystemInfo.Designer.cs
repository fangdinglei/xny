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
            this.label1 = new System.Windows.Forms.Label();
            this.text_id = new System.Windows.Forms.TextBox();
            this.text_totalmemory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.text_runtime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.text_os = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.text_cpucount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.text_pagesize = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_subuser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_totaldevice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_devicetype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_datapoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "编号";
            // 
            // text_id
            // 
            this.text_id.Location = new System.Drawing.Point(102, 18);
            this.text_id.Name = "text_id";
            this.text_id.ReadOnly = true;
            this.text_id.Size = new System.Drawing.Size(150, 30);
            this.text_id.TabIndex = 1;
            // 
            // text_totalmemory
            // 
            this.text_totalmemory.Location = new System.Drawing.Point(102, 90);
            this.text_totalmemory.Name = "text_totalmemory";
            this.text_totalmemory.ReadOnly = true;
            this.text_totalmemory.Size = new System.Drawing.Size(150, 30);
            this.text_totalmemory.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "内存";
            // 
            // text_runtime
            // 
            this.text_runtime.Location = new System.Drawing.Point(102, 126);
            this.text_runtime.Name = "text_runtime";
            this.text_runtime.ReadOnly = true;
            this.text_runtime.Size = new System.Drawing.Size(150, 30);
            this.text_runtime.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "运行时长";
            // 
            // text_os
            // 
            this.text_os.Location = new System.Drawing.Point(102, 57);
            this.text_os.Name = "text_os";
            this.text_os.ReadOnly = true;
            this.text_os.Size = new System.Drawing.Size(150, 30);
            this.text_os.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 24);
            this.label2.TabIndex = 13;
            this.label2.Text = "系统";
            // 
            // text_cpucount
            // 
            this.text_cpucount.Location = new System.Drawing.Point(102, 162);
            this.text_cpucount.Name = "text_cpucount";
            this.text_cpucount.ReadOnly = true;
            this.text_cpucount.Size = new System.Drawing.Size(150, 30);
            this.text_cpucount.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 24);
            this.label8.TabIndex = 15;
            this.label8.Text = "逻辑核心";
            // 
            // text_pagesize
            // 
            this.text_pagesize.Location = new System.Drawing.Point(102, 198);
            this.text_pagesize.Name = "text_pagesize";
            this.text_pagesize.ReadOnly = true;
            this.text_pagesize.Size = new System.Drawing.Size(150, 30);
            this.text_pagesize.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 24);
            this.label7.TabIndex = 17;
            this.label7.Text = "页大小";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_id,
            this.col_subuser,
            this.col_totaldevice,
            this.col_devicetype,
            this.col_datapoints});
            this.dataGridView1.Location = new System.Drawing.Point(258, 18);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.Size = new System.Drawing.Size(1047, 647);
            this.dataGridView1.TabIndex = 19;
            // 
            // col_id
            // 
            this.col_id.HeaderText = "ID";
            this.col_id.MinimumWidth = 8;
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.Width = 150;
            // 
            // col_subuser
            // 
            this.col_subuser.HeaderText = "子用户数量";
            this.col_subuser.MinimumWidth = 8;
            this.col_subuser.Name = "col_subuser";
            this.col_subuser.ReadOnly = true;
            this.col_subuser.Width = 150;
            // 
            // col_totaldevice
            // 
            this.col_totaldevice.HeaderText = "设备数量";
            this.col_totaldevice.MinimumWidth = 8;
            this.col_totaldevice.Name = "col_totaldevice";
            this.col_totaldevice.ReadOnly = true;
            this.col_totaldevice.Width = 150;
            // 
            // col_devicetype
            // 
            this.col_devicetype.HeaderText = "设备类型数量";
            this.col_devicetype.MinimumWidth = 8;
            this.col_devicetype.Name = "col_devicetype";
            this.col_devicetype.ReadOnly = true;
            this.col_devicetype.Width = 150;
            // 
            // col_datapoints
            // 
            this.col_datapoints.HeaderText = "数据数量";
            this.col_datapoints.MinimumWidth = 8;
            this.col_datapoints.Name = "col_datapoints";
            this.col_datapoints.ReadOnly = true;
            this.col_datapoints.Width = 150;
            // 
            // FSystemInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 703);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.text_pagesize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.text_cpucount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.text_os);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.text_runtime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.text_totalmemory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text_id);
            this.Controls.Add(this.label1);
            this.Name = "FSystemInfo";
            this.Text = "FSystemInfo";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn col_id;
        private DataGridViewTextBoxColumn col_subuser;
        private DataGridViewTextBoxColumn col_totaldevice;
        private DataGridViewTextBoxColumn col_devicetype;
        private DataGridViewTextBoxColumn col_datapoints;
    }
}