

namespace FdlWindows.View.LoginView
{
    public partial class FLogin : Form
    {
        IServiceProvider serviceProvider;
        FLoginOption _option;
        public FLogin(IServiceProvider serviceProvider, FLoginOption option)
        {
            this.serviceProvider = serviceProvider;
            _option = option;
            InitializeComponent();
        }


        async Task TryLogin()
        {
            string uname = tUName.Text.Trim();
            string pass = tPass.Text.Trim();
            object a;
            a = await _option.LoginCall(serviceProvider, uname, pass);
            if (a == null)
            {
                MessageBox.Show("登陆失败", "提示");
                return;
            }
            _option.SuccessCall(serviceProvider, a);
            this.Hide();
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
