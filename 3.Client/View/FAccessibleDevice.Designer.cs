
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COP = new System.Windows.Forms.DataGridViewButtonColumn();
            this.COP2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.brefresh = new System.Windows.Forms.Button();
            this.CB_ShowOnline = new System.Windows.Forms.CheckBox();
            this.CB_ShowNotOnline = new System.Windows.Forms.CheckBox();
            this.list_Group = new System.Windows.Forms.ListBox();
            this.btn_groupmgr = new System.Windows.Forms.Button();
            this.b_sendcmd = new System.Windows.Forms.Button();
            this.bgroupmove = new System.Windows.Forms.Button();
            this.bselectall = new System.Windows.Forms.Button();
            this.bsetting = new System.Windows.Forms.Button();
            this.btn_creatdevice = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CCheck,
            this.CID,
            this.CName,
            this.CStatus,
            this.CType,
            this.COP,
            this.COP2});
            this.dataGridView1.Location = new System.Drawing.Point(246, 12);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1128, 967);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // CCheck
            // 
            this.CCheck.HeaderText = "选中";
            this.CCheck.MinimumWidth = 50;
            this.CCheck.Name = "CCheck";
            this.CCheck.Width = 50;
            // 
            // CID
            // 
            this.CID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CID.DataPropertyName = "ID";
            this.CID.HeaderText = "ID";
            this.CID.MinimumWidth = 8;
            this.CID.Name = "CID";
            this.CID.ReadOnly = true;
            // 
            // CName
            // 
            this.CName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CName.DataPropertyName = "Name";
            this.CName.HeaderText = "名称";
            this.CName.MinimumWidth = 8;
            this.CName.Name = "CName";
            this.CName.ReadOnly = true;
            // 
            // CStatus
            // 
            this.CStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CStatus.DataPropertyName = "Status";
            this.CStatus.HeaderText = "状态";
            this.CStatus.MinimumWidth = 8;
            this.CStatus.Name = "CStatus";
            this.CStatus.ReadOnly = true;
            // 
            // CType
            // 
            this.CType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CType.DataPropertyName = "Type";
            this.CType.HeaderText = "类型";
            this.CType.MinimumWidth = 8;
            this.CType.Name = "CType";
            this.CType.ReadOnly = true;
            // 
            // COP
            // 
            this.COP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.COP.DataPropertyName = "OP1";
            this.COP.HeaderText = "操作";
            this.COP.MinimumWidth = 8;
            this.COP.Name = "COP";
            this.COP.ReadOnly = true;
            // 
            // COP2
            // 
            this.COP2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.COP2.DataPropertyName = "OP2";
            this.COP2.HeaderText = "操作";
            this.COP2.MinimumWidth = 8;
            this.COP2.Name = "COP2";
            this.COP2.ReadOnly = true;
            // 
            // brefresh
            // 
            this.brefresh.Location = new System.Drawing.Point(1381, 125);
            this.brefresh.Margin = new System.Windows.Forms.Padding(4);
            this.brefresh.Name = "brefresh";
            this.brefresh.Size = new System.Drawing.Size(186, 68);
            this.brefresh.TabIndex = 1;
            this.brefresh.Text = "刷新";
            this.brefresh.UseVisualStyleBackColor = true;
            this.brefresh.Click += new System.EventHandler(this.brefresh_Click);
            // 
            // CB_ShowOnline
            // 
            this.CB_ShowOnline.AutoSize = true;
            this.CB_ShowOnline.Checked = true;
            this.CB_ShowOnline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_ShowOnline.Location = new System.Drawing.Point(1409, 17);
            this.CB_ShowOnline.Margin = new System.Windows.Forms.Padding(4);
            this.CB_ShowOnline.Name = "CB_ShowOnline";
            this.CB_ShowOnline.Size = new System.Drawing.Size(108, 28);
            this.CB_ShowOnline.TabIndex = 2;
            this.CB_ShowOnline.Text = "显示在线";
            this.CB_ShowOnline.UseVisualStyleBackColor = true;
            this.CB_ShowOnline.CheckedChanged += new System.EventHandler(this.CB_ShowOnline_CheckedChanged);
            // 
            // CB_ShowNotOnline
            // 
            this.CB_ShowNotOnline.AutoSize = true;
            this.CB_ShowNotOnline.Checked = true;
            this.CB_ShowNotOnline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_ShowNotOnline.Location = new System.Drawing.Point(1409, 55);
            this.CB_ShowNotOnline.Margin = new System.Windows.Forms.Padding(4);
            this.CB_ShowNotOnline.Name = "CB_ShowNotOnline";
            this.CB_ShowNotOnline.Size = new System.Drawing.Size(108, 28);
            this.CB_ShowNotOnline.TabIndex = 3;
            this.CB_ShowNotOnline.Text = "显示离线";
            this.CB_ShowNotOnline.UseVisualStyleBackColor = true;
            this.CB_ShowNotOnline.CheckedChanged += new System.EventHandler(this.CB_ShowNotOnline_CheckedChanged);
            // 
            // list_Group
            // 
            this.list_Group.AllowDrop = true;
            this.list_Group.FormattingEnabled = true;
            this.list_Group.ItemHeight = 24;
            this.list_Group.Location = new System.Drawing.Point(15, 13);
            this.list_Group.Margin = new System.Windows.Forms.Padding(4);
            this.list_Group.Name = "list_Group";
            this.list_Group.Size = new System.Drawing.Size(223, 964);
            this.list_Group.TabIndex = 4;
            this.list_Group.SelectedIndexChanged += new System.EventHandler(this.list_Group_SelectedIndexChanged);
            this.list_Group.DragDrop += new System.Windows.Forms.DragEventHandler(this.list_Group_DragDrop);
            this.list_Group.DragEnter += new System.Windows.Forms.DragEventHandler(this.list_Group_DragEnter);
            // 
            // btn_groupmgr
            // 
            this.btn_groupmgr.Location = new System.Drawing.Point(1381, 277);
            this.btn_groupmgr.Margin = new System.Windows.Forms.Padding(4);
            this.btn_groupmgr.Name = "btn_groupmgr";
            this.btn_groupmgr.Size = new System.Drawing.Size(186, 68);
            this.btn_groupmgr.TabIndex = 5;
            this.btn_groupmgr.Text = "分组";
            this.btn_groupmgr.UseVisualStyleBackColor = true;
            this.btn_groupmgr.Click += new System.EventHandler(this.btn_groupmgr_Click);
            // 
            // b_sendcmd
            // 
            this.b_sendcmd.Location = new System.Drawing.Point(1381, 353);
            this.b_sendcmd.Margin = new System.Windows.Forms.Padding(4);
            this.b_sendcmd.Name = "b_sendcmd";
            this.b_sendcmd.Size = new System.Drawing.Size(186, 68);
            this.b_sendcmd.TabIndex = 6;
            this.b_sendcmd.Text = "发送命令";
            this.b_sendcmd.UseVisualStyleBackColor = true;
            this.b_sendcmd.Click += new System.EventHandler(this.b_sendcmd_Click);
            // 
            // bgroupmove
            // 
            this.bgroupmove.Location = new System.Drawing.Point(1381, 429);
            this.bgroupmove.Margin = new System.Windows.Forms.Padding(4);
            this.bgroupmove.Name = "bgroupmove";
            this.bgroupmove.Size = new System.Drawing.Size(186, 68);
            this.bgroupmove.TabIndex = 7;
            this.bgroupmove.Text = "移动分组";
            this.bgroupmove.UseVisualStyleBackColor = true;
            this.bgroupmove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bgroupmove_MouseDown);
            // 
            // bselectall
            // 
            this.bselectall.Location = new System.Drawing.Point(1381, 201);
            this.bselectall.Margin = new System.Windows.Forms.Padding(4);
            this.bselectall.Name = "bselectall";
            this.bselectall.Size = new System.Drawing.Size(186, 68);
            this.bselectall.TabIndex = 8;
            this.bselectall.Text = "全选";
            this.bselectall.UseVisualStyleBackColor = true;
            this.bselectall.Click += new System.EventHandler(this.bselectall_Click);
            // 
            // bsetting
            // 
            this.bsetting.Location = new System.Drawing.Point(1381, 505);
            this.bsetting.Margin = new System.Windows.Forms.Padding(4);
            this.bsetting.Name = "bsetting";
            this.bsetting.Size = new System.Drawing.Size(186, 68);
            this.bsetting.TabIndex = 9;
            this.bsetting.Text = "定时配置";
            this.bsetting.UseVisualStyleBackColor = true;
            this.bsetting.Click += new System.EventHandler(this.bsetting_Click);
            // 
            // btn_creatdevice
            // 
            this.btn_creatdevice.Location = new System.Drawing.Point(1382, 581);
            this.btn_creatdevice.Margin = new System.Windows.Forms.Padding(4);
            this.btn_creatdevice.Name = "btn_creatdevice";
            this.btn_creatdevice.Size = new System.Drawing.Size(186, 68);
            this.btn_creatdevice.TabIndex = 10;
            this.btn_creatdevice.Text = "创建设备";
            this.btn_creatdevice.UseVisualStyleBackColor = true;
            this.btn_creatdevice.Click += new System.EventHandler(this.btn_creatdevice_Click);
            // 
            // FAccessibleDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1611, 1005);
            this.Controls.Add(this.btn_creatdevice);
            this.Controls.Add(this.bsetting);
            this.Controls.Add(this.bselectall);
            this.Controls.Add(this.bgroupmove);
            this.Controls.Add(this.b_sendcmd);
            this.Controls.Add(this.btn_groupmgr);
            this.Controls.Add(this.list_Group);
            this.Controls.Add(this.CB_ShowNotOnline);
            this.Controls.Add(this.CB_ShowOnline);
            this.Controls.Add(this.brefresh);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FAccessibleDevice";
            this.Text = "FAccessibleDevice";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private Button btn_creatdevice;
    }
}