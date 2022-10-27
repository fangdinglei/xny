namespace MyClient.View
{
    partial class FSendInternalMail
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
            this.text_title = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.text_context = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "收件人:AAA";
            // 
            // text_title
            // 
            this.text_title.Location = new System.Drawing.Point(64, 33);
            this.text_title.Name = "text_title";
            this.text_title.Size = new System.Drawing.Size(510, 30);
            this.text_title.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "标题";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "内容";
            // 
            // text_context
            // 
            this.text_context.Location = new System.Drawing.Point(64, 69);
            this.text_context.Multiline = true;
            this.text_context.Name = "text_context";
            this.text_context.Size = new System.Drawing.Size(510, 483);
            this.text_context.TabIndex = 4;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(629, 29);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(112, 34);
            this.btn_send.TabIndex = 5;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // FSendInternalMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 648);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.text_context);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.text_title);
            this.Controls.Add(this.label1);
            this.Name = "FSendInternalMail";
            this.Text = "FSendInternalMail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox text_title;
        private Label label2;
        private Label label3;
        private TextBox text_context;
        private Button btn_send;
    }
}