namespace FdlWindows.View.LoginView
{
    partial class FLogin
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
            groupBox5 = new GroupBox();
            tUName = new TextBox();
            label = new Label();
            tPass = new TextBox();
            label3 = new Label();
            btLogin = new Button();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox5.Controls.Add(tUName);
            groupBox5.Controls.Add(label);
            groupBox5.Controls.Add(tPass);
            groupBox5.Controls.Add(label3);
            groupBox5.Controls.Add(btLogin);
            groupBox5.Location = new Point(15, 16);
            groupBox5.Margin = new Padding(4);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(4);
            groupBox5.Size = new Size(488, 477);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            // 
            // tUName
            // 
            tUName.Location = new Point(188, 141);
            tUName.Margin = new Padding(4);
            tUName.Name = "tUName";
            tUName.Size = new Size(279, 30);
            tUName.TabIndex = 17;
            tUName.Text = "2";
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("宋体", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            label.ForeColor = Color.Black;
            label.Location = new Point(55, 141);
            label.Margin = new Padding(4, 0, 4, 0);
            label.Name = "label";
            label.Size = new Size(78, 21);
            label.TabIndex = 14;
            label.Text = "用户Id";
            label.Click += label_Click;
            // 
            // tPass
            // 
            tPass.Location = new Point(188, 241);
            tPass.Margin = new Padding(4);
            tPass.Name = "tPass";
            tPass.PasswordChar = '*';
            tPass.Size = new Size(279, 30);
            tPass.TabIndex = 0;
            tPass.Text = "123";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("宋体", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(53, 251);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(78, 21);
            label3.TabIndex = 13;
            label3.Text = "密  码";
            // 
            // btLogin
            // 
            btLogin.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btLogin.ForeColor = Color.Black;
            btLogin.Location = new Point(188, 392);
            btLogin.Margin = new Padding(4);
            btLogin.Name = "btLogin";
            btLogin.Size = new Size(110, 45);
            btLogin.TabIndex = 2;
            btLogin.Text = "登录";
            btLogin.UseVisualStyleBackColor = true;
            btLogin.Click += btLogin_ClickAsync;
            // 
            // FLogin
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 516);
            Controls.Add(groupBox5);
            Margin = new Padding(4);
            Name = "FLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "登录";
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btLogin;
        private System.Windows.Forms.TextBox tUName;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox tPass;
        private System.Windows.Forms.Label label3;
    }
}