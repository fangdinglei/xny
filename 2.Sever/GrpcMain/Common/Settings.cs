namespace GrpcMain.Common
{
    public class Settings
    {

        static public int GrapePickBatch_TokenLength = 45;
        static public int SendMoneyEventItemTokenLength = 30;
        static public int GrapePickInfo_TokenLength = 5;
        static public string WeChatCertPah = "apiclient_cert.p12";
        static public string WeChat_MySerialNo = "377D668EA75B93D3879626395FE2A945C518805D";
        static public string WeChat_mchid = "1628526441";
        static public string WeChat_V3Key = "g31v8mqqt39cg11d480if1wi08ds3mos";

        static public string WeChat_CertPass => WeChat_mchid;
    }


}
