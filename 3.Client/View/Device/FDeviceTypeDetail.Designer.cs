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
            label1 = new Label();
            label2 = new Label();
            text_id = new TextBox();
            text_name = new TextBox();
            list_thingmodels = new ListBox();
            btn_submit = new Button();
            text_thingmodel_name = new TextBox();
            text_thingmodel_id = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            text_thingmodel_remark = new TextBox();
            label6 = new Label();
            text_thingmodel_min = new TextBox();
            label7 = new Label();
            text_thingmodel_max = new TextBox();
            label8 = new Label();
            btn_thingmodel_creat = new Button();
            text_thingmodel_unit = new TextBox();
            label9 = new Label();
            check_thingmodel_abandonted = new CheckBox();
            btn_thingmodel_update = new Button();
            text_thingmodel_type = new ComboBox();
            text_alterhigh = new TextBox();
            label10 = new Label();
            text_alterlow = new TextBox();
            label11 = new Label();
            btn_creatdevice = new Button();
            text_alertTime = new TextBox();
            label12 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 56);
            label1.Name = "label1";
            label1.Size = new Size(46, 24);
            label1.TabIndex = 0;
            label1.Text = "名称";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 15);
            label2.Name = "label2";
            label2.Size = new Size(29, 24);
            label2.TabIndex = 1;
            label2.Text = "ID";
            // 
            // text_id
            // 
            text_id.Location = new Point(71, 12);
            text_id.Name = "text_id";
            text_id.ReadOnly = true;
            text_id.Size = new Size(150, 30);
            text_id.TabIndex = 2;
            // 
            // text_name
            // 
            text_name.Location = new Point(71, 53);
            text_name.Name = "text_name";
            text_name.Size = new Size(150, 30);
            text_name.TabIndex = 3;
            // 
            // list_thingmodels
            // 
            list_thingmodels.FormattingEnabled = true;
            list_thingmodels.ItemHeight = 24;
            list_thingmodels.Location = new Point(12, 97);
            list_thingmodels.Name = "list_thingmodels";
            list_thingmodels.Size = new Size(306, 388);
            list_thingmodels.TabIndex = 4;
            list_thingmodels.SelectedIndexChanged += list_thingmodels_SelectedIndexChanged;
            // 
            // btn_submit
            // 
            btn_submit.Location = new Point(241, 15);
            btn_submit.Name = "btn_submit";
            btn_submit.Size = new Size(129, 34);
            btn_submit.TabIndex = 5;
            btn_submit.Text = "确定";
            btn_submit.UseVisualStyleBackColor = true;
            btn_submit.Click += btn_submit_Click;
            // 
            // text_thingmodel_name
            // 
            text_thingmodel_name.Location = new Point(458, 123);
            text_thingmodel_name.Name = "text_thingmodel_name";
            text_thingmodel_name.Size = new Size(150, 30);
            text_thingmodel_name.TabIndex = 9;
            // 
            // text_thingmodel_id
            // 
            text_thingmodel_id.Location = new Point(458, 88);
            text_thingmodel_id.Name = "text_thingmodel_id";
            text_thingmodel_id.ReadOnly = true;
            text_thingmodel_id.Size = new Size(150, 30);
            text_thingmodel_id.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(324, 88);
            label3.Name = "label3";
            label3.Size = new Size(29, 24);
            label3.TabIndex = 7;
            label3.Text = "ID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(324, 124);
            label4.Name = "label4";
            label4.Size = new Size(46, 24);
            label4.TabIndex = 6;
            label4.Text = "名称";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(324, 160);
            label5.Name = "label5";
            label5.Size = new Size(46, 24);
            label5.TabIndex = 10;
            label5.Text = "类型";
            // 
            // text_thingmodel_remark
            // 
            text_thingmodel_remark.Location = new Point(458, 230);
            text_thingmodel_remark.Name = "text_thingmodel_remark";
            text_thingmodel_remark.Size = new Size(150, 30);
            text_thingmodel_remark.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(324, 232);
            label6.Name = "label6";
            label6.Size = new Size(46, 24);
            label6.TabIndex = 12;
            label6.Text = "备注";
            // 
            // text_thingmodel_min
            // 
            text_thingmodel_min.Location = new Point(458, 265);
            text_thingmodel_min.Name = "text_thingmodel_min";
            text_thingmodel_min.Size = new Size(150, 30);
            text_thingmodel_min.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(324, 268);
            label7.Name = "label7";
            label7.Size = new Size(64, 24);
            label7.TabIndex = 14;
            label7.Text = "最小值";
            // 
            // text_thingmodel_max
            // 
            text_thingmodel_max.Location = new Point(458, 300);
            text_thingmodel_max.Name = "text_thingmodel_max";
            text_thingmodel_max.Size = new Size(150, 30);
            text_thingmodel_max.TabIndex = 17;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(324, 304);
            label8.Name = "label8";
            label8.Size = new Size(64, 24);
            label8.TabIndex = 16;
            label8.Text = "最大值";
            // 
            // btn_thingmodel_creat
            // 
            btn_thingmodel_creat.Location = new Point(400, 491);
            btn_thingmodel_creat.Name = "btn_thingmodel_creat";
            btn_thingmodel_creat.Size = new Size(150, 34);
            btn_thingmodel_creat.TabIndex = 18;
            btn_thingmodel_creat.Text = "新建";
            btn_thingmodel_creat.UseVisualStyleBackColor = true;
            btn_thingmodel_creat.Click += btn_thingmodel_creat_Click;
            // 
            // text_thingmodel_unit
            // 
            text_thingmodel_unit.Location = new Point(458, 195);
            text_thingmodel_unit.Name = "text_thingmodel_unit";
            text_thingmodel_unit.Size = new Size(150, 30);
            text_thingmodel_unit.TabIndex = 20;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(324, 196);
            label9.Name = "label9";
            label9.Size = new Size(46, 24);
            label9.TabIndex = 19;
            label9.Text = "单位";
            // 
            // check_thingmodel_abandonted
            // 
            check_thingmodel_abandonted.AutoSize = true;
            check_thingmodel_abandonted.Location = new Point(322, 451);
            check_thingmodel_abandonted.Name = "check_thingmodel_abandonted";
            check_thingmodel_abandonted.Size = new Size(72, 28);
            check_thingmodel_abandonted.TabIndex = 21;
            check_thingmodel_abandonted.Text = "弃用";
            check_thingmodel_abandonted.UseVisualStyleBackColor = true;
            // 
            // btn_thingmodel_update
            // 
            btn_thingmodel_update.Location = new Point(400, 451);
            btn_thingmodel_update.Name = "btn_thingmodel_update";
            btn_thingmodel_update.Size = new Size(150, 34);
            btn_thingmodel_update.TabIndex = 22;
            btn_thingmodel_update.Text = "修改";
            btn_thingmodel_update.UseVisualStyleBackColor = true;
            btn_thingmodel_update.Click += btn_thingmodel_update_Click;
            // 
            // text_thingmodel_type
            // 
            text_thingmodel_type.DropDownStyle = ComboBoxStyle.DropDownList;
            text_thingmodel_type.FormattingEnabled = true;
            text_thingmodel_type.Items.AddRange(new object[] { "None", "Int", "Float", "Bool" });
            text_thingmodel_type.Location = new Point(458, 158);
            text_thingmodel_type.Name = "text_thingmodel_type";
            text_thingmodel_type.Size = new Size(150, 32);
            text_thingmodel_type.TabIndex = 23;
            text_thingmodel_type.SelectedIndexChanged += text_thingmodel_type_SelectedIndexChanged;
            // 
            // text_alterhigh
            // 
            text_alterhigh.Location = new Point(458, 370);
            text_alterhigh.Name = "text_alterhigh";
            text_alterhigh.Size = new Size(150, 30);
            text_alterhigh.TabIndex = 27;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(324, 373);
            label10.Name = "label10";
            label10.Size = new Size(64, 24);
            label10.TabIndex = 26;
            label10.Text = "预警高";
            // 
            // text_alterlow
            // 
            text_alterlow.Location = new Point(458, 335);
            text_alterlow.Name = "text_alterlow";
            text_alterlow.Size = new Size(150, 30);
            text_alterlow.TabIndex = 25;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(324, 340);
            label11.Name = "label11";
            label11.Size = new Size(64, 24);
            label11.TabIndex = 24;
            label11.Text = "预警低";
            // 
            // btn_creatdevice
            // 
            btn_creatdevice.Location = new Point(400, 15);
            btn_creatdevice.Name = "btn_creatdevice";
            btn_creatdevice.Size = new Size(129, 34);
            btn_creatdevice.TabIndex = 28;
            btn_creatdevice.Text = "创建设备";
            btn_creatdevice.UseVisualStyleBackColor = true;
            btn_creatdevice.Click += btn_creatdevice_Click;
            // 
            // text_alertTime
            // 
            text_alertTime.Location = new Point(458, 406);
            text_alertTime.Name = "text_alertTime";
            text_alertTime.Size = new Size(150, 30);
            text_alertTime.TabIndex = 30;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(324, 409);
            label12.Name = "label12";
            label12.Size = new Size(130, 24);
            label12.TabIndex = 29;
            label12.Text = "预警时间(分钟)";
            // 
            // FDeviceTypeDetail
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(623, 557);
            Controls.Add(text_alertTime);
            Controls.Add(label12);
            Controls.Add(btn_creatdevice);
            Controls.Add(text_alterhigh);
            Controls.Add(label10);
            Controls.Add(text_alterlow);
            Controls.Add(label11);
            Controls.Add(text_thingmodel_type);
            Controls.Add(btn_thingmodel_update);
            Controls.Add(check_thingmodel_abandonted);
            Controls.Add(text_thingmodel_unit);
            Controls.Add(label9);
            Controls.Add(btn_thingmodel_creat);
            Controls.Add(text_thingmodel_max);
            Controls.Add(label8);
            Controls.Add(text_thingmodel_min);
            Controls.Add(label7);
            Controls.Add(text_thingmodel_remark);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(text_thingmodel_name);
            Controls.Add(text_thingmodel_id);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(btn_submit);
            Controls.Add(list_thingmodels);
            Controls.Add(text_name);
            Controls.Add(text_id);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FDeviceTypeDetail";
            Text = "FDeviceType";
            ResumeLayout(false);
            PerformLayout();
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
        private TextBox text_alertTime;
        private Label label12;
    }
}