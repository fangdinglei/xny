namespace MyClient.View
{
    partial class FInternalMail
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
            this.btn_deletmail = new System.Windows.Forms.Button();
            this.btn_outteremail = new System.Windows.Forms.Button();
            this.list_mails = new System.Windows.Forms.ListBox();
            this.text_context = new System.Windows.Forms.TextBox();
            this.pageController1 = new FdlWindows.View.PageController();
            this.text_baseinfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_deletmail
            // 
            this.btn_deletmail.Location = new System.Drawing.Point(1030, 84);
            this.btn_deletmail.Name = "btn_deletmail";
            this.btn_deletmail.Size = new System.Drawing.Size(137, 69);
            this.btn_deletmail.TabIndex = 20;
            this.btn_deletmail.Text = "删除此邮件";
            this.btn_deletmail.UseVisualStyleBackColor = true;
            this.btn_deletmail.Click += new System.EventHandler(this.btn_deletmail_Click);
            // 
            // btn_outteremail
            // 
            this.btn_outteremail.Location = new System.Drawing.Point(1030, 9);
            this.btn_outteremail.Name = "btn_outteremail";
            this.btn_outteremail.Size = new System.Drawing.Size(137, 69);
            this.btn_outteremail.TabIndex = 17;
            this.btn_outteremail.Text = "站外邮件推送";
            this.btn_outteremail.UseVisualStyleBackColor = true;
            this.btn_outteremail.Click += new System.EventHandler(this.btn_outteremail_Click);
            // 
            // list_mails
            // 
            this.list_mails.FormattingEnabled = true;
            this.list_mails.ItemHeight = 24;
            this.list_mails.Location = new System.Drawing.Point(10, 13);
            this.list_mails.Margin = new System.Windows.Forms.Padding(4);
            this.list_mails.Name = "list_mails";
            this.list_mails.Size = new System.Drawing.Size(237, 604);
            this.list_mails.TabIndex = 19;
            this.list_mails.SelectedIndexChanged += new System.EventHandler(this.list_mails_SelectedIndexChanged);
            // 
            // text_context
            // 
            this.text_context.Location = new System.Drawing.Point(254, 60);
            this.text_context.Multiline = true;
            this.text_context.Name = "text_context";
            this.text_context.Size = new System.Drawing.Size(770, 605);
            this.text_context.TabIndex = 18;
            // 
            // pageController1
            // 
            this.pageController1.BackColor = System.Drawing.Color.Lime;
            this.pageController1.Location = new System.Drawing.Point(10, 626);
            this.pageController1.Name = "pageController1";
            this.pageController1.Page = 1;
            this.pageController1.PageSize = 1;
            this.pageController1.RecordCount = 0;
            this.pageController1.Size = new System.Drawing.Size(237, 39);
            this.pageController1.TabIndex = 21;
            this.pageController1.OnPageChanged += new System.Action(this.pageController1_OnPageChanged);
            // 
            // text_baseinfo
            // 
            this.text_baseinfo.Location = new System.Drawing.Point(254, 12);
            this.text_baseinfo.Name = "text_baseinfo";
            this.text_baseinfo.ReadOnly = true;
            this.text_baseinfo.Size = new System.Drawing.Size(554, 30);
            this.text_baseinfo.TabIndex = 22;
            // 
            // FInternalMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 706);
            this.Controls.Add(this.text_baseinfo);
            this.Controls.Add(this.pageController1);
            this.Controls.Add(this.btn_deletmail);
            this.Controls.Add(this.btn_outteremail);
            this.Controls.Add(this.list_mails);
            this.Controls.Add(this.text_context);
            this.Name = "FInternalMail";
            this.Text = "FInternalMail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btn_deletmail;
        private Button btn_outteremail;
        private ListBox list_mails;
        private TextBox text_context;
        private FdlWindows.View.PageController pageController1;
        private TextBox text_baseinfo;
    }
}