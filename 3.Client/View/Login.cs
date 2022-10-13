
using FdlWindows.View;
using Grpc.Core;
using Grpc.Core.Interceptors; 
using GrpcMain.Account;
using Microsoft.Extensions.DependencyInjection;
using MyClient.Grpc;
using System;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace MyClient.View
{
  
    public partial class FLogin : Form
    {
        IServiceProvider serviceProvider;
        IServiceCollection serviceCollection;
        AccountService.AccountServiceClient client;
        ClientCallContextInterceptor interceptor;
        public FLogin(IServiceProvider serviceProvider, IServiceCollection serviceCollection, AccountService.AccountServiceClient accountServiceClient, ClientCallContextInterceptor interceptor)
        {
            this.serviceProvider = serviceProvider;
            this.serviceCollection = serviceCollection;
            InitializeComponent();
            this.client = accountServiceClient;
            this.interceptor = interceptor;
        }


        async Task TryLogin() {
            string uname = tUName.Text.Trim();
            string pass = tPass.Text.Trim();
            try
            {
                var a = await client.LoginByUserNameAsync(new DTODefine.Types.Request_LoginByUserName()
                {
                    PassWord = pass,
                    UserName = uname,
                });
                if (a.HasToken == false)
                {
                    MessageBox.Show("登录失败！\r\n", "提示", MessageBoxButtons.OK);
                }
                //注册token
                interceptor.Token = a.Token;
                //进入主界面
                FMain? m = serviceProvider.GetService<FMain>();
                if (m == null)
                {
                    throw new Exception("注入失败");
                }
                m.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show("登录失败！\r\n" + ex.Message, "提示", MessageBoxButtons.OK);
                return;
            }
        } 

        /// <summary>
        /// 登录按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btLogin_ClickAsync(object sender, EventArgs e)
        {

            tUName.Enabled = false;
            tPass.Enabled = false;
            btLogin.Enabled = false;
            await TryLogin();
            tUName.Enabled = true;
            tPass.Enabled = true;
            btLogin.Enabled = true;
        }

        private void label_Click(object sender, EventArgs e)
        {

        }
    }
}
