using FdlWindows.View.LoginView;
using Microsoft.Extensions.DependencyInjection;

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
            var f = Global.serviceProvider.GetService<FLogin>();
            Application.Run(f);
        }
    }
}