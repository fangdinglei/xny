// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using GrpcMain.Account;
using static GrpcMain.Account.DTODefine.Types;
Thread.Sleep(5000);
Console.WriteLine("Hello, World!");
GrpcChannel grpcChannel = GrpcChannel.ForAddress("https://bs-s-gacyedficd.cn-shanghai.fcapp.run");
//var channel = new Channel("dns:localhost",  8089,  ChannelCredentials.SecureSsl);
var client = new AccountService.AccountServiceClient(grpcChannel);

var a = await client.LoginByUserNameAsync(new  Request_LoginByUserName()
{
    PassWord = "1",
    UserName ="2",
});
if (a.HasToken == false)
{
    throw new Exception();
}