namespace MyClient.View
{
    partial class FDeviceTypeDetail
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
            this.label2 = new System.Windows.Forms.Label();
            this.text_id = new System.Windows.Forms.TextBox();
            this.text_name = new System.Windows.Forms.TextBox();
            this.list_thingmodels = new System.Windows.Forms.ListBox();
            this.btn_submit = new System.Windows.Forms.Button();
            this.text_thingmodel_name = new System.Windows.Forms.TextBox();
            this.text_thingmodel_id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.text_thingmodel_remark = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.text_thingmodel_min = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.text_thingmodel_max = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_thingmodel_creat = new System.Windows.Forms.Button();
            this.text_thingmodel_unit = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.check_thingmodel_abandonted = new System.Windows.Forms.CheckBox();
            this.btn_thingmodel_update = new System.Windows.Forms.Button();
            this.text_thingmodel_type = new System.Windows.Forms.ComboBox();
            this.text_alterhigh = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.text_alterlow = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_creatdevice = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID";
            // 
            // text_id
            // 
            this.text_id.Location = new System.Drawing.Point(71, 12);
            this.text_id.Name = "text_id";
            this.text_id.ReadOnly = true;
            this.text_id.Size = new System.Drawing.Size(150, 30);
            this.text_id.TabIndex = 2;
            // 
            // text_name
            // 
            this.text_name.Location = new System.Drawing.Point(71, 53);
            this.text_name.Name = "text_name";
            this.text_name.Size = new System.Drawing.Size(150, 30);
            this.text_name.TabIndex = 3;
            // 
            // list_thingmodels
            // 
            this.list_thingmodels.FormattingEnabled = true;
            this.list_thingmodels.ItemHeight = 24;
            this.list_thingmodels.Location = new System.Drawing.Point(12, 97);
            this.list_thingmodels.Name = "list_thingmodels";
            this.list_thingmodels.Size = new System.Drawing.Size(306, 388);
            this.list_thingmodels.TabIndex = 4;
            this.list_thingmodels.SelectedIndexChanged += new System.EventHandler(this.list_thingmodels_SelectedIndexChanged);
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(241, 15);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(129, 34);
            this.btn_submit.TabIndex = 5;
            this.btn_submit.Text = "确定";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // text_thingmodel_name
            // 
            this.text_thingmodel_name.Location = new System.Drawing.Point(400, 129);
            this.text_thingmodel_name.Name = "text_thingmodel_name";
            this.text_thingmodel_name.Size = new System.Drawing.Size(150, 30);
            this.text_thingmodel_name.TabIndex = 9;
            // 
            // text_thingmodel_id
            // 
            this.text_thingmodel_id.Location = new System.Drawing.Point(400, 88);
            this.text_thingmodel_id.Name = "text_thingmodel_id";
            this.text_thingmodel_id.ReadOnly = true;
            this.text_thingmodel_id.Size = new System.Drawing.Size(150, 30);
            this.text_thingmodel_id.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(324, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "名称";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(324, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 24);
            this.label5.TabIndex = 10;
            this.label5.Text = "类型";
            // 
            // text_thingmodel_remark
            // 
            this.text_thingmodel_remark.Location = new System.Drawing.Point(400, 237);
            this.text_thingmodel_remark.Name = "text_thingmodel_remark";
            this.text_thingmodel_remark.Size = new System.Drawing.Size(150, 30);
            this.text_thingmodel_remark.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(324, 237);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "备注";
            // 
            // text_thingmodel_min
            // 
            this.text_thingmodel_min.Location = new System.Drawing.Point(400, 273);
            this.text_thingmodel_min.Name = "text_thingmodel_min";
            this.text_thingmodel_min.Size = new System.Drawing.Size(150, 30);
            this.text_thingmodel_min.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(324, 273);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 24);
            this.label7.TabIndex = 14;
            this.label7.Text = "最小值";
            // 
            // text_thingmodel_max
            // 
            this.text_thingmodel_max.Location = new System.Drawing.Point(400, 309);
            this.text_thingmodel_max.Name = "text_thingmodel_max";
            this.text_thingmodel_max.Size = new System.Drawing.Size(150, 30);
            this.text_thingmodel_max.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(324, 309);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 24);
            this.label8.TabIndex = 16;
            this.label8.Text = "最大值";
            // 
            // btn_thingmodel_creat
            // 
            this.btn_thingmodel_creat.Location = new System.Drawing.Point(400, 452);
            this.btn_thingmodel_creat.Name = "btn_thingmodel_creat";
            this.btn_thingmodel_creat.Size = new System.Drawing.Size(150, 34);
            this.btn_thingmodel_creat.TabIndex = 18;
            this.btn_thingmodel_creat.Text = "新建";
            this.btn_thingmodel_creat.UseVisualStyleBackColor = true;
            this.btn_thingmodel_creat.Click += new System.EventHandler(this.btn_thingmodel_creat_Click);
            // 
            // text_thingmodel_unit
            // 
            this.text_thingmodel_unit.Location = new System.Drawing.Point(400, 201);
            this.text_thingmodel_unit.Name = "text_thingmodel_unit";
            this.text_thingmodel_unit.Size = new System.Drawing.Size(150, 30);
            this.text_thingmodel_unit.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(324, 201);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 24);
            this.label9.TabIndex = 19;
            this.label9.Text = "单位";
            // 
            // check_thingmodel_abandonted
            // 
            this.check_thingmodel_abandonted.AutoSize = true;
            this.check_thingmodel_abandonted.Location = new System.Drawing.Point(322, 412);
            this.check_thingmodel_abandonted.Name = "check_thingmodel_abandonted";
            this.check_thingmodel_abandonted.Size = new System.Drawing.Size(72, 28);
            this.check_thingmodel_abandonted.TabIndex = 21;
            this.check_thingmodel_abandonted.Text = "弃用";
            this.check_thingmodel_abandonted.UseVisualStyleBackColor = true;
            // 
            // btn_thingmodel_update
            // 
            this.btn_thingmodel_update.Location = new System.Drawing.Point(400, 412);
            this.btn_thingmodel_update.Name = "btn_thingmodel_update";
            this.btn_thingmodel_update.Size = new System.Drawing.Size(150, 34);
            this.btn_thingmodel_update.TabIndex = 22;
            this.btn_thingmodel_update.Text = "修改";
            this.btn_thingmodel_update.UseVisualStyleBackColor = true;
            this.btn_thingmodel_update.Click += new System.EventHandler(this.btn_thingmodel_update_Click);
            // 
            // text_thingmodel_type
            // 
            this.text_thingmodel_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.text_thingmodel_type.FormattingEnabled = true;
            this.text_thingmodel_type.Items.AddRange(new object[] {
            "None",
            "Int",
            "Float",
            "Bool"});
            this.text_thingmodel_type.Location = new System.Drawing.Point(400, 165);
            this.text_thingmodel_type.Name = "text_thingmodel_type";
            this.text_thingmodel_type.Size = new System.Drawing.Size(150, 32);
            this.text_thingmodel_type.TabIndex = 23;
            this.text_thingmodel_type.SelectedIndexChanged += new System.EventHandler(this.text_thingmodel_type_SelectedIndexChanged);
            // 
            // text_alterhigh
            // 
            this.text_alterhigh.Location = new System.Drawing.Point(400, 381);
            this.text_alterhigh.Name = "text_alterhigh";
            this.text_alterhigh.Size = new System.Drawing.Size(150, 30);
            this.text_alterhigh.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(324, 381);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 24);
            this.label10.TabIndex = 26;
            this.label10.Text = "预警高";
            // 
            // text_alterlow
            // 
            this.text_alterlow.Location = new System.Drawing.Point(400, 345);
            this.text_alterlow.Name = "text_alterlow";
            this.text_alterlow.Size = new System.Drawing.Size(150, 30);
            this.text_alterlow.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(324, 345);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 24);
            this.label11.TabIndex = 24;
            this.label11.Text = "预警低";
            // 
            // btn_creatdevice
            // 
            this.btn_creatdevice.Location = new System.Drawing.Point(400, 15);
            this.btn_creatdevice.Name = "btn_creatdevice";
            this.btn_creatdevice.Size = new System.Drawing.Size(129, 34);
            this.btn_creatdevice.TabIndex = 28;
            this.btn_creatdevice.Text = "创建设备";
            this.btn_creatdevice.UseVisualStyleBackColor = true;
            // 
            // FDeviceTypeDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 507);
            this.Controls.Add(this.btn_creatdevice);
            this.Controls.Add(this.text_alterhigh);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.text_alterlow);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.text_thingmodel_type);
            this.Controls.Add(this.btn_thingmodel_update);
            this.Controls.Add(this.check_thingmodel_abandonted);
            this.Controls.Add(this.text_thingmodel_unit);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btn_thingmodel_creat);
            this.Controls.Add(this.text_thingmodel_max);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.text_thingmodel_min);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.text_thingmodel_remark);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.text_thingmodel_name);
            this.Controls.Add(this.text_thingmodel_id);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.list_thingmodels);
            this.Controls.Add(this.text_name);
            this.Controls.Add(this.text_id);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FDeviceTypeDetail";
            this.Text = "FDeviceType";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox text_id;
        private TextBox text_name;
        private ListBox list_thingmodels;
        private Button btn_submit;
        private TextBox text_thingmodel_name;
        private TextBox text_thingmodel_id;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox text_thingmodel_remark;
        private Label label6;
        private TextBox text_thingmodel_min;
        private Label label7;
        private TextBox text_thingmodel_max;
        private Label label8;
        private Button btn_thingmodel_creat;
        private TextBox text_thingmodel_unit;
        private Label label9;
        private CheckBox check_thingmodel_abandonted;
        private Button btn_thingmodel_update;
        private ComboBox text_thingmodel_type;
        private TextBox text_alterhigh;
        private Label label10;
        private TextBox text_alterlow;
        private Label label11;
        private Button btn_creatdevice;
    }
}