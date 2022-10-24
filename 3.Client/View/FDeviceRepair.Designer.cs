namespace MyClient.View
{
    partial class FDeviceRepair
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
            this.list_infos = new System.Windows.Forms.ListBox();
            this.time_DiscoveryTime = new System.Windows.Forms.DateTimePicker();
            this.time_CompletionTime = new System.Windows.Forms.DateTimePicker();
            this.btn_submit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.text_context = new System.Windows.Forms.TextBox();
            this.text_dvname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list_infos
            // 
            this.list_infos.FormattingEnabled = true;
            this.list_infos.ItemHeight = 24;
            this.list_infos.Location = new System.Drawing.Point(12, 12);
            this.list_infos.Name = "list_infos";
            this.list_infos.Size = new System.Drawing.Size(202, 508);
            this.list_infos.TabIndex = 0;
            // 
            // time_DiscoveryTime
            // 
            this.time_DiscoveryTime.Location = new System.Drawing.Point(344, 13);
            this.time_DiscoveryTime.Name = "time_DiscoveryTime";
            this.time_DiscoveryTime.Size = new System.Drawing.Size(300, 30);
            this.time_DiscoveryTime.TabIndex = 1;
            // 
            // time_CompletionTime
            // 
            this.time_CompletionTime.Location = new System.Drawing.Point(344, 49);
            this.time_CompletionTime.Name = "time_CompletionTime";
            this.time_CompletionTime.Size = new System.Drawing.Size(300, 30);
            this.time_CompletionTime.TabIndex = 2;
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(663, 12);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(112, 34);
            this.btn_submit.TabIndex = 3;
            this.btn_submit.Text = "提交";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "发现问题时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "维修完成时间";
            // 
            // text_context
            // 
            this.text_context.Location = new System.Drawing.Point(220, 143);
            this.text_context.Multiline = true;
            this.text_context.Name = "text_context";
            this.text_context.Size = new System.Drawing.Size(538, 377);
            this.text_context.TabIndex = 6;
            // 
            // text_dvname
            // 
            this.text_dvname.Location = new System.Drawing.Point(344, 97);
            this.text_dvname.Name = "text_dvname";
            this.text_dvname.Size = new System.Drawing.Size(300, 30);
            this.text_dvname.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "设备";
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(12, 526);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(112, 34);
            this.btn_search.TabIndex = 9;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            // 
            // FDeviceRepair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 638);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text_dvname);
            this.Controls.Add(this.text_context);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.time_CompletionTime);
            this.Controls.Add(this.time_DiscoveryTime);
            this.Controls.Add(this.list_infos);
            this.Name = "FDeviceRepair";
            this.Text = "FDeviceRepair";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox list_infos;
        private DateTimePicker time_DiscoveryTime;
        private DateTimePicker time_CompletionTime;
        private Button btn_submit;
        private Label label1;
        private Label label2;
        private TextBox text_context;
        private TextBox text_dvname;
        private Label label3;
        private Button btn_search;
    }
}