using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace System.Windows.Forms
{ 
    public partial class FInputBox : Form
    {
        public string Title { get =>Text; set => Text = value; }
        public string Tip { get => ltip.Text; set => ltip.Text = value; }
        public string Input { get => tinput.Text; set => tinput.Text = value; }
        public DialogResult Result;
        public FInputBox()
        {
            InitializeComponent();
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void bcacel_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
        }

        private void bok_Click(object sender, EventArgs e)
        {
            Result = DialogResult.OK;
            Hide();
        }
    }
    /// <summary>
    /// 输入框
    /// </summary>
    static public class InputBox {
        static  FInputBox m_form;
        static  FInputBox Form
        {
            get
            {
                if (m_form==null)
                {
                    m_form = new FInputBox();
                }
                return m_form;
            } 
        }
        /// <summary>
        /// 获取一个输入
        /// </summary>
        /// <param name="title"></param>
        /// <param name="tip"></param>
        /// <param name="input"></param>
        /// <returns>OK or Cancel</returns>
        static public DialogResult GetString(string title,string tip,out string input) {
            Form.Title = title;
            Form.Tip = tip;
            Form.Input = "";
            Form.Result = DialogResult.None;
            Form.ShowDialog();
            input = Form.Input;
            return Form.Result;
        }
    
    }
}
