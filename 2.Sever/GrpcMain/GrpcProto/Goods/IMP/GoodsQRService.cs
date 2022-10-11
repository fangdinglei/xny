using FluentValidation;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using pg.GrpcController.Admin.Goods;
using MyDBContext.Main;
using static pg.GrpcController.Admin.Goods.GoodsQRServiceTypes.Types;

namespace pg.GrpcProto.Goods
{
    public class GoodsQRService : IGoodsQRService.IGoodsQRServiceBase
    {
        public override async Task<UpdateGoodsQRInfo_Response> UpdateGoodsQRInfo(UpdateGoodsQRInfo_Request request, ServerCallContext context)
        {
            
        }

    }
}




