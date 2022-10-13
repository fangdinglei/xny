
namespace MyClient.View.AutoControl
{
    partial class FAutoControl
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
            this.datalist = new System.Windows.Forms.ListBox();
            this.linfo = new System.Windows.Forms.Label();
            this.bdelete = new System.Windows.Forms.Button();
            this.bupdate = new System.Windows.Forms.Button();
            this.bcreat = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ctimeplanopen = new System.Windows.Forms.CheckBox();
            this.cadvancedcontrol = new System.Windows.Forms.CheckBox();
            this.bpriorityup = new System.Windows.Forms.Button();
            this.bprioritydown = new System.Windows.Forms.Button();
            this.btest = new System.Windows.Forms.Button();
            this.bclose = new System.Windows.Forms.Button();
            this.bopen = new System.Windows.Forms.Button();
            this.bok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // datalist
            // 
            this.datalist.FormattingEnabled = true;
            this.datalist.ItemHeight = 18;
            this.datalist.Location = new System.Drawing.Point(12, 75);
            this.datalist.Name = "datalist";
            this.datalist.Size = new System.Drawing.Size(761, 382);
            this.datalist.TabIndex = 0;
            // 
            // linfo
            // 
            this.linfo.AutoSize = true;
            this.linfo.Location = new System.Drawing.Point(12, 31);
            this.linfo.Name = "linfo";
            this.linfo.Size = new System.Drawing.Size(152, 18);
            this.linfo.TabIndex = 1;
            this.linfo.Text = "设备{ID}名称{ID}";
            // 
            // bdelete
            // 
            this.bdelete.Font = new System.Drawing.Font("宋体", 15F);
            this.bdelete.Location = new System.Drawing.Point(790, 187);
            this.bdelete.Name = "bdelete";
            this.bdelete.Size = new System.Drawing.Size(124, 50);
            this.bdelete.TabIndex = 2;
            this.bdelete.Text = "删除";
            this.bdelete.UseVisualStyleBackColor = true;
            this.bdelete.Click += new System.EventHandler(this.bdelete_Click);
            // 
            // bupdate
            // 
            this.bupdate.Font = new System.Drawing.Font("宋体", 15F);
            this.bupdate.Location = new System.Drawing.Point(790, 131);
            this.bupdate.Name = "bupdate";
            this.bupdate.Size = new System.Drawing.Size(124, 50);
            this.bupdate.TabIndex = 3;
            this.bupdate.Text = "修改";
            this.bupdate.UseVisualStyleBackColor = true;
            this.bupdate.Click += new System.EventHandler(this.bupdate_Click);
            // 
            // bcreat
            // 
            this.bcreat.Font = new System.Drawing.Font("宋体", 15F);
            this.bcreat.Location = new System.Drawing.Point(790, 75);
            this.bcreat.Name = "bcreat";
            this.bcreat.Size = new System.Drawing.Size(124, 50);
            this.bcreat.TabIndex = 4;
            this.bcreat.Text = "创建";
            this.bcreat.UseVisualStyleBackColor = true;
            this.bcreat.Click += new System.EventHandler(this.bcreat_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 15F);
            this.textBox1.Location = new System.Drawing.Point(779, 416);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(280, 41);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "越往下越优先触发";
            // 
            // ctimeplanopen
            // 
            this.ctimeplanopen.AutoSize = true;
            this.ctimeplanopen.Font = new System.Drawing.Font("宋体", 15F);
            this.ctimeplanopen.Location = new System.Drawing.Point(920, 75);
            this.ctimeplanopen.Name = "ctimeplanopen";
            this.ctimeplanopen.Size = new System.Drawing.Size(159, 34);
            this.ctimeplanopen.TabIndex = 6;
            this.ctimeplanopen.Text = "定时控制";
            this.ctimeplanopen.UseVisualStyleBackColor = true;
            this.ctimeplanopen.CheckedChanged += new System.EventHandler(this.ctimeplanopen_CheckedChanged);
            // 
            // cadvancedcontrol
            // 
            this.cadvancedcontrol.AutoSize = true;
            this.cadvancedcontrol.Font = new System.Drawing.Font("宋体", 15F);
            this.cadvancedcontrol.Location = new System.Drawing.Point(920, 116);
            this.cadvancedcontrol.Name = "cadvancedcontrol";
            this.cadvancedcontrol.Size = new System.Drawing.Size(159, 34);
            this.cadvancedcontrol.TabIndex = 7;
            this.cadvancedcontrol.Text = "智能控制";
            this.cadvancedcontrol.UseVisualStyleBackColor = true;
            this.cadvancedcontrol.CheckedChanged += new System.EventHandler(this.cadvancedcontrol_CheckedChanged);
            // 
            // bpriorityup
            // 
            this.bpriorityup.Font = new System.Drawing.Font("宋体", 15F);
            this.bpriorityup.Location = new System.Drawing.Point(790, 243);
            this.bpriorityup.Name = "bpriorityup";
            this.bpriorityup.Size = new System.Drawing.Size(124, 50);
            this.bpriorityup.TabIndex = 8;
            this.bpriorityup.Text = "↑";
            this.bpriorityup.UseVisualStyleBackColor = true;
            this.bpriorityup.Click += new System.EventHandler(this.bpriorityup_Click);
            // 
            // bprioritydown
            // 
            this.bprioritydown.Font = new System.Drawing.Font("宋体", 15F);
            this.bprioritydown.Location = new System.Drawing.Point(790, 299);
            this.bprioritydown.Name = "bprioritydown";
            this.bprioritydown.Size = new System.Drawing.Size(124, 50);
            this.bprioritydown.TabIndex = 9;
            this.bprioritydown.Text = "↓";
            this.bprioritydown.UseVisualStyleBackColor = true;
            this.bprioritydown.Click += new System.EventHandler(this.bprioritydown_Click);
            // 
            // btest
            // 
            this.btest.Font = new System.Drawing.Font("宋体", 15F);
            this.btest.Location = new System.Drawing.Point(955, 187);
            this.btest.Name = "btest";
            this.btest.Size = new System.Drawing.Size(124, 50);
            this.btest.TabIndex = 10;
            this.btest.Text = "测试";
            this.btest.UseVisualStyleBackColor = true;
            this.btest.Click += new System.EventHandler(this.btest_Click);
            // 
            // bclose
            // 
            this.bclose.Font = new System.Drawing.Font("宋体", 15F);
            this.bclose.Location = new System.Drawing.Point(955, 336);
            this.bclose.Name = "bclose";
            this.bclose.Size = new System.Drawing.Size(124, 50);
            this.bclose.TabIndex = 11;
            this.bclose.Text = "手动关";
            this.bclose.UseVisualStyleBackColor = true;
            this.bclose.Click += new System.EventHandler(this.bclose_Click);
            // 
            // bopen
            // 
            this.bopen.Font = new System.Drawing.Font("宋体", 15F);
            this.bopen.Location = new System.Drawing.Point(955, 280);
            this.bopen.Name = "bopen";
            this.bopen.Size = new System.Drawing.Size(124, 50);
            this.bopen.TabIndex = 12;
            this.bopen.Text = "手动开";
            this.bopen.UseVisualStyleBackColor = true;
            this.bopen.Click += new System.EventHandler(this.bopen_Click);
            // 
            // bok
            // 
            this.bok.Font = new System.Drawing.Font("宋体", 15F);
            this.bok.Location = new System.Drawing.Point(790, 355);
            this.bok.Name = "bok";
            this.bok.Size = new System.Drawing.Size(124, 50);
            this.bok.TabIndex = 13;
            this.bok.Text = "确认";
            this.bok.UseVisualStyleBackColor = true;
            this.bok.Click += new System.EventHandler(this.bok_Click);
            // 
            // FAutoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 519);
            this.Controls.Add(this.bok);
            this.Controls.Add(this.bopen);
            this.Controls.Add(this.bclose);
            this.Controls.Add(this.btest);
            this.Controls.Add(this.bprioritydown);
            this.Controls.Add(this.bpriorityup);
            this.Controls.Add(this.cadvancedcontrol);
            this.Controls.Add(this.ctimeplanopen);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.bcreat);
            this.Controls.Add(this.bupdate);
            this.Controls.Add(this.bdelete);
            this.Controls.Add(this.linfo);
            this.Controls.Add(this.datalist);
            this.Name = "FAutoControl";
            this.Text = "自动控制";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox datalist;
        private System.Windows.Forms.Label linfo;
        private System.Windows.Forms.Button bdelete;
        private System.Windows.Forms.Button bupdate;
        private System.Windows.Forms.Button bcreat;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox ctimeplanopen;
        private System.Windows.Forms.CheckBox cadvancedcontrol;
        private System.Windows.Forms.Button bpriorityup;
        private System.Windows.Forms.Button bprioritydown;
        private System.Windows.Forms.Button btest;
        private System.Windows.Forms.Button bclose;
        private System.Windows.Forms.Button bopen;
        private System.Windows.Forms.Button bok;
    }
}