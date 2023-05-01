namespace MyClient.View
{
    partial class FDeviceCmdHistory
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
            listHistory = new ListBox();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // listHistory
            // 
            listHistory.FormattingEnabled = true;
            listHistory.ItemHeight = 24;
            listHistory.Location = new Point(12, 12);
            listHistory.Name = "listHistory";
            listHistory.Size = new Size(235, 580);
            listHistory.TabIndex = 1;
            listHistory.SelectedIndexChanged += listHistory_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(253, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(399, 580);
            textBox1.TabIndex = 2;
            // 
            // FDeviceCmdHistory
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(974, 613);
            Controls.Add(textBox1);
            Controls.Add(listHistory);
            Name = "FDeviceCmdHistory";
            Text = "FDeviceCmdHistory";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listHistory;
        private TextBox textBox1;
    }
}