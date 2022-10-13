
using FdlWindows.View;
using Grpc.Core;
using Grpc.Core.Interceptors; 
using GrpcMain.Account;
using System;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace MyClient.View
{
  
    public partial class FLogin : Form
    {
        public FLogin()
        {
            InitializeComponent();
        }


        async Task TryLogin() {
            try
            {
                string uname = tUName.Text.Trim();
                string pass = tPass.Text.Trim();  
                try
                {
                   
                    var channel = new Channel("http://localhost:5001", ChannelCredentials.Insecure);
                    var client = new AccountService.AccountServiceClient(channel);
                    channel.Intercept();
                    var a=  await  client.LoginByUserNameAsync(new AccountServiceTypes.Types.Request_LoginByUserName() { 
                         PassWord = pass,
                         UserName = uname,
                    });
                    if (a.HasToken==false)
                    {
                        throw new Exception();
                    } 
                }
                catch (Exception)
                {
                   
                }
                if (!b)
                {
                    MessageBox.Show("登录失败！\r\n" + err, "提示", MessageBoxButtons.OK);
                    return;
                }
                //进入主界面
                FMain m = new FMain(0, "智慧农业", () => { });
                m.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作异常！" + ex.Message, "提示", MessageBoxButtons.OK);
            }
        }
        async Task LoginM()
        {
            tUName.Enabled = false;
            tPass.Enabled = false;
            btLogin.Enabled = false;
            await TryLogin ();
            tUName.Enabled = true;
            tPass.Enabled = true;
            btLogin.Enabled = true;
        }

        /// <summary>
        /// 登录按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btLogin_ClickAsync(object sender, EventArgs e)
        {
            await LoginM();
        }

        private void label_Click(object sender, EventArgs e)
        {

        }
    }
}
