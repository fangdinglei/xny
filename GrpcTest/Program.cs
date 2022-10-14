// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using GrpcMain.Account;
using static GrpcMain.Account.DTODefine.Types;

Console.WriteLine("Hello, World!");
Thread.Sleep(5000);
var channel = new Channel("127.0.0.1", 5008, ChannelCredentials.Insecure);
var client = new AccountService.AccountServiceClient(channel);
 
var a = await client.LoginByUserNameAsync(new  Request_LoginByUserName()
{
    PassWord = "1",
    UserName ="2",
});
if (a.HasToken == false)
{
    throw new Exception();
}