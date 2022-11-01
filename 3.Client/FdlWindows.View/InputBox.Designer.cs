
namespace System.Windows.Forms
{
    partial class FInputBox
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
            this.tinput = new System.Windows.Forms.TextBox();
            this.bcacel = new System.Windows.Forms.Button();
            this.bok = new System.Windows.Forms.Button();
            this.ltip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tinput
            // 
            this.tinput.Font = new System.Drawing.Font("宋体", 13F);
            this.tinput.Location = new System.Drawing.Point(33, 69);
            this.tinput.Name = "tinput";
            this.tinput.Size = new System.Drawing.Size(276, 37);
            this.tinput.TabIndex = 0;
            // 
            // bcacel
            // 
            this.bcacel.Location = new System.Drawing.Point(33, 140);
            this.bcacel.Name = "bcacel";
            this.bcacel.Size = new System.Drawing.Size(99, 40);
            this.bcacel.TabIndex = 1;
            this.bcacel.Text = "取消";
            this.bcacel.UseVisualStyleBackColor = true;
            this.bcacel.Click += new System.EventHandler(this.bcacel_Click);
            // 
            // bok
            // 
            this.bok.Location = new System.Drawing.Point(210, 140);
            this.bok.Name = "bok";
            this.bok.Size = new System.Drawing.Size(99, 40);
            this.bok.TabIndex = 2;
            this.bok.Text = "确认";
            this.bok.UseVisualStyleBackColor = true;
            this.bok.Click += new System.EventHandler(this.bok_Click);
            // 
            // ltip
            // 
            this.ltip.AutoSize = true;
            this.ltip.Location = new System.Drawing.Point(30, 32);
            this.ltip.Name = "ltip";
            this.ltip.Size = new System.Drawing.Size(62, 18);
            this.ltip.TabIndex = 3;
            this.ltip.Text = "请输入";
            // 
            // FInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 229);
            this.Controls.Add(this.ltip);
            this.Controls.Add(this.bok);
            this.Controls.Add(this.bcacel);
            this.Controls.Add(this.tinput);
            this.Name = "FInputBox";
            this.Text = "InputBox";
            this.Load += new System.EventHandler(this.InputBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tinput;
        private System.Windows.Forms.Button bcacel;
        private System.Windows.Forms.Button bok;
        private System.Windows.Forms.Label ltip;
    }
}