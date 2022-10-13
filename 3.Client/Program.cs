using FdlWindows.View;
using Grpc.Net.Client;
using MyClient.View;

namespace MyClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {  
            ApplicationConfiguration.Initialize();
            Application.Run(new FLogin());
        }
    }
}