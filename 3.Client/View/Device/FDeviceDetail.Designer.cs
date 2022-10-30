namespace MyClient.View.Device
{
    partial class FDeviceDetail
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
            this.text_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.text_status = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.text_type = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_typeinfo = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "编号";
            // 
            // text_id
            // 
            this.text_id.Location = new System.Drawing.Point(112, 12);
            this.text_id.Name = "text_id";
            this.text_id.ReadOnly = true;
            this.text_id.Size = new System.Drawing.Size(150, 30);
            this.text_id.TabIndex = 1;
            // 
            // text_name
            // 
            this.text_name.Location = new System.Drawing.Point(112, 48);
            this.text_name.Name = "text_name";
            this.text_name.Size = new System.Drawing.Size(150, 30);
            this.text_name.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "名称";
            // 
            // text_status
            // 
            this.text_status.Location = new System.Drawing.Point(112, 84);
            this.text_status.Name = "text_status";
            this.text_status.Size = new System.Drawing.Size(150, 30);
            this.text_status.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "状态";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "设备类型";
            // 
            // text_type
            // 
            this.text_type.Location = new System.Drawing.Point(112, 120);
            this.text_type.Name = "text_type";
            this.text_type.Size = new System.Drawing.Size(150, 30);
            this.text_type.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 9;
            this.button1.Text = "发送命令";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 34);
            this.button2.TabIndex = 10;
            this.button2.Text = "更多设置";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btn_typeinfo
            // 
            this.btn_typeinfo.Location = new System.Drawing.Point(130, 235);
            this.btn_typeinfo.Name = "btn_typeinfo";
            this.btn_typeinfo.Size = new System.Drawing.Size(112, 34);
            this.btn_typeinfo.TabIndex = 11;
            this.btn_typeinfo.Text = "设备类型";
            this.btn_typeinfo.UseVisualStyleBackColor = true;
            this.btn_typeinfo.Click += new System.EventHandler(this.btn_typeinfo_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(130, 275);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 34);
            this.button4.TabIndex = 12;
            this.button4.Text = "删除设备";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(53, 167);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 34);
            this.button5.TabIndex = 13;
            this.button5.Text = "提交更新";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // chromiumWebBrowser1
            // 
            this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            this.chromiumWebBrowser1.Location = new System.Drawing.Point(289, 12);
            this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            this.chromiumWebBrowser1.Size = new System.Drawing.Size(972, 634);
            this.chromiumWebBrowser1.TabIndex = 14;
            this.chromiumWebBrowser1.Text = "chromiumWebBrowser1";
            // 
            // FDeviceDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 671);
            this.Controls.Add(this.chromiumWebBrowser1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btn_typeinfo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.text_type);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.text_status);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text_name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.text_id);
            this.Controls.Add(this.label1);
            this.Name = "FDeviceDetail";
            this.Text = "FDeviceDetail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox text_id;
        private TextBox text_name;
        private Label label2;
        private TextBox text_status;
        private Label label3;
        private Label label4;
        private TextBox text_type;
        private Button button1;
        private Button button2;
        private Button btn_typeinfo;
        private Button button4;
        private Button button5;
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
    }
}