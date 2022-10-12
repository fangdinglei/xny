using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyEmailUtility;
using MyUtility;
using System.Text.RegularExpressions;
using static GrpcMain.InternalMail.InternalMailTypes.Types;

namespace GrpcMain.InternalMail
{
    public class InternalMailServiceImp : InternalMailService.InternalMailServiceBase
    {
        ITimeUtility _timeutility;
        IGrpcCursorUtility _cursorUtility;
        IMyEmailUtility myEmailUtility;
        public InternalMailServiceImp(ITimeUtility time, IGrpcCursorUtility cursorUtility, IMyEmailUtility myEmailUtility)
        {
            _timeutility = time;
            _cursorUtility = cursorUtility;
            this.myEmailUtility = myEmailUtility;
        }

        [GrpcRequireAuthority]
        public override async Task<CommonResponse> SendMail(Request_SendInternalMail request, ServerCallContext context)
        {
            request.Mail.Time = _timeutility.GetTicket();
            using (MainContext ct = new MainContext())
            {
                ct.Internal_Mails.Add(
                    new Internal_Mail
                    {
                        Readed = false,
                        Context = request.Mail.Context,
                        Title = request.Mail.Title,
                        Time = request.Mail.Time,
                    }
                );
                await ct.SaveChangesAsync();
            }
            return new CommonResponse() { Success = true };
        }
        [GrpcRequireAuthority]
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
                mail.Readed = true;
                await ct.SaveChangesAsync();
            }
            return new CommonResponse() { Success = true };
        }
        [GrpcRequireAuthority]
        public override async Task<Response_GetMail> GetMail(Request_GetMail request, ServerCallContext context)
        {
            int maxcount = 100 + 1;
            if (request.HasMaxCount)
            {
                maxcount = request.MaxCount + 1;
            }
            long id = (long)context.UserState["CreatorId"];
            Response_GetMail res = new Response_GetMail();
            using (MainContext ct = new MainContext())
            {
                var bd = ct.Internal_Mails.Where(it => it.ReceiverId == id);
                if (request.HasCursor)
                {
                    bd = bd.Where(it => it.Id >= request.Cursor);
                }
                bd = bd.Take(maxcount);
                var mails = await bd.AsNoTracking().ToListAsync();
                 
                IEnumerable<Internal_Mail> lsx 
                    = _cursorUtility.Run(mails,   maxcount,(it)=> {
                        res.Cursor = it==null?0:it.Id;
                    });  
                res.Mails.AddRange(lsx.Select(
                    it => new InternalMailTypes.Types.InternalMail()
                    {
                        Id = it.Id,
                        Context = it.Context,
                        Readed = it.Readed,
                        Time = it.Time,
                        Title = it.Title,
                    }
                ));
            }
            return res;
        }
         [GrpcRequireAuthority]
        public override async Task<CommonResponse> SendEMail(Request_SendEMail request, ServerCallContext context)
        {

            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                var mail = await ct.Internal_Mails.Where(it => it.SenderId == id && it.Id == request.MailId)
                    .AsNoTracking().FirstOrDefaultAsync();
                if (mail == null)
                {
                    context.Status = new Status(StatusCode.PermissionDenied, "没有该信件");
                    return null;
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
                    
                var sendsuc= await myEmailUtility.Send(user.EMail,mail.Title,mail.Context,out _);
                if (sendsuc)
                {
                    return new CommonResponse()
                    {
                        Success = true,
                        Message =null,
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
