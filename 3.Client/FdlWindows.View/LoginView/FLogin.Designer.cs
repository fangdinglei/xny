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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tUName = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.tPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btLogin = new System.Windows.Forms.Button();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox5.Controls.Add(this.tUName);
            this.groupBox5.Controls.Add(this.label);
            this.groupBox5.Controls.Add(this.tPass);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.btLogin);
            this.groupBox5.Location = new System.Drawing.Point(15, 16);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(488, 477);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            // 
            // tUName
            // 
            this.tUName.Location = new System.Drawing.Point(188, 132);
            this.tUName.Margin = new System.Windows.Forms.Padding(4);
            this.tUName.Name = "tUName";
            this.tUName.Size = new System.Drawing.Size(279, 30);
            this.tUName.TabIndex = 17;
            this.tUName.Text = "user2";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label.ForeColor = System.Drawing.Color.Black;
            this.label.Location = new System.Drawing.Point(55, 141);
            this.label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(76, 21);
            this.label.TabIndex = 14;
            this.label.Text = "用户名";
            this.label.Click += new System.EventHandler(this.label_Click);
            // 
            // tPass
            // 
            this.tPass.Location = new System.Drawing.Point(188, 241);
            this.tPass.Margin = new System.Windows.Forms.Padding(4);
            this.tPass.Name = "tPass";
            this.tPass.PasswordChar = '*';
            this.tPass.Size = new System.Drawing.Size(279, 30);
            this.tPass.TabIndex = 0;
            this.tPass.Text = "123";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(53, 251);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 21);
            this.label3.TabIndex = 13;
            this.label3.Text = "密  码";
            // 
            // btLogin
            // 
            this.btLogin.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btLogin.ForeColor = System.Drawing.Color.Black;
            this.btLogin.Location = new System.Drawing.Point(188, 392);
            this.btLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(110, 45);
            this.btLogin.TabIndex = 2;
            this.btLogin.Text = "登录";
            this.btLogin.UseVisualStyleBackColor = true;
            this.btLogin.Click += new System.EventHandler(this.btLogin_ClickAsync);
            // 
            // FLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 516);
            this.Controls.Add(this.groupBox5);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

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