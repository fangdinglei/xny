namespace MyClient.View.Ext
{
    partial class FDeviceSelector
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
            this.list_devices = new System.Windows.Forms.ListBox();
            this.list_devicegroup = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_ok = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // list_devices
            // 
            this.list_devices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_devices.FormattingEnabled = true;
            this.list_devices.ItemHeight = 24;
            this.list_devices.Location = new System.Drawing.Point(431, 3);
            this.list_devices.Name = "list_devices";
            this.list_devices.Size = new System.Drawing.Size(422, 655);
            this.list_devices.TabIndex = 3;
            // 
            // list_devicegroup
            // 
            this.list_devicegroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_devicegroup.FormattingEnabled = true;
            this.list_devicegroup.ItemHeight = 24;
            this.list_devicegroup.Location = new System.Drawing.Point(3, 3);
            this.list_devicegroup.Name = "list_devicegroup";
            this.list_devicegroup.Size = new System.Drawing.Size(422, 655);
            this.list_devicegroup.TabIndex = 2;
            this.list_devicegroup.SelectedIndexChanged += new System.EventHandler(this.list_devicegroup_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.Controls.Add(this.list_devicegroup, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.list_devices, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_ok, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(964, 661);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // btn_ok
            // 
            this.btn_ok.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ok.Location = new System.Drawing.Point(859, 3);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(102, 655);
            this.btn_ok.TabIndex = 4;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // FDeviceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 661);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FDeviceSelector";
            this.Text = "FDeviceSelector";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox list_devices;
        private ListBox list_devicegroup;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btn_ok;
    }
}