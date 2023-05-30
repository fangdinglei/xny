
namespace DeviceSimulator
{
    partial class FMQTTMock
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
            components = new System.ComponentModel.Container();
            bar1 = new HScrollBar();
            label1 = new Label();
            label6 = new Label();
            bar2 = new HScrollBar();
            label9 = new Label();
            bar4 = new HScrollBar();
            label12 = new Label();
            bar3 = new HScrollBar();
            button1 = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            button2 = new Button();
            button3 = new Button();
            listBox1 = new ListBox();
            button4 = new Button();
            text_id1 = new TextBox();
            text_id2 = new TextBox();
            textBox4 = new TextBox();
            SuspendLayout();
            // 
            // bar1
            // 
            bar1.Location = new Point(196, 82);
            bar1.Maximum = 1000;
            bar1.Name = "bar1";
            bar1.Size = new Size(676, 30);
            bar1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 82);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(46, 24);
            label1.TabIndex = 3;
            label1.Text = "温度";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(32, 140);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(46, 24);
            label6.TabIndex = 7;
            label6.Text = "湿度";
            // 
            // bar2
            // 
            bar2.Location = new Point(196, 140);
            bar2.Maximum = 1000;
            bar2.Name = "bar2";
            bar2.Size = new Size(676, 30);
            bar2.TabIndex = 6;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(32, 250);
            label9.Margin = new Padding(5, 0, 5, 0);
            label9.Name = "label9";
            label9.Size = new Size(46, 24);
            label9.TabIndex = 11;
            label9.Text = "光照";
            label9.Visible = false;
            // 
            // bar4
            // 
            bar4.Location = new Point(196, 250);
            bar4.Maximum = 1000;
            bar4.Name = "bar4";
            bar4.Size = new Size(676, 30);
            bar4.TabIndex = 10;
            bar4.Visible = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(32, 195);
            label12.Margin = new Padding(5, 0, 5, 0);
            label12.Name = "label12";
            label12.Size = new Size(46, 24);
            label12.TabIndex = 15;
            label12.Text = "电量";
            label12.Visible = false;
            // 
            // bar3
            // 
            bar3.Location = new Point(196, 195);
            bar3.Maximum = 1000;
            bar3.Name = "bar3";
            bar3.Size = new Size(676, 30);
            bar3.TabIndex = 14;
            bar3.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(324, 18);
            button1.Name = "button1";
            button1.Size = new Size(137, 50);
            button1.TabIndex = 16;
            button1.Text = "开始";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(120, 18);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(188, 30);
            textBox1.TabIndex = 17;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 18);
            label2.Name = "label2";
            label2.Size = new Size(82, 24);
            label2.TabIndex = 18;
            label2.Text = "设备编号";
            // 
            // timer1
            // 
            timer1.Interval = 20000;
            timer1.Tick += timer1_Tick;
            // 
            // button2
            // 
            button2.Location = new Point(467, 18);
            button2.Name = "button2";
            button2.Size = new Size(137, 50);
            button2.TabIndex = 19;
            button2.Text = "一次";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(1106, 18);
            button3.Name = "button3";
            button3.Size = new Size(137, 50);
            button3.TabIndex = 20;
            button3.Text = "绑定";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 24;
            listBox1.Location = new Point(920, 18);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(180, 412);
            listBox1.TabIndex = 21;
            // 
            // button4
            // 
            button4.Location = new Point(1106, 74);
            button4.Name = "button4";
            button4.Size = new Size(137, 50);
            button4.TabIndex = 22;
            button4.Text = "清除";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // text_id1
            // 
            text_id1.Location = new Point(76, 82);
            text_id1.Name = "text_id1";
            text_id1.Size = new Size(94, 30);
            text_id1.TabIndex = 23;
            text_id1.Text = "1";
            // 
            // text_id2
            // 
            text_id2.Location = new Point(76, 137);
            text_id2.Name = "text_id2";
            text_id2.Size = new Size(94, 30);
            text_id2.TabIndex = 24;
            text_id2.Text = "2";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(76, 195);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(94, 30);
            textBox4.TabIndex = 25;
            textBox4.Visible = false;
            // 
            // FMQTTMock
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1295, 449);
            Controls.Add(textBox4);
            Controls.Add(text_id2);
            Controls.Add(text_id1);
            Controls.Add(button4);
            Controls.Add(listBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(label12);
            Controls.Add(bar3);
            Controls.Add(label9);
            Controls.Add(bar4);
            Controls.Add(label6);
            Controls.Add(bar2);
            Controls.Add(label1);
            Controls.Add(bar1);
            Margin = new Padding(5, 4, 5, 4);
            Name = "FMQTTMock";
            Text = "FMQTTMock";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.HScrollBar bar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.HScrollBar bar2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.HScrollBar bar4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.HScrollBar bar3;
        private Button button1;
        private TextBox textBox1;
        private Label label2;
        private System.Windows.Forms.Timer timer1;
        private Button button2;
        private Button button3;
        private ListBox listBox1;
        private Button button4;
        private TextBox text_id1;
        private TextBox text_id2;
        private TextBox textBox4;
    }
}