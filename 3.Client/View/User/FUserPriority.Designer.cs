namespace MyClient.View.User
{
    partial class FUserPriority
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.group_priority_btn_ok = new System.Windows.Forms.Button();
            this.group_priority_list = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.group_priority_btn_ok, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.group_priority_list, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(514, 608);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // group_priority_btn_ok
            // 
            this.group_priority_btn_ok.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.group_priority_btn_ok.Location = new System.Drawing.Point(166, 419);
            this.group_priority_btn_ok.Name = "group_priority_btn_ok";
            this.group_priority_btn_ok.Size = new System.Drawing.Size(181, 54);
            this.group_priority_btn_ok.TabIndex = 1;
            this.group_priority_btn_ok.Text = "提交变更";
            this.group_priority_btn_ok.UseVisualStyleBackColor = true;
            // 
            // group_priority_list
            // 
            this.group_priority_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group_priority_list.FormattingEnabled = true;
            this.group_priority_list.Location = new System.Drawing.Point(3, 3);
            this.group_priority_list.Name = "group_priority_list";
            this.group_priority_list.Size = new System.Drawing.Size(508, 382);
            this.group_priority_list.TabIndex = 0;
            // 
            // FUserPriority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 650);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FUserPriority";
            this.Text = "FUserPriority";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button group_priority_btn_ok;
        private CheckedListBox group_priority_list;
    }
}