
namespace MyClient.View
{
    partial class FDeviceGroupManager
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
            this.list_Group = new System.Windows.Forms.ListBox();
            this.btn_del = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.tname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_creat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list_Group
            // 
            this.list_Group.FormattingEnabled = true;
            this.list_Group.ItemHeight = 18;
            this.list_Group.Location = new System.Drawing.Point(12, 12);
            this.list_Group.Name = "list_Group";
            this.list_Group.Size = new System.Drawing.Size(246, 328);
            this.list_Group.TabIndex = 5;
            this.list_Group.SelectedIndexChanged += new System.EventHandler(this.list_Group_SelectedIndexChanged);
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(267, 149);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(116, 58);
            this.btn_del.TabIndex = 6;
            this.btn_del.Text = "删除";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(538, 149);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(116, 58);
            this.btn_update.TabIndex = 7;
            this.btn_update.Text = "确认修改";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // tname
            // 
            this.tname.Location = new System.Drawing.Point(355, 64);
            this.tname.Name = "tname";
            this.tname.Size = new System.Drawing.Size(173, 28);
            this.tname.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "名称";
            // 
            // btn_creat
            // 
            this.btn_creat.Location = new System.Drawing.Point(406, 149);
            this.btn_creat.Name = "btn_creat";
            this.btn_creat.Size = new System.Drawing.Size(116, 58);
            this.btn_creat.TabIndex = 10;
            this.btn_creat.Text = "创建";
            this.btn_creat.UseVisualStyleBackColor = true;
            this.btn_creat.Click += new System.EventHandler(this.btn_creat_Click);
            // 
            // FDeviceGroupManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 378);
            this.Controls.Add(this.btn_creat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tname);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.list_Group);
            this.Name = "FDeviceGroupManager";
            this.Text = "FDeviceGroupManager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox list_Group;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.TextBox tname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_creat;
    }
}