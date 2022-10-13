using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XNYAPI.Request.Device;

namespace MyClient.View
{
    public partial class FSendCMD : Form,IView
    {
        List<ValueTuple<uint, string>> dvs;
        public FSendCMD()
        {
            InitializeComponent();
        }


        void RefreshLab() {
            if (dvs == null || dvs.Count == 0)
            {
                label1.Text = "异常";
                return;
            }
            string s = "";
            for (int i = 0; i < dvs.Count; i++)
            { 
                s += dvs[i].Item2 + ",";
                if (s.Length>40)
                {
                    break;
                }
            }
            if (s.Length > 40)
                s = s.Substring(0, 40) + "...";
            else
                s = s.Substring(0,  s.Length - 1);
            label1.Text ="向设备["+ s+"]发送命令";
        }
        private void btnok_Click(object sender, EventArgs e)
        {
            if (dvs==null||dvs.Count==0)
            {
                MessageBox.Show("没有选择设备", "提示");
                return;
            }
            if (tcmd.Text=="")
            {
                MessageBox.Show("请输入命令","提示");
                return;
            }
            try
            {
                var res= Global.client.Exec(new SendCMDRequest(dvs.Select(it => it.Item1).ToList(), tcmd.Text));
                if (res.IsError)
                    throw new Exception();
                MessageBox.Show("发送成功","提示");
            }
            catch (Exception ex)
            {
                 
            }
            
        }

        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {
            
        }

        public void PrePare(params object[] par)
        {
            if (par.Count()==1&&par[0]!=null && ( par[0] is List<ValueTuple<uint, string>>)  )
            {
                dvs = (List<ValueTuple<uint, string>>)par[0];
                RefreshLab();
            }
        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            
        }

        public void OnTick()
        {
           
        }
    }
}
