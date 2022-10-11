using System;


namespace FDL.Program
{
    public class NetFailedException : Exception
    {
        public NetFailedException() :
           base($"网络异常")
        {

        }
    }
}
