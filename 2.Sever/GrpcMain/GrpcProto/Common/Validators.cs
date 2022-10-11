using FluentValidation;
using pg.GrpcController.Admin;

namespace PgGrpcMain.GrpcProto.Common
{
    //TODO
    public class Validators
        {
            public class AdminInfo_Validator
               : AbstractValidator<AdminInfo>
            {
                public AdminInfo_Validator()
                {
                    RuleFor(a => a.Name).NotNull().MaximumLength(1).NotEmpty().WithMessage("姓名不能空");
                    RuleFor(a => a.Pass).NotNull().NotEmpty().WithMessage("密钥不能空");
                }
            }  
        } 
}
