
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
            comboBox1 = new ComboBox();
            g1panel = new Panel();
            g1_week = new CheckedListBox();
            label8 = new Label();
            label7 = new Label();
            g1_starttimepicker = new DateTimePicker();
            g1_endtimepicker = new DateTimePicker();
            g2panel = new Panel();
            label6 = new Label();
            g2_endtimepicker = new DateTimePicker();
            g2_enddatepicker = new DateTimePicker();
            label5 = new Label();
            g2_starttimepicker = new DateTimePicker();
            g2_startdatepicker = new DateTimePicker();
            bok = new Button();
            bcancel = new Button();
            label2 = new Label();
            label3 = new Label();
            t_cmd = new TextBox();
            label1 = new Label();
            cbTimeZone = new ComboBox();
            g1panel.SuspendLayout();
            g2panel.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("宋体", 15F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "总 是", "时间段", "周定时" });
            comboBox1.Location = new Point(21, 109);
            comboBox1.Margin = new Padding(4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(156, 38);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // g1panel
            // 
            g1panel.Controls.Add(g1_week);
            g1panel.Controls.Add(label8);
            g1panel.Controls.Add(label7);
            g1panel.Controls.Add(g1_starttimepicker);
            g1panel.Controls.Add(g1_endtimepicker);
            g1panel.Location = new Point(202, 77);
            g1panel.Margin = new Padding(4);
            g1panel.Name = "g1panel";
            g1panel.Size = new Size(815, 504);
            g1panel.TabIndex = 4;
            // 
            // g1_week
            // 
            g1_week.FormattingEnabled = true;
            g1_week.Items.AddRange(new object[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" });
            g1_week.Location = new Point(490, 89);
            g1_week.Margin = new Padding(4);
            g1_week.Name = "g1_week";
            g1_week.Size = new Size(297, 220);
            g1_week.TabIndex = 20;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(86, 161);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(82, 24);
            label8.TabIndex = 19;
            label8.Text = "开始时间";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(86, 239);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(82, 24);
            label7.TabIndex = 21;
            label7.Text = "结束时间";
            // 
            // g1_starttimepicker
            // 
            g1_starttimepicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            g1_starttimepicker.Format = DateTimePickerFormat.Time;
            g1_starttimepicker.Location = new Point(89, 189);
            g1_starttimepicker.Margin = new Padding(4);
            g1_starttimepicker.Name = "g1_starttimepicker";
            g1_starttimepicker.ShowUpDown = true;
            g1_starttimepicker.Size = new Size(244, 30);
            g1_starttimepicker.TabIndex = 18;
            // 
            // g1_endtimepicker
            // 
            g1_endtimepicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            g1_endtimepicker.Format = DateTimePickerFormat.Time;
            g1_endtimepicker.Location = new Point(89, 267);
            g1_endtimepicker.Margin = new Padding(4);
            g1_endtimepicker.Name = "g1_endtimepicker";
            g1_endtimepicker.ShowUpDown = true;
            g1_endtimepicker.Size = new Size(244, 30);
            g1_endtimepicker.TabIndex = 20;
            // 
            // g2panel
            // 
            g2panel.Controls.Add(label6);
            g2panel.Controls.Add(g2_endtimepicker);
            g2panel.Controls.Add(g2_enddatepicker);
            g2panel.Controls.Add(label5);
            g2panel.Controls.Add(g2_starttimepicker);
            g2panel.Controls.Add(g2_startdatepicker);
            g2panel.Location = new Point(202, 77);
            g2panel.Margin = new Padding(4);
            g2panel.Name = "g2panel";
            g2panel.Size = new Size(815, 504);
            g2panel.TabIndex = 22;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(39, 216);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(82, 24);
            label6.TabIndex = 23;
            label6.Text = "结束时间";
            // 
            // g2_endtimepicker
            // 
            g2_endtimepicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            g2_endtimepicker.Format = DateTimePickerFormat.Time;
            g2_endtimepicker.Location = new Point(308, 244);
            g2_endtimepicker.Margin = new Padding(4);
            g2_endtimepicker.Name = "g2_endtimepicker";
            g2_endtimepicker.ShowUpDown = true;
            g2_endtimepicker.Size = new Size(244, 30);
            g2_endtimepicker.TabIndex = 22;
            // 
            // g2_enddatepicker
            // 
            g2_enddatepicker.Location = new Point(43, 244);
            g2_enddatepicker.Margin = new Padding(4);
            g2_enddatepicker.Name = "g2_enddatepicker";
            g2_enddatepicker.Size = new Size(244, 30);
            g2_enddatepicker.TabIndex = 21;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(39, 139);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(82, 24);
            label5.TabIndex = 20;
            label5.Text = "开始时间";
            // 
            // g2_starttimepicker
            // 
            g2_starttimepicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            g2_starttimepicker.Format = DateTimePickerFormat.Time;
            g2_starttimepicker.Location = new Point(308, 167);
            g2_starttimepicker.Margin = new Padding(4);
            g2_starttimepicker.Name = "g2_starttimepicker";
            g2_starttimepicker.ShowUpDown = true;
            g2_starttimepicker.Size = new Size(244, 30);
            g2_starttimepicker.TabIndex = 19;
            // 
            // g2_startdatepicker
            // 
            g2_startdatepicker.Location = new Point(43, 167);
            g2_startdatepicker.Margin = new Padding(4);
            g2_startdatepicker.Name = "g2_startdatepicker";
            g2_startdatepicker.Size = new Size(244, 30);
            g2_startdatepicker.TabIndex = 18;
            // 
            // bok
            // 
            bok.Location = new Point(202, 639);
            bok.Margin = new Padding(4);
            bok.Name = "bok";
            bok.Size = new Size(172, 69);
            bok.TabIndex = 11;
            bok.Text = "确定";
            bok.UseVisualStyleBackColor = true;
            bok.Click += bok_Click;
            // 
            // bcancel
            // 
            bcancel.Location = new Point(433, 639);
            bcancel.Margin = new Padding(4);
            bcancel.Name = "bcancel";
            bcancel.Size = new Size(172, 69);
            bcancel.TabIndex = 12;
            bcancel.Text = "取消";
            bcancel.UseVisualStyleBackColor = true;
            bcancel.Click += bcancel_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("宋体", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(15, 65);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(133, 30);
            label2.TabIndex = 13;
            label2.Text = "触发条件";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("宋体", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(15, 161);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(133, 30);
            label3.TabIndex = 20;
            label3.Text = "任务事件";
            // 
            // t_cmd
            // 
            t_cmd.Location = new Point(15, 194);
            t_cmd.Name = "t_cmd";
            t_cmd.Size = new Size(162, 30);
            t_cmd.TabIndex = 23;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("宋体", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(21, 249);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(73, 30);
            label1.TabIndex = 25;
            label1.Text = "时区";
            // 
            // cbTimeZone
            // 
            cbTimeZone.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTimeZone.Font = new Font("宋体", 15F, FontStyle.Regular, GraphicsUnit.Point);
            cbTimeZone.FormattingEnabled = true;
            cbTimeZone.Items.AddRange(new object[] { "-12", "-11", "-10", "-9", "-8", "-7", "-6", "-5", "-4", "-3", "-2", "-1", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            cbTimeZone.Location = new Point(15, 283);
            cbTimeZone.Margin = new Padding(4);
            cbTimeZone.Name = "cbTimeZone";
            cbTimeZone.Size = new Size(156, 38);
            cbTimeZone.TabIndex = 24;
            // 
            // FCreatOrUpdate
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1271, 828);
            Controls.Add(label1);
            Controls.Add(cbTimeZone);
            Controls.Add(t_cmd);
            Controls.Add(g2panel);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(bcancel);
            Controls.Add(bok);
            Controls.Add(g1panel);
            Controls.Add(comboBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            Name = "FCreatOrUpdate";
            Text = "FCreatOrUpdate";
            g1panel.ResumeLayout(false);
            g1panel.PerformLayout();
            g2panel.ResumeLayout(false);
            g2panel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Panel g2panel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker g2_endtimepicker;
        private System.Windows.Forms.DateTimePicker g2_enddatepicker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker g2_starttimepicker;
        private System.Windows.Forms.DateTimePicker g2_startdatepicker;
        private TextBox t_cmd;
        private Label label1;
        private ComboBox cbTimeZone;
    }
}