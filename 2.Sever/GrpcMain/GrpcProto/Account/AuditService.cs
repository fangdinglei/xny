using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;

namespace GrpcMain.Account.Audit
{
    static public class Ext
    {
        static public AuditInfo AsGrpc(this User_Op_Audit data)
        {
            return new AuditInfo()
            {
                AuditorId = data.AuditorId,
                Data = data.Data,
                Id = data.Id,
                Op = data.Op,
                SponsorId = data.SponsorId,
                Time = data.Time,
            };
        }

        static public User_Op_Audit AsDBObj(this AuditInfo data)
        {
            return new User_Op_Audit()
            {
                AuditorId = data.AuditorId,
                Data = data.Data,
                Id = data.Id,
                Op = data.Op,
                SponsorId = data.SponsorId,
                Time = data.Time,
            };
        }

    }
    public class AuditServiceImp : AuditService.AuditServiceBase
    {
        public AuditServiceImp()
        {

        }

        public override async Task<Response_Get> Get(Request_Get request, ServerCallContext context)
        {
            long userid = (long)context.UserState["CreatorId"];
            using var ct = new MainContext();

            //设置 take数量
            var take = 0;
            if (request.HasCount)
                take = request.Count + 1;
            else
                take = 500 + 1;

            //初始化条件
            IQueryable<User_Op_Audit> q = ct.User_Op_Audits.AsNoTracking();
            if (request.HasCursor)
                q.Where(it => it.Id >= request.Cursor);

            //按模式设置条件
            if (request.Mode == 0)
            {
                q = q.Where(it => ct.Users.Where(it => it.CreatorId == userid).Select(it => it.Id).Contains(it.SponsorId));
            }
            else if (request.Mode == 1)
            {
                q = q.Where(it => it.SponsorId == userid);
            }
            else if (request.Mode == 2)
            {
                q = q.Where(it => it.SponsorId == userid ||
                   ct.Users.Where(it => it.CreatorId == userid).Select(it => it.Id).Contains(it.SponsorId)
                );
            }
            else if (request.Mode == 3)
            {
                //todo
                throw new NotImplementedException();
            }
            q.Take(take);

            //提取结果
            var ls = (await q.ToListAsync()).Select(it => it.AsGrpc()).ToList();
            var res = new Response_Get();
            if (ls.Count == take)
            {
                //还有其他数据
                res.Infos.Add(ls.Take(ls.Count - 1));
                res.Cursor = ls.Last().Id;
            }
            else
            {
                //没有数据了 清空游标
                res.Infos.Add(ls);
                res.Cursor = 0;
            }
            return res;
        }

        public override async Task<CommonResponse> Set(Request_Set request, ServerCallContext context)
        {
            /*
             修改审计状态 不执行具体操作
             */
            long userid = (long)context.UserState["CreatorId"];
            using var ct = new MainContext();
            var audit = await ct.User_Op_Audits.Where(it => it.Id == request.ID).FirstOrDefaultAsync();
            if (audit == null)
            {
                return new CommonResponse()
                {
                    Success = false,
                    Message = "没有该审计权限"
                };
            }
            var otype = await audit.SponsorId.GetOwnerTypeAsync(ct, userid);
            if (otype != AuthorityUtility.OwnerType.FatherOfCreator)
            {
                return new CommonResponse()
                {
                    Success = false,
                    Message = "只有该用户的上级才能审计该操作"
                };
            }
            if (audit.Status == 0)
            {
                audit.Status = (byte)(request.Accept ? 1 : 2);
                await ct.SaveChangesAsync();
                return new CommonResponse()
                {
                    Success = true,
                    Message = "成功"
                };
            }
            else if (audit.Status == 1 || audit.Status == 2)
            {
                return new CommonResponse()
                {
                    Success = false,
                    Message = "已经审计完成"
                };
            }
            else
            {
                throw new Exception("未知分支");
            }
        }
    }
}
