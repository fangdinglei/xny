using FdlWindows.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyClient.FdlWindows.View
{
    /// <summary>
    /// 日期筛选器 获取选中开始日期0.0.0 到结束日期23.59.59
    /// </summary>
    [AutoDetectView("FDateSelector", "", "", false)]
    public partial class FDateSelector : Form, IView
    {
        Action<DateTime, DateTime> okcall;
        public FDateSelector()
        {
            InitializeComponent();
        }
        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {
            if (name == "Exit")
            {
                //FormExitEventArg arg = pars[0] as FormExitEventArg;
                //arg.Cancel =true; 
            }   
        }

        public void PrePare(params object[] par)
        {
            okcall = par[0] as Action<DateTime, DateTime>; 
        }

        public void SetViewHolder(IViewHolder viewholder)
        {

        }

        public void OnTick()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var vs = dateTimePicker1.Value;
            var ve = dateTimePicker2.Value;
            okcall.Invoke(new DateTime(vs.Year,vs.Month,vs.Day)
                , new DateTime(ve.Year, ve.Month, ve.Day,23,59,59));
        }
    }
} 