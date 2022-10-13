
namespace MyClient.View
{
    partial class FDataPoints
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.CDevice = new System.Windows.Forms.ComboBox();
            this.CStreamName = new System.Windows.Forms.ComboBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(177, 12);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(939, 665);
            this.webBrowser1.TabIndex = 0;
            // 
            // CDevice
            // 
            this.CDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CDevice.FormattingEnabled = true;
            this.CDevice.Location = new System.Drawing.Point(12, 12);
            this.CDevice.Name = "CDevice";
            this.CDevice.Size = new System.Drawing.Size(159, 26);
            this.CDevice.TabIndex = 1;
            this.CDevice.SelectedIndexChanged += new System.EventHandler(this.CDevice_SelectedIndexChanged);
            // 
            // CStreamName
            // 
            this.CStreamName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CStreamName.FormattingEnabled = true;
            this.CStreamName.Location = new System.Drawing.Point(12, 44);
            this.CStreamName.Name = "CStreamName";
            this.CStreamName.Size = new System.Drawing.Size(159, 26);
            this.CStreamName.TabIndex = 2;
            this.CStreamName.SelectedIndexChanged += new System.EventHandler(this.CStreamName_SelectedIndexChanged);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(12, 85);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(159, 28);
            this.dateTimePicker.TabIndex = 3;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // FDataPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 681);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.CStreamName);
            this.Controls.Add(this.CDevice);
            this.Controls.Add(this.webBrowser1);
            this.Name = "FDataPoints";
            this.Text = "FDataPoints";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ComboBox CDevice;
        private System.Windows.Forms.ComboBox CStreamName;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
    }
}