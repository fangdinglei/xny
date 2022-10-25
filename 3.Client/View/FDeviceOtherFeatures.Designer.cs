namespace MyClient.View
{
    partial class FDeviceOtherFeatures
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
            this.btn_repair = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_repair
            // 
            this.btn_repair.Location = new System.Drawing.Point(24, 12);
            this.btn_repair.Name = "btn_repair";
            this.btn_repair.Size = new System.Drawing.Size(134, 53);
            this.btn_repair.TabIndex = 0;
            this.btn_repair.Text = "维修信息";
            this.btn_repair.UseVisualStyleBackColor = true;
            this.btn_repair.Click += new System.EventHandler(this.btn_repair_Click);
            // 
            // FDeviceOtherFeatures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_repair);
            this.Name = "FDeviceOtherFeatures";
            this.Text = "FDeviceOtherFeatures";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btn_repair;
    }
}