namespace MyClient.View.Device
{
    partial class FDeviceTypes
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
            this.list_types = new System.Windows.Forms.ListBox();
            this.location_lab = new System.Windows.Forms.Label();
            this.btn_creat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list_types
            // 
            this.list_types.FormattingEnabled = true;
            this.list_types.ItemHeight = 24;
            this.list_types.Location = new System.Drawing.Point(12, 12);
            this.list_types.Name = "list_types";
            this.list_types.Size = new System.Drawing.Size(204, 412);
            this.list_types.TabIndex = 0;
            this.list_types.SelectedIndexChanged += new System.EventHandler(this.list_types_SelectedIndexChanged);
            // 
            // location_lab
            // 
            this.location_lab.AutoSize = true;
            this.location_lab.Location = new System.Drawing.Point(222, 12);
            this.location_lab.Name = "location_lab";
            this.location_lab.Size = new System.Drawing.Size(63, 24);
            this.location_lab.TabIndex = 1;
            this.location_lab.Text = "label1";
            this.location_lab.Visible = false;
            // 
            // btn_creat
            // 
            this.btn_creat.Location = new System.Drawing.Point(12, 430);
            this.btn_creat.Name = "btn_creat";
            this.btn_creat.Size = new System.Drawing.Size(112, 34);
            this.btn_creat.TabIndex = 2;
            this.btn_creat.Text = "新建";
            this.btn_creat.UseVisualStyleBackColor = true;
            this.btn_creat.Click += new System.EventHandler(this.btn_creat_Click);
            // 
            // FDeviceTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 549);
            this.Controls.Add(this.btn_creat);
            this.Controls.Add(this.location_lab);
            this.Controls.Add(this.list_types);
            this.Name = "FDeviceTypes";
            this.Text = "FDeviceTypes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox list_types;
        private Label location_lab;
        private Button btn_creat;
    }
}