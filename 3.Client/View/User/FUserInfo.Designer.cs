namespace MyClient.View.User
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
            btn_sendMail = new Button();
            label6 = new Label();
            temail = new TextBox();
            baseinfo_authoritys = new TextBox();
            btn_delet = new Button();
            label5 = new Label();
            label4 = new Label();
            tphone = new TextBox();
            label1 = new Label();
            tpass = new TextBox();
            tuname = new TextBox();
            btn_passupdate = new Button();
            label2 = new Label();
            label3 = new Label();
            tuid = new TextBox();
            btn_update = new Button();
            label7 = new Label();
            tdeep = new TextBox();
            label8 = new Label();
            tmaxdeep = new TextBox();
            tmaxsubuser = new TextBox();
            label9 = new Label();
            btn_adduser = new Button();
            SuspendLayout();
            // 
            // btn_sendMail
            // 
            btn_sendMail.Location = new Point(13, 469);
            btn_sendMail.Margin = new Padding(4);
            btn_sendMail.Name = "btn_sendMail";
            btn_sendMail.Size = new Size(114, 49);
            btn_sendMail.TabIndex = 32;
            btn_sendMail.Text = "发送邮件";
            btn_sendMail.UseVisualStyleBackColor = true;
            btn_sendMail.Click += btn_sendMail_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(13, 230);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(46, 24);
            label6.TabIndex = 30;
            label6.Text = "邮箱";
            // 
            // temail
            // 
            temail.Location = new Point(115, 226);
            temail.Margin = new Padding(4);
            temail.Name = "temail";
            temail.Size = new Size(242, 30);
            temail.TabIndex = 31;
            // 
            // baseinfo_authoritys
            // 
            baseinfo_authoritys.Location = new Point(115, 264);
            baseinfo_authoritys.Margin = new Padding(4);
            baseinfo_authoritys.Multiline = true;
            baseinfo_authoritys.Name = "baseinfo_authoritys";
            baseinfo_authoritys.ScrollBars = ScrollBars.Vertical;
            baseinfo_authoritys.Size = new Size(242, 129);
            baseinfo_authoritys.TabIndex = 29;
            // 
            // btn_delet
            // 
            btn_delet.Location = new Point(264, 412);
            btn_delet.Margin = new Padding(4);
            btn_delet.Name = "btn_delet";
            btn_delet.Size = new Size(114, 49);
            btn_delet.TabIndex = 27;
            btn_delet.Text = "删除用户";
            btn_delet.UseVisualStyleBackColor = true;
            btn_delet.Click += btn_delet_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 264);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(46, 24);
            label5.TabIndex = 28;
            label5.Text = "权限";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 192);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(46, 24);
            label4.TabIndex = 23;
            label4.Text = "电话";
            // 
            // tphone
            // 
            tphone.Location = new Point(115, 188);
            tphone.Margin = new Padding(4);
            tphone.Name = "tphone";
            tphone.Size = new Size(242, 30);
            tphone.TabIndex = 24;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 16);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(64, 24);
            label1.TabIndex = 17;
            label1.Text = "用户名";
            // 
            // tpass
            // 
            tpass.Location = new Point(115, 138);
            tpass.Margin = new Padding(4);
            tpass.Name = "tpass";
            tpass.ReadOnly = true;
            tpass.Size = new Size(242, 30);
            tpass.TabIndex = 22;
            // 
            // tuname
            // 
            tuname.Location = new Point(115, 16);
            tuname.Margin = new Padding(4);
            tuname.Name = "tuname";
            tuname.Size = new Size(242, 30);
            tuname.TabIndex = 18;
            // 
            // btn_passupdate
            // 
            btn_passupdate.Location = new Point(13, 412);
            btn_passupdate.Margin = new Padding(4);
            btn_passupdate.Name = "btn_passupdate";
            btn_passupdate.Size = new Size(114, 49);
            btn_passupdate.TabIndex = 25;
            btn_passupdate.Text = "修改密码";
            btn_passupdate.UseVisualStyleBackColor = true;
            btn_passupdate.Click += btn_passupdate_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 80);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(46, 24);
            label2.TabIndex = 19;
            label2.Text = "账号";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 142);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(46, 24);
            label3.TabIndex = 21;
            label3.Text = "密码";
            // 
            // tuid
            // 
            tuid.Location = new Point(115, 76);
            tuid.Margin = new Padding(4);
            tuid.Name = "tuid";
            tuid.ReadOnly = true;
            tuid.Size = new Size(242, 30);
            tuid.TabIndex = 20;
            // 
            // btn_update
            // 
            btn_update.Location = new Point(134, 412);
            btn_update.Margin = new Padding(4);
            btn_update.Name = "btn_update";
            btn_update.Size = new Size(122, 49);
            btn_update.TabIndex = 26;
            btn_update.Text = "修改信息";
            btn_update.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(384, 16);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(82, 24);
            label7.TabIndex = 33;
            label7.Text = "用户深度";
            // 
            // tdeep
            // 
            tdeep.Location = new Point(384, 44);
            tdeep.Margin = new Padding(4);
            tdeep.Name = "tdeep";
            tdeep.Size = new Size(112, 30);
            tdeep.TabIndex = 34;
            tdeep.Text = "1";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(384, 80);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(136, 24);
            label8.TabIndex = 35;
            label8.Text = "最大子用户深度";
            // 
            // tmaxdeep
            // 
            tmaxdeep.Location = new Point(384, 108);
            tmaxdeep.Margin = new Padding(4);
            tmaxdeep.Name = "tmaxdeep";
            tmaxdeep.Size = new Size(112, 30);
            tmaxdeep.TabIndex = 36;
            tmaxdeep.Text = "1";
            // 
            // tmaxsubuser
            // 
            tmaxsubuser.Location = new Point(384, 172);
            tmaxsubuser.Margin = new Padding(4);
            tmaxsubuser.Name = "tmaxsubuser";
            tmaxsubuser.Size = new Size(112, 30);
            tmaxsubuser.TabIndex = 37;
            tmaxsubuser.Text = "1";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(382, 144);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(136, 24);
            label9.TabIndex = 38;
            label9.Text = "最大子用户数量";
            // 
            // btn_adduser
            // 
            btn_adduser.Location = new Point(135, 469);
            btn_adduser.Margin = new Padding(4);
            btn_adduser.Name = "btn_adduser";
            btn_adduser.Size = new Size(122, 49);
            btn_adduser.TabIndex = 39;
            btn_adduser.Text = "新增用户";
            btn_adduser.UseVisualStyleBackColor = true;
            btn_adduser.Click += btn_adduser_Click;
            // 
            // FUserInfo
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(531, 572);
            Controls.Add(btn_adduser);
            Controls.Add(label9);
            Controls.Add(tmaxsubuser);
            Controls.Add(tmaxdeep);
            Controls.Add(label8);
            Controls.Add(tdeep);
            Controls.Add(label7);
            Controls.Add(btn_sendMail);
            Controls.Add(label6);
            Controls.Add(temail);
            Controls.Add(baseinfo_authoritys);
            Controls.Add(btn_delet);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(tphone);
            Controls.Add(label1);
            Controls.Add(tpass);
            Controls.Add(tuname);
            Controls.Add(btn_passupdate);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(tuid);
            Controls.Add(btn_update);
            Name = "FUserInfo";
            Text = "FUserInfo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_sendMail;
        private Label label6;
        private TextBox temail;
        private TextBox baseinfo_authoritys;
        private Button btn_delet;
        private Label label5;
        private Label label4;
        private TextBox tphone;
        private Label label1;
        private TextBox tpass;
        private TextBox tuname;
        private Button btn_passupdate;
        private Label label2;
        private Label label3;
        private TextBox tuid;
        private Button btn_update;
        private Label label7;
        private TextBox tdeep;
        private Label label8;
        private TextBox tmaxdeep;
        private TextBox tmaxsubuser;
        private Label label9;
        private Button btn_adduser;
    }
}