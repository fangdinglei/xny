using FdlWindows.View; 
using Microsoft.Extensions.DependencyInjection;
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
            Application.Run(Global.Provider.GetService<FLogin>());
        }
    }
}