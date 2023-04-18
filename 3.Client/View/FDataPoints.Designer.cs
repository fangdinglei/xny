
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
            dateTimePicker1 = new DateTimePicker();
            chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            CDevice = new CheckedListBox();
            dateTimePicker2 = new DateTimePicker();
            CStreamName = new ComboBox();
            list_Type = new ListBox();
            button1 = new Button();
            button2 = new Button();
            cbUseCold = new CheckBox();
            SuspendLayout();
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(12, 13);
            dateTimePicker1.Margin = new Padding(3, 4, 3, 4);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(194, 30);
            dateTimePicker1.TabIndex = 3;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // chromiumWebBrowser1
            // 
            chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            chromiumWebBrowser1.Location = new Point(412, 69);
            chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            chromiumWebBrowser1.Size = new Size(873, 735);
            chromiumWebBrowser1.TabIndex = 4;
            chromiumWebBrowser1.Text = "chromiumWebBrowser1";
            // 
            // CDevice
            // 
            CDevice.FormattingEnabled = true;
            CDevice.Location = new Point(210, 50);
            CDevice.Name = "CDevice";
            CDevice.Size = new Size(196, 463);
            CDevice.TabIndex = 5;
            CDevice.ItemCheck += CDevice_ItemCheck;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(212, 13);
            dateTimePicker2.Margin = new Padding(3, 4, 3, 4);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(194, 30);
            dateTimePicker2.TabIndex = 7;
            dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
            // 
            // CStreamName
            // 
            CStreamName.DropDownStyle = ComboBoxStyle.DropDownList;
            CStreamName.FormattingEnabled = true;
            CStreamName.Location = new Point(10, 50);
            CStreamName.Name = "CStreamName";
            CStreamName.Size = new Size(182, 32);
            CStreamName.TabIndex = 8;
            // 
            // list_Type
            // 
            list_Type.FormattingEnabled = true;
            list_Type.ItemHeight = 24;
            list_Type.Location = new Point(10, 88);
            list_Type.Name = "list_Type";
            list_Type.Size = new Size(194, 436);
            list_Type.TabIndex = 9;
            list_Type.SelectedIndexChanged += list_Type_SelectedIndexChangedAsync;
            // 
            // button1
            // 
            button1.Location = new Point(445, 2);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 10;
            button1.Text = "查询";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(563, 2);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 11;
            button2.Text = "查询";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // cbUseCold
            // 
            cbUseCold.AutoSize = true;
            cbUseCold.Location = new Point(681, 2);
            cbUseCold.Name = "cbUseCold";
            cbUseCold.Size = new Size(126, 28);
            cbUseCold.TabIndex = 12;
            cbUseCold.Text = "查看冷数据";
            cbUseCold.UseVisualStyleBackColor = true;
            // 
            // FDataPoints
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1754, 1050);
            Controls.Add(cbUseCold);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(list_Type);
            Controls.Add(CStreamName);
            Controls.Add(dateTimePicker2);
            Controls.Add(CDevice);
            Controls.Add(chromiumWebBrowser1);
            Controls.Add(dateTimePicker1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FDataPoints";
            Text = "FDataPoints";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
        private CheckedListBox CDevice;
        private DateTimePicker dateTimePicker2;
        private ComboBox CStreamName;
        private ListBox list_Type;
        private Button button1;
        private Button button2;
        private CheckBox cbUseCold;
    }
}