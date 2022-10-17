using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using Ubiety.Dns.Core;
using static GrpcMain.History.DTODefine.Types;
namespace GrpcMain.History
{
    public class HistoryServiceImp : HistoryService.HistoryServiceBase
    {
        IGrpcHandle _handle;
        ITimeUtility _timeutility;
        public HistoryServiceImp(IGrpcHandle handle, ITimeUtility time)
        {
            _handle = handle;
            _timeutility = time;
        }

        public override async Task<Response_GetHistory?> GetHistory(Request_GetHistory request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            long qid = id;
            Response_GetHistory res = new Response_GetHistory();
            using (MainContext ct=new MainContext())
            {
                if (request.HasUserId&&request.UserId!=id)
                {
                    qid=request.UserId;
                    var sf = await ct.User_SFs.Where(it => it.User1Id == id && it.User2Id == request.UserId && it.IsFather)
                      .AsNoTracking().FirstOrDefaultAsync();
                    if (sf == null)
                    {
                        context.Status = new Status(StatusCode.PermissionDenied,"无该子用户的权限");
                        return null;
                    }
                }

                var bd = ct.Historys.Where(it => it.Type == request.Type && it.CreatorId == qid);
                if (request.HasStartTime)
                {
                    bd = bd.Where(it => it.Time >= request.StartTime);
                }
                if (request.HasEndTime)
                {
                    bd = bd.Where(it => it.Time < request.EndTime);
                }
                if (request.HasMaxCount)
                {
                    bd = bd.Take(request.MaxCount);
                }
                else
                {
                    bd = bd.Take(400);
                }
                bd = bd.AsNoTracking();
                res.Historys.Add((await bd.ToListAsync()).Select(it => {
                    return new DTODefine.Types.History
                    {
                        Data=it.Data,
                        Id=it.Id,
                        Success=it.Success,
                        Time = it.Time,
                        Type = it.Type
                    };
                }));
                return res;
            }
        }

    }

}
