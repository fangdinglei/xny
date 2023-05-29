
namespace MyClient.View
{
    partial class FAccessibleDevice
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
            dataGridView1 = new DataGridView();
            CCheck = new DataGridViewCheckBoxColumn();
            CID = new DataGridViewTextBoxColumn();
            CName = new DataGridViewTextBoxColumn();
            CStatus = new DataGridViewTextBoxColumn();
            CType = new DataGridViewTextBoxColumn();
            COP = new DataGridViewButtonColumn();
            COP2 = new DataGridViewButtonColumn();
            brefresh = new Button();
            CB_ShowOnline = new CheckBox();
            CB_ShowNotOnline = new CheckBox();
            list_Group = new ListBox();
            btn_groupmgr = new Button();
            b_sendcmd = new Button();
            bgroupmove = new Button();
            bselectall = new Button();
            bsetting = new Button();
            cbAlert = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { CCheck, CID, CName, CStatus, CType, COP, COP2 });
            dataGridView1.Location = new Point(246, 12);
            dataGridView1.Margin = new Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.Size = new Size(1128, 967);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // CCheck
            // 
            CCheck.HeaderText = "选中";
            CCheck.MinimumWidth = 50;
            CCheck.Name = "CCheck";
            CCheck.Width = 50;
            // 
            // CID
            // 
            CID.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            CID.DataPropertyName = "ID";
            CID.HeaderText = "ID";
            CID.MinimumWidth = 8;
            CID.Name = "CID";
            CID.ReadOnly = true;
            // 
            // CName
            // 
            CName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            CName.DataPropertyName = "Name";
            CName.HeaderText = "名称";
            CName.MinimumWidth = 8;
            CName.Name = "CName";
            CName.ReadOnly = true;
            // 
            // CStatus
            // 
            CStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            CStatus.DataPropertyName = "Status";
            CStatus.HeaderText = "状态";
            CStatus.MinimumWidth = 8;
            CStatus.Name = "CStatus";
            CStatus.ReadOnly = true;
            // 
            // CType
            // 
            CType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            CType.DataPropertyName = "Type";
            CType.HeaderText = "类型";
            CType.MinimumWidth = 8;
            CType.Name = "CType";
            CType.ReadOnly = true;
            // 
            // COP
            // 
            COP.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            COP.DataPropertyName = "OP1";
            COP.HeaderText = "操作";
            COP.MinimumWidth = 8;
            COP.Name = "COP";
            COP.ReadOnly = true;
            // 
            // COP2
            // 
            COP2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            COP2.DataPropertyName = "OP2";
            COP2.HeaderText = "操作";
            COP2.MinimumWidth = 8;
            COP2.Name = "COP2";
            COP2.ReadOnly = true;
            // 
            // brefresh
            // 
            brefresh.Location = new Point(1381, 125);
            brefresh.Margin = new Padding(4);
            brefresh.Name = "brefresh";
            brefresh.Size = new Size(186, 68);
            brefresh.TabIndex = 1;
            brefresh.Text = "刷新";
            brefresh.UseVisualStyleBackColor = true;
            brefresh.Click += brefresh_Click;
            // 
            // CB_ShowOnline
            // 
            CB_ShowOnline.AutoSize = true;
            CB_ShowOnline.Checked = true;
            CB_ShowOnline.CheckState = CheckState.Checked;
            CB_ShowOnline.Location = new Point(1420, 581);
            CB_ShowOnline.Margin = new Padding(4);
            CB_ShowOnline.Name = "CB_ShowOnline";
            CB_ShowOnline.Size = new Size(108, 28);
            CB_ShowOnline.TabIndex = 2;
            CB_ShowOnline.Text = "显示在线";
            CB_ShowOnline.UseVisualStyleBackColor = true;
            CB_ShowOnline.Visible = false;
            CB_ShowOnline.CheckedChanged += CB_ShowOnline_CheckedChanged;
            // 
            // CB_ShowNotOnline
            // 
            CB_ShowNotOnline.AutoSize = true;
            CB_ShowNotOnline.Checked = true;
            CB_ShowNotOnline.CheckState = CheckState.Checked;
            CB_ShowNotOnline.Location = new Point(1420, 619);
            CB_ShowNotOnline.Margin = new Padding(4);
            CB_ShowNotOnline.Name = "CB_ShowNotOnline";
            CB_ShowNotOnline.Size = new Size(108, 28);
            CB_ShowNotOnline.TabIndex = 3;
            CB_ShowNotOnline.Text = "显示离线";
            CB_ShowNotOnline.UseVisualStyleBackColor = true;
            CB_ShowNotOnline.Visible = false;
            CB_ShowNotOnline.CheckedChanged += CB_ShowNotOnline_CheckedChanged;
            // 
            // list_Group
            // 
            list_Group.AllowDrop = true;
            list_Group.FormattingEnabled = true;
            list_Group.ItemHeight = 24;
            list_Group.Location = new Point(15, 13);
            list_Group.Margin = new Padding(4);
            list_Group.Name = "list_Group";
            list_Group.Size = new Size(223, 964);
            list_Group.TabIndex = 4;
            list_Group.SelectedIndexChanged += list_Group_SelectedIndexChanged;
            list_Group.DragDrop += list_Group_DragDrop;
            list_Group.DragEnter += list_Group_DragEnter;
            // 
            // btn_groupmgr
            // 
            btn_groupmgr.Location = new Point(1381, 277);
            btn_groupmgr.Margin = new Padding(4);
            btn_groupmgr.Name = "btn_groupmgr";
            btn_groupmgr.Size = new Size(186, 68);
            btn_groupmgr.TabIndex = 5;
            btn_groupmgr.Text = "分组";
            btn_groupmgr.UseVisualStyleBackColor = true;
            btn_groupmgr.Click += btn_groupmgr_Click;
            // 
            // b_sendcmd
            // 
            b_sendcmd.Location = new Point(1381, 353);
            b_sendcmd.Margin = new Padding(4);
            b_sendcmd.Name = "b_sendcmd";
            b_sendcmd.Size = new Size(186, 68);
            b_sendcmd.TabIndex = 6;
            b_sendcmd.Text = "发送命令";
            b_sendcmd.UseVisualStyleBackColor = true;
            b_sendcmd.Click += b_sendcmd_Click;
            // 
            // bgroupmove
            // 
            bgroupmove.Location = new Point(1381, 429);
            bgroupmove.Margin = new Padding(4);
            bgroupmove.Name = "bgroupmove";
            bgroupmove.Size = new Size(186, 68);
            bgroupmove.TabIndex = 7;
            bgroupmove.Text = "移动分组";
            bgroupmove.UseVisualStyleBackColor = true;
            bgroupmove.MouseDown += bgroupmove_MouseDown;
            // 
            // bselectall
            // 
            bselectall.Location = new Point(1381, 201);
            bselectall.Margin = new Padding(4);
            bselectall.Name = "bselectall";
            bselectall.Size = new Size(186, 68);
            bselectall.TabIndex = 8;
            bselectall.Text = "全选";
            bselectall.UseVisualStyleBackColor = true;
            bselectall.Click += bselectall_Click;
            // 
            // bsetting
            // 
            bsetting.Location = new Point(1381, 505);
            bsetting.Margin = new Padding(4);
            bsetting.Name = "bsetting";
            bsetting.Size = new Size(186, 68);
            bsetting.TabIndex = 9;
            bsetting.Text = "定时配置";
            bsetting.UseVisualStyleBackColor = true;
            bsetting.Click += bsetting_Click;
            // 
            // cbAlert
            // 
            cbAlert.AutoSize = true;
            cbAlert.Location = new Point(1382, 51);
            cbAlert.Margin = new Padding(4);
            cbAlert.Name = "cbAlert";
            cbAlert.Size = new Size(126, 28);
            cbAlert.TabIndex = 10;
            cbAlert.Text = "仅显示异常";
            cbAlert.UseVisualStyleBackColor = true;
            cbAlert.CheckedChanged += cbAlert_CheckedChanged;
            // 
            // FAccessibleDevice
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1611, 1005);
            Controls.Add(cbAlert);
            Controls.Add(bsetting);
            Controls.Add(bselectall);
            Controls.Add(bgroupmove);
            Controls.Add(b_sendcmd);
            Controls.Add(btn_groupmgr);
            Controls.Add(list_Group);
            Controls.Add(CB_ShowNotOnline);
            Controls.Add(CB_ShowOnline);
            Controls.Add(brefresh);
            Controls.Add(dataGridView1);
            Margin = new Padding(4);
            Name = "FAccessibleDevice";
            Text = "FAccessibleDevice";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button brefresh;
        private System.Windows.Forms.CheckBox CB_ShowOnline;
        private System.Windows.Forms.CheckBox CB_ShowNotOnline;
        private System.Windows.Forms.ListBox list_Group;
        private System.Windows.Forms.Button btn_groupmgr;
        private System.Windows.Forms.Button b_sendcmd;
        private System.Windows.Forms.Button bgroupmove;
        private System.Windows.Forms.Button bselectall;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn CID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn CType;
        private System.Windows.Forms.DataGridViewButtonColumn COP;
        private System.Windows.Forms.DataGridViewButtonColumn COP2;
        private System.Windows.Forms.Button bsetting;
        private CheckBox cbAlert;
    }
}