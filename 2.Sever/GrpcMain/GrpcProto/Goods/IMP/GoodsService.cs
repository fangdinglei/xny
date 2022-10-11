using FluentValidation;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using pg.GrpcController.Admin.Goods;
using MyDBContext.Main;
using PgGrpcMain.Common;
using System.Threading.Tasks;
using static pg.GrpcController.Admin.Goods.GoodsServiceTypes.Types;

namespace pg.GrpcProto.Goods
{ 
    public class GoodsService : IGoodsService.IGoodsServiceBase
    {
        public override async Task<CreatGoodsBatch_Response> CreatGoodsBatch(CreatGoodsBatch_Request request, ServerCallContext context)
        {
         
        }
    }
}




