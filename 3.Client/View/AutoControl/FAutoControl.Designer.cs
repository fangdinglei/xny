
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
            this.bpriorityup = new System.Windows.Forms.Button();
            this.bprioritydown = new System.Windows.Forms.Button();
            this.btest = new System.Windows.Forms.Button();
            this.bok = new System.Windows.Forms.Button();
            this.list_names = new System.Windows.Forms.ListBox();
            this.btn_addname = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // datalist
            // 
            this.datalist.FormattingEnabled = true;
            this.datalist.ItemHeight = 24;
            this.datalist.Location = new System.Drawing.Point(183, 13);
            this.datalist.Margin = new System.Windows.Forms.Padding(4);
            this.datalist.Name = "datalist";
            this.datalist.Size = new System.Drawing.Size(867, 628);
            this.datalist.TabIndex = 0;
            // 
            // linfo
            // 
            this.linfo.AutoSize = true;
            this.linfo.Location = new System.Drawing.Point(13, 39);
            this.linfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linfo.Name = "linfo";
            this.linfo.Size = new System.Drawing.Size(144, 24);
            this.linfo.TabIndex = 1;
            this.linfo.Text = "设备{ID}名称{ID}";
            // 
            // bdelete
            // 
            this.bdelete.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bdelete.Location = new System.Drawing.Point(1058, 197);
            this.bdelete.Margin = new System.Windows.Forms.Padding(4);
            this.bdelete.Name = "bdelete";
            this.bdelete.Size = new System.Drawing.Size(152, 67);
            this.bdelete.TabIndex = 2;
            this.bdelete.Text = "删除";
            this.bdelete.UseVisualStyleBackColor = true;
            this.bdelete.Click += new System.EventHandler(this.bdelete_Click);
            // 
            // bupdate
            // 
            this.bupdate.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bupdate.Location = new System.Drawing.Point(1058, 123);
            this.bupdate.Margin = new System.Windows.Forms.Padding(4);
            this.bupdate.Name = "bupdate";
            this.bupdate.Size = new System.Drawing.Size(152, 67);
            this.bupdate.TabIndex = 3;
            this.bupdate.Text = "修改";
            this.bupdate.UseVisualStyleBackColor = true;
            this.bupdate.Click += new System.EventHandler(this.bupdate_Click);
            // 
            // bcreat
            // 
            this.bcreat.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bcreat.Location = new System.Drawing.Point(1058, 48);
            this.bcreat.Margin = new System.Windows.Forms.Padding(4);
            this.bcreat.Name = "bcreat";
            this.bcreat.Size = new System.Drawing.Size(152, 67);
            this.bcreat.TabIndex = 4;
            this.bcreat.Text = "创建";
            this.bcreat.UseVisualStyleBackColor = true;
            this.bcreat.Click += new System.EventHandler(this.bcreat_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(1058, 574);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(166, 67);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "越往下越优先触发";
            // 
            // ctimeplanopen
            // 
            this.ctimeplanopen.AutoSize = true;
            this.ctimeplanopen.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ctimeplanopen.Location = new System.Drawing.Point(1058, 13);
            this.ctimeplanopen.Margin = new System.Windows.Forms.Padding(4);
            this.ctimeplanopen.Name = "ctimeplanopen";
            this.ctimeplanopen.Size = new System.Drawing.Size(159, 34);
            this.ctimeplanopen.TabIndex = 6;
            this.ctimeplanopen.Text = "定时控制";
            this.ctimeplanopen.UseVisualStyleBackColor = true;
            this.ctimeplanopen.CheckedChanged += new System.EventHandler(this.ctimeplanopen_CheckedChanged);
            // 
            // bpriorityup
            // 
            this.bpriorityup.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bpriorityup.Location = new System.Drawing.Point(1058, 272);
            this.bpriorityup.Margin = new System.Windows.Forms.Padding(4);
            this.bpriorityup.Name = "bpriorityup";
            this.bpriorityup.Size = new System.Drawing.Size(152, 67);
            this.bpriorityup.TabIndex = 8;
            this.bpriorityup.Text = "↑";
            this.bpriorityup.UseVisualStyleBackColor = true;
            this.bpriorityup.Click += new System.EventHandler(this.bpriorityup_Click);
            // 
            // bprioritydown
            // 
            this.bprioritydown.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bprioritydown.Location = new System.Drawing.Point(1058, 347);
            this.bprioritydown.Margin = new System.Windows.Forms.Padding(4);
            this.bprioritydown.Name = "bprioritydown";
            this.bprioritydown.Size = new System.Drawing.Size(152, 67);
            this.bprioritydown.TabIndex = 9;
            this.bprioritydown.Text = "↓";
            this.bprioritydown.UseVisualStyleBackColor = true;
            this.bprioritydown.Click += new System.EventHandler(this.bprioritydown_Click);
            // 
            // btest
            // 
            this.btest.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btest.Location = new System.Drawing.Point(1058, 496);
            this.btest.Margin = new System.Windows.Forms.Padding(4);
            this.btest.Name = "btest";
            this.btest.Size = new System.Drawing.Size(152, 67);
            this.btest.TabIndex = 10;
            this.btest.Text = "测试";
            this.btest.UseVisualStyleBackColor = true;
            this.btest.Click += new System.EventHandler(this.btest_Click);
            // 
            // bok
            // 
            this.bok.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bok.Location = new System.Drawing.Point(1058, 421);
            this.bok.Margin = new System.Windows.Forms.Padding(4);
            this.bok.Name = "bok";
            this.bok.Size = new System.Drawing.Size(152, 67);
            this.bok.TabIndex = 13;
            this.bok.Text = "确认";
            this.bok.UseVisualStyleBackColor = true;
            this.bok.Click += new System.EventHandler(this.bok_Click);
            // 
            // list_names
            // 
            this.list_names.FormattingEnabled = true;
            this.list_names.ItemHeight = 24;
            this.list_names.Location = new System.Drawing.Point(1, 81);
            this.list_names.Name = "list_names";
            this.list_names.Size = new System.Drawing.Size(175, 436);
            this.list_names.TabIndex = 14;
            // 
            // btn_addname
            // 
            this.btn_addname.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_addname.Location = new System.Drawing.Point(2, 521);
            this.btn_addname.Margin = new System.Windows.Forms.Padding(4);
            this.btn_addname.Name = "btn_addname";
            this.btn_addname.Size = new System.Drawing.Size(174, 67);
            this.btn_addname.TabIndex = 15;
            this.btn_addname.Text = "新建";
            this.btn_addname.UseVisualStyleBackColor = true;
            this.btn_addname.Click += new System.EventHandler(this.btn_addname_Click);
            // 
            // FAutoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 692);
            this.Controls.Add(this.btn_addname);
            this.Controls.Add(this.list_names);
            this.Controls.Add(this.bok);
            this.Controls.Add(this.btest);
            this.Controls.Add(this.bprioritydown);
            this.Controls.Add(this.bpriorityup);
            this.Controls.Add(this.ctimeplanopen);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.bcreat);
            this.Controls.Add(this.bupdate);
            this.Controls.Add(this.bdelete);
            this.Controls.Add(this.linfo);
            this.Controls.Add(this.datalist);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Button bpriorityup;
        private System.Windows.Forms.Button bprioritydown;
        private System.Windows.Forms.Button btest;
        private System.Windows.Forms.Button bok;
        private ListBox list_names;
        private Button btn_addname;
    }
}