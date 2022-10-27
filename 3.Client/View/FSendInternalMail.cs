using FdlWindows.View;
using GrpcMain.InternalMail;

namespace MyClient.View
{
    [AutoDetectView("FSendInternalMail", "", "", false)]
    public partial class FSendInternalMail : Form, IView
    {
        IViewHolder _viewholder;
        InternalMailService.InternalMailServiceClient _client;
        public FSendInternalMail(InternalMailService.InternalMailServiceClient client)
        {
            InitializeComponent();
            _client = client;
        }


        long _sendererid;
        long _receiverid;
        public Control View => this;

        public void OnEvent(string name, params object[] pars)
        {

        }

        public void PrePare(params object[] par)
        {
            _sendererid = (long)par[0];
            _receiverid = (long)par[1];
            text_title.Text = "";
            text_context.Text = "";
        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }

        public void OnTick()
        {

        }

        private void btn_send_Click(object sender, EventArgs e)
        {

            try
            {
                var res = _client.SendMail(new Request_SendInternalMail
                {
                    Mail = new InternalMail
                    {
                        SenderId = _sendererid,
                        ReceiverId = _receiverid,
                        Context = text_context.Text,
                        Title = text_context.Text,
                    }
                });
                MessageBox.Show("成功", "提示");
                _viewholder.Back();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message, "错误");
            }
        }
    }
}
