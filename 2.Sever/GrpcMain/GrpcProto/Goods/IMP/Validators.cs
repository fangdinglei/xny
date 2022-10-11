using FluentValidation;
using pg.GrpcController.Admin;
using pg.GrpcController.Admin.Goods;
using static pg.GrpcController.Admin.Goods.GoodsQRServiceTypes.Types;
using static pg.GrpcController.Admin.Goods.GoodsServiceTypes.Types;

namespace pg.GrpcProto.Goods
{
    public class Validators {
        static public PgGrpcMain.GrpcProto.Common.Validators.AdminInfo_Validator _AdminInfo_Validator = new  ();
        static public CreatGoodsBatch_Request_Validator _CreatGoodsBatch_Request_Validator = new();
        static public GoodsQRInfo_Validator _GoodsQRInfo_Validator = new();
        static public UpdateGoodsQRInfo_Request_Validator _UpdateGoodsQRInfo_Request_Validator = new();

        public class CreatGoodsBatch_Request_Validator
            : AbstractValidator<CreatGoodsBatch_Request>
        {
            public CreatGoodsBatch_Request_Validator()
            {
                RuleFor(a => a.AdminInfo).SetValidator(_AdminInfo_Validator);
                RuleFor(a=>a.Appid)
                    .NotNull().NotEmpty() ;
                RuleFor(a=>a. BatchName).NotNull().NotEmpty();
                RuleFor(a => a.GoodsName).NotNull().NotEmpty();
                RuleFor(a=>a.Count)
                    .GreaterThan(0) .LessThanOrEqualTo(100) ;
                RuleFor(a => a.CreatTime)
                   .GreaterThan(0) ;
                RuleFor(a => a.StartTime) .GreaterThan(it=>it.CreatTime);
                RuleFor(a => a.StartTime)
                    .GreaterThan(it => it.StartTime); 
            }
        }

        /// <summary>
        /// UpdateGoodsQRInfo_Request请求中的数据
        /// </summary>
        public class GoodsQRInfo_Validator
          : AbstractValidator<GoodsQRInfo>
        {
            public GoodsQRInfo_Validator()
            {
                RuleFor(a => a.GiftExpirationTime).GreaterThanOrEqualTo(a=>a.GiftStartTime);
                RuleFor(a => a.GiftStartTime).GreaterThan(0);

            }
        }
        public class UpdateGoodsQRInfo_Request_Validator
            : AbstractValidator<UpdateGoodsQRInfo_Request>
        {
            public UpdateGoodsQRInfo_Request_Validator() 
            {
                RuleFor(a => a.AdminInfo).SetValidator(_AdminInfo_Validator);
                RuleFor(a => a.Infos).ForEach(it => it.SetValidator(_GoodsQRInfo_Validator));

            }
        }


    }
}
