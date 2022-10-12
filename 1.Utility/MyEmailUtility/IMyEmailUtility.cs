using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace MyEmailUtility
{
    public interface IMyEmailUtility
    {
        Task<bool> Send(string toaddress, string title, string context );
    }
    public class Imp : IMyEmailUtility
    {

        public string FromAddress = "3045034329@qq.com";
        public string Key = "wmdkeipkhuwndghg";
        public string Host = "smtp.qq.com";
        public async Task<bool> Send(string toaddress, string title, string context )
        {

            if (toaddress == null || !Regex.Match(toaddress, @"^.*@.*\.com$").Success)
            { 
                return false;
            }
            //实例化两个必要的
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            //发送邮箱地址
            mail.From = new MailAddress(FromAddress);

            //收件人(可以群发)
            mail.To.Add(new MailAddress(toaddress));

            //是否以HTML格式发送
            mail.IsBodyHtml = true;
            //主题的编码格式
            mail.SubjectEncoding = Encoding.UTF8;
            //邮件的标题
            mail.Subject = title;
            //内容的编码格式
            mail.BodyEncoding = Encoding.UTF8;
            //邮件的优先级
            mail.Priority = MailPriority.Normal;
            //发送内容,带一个图片标签,用于对方打开之后,回发你填写的地址信息
            mail.Body = context;
            //收件人可以在邮件里面
            //mail.Headers.Add("Disposition-Notification-To", "回执信息");

            //发件邮箱的服务器地址
            smtp.Host = Host;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Timeout = 5000;
            //设置端口,如果不设置的话,默认端口为25
            // smtp.Port = 465;
            smtp.UseDefaultCredentials = false;
            //验证发件人的凭据
            smtp.Credentials = new System.Net.NetworkCredential(FromAddress, Key);
            //是否为SSL加密
            smtp.EnableSsl = true;

            try
            {
                //发送邮件
               await smtp.SendMailAsync(mail); 
                return true;
            }
            catch (Exception e1)
            { 
                return false;
            }
        }
    }
    static public class MyEmailExtension
    {
        static public void UseMyEmail(this IServiceCollection services )
        {
            services.TryAddSingleton<IMyEmailUtility,Imp>();
        }  
    }
}