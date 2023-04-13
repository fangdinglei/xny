namespace MyClient.View.User
{
    partial class FUserLoginHistory
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
            this.group_loginhistory_datepicker1 = new System.Windows.Forms.DateTimePicker();
            this.group_loginhistory_datepicker2 = new System.Windows.Forms.DateTimePicker();
            this.group_loginhistory_list = new System.Windows.Forms.ListBox();
            this.group_loginhistory_maxcount = new System.Windows.Forms.ComboBox();
            this.group_loginhistory_usetimes = new System.Windows.Forms.CheckBox();
            this.group_loginhistory_getinfos = new System.Windows.Forms.Button();
            this.group_loginhistory_multidelet = new System.Windows.Forms.Button();
            this.group_loginhistory_delet = new System.Windows.Forms.Button();
            this.group_loginhistory_text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // group_loginhistory_datepicker1
            // 
            this.group_loginhistory_datepicker1.CustomFormat = " ";
            this.group_loginhistory_datepicker1.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.group_loginhistory_datepicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.group_loginhistory_datepicker1.Location = new System.Drawing.Point(208, 1);
            this.group_loginhistory_datepicker1.Name = "group_loginhistory_datepicker1";
            this.group_loginhistory_datepicker1.Size = new System.Drawing.Size(214, 48);
            this.group_loginhistory_datepicker1.TabIndex = 19;
            // 
            // group_loginhistory_datepicker2
            // 
            this.group_loginhistory_datepicker2.CustomFormat = " ";
            this.group_loginhistory_datepicker2.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.group_loginhistory_datepicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.group_loginhistory_datepicker2.Location = new System.Drawing.Point(208, 52);
            this.group_loginhistory_datepicker2.Name = "group_loginhistory_datepicker2";
            this.group_loginhistory_datepicker2.Size = new System.Drawing.Size(214, 48);
            this.group_loginhistory_datepicker2.TabIndex = 20;
            // 
            // group_loginhistory_list
            // 
            this.group_loginhistory_list.FormattingEnabled = true;
            this.group_loginhistory_list.ItemHeight = 24;
            this.group_loginhistory_list.Location = new System.Drawing.Point(3, 1);
            this.group_loginhistory_list.Name = "group_loginhistory_list";
            this.group_loginhistory_list.Size = new System.Drawing.Size(199, 484);
            this.group_loginhistory_list.TabIndex = 18;
            // 
            // group_loginhistory_maxcount
            // 
            this.group_loginhistory_maxcount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.group_loginhistory_maxcount.FormattingEnabled = true;
            this.group_loginhistory_maxcount.Items.AddRange(new object[] {
            "100",
            "200",
            "400",
            "800"});
            this.group_loginhistory_maxcount.Location = new System.Drawing.Point(520, 8);
            this.group_loginhistory_maxcount.Name = "group_loginhistory_maxcount";
            this.group_loginhistory_maxcount.Size = new System.Drawing.Size(182, 32);
            this.group_loginhistory_maxcount.TabIndex = 17;
            // 
            // group_loginhistory_usetimes
            // 
            this.group_loginhistory_usetimes.AutoSize = true;
            this.group_loginhistory_usetimes.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.group_loginhistory_usetimes.Location = new System.Drawing.Point(428, 0);
            this.group_loginhistory_usetimes.Name = "group_loginhistory_usetimes";
            this.group_loginhistory_usetimes.Size = new System.Drawing.Size(86, 49);
            this.group_loginhistory_usetimes.TabIndex = 16;
            this.group_loginhistory_usetimes.Text = "时间筛选";
            this.group_loginhistory_usetimes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.group_loginhistory_usetimes.UseVisualStyleBackColor = true;
            // 
            // group_loginhistory_getinfos
            // 
            this.group_loginhistory_getinfos.Location = new System.Drawing.Point(428, 52);
            this.group_loginhistory_getinfos.Name = "group_loginhistory_getinfos";
            this.group_loginhistory_getinfos.Size = new System.Drawing.Size(154, 51);
            this.group_loginhistory_getinfos.TabIndex = 15;
            this.group_loginhistory_getinfos.Text = "搜索";
            this.group_loginhistory_getinfos.UseVisualStyleBackColor = true;
            // 
            // group_loginhistory_multidelet
            // 
            this.group_loginhistory_multidelet.Location = new System.Drawing.Point(661, 55);
            this.group_loginhistory_multidelet.Name = "group_loginhistory_multidelet";
            this.group_loginhistory_multidelet.Size = new System.Drawing.Size(68, 79);
            this.group_loginhistory_multidelet.TabIndex = 14;
            this.group_loginhistory_multidelet.Text = "批量删除";
            this.group_loginhistory_multidelet.UseVisualStyleBackColor = true;
            // 
            // group_loginhistory_delet
            // 
            this.group_loginhistory_delet.Location = new System.Drawing.Point(208, 350);
            this.group_loginhistory_delet.Name = "group_loginhistory_delet";
            this.group_loginhistory_delet.Size = new System.Drawing.Size(154, 45);
            this.group_loginhistory_delet.TabIndex = 13;
            this.group_loginhistory_delet.Text = "删除";
            this.group_loginhistory_delet.UseVisualStyleBackColor = true;
            // 
            // group_loginhistory_text
            // 
            this.group_loginhistory_text.Location = new System.Drawing.Point(208, 112);
            this.group_loginhistory_text.Multiline = true;
            this.group_loginhistory_text.Name = "group_loginhistory_text";
            this.group_loginhistory_text.ReadOnly = true;
            this.group_loginhistory_text.Size = new System.Drawing.Size(239, 232);
            this.group_loginhistory_text.TabIndex = 12;
            // 
            // FUserLoginHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 502);
            this.Controls.Add(this.group_loginhistory_datepicker1);
            this.Controls.Add(this.group_loginhistory_datepicker2);
            this.Controls.Add(this.group_loginhistory_list);
            this.Controls.Add(this.group_loginhistory_maxcount);
            this.Controls.Add(this.group_loginhistory_usetimes);
            this.Controls.Add(this.group_loginhistory_getinfos);
            this.Controls.Add(this.group_loginhistory_multidelet);
            this.Controls.Add(this.group_loginhistory_delet);
            this.Controls.Add(this.group_loginhistory_text);
            this.Name = "FUserLoginHistory";
            this.Text = "FUserLoginHistory";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DateTimePicker group_loginhistory_datepicker1;
        private DateTimePicker group_loginhistory_datepicker2;
        private ListBox group_loginhistory_list;
        private ComboBox group_loginhistory_maxcount;
        private CheckBox group_loginhistory_usetimes;
        private Button group_loginhistory_getinfos;
        private Button group_loginhistory_multidelet;
        private Button group_loginhistory_delet;
        private TextBox group_loginhistory_text;
    }
}