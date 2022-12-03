using FluentValidation;
using FluentValidation.AspNetCore;
using GrpcMain.Extensions;
using GrpcMain.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MyEmailUtility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//添加工具类
builder.Services.TryAddSingleton<MyUtility.IRandomUtility, MyUtility.RandomUtility>();
builder.Services.TryAddSingleton<MyUtility.ITimeUtility, MyUtility.TimeUtility>();
builder.Services.UseMyGrpc("2432114474");
builder.Services.UseMyEmail();

#region 禁用模型校验

///// <summary>
///// 创建不会将任何字段标记为无效的 IObjectModelValidator 实现。
///// </summary>
//public class NullObjectModelValidator : IObjectModelValidator
//{
//    public void Validate(ActionContext actionContext,
//        ValidationStateDictionary validationState, string prefix, object model)
//    {

//        // 该方法故意为空
//    }
//}
//builder.Services.AddSingleton<IObjectModelValidator, NullObjectModelValidator>();
//builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

#endregion

#region 模型校验返回
//自定义校验返回
//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{ 
//    options.InvalidModelStateResponseFactory = (context) =>
//    {
//        var error = context.ModelState.ValidationState;

//        return new JsonResult($"参数验证不通过：{error.ToString()}" );
//    };
//});
//或
//builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
//不返回
#endregion

//FluentValidation并禁用原有的校验器
builder.Services.AddFluentValidationAutoValidation(
    c => c.DisableDataAnnotationsValidation = true
    );
builder.Services.AddValidatorsFromAssemblyContaining<GrpcInterceptor>();

//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v2", new OpenApiInfo
//    {
//        Version = "v2",
//        Title = "",
//        Description = "APIv2"
//    });
//    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
else
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.RegistMyGrpc();
//app.UseHttpsRedirection();
//app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
#if DEBUG
app.Urls.Add("https://localhost:8089");
#else
 app.Urls.Add("http://*:8089");
#endif
app.Run();


//https://localhost:44356/swagger/index.html


