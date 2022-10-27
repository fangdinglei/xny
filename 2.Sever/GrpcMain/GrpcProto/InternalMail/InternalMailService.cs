using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyEmailUtility;
using MyUtility;
using System.Text.RegularExpressions;

namespace GrpcMain.InternalMail
{
    static  public class Ext {
        /// <summary>
        /// 将请求对象转换为新的DB对象
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        static public Internal_Mail AsDBObj(this InternalMail mail )
        {
            return new Internal_Mail
            {
                Id = mail.Id,
                SenderId = mail.SenderId,
                LastEMailTime = mail.LastEMailTime,
                ReceiverId = mail .ReceiverId,
                Readed =mail.Readed,
                Context = mail .Context,
                Title = mail .Title,
                Time = mail .Time,
            };
        }
        static public InternalMail AsGrpcObj(this Internal_Mail mail)
        {
            return new InternalMail
            {
                Id = mail.Id,
                SenderId = mail.SenderId,
                LastEMailTime = mail.LastEMailTime,
                ReceiverId = mail.ReceiverId,
                Readed = mail.Readed,
                Context = mail.Context,
                Title = mail.Title,
                Time = mail.Time,
            };
        }
    }

    public class InternalMailServiceImp : InternalMailService.InternalMailServiceBase
    {
        public const int PageSize=20;

        ITimeUtility _timeutility;
        IGrpcCursorUtility _cursorUtility;
        IMyEmailUtility myEmailUtility;
        public InternalMailServiceImp(ITimeUtility time, IGrpcCursorUtility cursorUtility, IMyEmailUtility myEmailUtility)
        {
            _timeutility = time;
            _cursorUtility = cursorUtility;
            this.myEmailUtility = myEmailUtility;
        }


        public override async Task<Response_SendInternalMail?> SendMail(Request_SendInternalMail request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            request.Mail.Time = _timeutility.GetTicket();
            using (MainContext ct = new MainContext())
            {
                var count = await ct.User_SFs.Where(it => it.User1Id == id && it.User2Id == request.Mail.ReceiverId).CountAsync();
                if (count == 0)
                {
                    context.Status = new Status(StatusCode.PermissionDenied, "无权给该用户发邮件");
                    return null;
                }
                request.Mail.Id = 0;
                request.Mail.SenderId = id;
                request.Mail.Readed = false;
                request.Mail.LastEMailTime = 0;
                var mail = request.Mail.AsDBObj();
                ct.Internal_Mails.Add(  mail );
                await ct.SaveChangesAsync();
                request.Mail.Id = mail.Id;
                return new Response_SendInternalMail() {  Mail = request.Mail };
            }
        }

        public override async Task<CommonResponse?> SetMailReaded(Request_SetMailReaded request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                var mail = await ct.Internal_Mails.Where(it => it.ReceiverId == id && it.Id == request.MailId)
                    .FirstOrDefaultAsync();
                if (mail == null)
                {
                    context.Status = new Status(StatusCode.PermissionDenied, "没有该信件");
                    return null;
                }
                if (!mail.Readed)
                {
                    mail.Readed = true;
                    await ct.SaveChangesAsync();
                }
            }
            return new CommonResponse() { Success = true };
        }

        public override async Task<Response_GetMail> GetMail(Request_GetMail request, ServerCallContext context)
        { 
            if (!request.HasPage)
            {
                request.Page = 1;
            }
            long id = (long)context.UserState["CreatorId"];
            Response_GetMail res = new Response_GetMail();
            res.Page = request.Page;
            using (MainContext ct = new MainContext())
            {
                var bd = ct.Internal_Mails.Where(it => it.ReceiverId == id||it.SenderId==id);
                //if (request.HasCursor)
                //{
                //    bd = bd.Where(it => it.Id >= request.Cursor);
                //}
                bd = bd.OrderByDescending(it => it.Time);
                bd = bd.Skip(request.Page * PageSize ).Take(PageSize);
                var mails = await bd.AsNoTracking().ToListAsync();
                IEnumerable<Internal_Mail> lsx = mails;
                //    = _cursorUtility.Run(mails, maxcount, (it) =>
                //    {
                //        res.Cursor = it == null ? 0 : it.Id;
                //    });
               
                res.Mails.AddRange(lsx.Select( it =>it.AsGrpcObj()  ));
                res.Page=request.Page;
            }
            return res;
        }

        public override async Task<Response_CountMail> CountMail(Request_CountMail request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                var count =await ct.Internal_Mails.Where(it => it.ReceiverId == id || it.SenderId == id)
                    .CountAsync();
                return new Response_CountMail
                {
                    MailCount = count,
                    PageSize = PageSize,
                };
            }
        }

        public override async Task<CommonResponse> DeletMail(Request_DeletMail request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                var mail = await ct.Internal_Mails.Where(it => (it.SenderId == id || it.ReceiverId == id) && it.Id == request.MailId)
                    /*.AsNoTracking()*/.FirstOrDefaultAsync();
                if (mail == null)
                {
                    context.Status = new Status(StatusCode.PermissionDenied, "没有该信件");
                    return null;
                }
                if (mail.SenderId == id)
                {
                    return new CommonResponse()
                    {
                        Success = false,
                        Message = "只有发送者才能删除",
                    };
                    return null;
                }
                ct.Remove(mail);
                await ct.SaveChangesAsync();
                return new CommonResponse()
                {
                    Success = true,
                };
            }
        }

        public override async Task<CommonResponse> SendEMail(Request_SendEMail request, ServerCallContext context)
        {

            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                var mail = await ct.Internal_Mails.Where(it => (it.SenderId == id||it.ReceiverId==id) && it.Id == request.MailId)
                    /*.AsNoTracking()*/.FirstOrDefaultAsync();
                if (mail == null)
                {
                    context.Status = new Status(StatusCode.PermissionDenied, "没有该信件");
                    return null;
                }
                if (mail.ReceiverId==id)
                {
                    return new CommonResponse()
                    {
                        Success = false,
                        Message = "该信件是发送给你的,不能使用该功能",
                    }; 
                    return null;
                }
                //if (mail.Readed)
                //{
                //    return new CommonResponse()
                //    {
                //        Success = false,
                //        Message = "该信件已读,不能再发送站外信",
                //    };
                //}

                if ((_timeutility.GetTicket() - mail.LastEMailTime) < 60)
                {//上次发是60s内
                    return new CommonResponse()
                    {
                        Success = false,
                        Message = "发送过于频繁",
                    };
                }
                var user = await ct.Users.Where(it => it.Id == mail.ReceiverId)
                    .AsNoTracking().FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new Exception("该用户被删除但是信件没有");
                }
                if (user.EMail == null || !Regex.Match(user.EMail, @"^.*@.*\.com$").Success)
                {
                    return new CommonResponse()
                    {
                        Success = false,
                        Message = "用户未设置正确的邮件地址",
                    };
                }

                var sendsuc = await myEmailUtility.Send(user.EMail, mail.Title, mail.Context);
                if (sendsuc)
                {
                    mail.LastEMailTime = _timeutility.GetTicket();
                    await ct.SaveChangesAsync();
                    return new CommonResponse()
                    {
                        Success = true,
                    };
                }
                else
                {
                    return new CommonResponse()
                    {
                        Success = false,
                        Message = "发送失败",
                    };
                }
            }
        }
    }
}
