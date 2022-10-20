
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
            this.CDevice = new System.Windows.Forms.ComboBox();
            this.CStreamName = new System.Windows.Forms.ComboBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            this.SuspendLayout();
            // 
            // CDevice
            // 
            this.CDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CDevice.FormattingEnabled = true;
            this.CDevice.Location = new System.Drawing.Point(14, 16);
            this.CDevice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CDevice.Name = "CDevice";
            this.CDevice.Size = new System.Drawing.Size(194, 32);
            this.CDevice.TabIndex = 1;
            this.CDevice.SelectedIndexChanged += new System.EventHandler(this.CDevice_SelectedIndexChanged);
            // 
            // CStreamName
            // 
            this.CStreamName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CStreamName.FormattingEnabled = true;
            this.CStreamName.Location = new System.Drawing.Point(14, 59);
            this.CStreamName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CStreamName.Name = "CStreamName";
            this.CStreamName.Size = new System.Drawing.Size(194, 32);
            this.CStreamName.TabIndex = 2;
            this.CStreamName.SelectedIndexChanged += new System.EventHandler(this.CStreamName_SelectedIndexChanged);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(14, 113);
            this.dateTimePicker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(194, 30);
            this.dateTimePicker.TabIndex = 3;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // chromiumWebBrowser1
            // 
            this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            this.chromiumWebBrowser1.Location = new System.Drawing.Point(214, 16);
            this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            this.chromiumWebBrowser1.Size = new System.Drawing.Size(973, 617);
            this.chromiumWebBrowser1.TabIndex = 4;
            this.chromiumWebBrowser1.Text = "chromiumWebBrowser1";
            // 
            // FDataPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1754, 1050);
            this.Controls.Add(this.chromiumWebBrowser1);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.CStreamName);
            this.Controls.Add(this.CDevice);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FDataPoints";
            this.Text = "FDataPoints";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox CDevice;
        private System.Windows.Forms.ComboBox CStreamName;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
    }
}