
namespace MyClient.View.AutoControl
{
    partial class FCreatOrUpdate
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.g1panel = new System.Windows.Forms.Panel();
            this.g2panel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.g2_endtimepicker = new System.Windows.Forms.DateTimePicker();
            this.g2_enddatepicker = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.g2_starttimepicker = new System.Windows.Forms.DateTimePicker();
            this.g2_startdatepicker = new System.Windows.Forms.DateTimePicker();
            this.g1_week = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.g1_starttimepicker = new System.Windows.Forms.DateTimePicker();
            this.g1_endtimepicker = new System.Windows.Forms.DateTimePicker();
            this.bok = new System.Windows.Forms.Button();
            this.bcancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.g1panel.SuspendLayout();
            this.g2panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "总 是",
            "时间段",
            "周定时"});
            this.comboBox1.Location = new System.Drawing.Point(17, 82);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(128, 38);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // g1panel
            // 
            this.g1panel.Controls.Add(this.g1_week);
            this.g1panel.Controls.Add(this.label8);
            this.g1panel.Controls.Add(this.label7);
            this.g1panel.Controls.Add(this.g1_starttimepicker);
            this.g1panel.Controls.Add(this.g1_endtimepicker);
            this.g1panel.Location = new System.Drawing.Point(165, 58);
            this.g1panel.Name = "g1panel";
            this.g1panel.Size = new System.Drawing.Size(667, 378);
            this.g1panel.TabIndex = 4;
            // 
            // g2panel
            // 
            this.g2panel.Controls.Add(this.label6);
            this.g2panel.Controls.Add(this.g2_endtimepicker);
            this.g2panel.Controls.Add(this.g2_enddatepicker);
            this.g2panel.Controls.Add(this.label5);
            this.g2panel.Controls.Add(this.g2_starttimepicker);
            this.g2panel.Controls.Add(this.g2_startdatepicker);
            this.g2panel.Location = new System.Drawing.Point(165, 58);
            this.g2panel.Name = "g2panel";
            this.g2panel.Size = new System.Drawing.Size(667, 378);
            this.g2panel.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 23;
            this.label6.Text = "结束时间";
            // 
            // g2_endtimepicker
            // 
            this.g2_endtimepicker.Cursor = System.Windows.Forms.Cursors.Default;
            this.g2_endtimepicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.g2_endtimepicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.g2_endtimepicker.Location = new System.Drawing.Point(252, 183);
            this.g2_endtimepicker.Name = "g2_endtimepicker";
            this.g2_endtimepicker.ShowUpDown = true;
            this.g2_endtimepicker.Size = new System.Drawing.Size(200, 28);
            this.g2_endtimepicker.TabIndex = 22;
            // 
            // g2_enddatepicker
            // 
            this.g2_enddatepicker.Location = new System.Drawing.Point(35, 183);
            this.g2_enddatepicker.Name = "g2_enddatepicker";
            this.g2_enddatepicker.Size = new System.Drawing.Size(200, 28);
            this.g2_enddatepicker.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 20;
            this.label5.Text = "开始时间";
            // 
            // g2_starttimepicker
            // 
            this.g2_starttimepicker.Cursor = System.Windows.Forms.Cursors.Default;
            this.g2_starttimepicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.g2_starttimepicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.g2_starttimepicker.Location = new System.Drawing.Point(252, 125);
            this.g2_starttimepicker.Name = "g2_starttimepicker";
            this.g2_starttimepicker.ShowUpDown = true;
            this.g2_starttimepicker.Size = new System.Drawing.Size(200, 28);
            this.g2_starttimepicker.TabIndex = 19;
            // 
            // g2_startdatepicker
            // 
            this.g2_startdatepicker.Location = new System.Drawing.Point(35, 125);
            this.g2_startdatepicker.Name = "g2_startdatepicker";
            this.g2_startdatepicker.Size = new System.Drawing.Size(200, 28);
            this.g2_startdatepicker.TabIndex = 18;
            // 
            // g1_week
            // 
            this.g1_week.FormattingEnabled = true;
            this.g1_week.Items.AddRange(new object[] {
            "星期日",
            "星期一",
            "星期二",
            "星期三",
            "星期四",
            "星期五",
            "星期六"});
            this.g1_week.Location = new System.Drawing.Point(401, 67);
            this.g1_week.Name = "g1_week";
            this.g1_week.Size = new System.Drawing.Size(244, 179);
            this.g1_week.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(70, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 19;
            this.label8.Text = "开始时间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 21;
            this.label7.Text = "结束时间";
            // 
            // g1_starttimepicker
            // 
            this.g1_starttimepicker.Cursor = System.Windows.Forms.Cursors.Default;
            this.g1_starttimepicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.g1_starttimepicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.g1_starttimepicker.Location = new System.Drawing.Point(73, 142);
            this.g1_starttimepicker.Name = "g1_starttimepicker";
            this.g1_starttimepicker.ShowUpDown = true;
            this.g1_starttimepicker.Size = new System.Drawing.Size(200, 28);
            this.g1_starttimepicker.TabIndex = 18;
            // 
            // g1_endtimepicker
            // 
            this.g1_endtimepicker.Cursor = System.Windows.Forms.Cursors.Default;
            this.g1_endtimepicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.g1_endtimepicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.g1_endtimepicker.Location = new System.Drawing.Point(73, 200);
            this.g1_endtimepicker.Name = "g1_endtimepicker";
            this.g1_endtimepicker.ShowUpDown = true;
            this.g1_endtimepicker.Size = new System.Drawing.Size(200, 28);
            this.g1_endtimepicker.TabIndex = 20;
            // 
            // bok
            // 
            this.bok.Location = new System.Drawing.Point(165, 479);
            this.bok.Name = "bok";
            this.bok.Size = new System.Drawing.Size(141, 52);
            this.bok.TabIndex = 11;
            this.bok.Text = "确定";
            this.bok.UseVisualStyleBackColor = true;
            this.bok.Click += new System.EventHandler(this.bok_Click);
            // 
            // bcancel
            // 
            this.bcancel.Location = new System.Drawing.Point(354, 479);
            this.bcancel.Name = "bcancel";
            this.bcancel.Size = new System.Drawing.Size(141, 52);
            this.bcancel.TabIndex = 12;
            this.bcancel.Text = "取消";
            this.bcancel.UseVisualStyleBackColor = true;
            this.bcancel.Click += new System.EventHandler(this.bcancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 30);
            this.label2.TabIndex = 13;
            this.label2.Text = "触发条件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.Location = new System.Drawing.Point(12, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 30);
            this.label3.TabIndex = 20;
            this.label3.Text = "任务事件";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "忽略",
            "关闭",
            "启动",
            "智能"});
            this.comboBox2.Location = new System.Drawing.Point(17, 154);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(128, 38);
            this.comboBox2.TabIndex = 19;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // FCreatOrUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 621);
            this.Controls.Add(this.g2panel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bcancel);
            this.Controls.Add(this.bok);
            this.Controls.Add(this.g1panel);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FCreatOrUpdate";
            this.Text = "FCreatOrUpdate";
            this.g1panel.ResumeLayout(false);
            this.g1panel.PerformLayout();
            this.g2panel.ResumeLayout(false);
            this.g2panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel g1panel;
        private System.Windows.Forms.Button bok;
        private System.Windows.Forms.Button bcancel;
        private System.Windows.Forms.CheckedListBox g1_week;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker g1_starttimepicker;
        private System.Windows.Forms.DateTimePicker g1_endtimepicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Panel g2panel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker g2_endtimepicker;
        private System.Windows.Forms.DateTimePicker g2_enddatepicker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker g2_starttimepicker;
        private System.Windows.Forms.DateTimePicker g2_startdatepicker;
    }
}