using Grpc.Core;
using GrpcMain.Account;
using static GrpcMain.Account.DTODefine.Types;

namespace GRPCTest;



[TestClass]
public class AccountServiceTest
{
    static public Channel channel => new Channel("127.0.0.1", 5008, ChannelCredentials.Insecure);

    [TestMethod]
    public async Task CreatUserAsync()
    {
        var client = new AccountService.AccountServiceClient(channel);
        var a = await client.LoginByUserNameAsync(new Request_LoginByUserName()
        {
            PassWord = "123",
            UserName = "admin",
        });
        Assert.AreEqual(a.HasToken, true);
    }
}