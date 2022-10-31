namespace MyClient.View
{
    partial class FSystemInfo
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
            this.text_totalmemory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.text_runtime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.text_os = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.text_cpucount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.text_pagesize = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "编号";
            // 
            // text_id
            // 
            this.text_id.Location = new System.Drawing.Point(102, 18);
            this.text_id.Name = "text_id";
            this.text_id.ReadOnly = true;
            this.text_id.Size = new System.Drawing.Size(150, 30);
            this.text_id.TabIndex = 1;
            // 
            // text_totalmemory
            // 
            this.text_totalmemory.Location = new System.Drawing.Point(102, 90);
            this.text_totalmemory.Name = "text_totalmemory";
            this.text_totalmemory.ReadOnly = true;
            this.text_totalmemory.Size = new System.Drawing.Size(150, 30);
            this.text_totalmemory.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "内存";
            // 
            // text_runtime
            // 
            this.text_runtime.Location = new System.Drawing.Point(102, 126);
            this.text_runtime.Name = "text_runtime";
            this.text_runtime.ReadOnly = true;
            this.text_runtime.Size = new System.Drawing.Size(150, 30);
            this.text_runtime.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "运行时长";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(295, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 24);
            this.label5.TabIndex = 8;
            this.label5.Text = "冷数据时间";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "数据库",
            "磁盘"});
            this.comboBox1.Location = new System.Drawing.Point(401, 51);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(182, 32);
            this.comboBox1.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(295, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 24);
            this.label6.TabIndex = 11;
            this.label6.Text = "冷数据方式";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "1天",
            "2天",
            "3天",
            "4天",
            "5天",
            "6天",
            "1周",
            "2周",
            "1月",
            "2月"});
            this.comboBox2.Location = new System.Drawing.Point(401, 16);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(182, 32);
            this.comboBox2.TabIndex = 12;
            // 
            // text_os
            // 
            this.text_os.Location = new System.Drawing.Point(102, 57);
            this.text_os.Name = "text_os";
            this.text_os.ReadOnly = true;
            this.text_os.Size = new System.Drawing.Size(150, 30);
            this.text_os.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 24);
            this.label2.TabIndex = 13;
            this.label2.Text = "系统";
            // 
            // text_cpucount
            // 
            this.text_cpucount.Location = new System.Drawing.Point(102, 162);
            this.text_cpucount.Name = "text_cpucount";
            this.text_cpucount.ReadOnly = true;
            this.text_cpucount.Size = new System.Drawing.Size(150, 30);
            this.text_cpucount.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 24);
            this.label8.TabIndex = 15;
            this.label8.Text = "逻辑核心";
            // 
            // text_pagesize
            // 
            this.text_pagesize.Location = new System.Drawing.Point(102, 198);
            this.text_pagesize.Name = "text_pagesize";
            this.text_pagesize.ReadOnly = true;
            this.text_pagesize.Size = new System.Drawing.Size(150, 30);
            this.text_pagesize.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 24);
            this.label7.TabIndex = 17;
            this.label7.Text = "页大小";
            // 
            // FSystemInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 358);
            this.Controls.Add(this.text_pagesize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.text_cpucount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.text_os);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.text_runtime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.text_totalmemory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text_id);
            this.Controls.Add(this.label1);
            this.Name = "FSystemInfo";
            this.Text = "FSystemInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox text_id;
        private TextBox text_totalmemory;
        private Label label3;
        private TextBox text_runtime;
        private Label label4;
        private Label label5;
        private ComboBox comboBox1;
        private Label label6;
        private ComboBox comboBox2;
        private TextBox text_os;
        private Label label2;
        private TextBox text_cpucount;
        private Label label8;
        private TextBox text_pagesize;
        private Label label7;
    }
}