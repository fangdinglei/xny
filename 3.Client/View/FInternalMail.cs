using FdlWindows.View;
using GrpcMain.Device;
using GrpcMain.InternalMail;
using MyClient.Grpc;
using MyUtility;
using System.ComponentModel;

namespace MyClient.View
{
    [AutoDetectView("FInternalMail", "站内信", "", true)]
    public partial class FInternalMail : Form, IView
    {

        Response_CountMail _countMail;
        BindingList<InternalMail> _mails;
        InternalMailService.InternalMailServiceClient _client;
        LocalDataBase _localData;
        ITimeUtility _timeUtility;
        IViewHolder _viewholder;
        public FInternalMail(DeviceService.DeviceServiceClient deviceServiceClient, InternalMailService.InternalMailServiceClient client, LocalDataBase localData, ITimeUtility timeUtility)
        {
            InitializeComponent();
            _client = client;
            _localData = localData;
            _timeUtility = timeUtility;
        }




        void LoadPage(int page)
        {
            list_mails.ShowLoading(async () =>
            {
                _countMail = await _client.CountMailAsync(new Request_CountMail());
                var res2 = await _client.GetMailAsync(new Request_GetMail()
                {
                    Page = page,
                });
                _mails = new BindingList<InternalMail>(res2.Mails.ToList());
                return true;
            }, okcall: () =>
            {
                pageController1.RecordCount = _countMail.MailCount;
                pageController1.PageSize = _countMail.PageSize;
                list_mails.DataSource = _mails;
                list_mails.DisplayMember = "Title";
            });
        }

        public Control View => this;




        public void OnEvent(string name, params object[] pars)
        {

        }

        public void PrePare(params object[] par)
        {
            LoadPage(pageController1.Page - 1);
        }

        public void SetViewHolder(IViewHolder viewholder)
        {
            _viewholder = viewholder;
        }

        public void OnTick()
        {

        }

        private void list_mails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_mails.SelectedIndex < 0)
            {
                text_context.Text = "";
                text_baseinfo.Text = "";
                return;
            }
            var mail = _mails[list_mails.SelectedIndex];
            text_context.Text = mail.Context;
            text_baseinfo.Text = $"发件人{mail.SenderId} 收件人{mail.ReceiverId} 发件时间{_timeUtility.GetDateTime(mail.Time).ToString()} {(mail.Readed ? "已读" : "未读")}";
            btn_deletmail.Enabled = btn_outteremail.Enabled = mail.SenderId == _localData.User.ID;
            Task.Run(() =>
            {
                try
                {
                    _client.SetMailReaded(new Request_SetMailReaded()
                    {
                        MailId = mail.SenderId,
                    });
                    mail.Readed = true;
                }
                catch (Exception)
                {

                }
            });
        }

        private void btn_outteremail_Click(object sender, EventArgs e)
        {
            if (list_mails.SelectedIndex < 0)
            {
                MessageBox.Show("请选择合适的邮件", "提示");
                return;
            }
            try
            {
                var res = _client.SendEMail(new Request_SendEMail
                {
                    MailId = _mails[list_mails.SelectedIndex].Id
                });
                res.ThrowIfNotSuccess();
                MessageBox.Show("成功", "提示");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message, "错误");
            }

        }

        private void pageController1_OnPageChanged()
        {
            LoadPage(pageController1.Page - 1);
        }

        private void btn_deletmail_Click(object sender, EventArgs e)
        {
            if (list_mails.SelectedIndex < 0)
            {
                MessageBox.Show("请选择合适的邮件", "提示");
                return;
            }
            try
            {
                var res = _client.DeletMail(new Request_DeletMail
                {
                    MailId = _mails[list_mails.SelectedIndex].Id
                });
                res.ThrowIfNotSuccess();
                _mails.RemoveAt(list_mails.SelectedIndex);
                MessageBox.Show("成功", "提示");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message, "错误");
            }
        }
    }
}
