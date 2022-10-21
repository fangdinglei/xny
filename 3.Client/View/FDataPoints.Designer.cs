
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            this.CDevice = new System.Windows.Forms.CheckedListBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.CStreamName = new System.Windows.Forms.ComboBox();
            this.list_Type = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 13);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(194, 30);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // chromiumWebBrowser1
            // 
            this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            this.chromiumWebBrowser1.Location = new System.Drawing.Point(412, 69);
            this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            this.chromiumWebBrowser1.Size = new System.Drawing.Size(873, 735);
            this.chromiumWebBrowser1.TabIndex = 4;
            this.chromiumWebBrowser1.Text = "chromiumWebBrowser1";
            // 
            // CDevice
            // 
            this.CDevice.FormattingEnabled = true;
            this.CDevice.Location = new System.Drawing.Point(210, 50);
            this.CDevice.Name = "CDevice";
            this.CDevice.Size = new System.Drawing.Size(196, 463);
            this.CDevice.TabIndex = 5;
            this.CDevice.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CDevice_ItemCheck); 
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(212, 13);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(194, 30);
            this.dateTimePicker2.TabIndex = 7;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // CStreamName
            // 
            this.CStreamName.FormattingEnabled = true;
            this.CStreamName.Location = new System.Drawing.Point(10, 50);
            this.CStreamName.Name = "CStreamName";
            this.CStreamName.Size = new System.Drawing.Size(182, 32);
            this.CStreamName.TabIndex = 8;
            // 
            // list_Type
            // 
            this.list_Type.FormattingEnabled = true;
            this.list_Type.ItemHeight = 24;
            this.list_Type.Location = new System.Drawing.Point(10, 88);
            this.list_Type.Name = "list_Type";
            this.list_Type.Size = new System.Drawing.Size(194, 436);
            this.list_Type.TabIndex = 9;
            this.list_Type.SelectedIndexChanged += new System.EventHandler(this.list_Type_SelectedIndexChangedAsync);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(445, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 10;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FDataPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1754, 1050);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.list_Type);
            this.Controls.Add(this.CStreamName);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.CDevice);
            this.Controls.Add(this.chromiumWebBrowser1);
            this.Controls.Add(this.dateTimePicker1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FDataPoints";
            this.Text = "FDataPoints";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
        private CheckedListBox CDevice;
        private DateTimePicker dateTimePicker2;
        private ComboBox CStreamName;
        private ListBox list_Type;
        private Button button1;
    }
}