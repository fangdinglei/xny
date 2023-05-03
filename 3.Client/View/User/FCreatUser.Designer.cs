namespace MyClient.View.User
{
    partial class FCreatUser
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
            label9 = new Label();
            tmaxsubuser = new TextBox();
            tmaxdeep = new TextBox();
            label8 = new Label();
            label6 = new Label();
            temail = new TextBox();
            label4 = new Label();
            tphone = new TextBox();
            label1 = new Label();
            tpass = new TextBox();
            tuname = new TextBox();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(373, 114);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(136, 24);
            label9.TabIndex = 54;
            label9.Text = "最大子用户数量";
            // 
            // tmaxsubuser
            // 
            tmaxsubuser.Location = new Point(375, 142);
            tmaxsubuser.Margin = new Padding(4);
            tmaxsubuser.Name = "tmaxsubuser";
            tmaxsubuser.Size = new Size(112, 30);
            tmaxsubuser.TabIndex = 53;
            tmaxsubuser.Text = "1";
            // 
            // tmaxdeep
            // 
            tmaxdeep.Location = new Point(375, 78);
            tmaxdeep.Margin = new Padding(4);
            tmaxdeep.Name = "tmaxdeep";
            tmaxdeep.Size = new Size(112, 30);
            tmaxdeep.TabIndex = 52;
            tmaxdeep.Text = "1";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(375, 50);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(136, 24);
            label8.TabIndex = 51;
            label8.Text = "最大子用户深度";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(22, 168);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(46, 24);
            label6.TabIndex = 47;
            label6.Text = "邮箱";
            // 
            // temail
            // 
            temail.Location = new Point(94, 164);
            temail.Margin = new Padding(4);
            temail.Name = "temail";
            temail.Size = new Size(242, 30);
            temail.TabIndex = 48;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 130);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(46, 24);
            label4.TabIndex = 45;
            label4.Text = "电话";
            // 
            // tphone
            // 
            tphone.Location = new Point(94, 126);
            tphone.Margin = new Padding(4);
            tphone.Name = "tphone";
            tphone.Size = new Size(242, 30);
            tphone.TabIndex = 46;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 50);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(64, 24);
            label1.TabIndex = 39;
            label1.Text = "用户名";
            // 
            // tpass
            // 
            tpass.Location = new Point(94, 88);
            tpass.Margin = new Padding(4);
            tpass.Name = "tpass";
            tpass.Size = new Size(242, 30);
            tpass.TabIndex = 44;
            // 
            // tuname
            // 
            tuname.Location = new Point(94, 50);
            tuname.Margin = new Padding(4);
            tuname.Name = "tuname";
            tuname.Size = new Size(242, 30);
            tuname.TabIndex = 40;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 92);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(46, 24);
            label3.TabIndex = 43;
            label3.Text = "密码";
            // 
            // button1
            // 
            button1.Location = new Point(164, 235);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 55;
            button1.Text = "确定";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_ClickAsync;
            // 
            // button2
            // 
            button2.Location = new Point(309, 235);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 56;
            button2.Text = "取消";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // FCreatUser
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(574, 304);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label9);
            Controls.Add(tmaxsubuser);
            Controls.Add(tmaxdeep);
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(temail);
            Controls.Add(label4);
            Controls.Add(tphone);
            Controls.Add(label1);
            Controls.Add(tpass);
            Controls.Add(tuname);
            Controls.Add(label3);
            Name = "FCreatUser";
            Text = "FCreatUser";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label9;
        private TextBox tmaxsubuser;
        private TextBox tmaxdeep;
        private Label label8;
        private Label label6;
        private TextBox temail;
        private Label label4;
        private TextBox tphone;
        private Label label1;
        private TextBox tpass;
        private TextBox tuname;
        private Label label3;
        private Button button1;
        private Button button2;
    }
}