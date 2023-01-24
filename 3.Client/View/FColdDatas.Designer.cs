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
            this.dataGridView1.Location = new System.Drawing.Point(160, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.Size = new System.Drawing.Size(976, 492);
            this.dataGridView1.TabIndex = 0;
            // 
            // col_id
            // 
            this.col_id.HeaderText = "ID";
            this.col_id.MinimumWidth = 8;
            this.col_id.Name = "col_id";
            this.col_id.Width = 150;
            // 
            // col_device
            // 
            this.col_device.HeaderText = "设备";
            this.col_device.MinimumWidth = 8;
            this.col_device.Name = "col_device";
            this.col_device.Width = 150;
            // 
            // col_stream
            // 
            this.col_stream.HeaderText = "数据名称";
            this.col_stream.MinimumWidth = 8;
            this.col_stream.Name = "col_stream";
            this.col_stream.Width = 150;
            // 
            // col_count
            // 
            this.col_count.HeaderText = "数量";
            this.col_count.MinimumWidth = 8;
            this.col_count.Name = "col_count";
            this.col_count.Width = 150;
            // 
            // col_status
            // 
            this.col_status.HeaderText = "状态";
            this.col_status.MinimumWidth = 8;
            this.col_status.Name = "col_status";
            this.col_status.Width = 150;
            // 
            // col_delet
            // 
            this.col_delet.HeaderText = "操作1";
            this.col_delet.MinimumWidth = 8;
            this.col_delet.Name = "col_delet";
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
            // FColdDatas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 548);
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
        private DataGridViewTextBoxColumn col_id;
        private DataGridViewTextBoxColumn col_device;
        private DataGridViewTextBoxColumn col_stream;
        private DataGridViewTextBoxColumn col_count;
        private DataGridViewTextBoxColumn col_status;
        private DataGridViewButtonColumn col_delet;
        private Label label1;
        private CheckBox checkBox1;
    }
}