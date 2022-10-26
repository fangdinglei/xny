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
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.list_mails = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1030, 84);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(137, 69);
            this.button3.TabIndex = 20;
            this.button3.Text = "删除此邮件";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1030, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 69);
            this.button1.TabIndex = 17;
            this.button1.Text = "站外邮件推送";
            this.button1.UseVisualStyleBackColor = true;
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
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(254, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(770, 605);
            this.textBox1.TabIndex = 18;
            // 
            // FInternalMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 706);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.list_mails);
            this.Controls.Add(this.textBox1);
            this.Name = "FInternalMail";
            this.Text = "FInternalMail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button3;
        private Button button1;
        private ListBox list_mails;
        private TextBox textBox1;
    }
}