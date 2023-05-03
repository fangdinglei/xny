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
            btn_deletmail = new Button();
            btn_outteremail = new Button();
            list_mails = new ListBox();
            text_context = new TextBox();
            pageController1 = new FdlWindows.View.PageController();
            text_baseinfo = new TextBox();
            SuspendLayout();
            // 
            // btn_deletmail
            // 
            btn_deletmail.Location = new Point(1030, 84);
            btn_deletmail.Name = "btn_deletmail";
            btn_deletmail.Size = new Size(137, 69);
            btn_deletmail.TabIndex = 20;
            btn_deletmail.Text = "删除此邮件";
            btn_deletmail.UseVisualStyleBackColor = true;
            btn_deletmail.Visible = false;
            btn_deletmail.Click += btn_deletmail_Click;
            // 
            // btn_outteremail
            // 
            btn_outteremail.Location = new Point(1030, 9);
            btn_outteremail.Name = "btn_outteremail";
            btn_outteremail.Size = new Size(137, 69);
            btn_outteremail.TabIndex = 17;
            btn_outteremail.Text = "站外邮件推送";
            btn_outteremail.UseVisualStyleBackColor = true;
            btn_outteremail.Click += btn_outteremail_Click;
            // 
            // list_mails
            // 
            list_mails.FormattingEnabled = true;
            list_mails.ItemHeight = 24;
            list_mails.Location = new Point(10, 13);
            list_mails.Margin = new Padding(4);
            list_mails.Name = "list_mails";
            list_mails.Size = new Size(237, 604);
            list_mails.TabIndex = 19;
            list_mails.SelectedIndexChanged += list_mails_SelectedIndexChanged;
            // 
            // text_context
            // 
            text_context.Location = new Point(254, 60);
            text_context.Multiline = true;
            text_context.Name = "text_context";
            text_context.Size = new Size(770, 605);
            text_context.TabIndex = 18;
            // 
            // pageController1
            // 
            pageController1.BackColor = Color.Lime;
            pageController1.Location = new Point(10, 626);
            pageController1.Name = "pageController1";
            pageController1.Page = 1;
            pageController1.PageSize = 1;
            pageController1.RecordCount = 0;
            pageController1.Size = new Size(237, 39);
            pageController1.TabIndex = 21;
            pageController1.OnPageChanged += pageController1_OnPageChanged;
            // 
            // text_baseinfo
            // 
            text_baseinfo.Location = new Point(254, 12);
            text_baseinfo.Name = "text_baseinfo";
            text_baseinfo.ReadOnly = true;
            text_baseinfo.Size = new Size(554, 30);
            text_baseinfo.TabIndex = 22;
            // 
            // FInternalMail
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1340, 706);
            Controls.Add(text_baseinfo);
            Controls.Add(pageController1);
            Controls.Add(btn_deletmail);
            Controls.Add(btn_outteremail);
            Controls.Add(list_mails);
            Controls.Add(text_context);
            Name = "FInternalMail";
            Text = "FInternalMail";
            ResumeLayout(false);
            PerformLayout();
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