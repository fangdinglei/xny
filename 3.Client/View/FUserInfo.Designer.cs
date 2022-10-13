
namespace MyClient.View
{
    partial class FUserInfo
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
            this.tuname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tuid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tpass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tphone = new System.Windows.Forms.TextBox();
            this.btn_passupdate = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.list_user = new System.Windows.Forms.ListBox();
            this.btn_delet = new System.Windows.Forms.Button();
            this.list_device = new System.Windows.Forms.CheckedListBox();
            this.bok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // tuname
            // 
            this.tuname.Location = new System.Drawing.Point(298, 34);
            this.tuname.Name = "tuname";
            this.tuname.Size = new System.Drawing.Size(199, 28);
            this.tuname.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "账号";
            // 
            // tuid
            // 
            this.tuid.Location = new System.Drawing.Point(298, 79);
            this.tuid.Name = "tuid";
            this.tuid.ReadOnly = true;
            this.tuid.Size = new System.Drawing.Size(199, 28);
            this.tuid.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码";
            // 
            // tpass
            // 
            this.tpass.Location = new System.Drawing.Point(298, 125);
            this.tpass.Name = "tpass";
            this.tpass.Size = new System.Drawing.Size(199, 28);
            this.tpass.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(214, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "电话";
            // 
            // tphone
            // 
            this.tphone.Location = new System.Drawing.Point(298, 178);
            this.tphone.Name = "tphone";
            this.tphone.Size = new System.Drawing.Size(199, 28);
            this.tphone.TabIndex = 7;
            // 
            // btn_passupdate
            // 
            this.btn_passupdate.Location = new System.Drawing.Point(217, 251);
            this.btn_passupdate.Name = "btn_passupdate";
            this.btn_passupdate.Size = new System.Drawing.Size(93, 37);
            this.btn_passupdate.TabIndex = 8;
            this.btn_passupdate.Text = "修改密码";
            this.btn_passupdate.UseVisualStyleBackColor = true;
            this.btn_passupdate.Click += new System.EventHandler(this.btn_passupdate_Click);
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(316, 251);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(100, 37);
            this.btn_update.TabIndex = 9;
            this.btn_update.Text = "修改信息";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // list_user
            // 
            this.list_user.FormattingEnabled = true;
            this.list_user.ItemHeight = 18;
            this.list_user.Location = new System.Drawing.Point(12, 12);
            this.list_user.Name = "list_user";
            this.list_user.Size = new System.Drawing.Size(164, 508);
            this.list_user.TabIndex = 10;
            this.list_user.SelectedIndexChanged += new System.EventHandler(this.list_user_SelectedIndexChanged);
            // 
            // btn_delet
            // 
            this.btn_delet.Location = new System.Drawing.Point(422, 251);
            this.btn_delet.Name = "btn_delet";
            this.btn_delet.Size = new System.Drawing.Size(93, 37);
            this.btn_delet.TabIndex = 11;
            this.btn_delet.Text = "删除用户";
            this.btn_delet.UseVisualStyleBackColor = true;
            this.btn_delet.Click += new System.EventHandler(this.btn_delet_Click);
            // 
            // list_device
            // 
            this.list_device.FormattingEnabled = true;
            this.list_device.Location = new System.Drawing.Point(562, 12);
            this.list_device.Name = "list_device";
            this.list_device.Size = new System.Drawing.Size(276, 479);
            this.list_device.TabIndex = 14;
            // 
            // bok
            // 
            this.bok.Location = new System.Drawing.Point(844, 12);
            this.bok.Name = "bok";
            this.bok.Size = new System.Drawing.Size(93, 37);
            this.bok.TabIndex = 15;
            this.bok.Text = "确认修改";
            this.bok.UseVisualStyleBackColor = true;
            this.bok.Click += new System.EventHandler(this.bok_Click);
            // 
            // FUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 537);
            this.Controls.Add(this.bok);
            this.Controls.Add(this.list_device);
            this.Controls.Add(this.btn_delet);
            this.Controls.Add(this.list_user);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.btn_passupdate);
            this.Controls.Add(this.tphone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tpass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tuid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tuname);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FUserInfo";
            this.Text = "FUserInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tuname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tuid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tpass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tphone;
        private System.Windows.Forms.Button btn_passupdate;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.ListBox list_user;
        private System.Windows.Forms.Button btn_delet;
        private System.Windows.Forms.CheckedListBox list_device;
        private System.Windows.Forms.Button bok;
    }
}