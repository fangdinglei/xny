using GrpcMain.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GrpcMain.Account.DTODefine.Types;

namespace MyClient.View.User
{
    public partial class FCreatUser : Form
    {
        AccountService.AccountServiceClient client;
        public FCreatUser(AccountService.AccountServiceClient client)
        {
            InitializeComponent();
            this.client = client;
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tuname.Text)
                    || !int.TryParse(tmaxsubuser.Text, out _)
                     || !int.TryParse(tmaxdeep.Text, out _))
                {
                    MessageBox.Show("请输入合法的参数", "提示");
                    return;
                }
                await client.CreatUserAsync(new Request_CreatUser
                {
                    User = new UserInfo
                    {
                        Authoritys = "[]",
                        Email = temail.Text,
                        Phone = tphone.Text,
                        PassWord = tpass.Text,
                        MaxSubUser = int.Parse(tmaxsubuser.Text),
                        MaxSubUserDepth = int.Parse(tmaxdeep.Text),
                        UserName = tuname.Text,
                    }
                });
                this.Close();
                MessageBox.Show("成功", "提示");
                return;
            }
            catch (Exception ex)
            {
                this.Close();
                MessageBox.Show(ex.Message, "提示");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
