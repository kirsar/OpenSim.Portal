using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OpenSim.WebServer.App.Model;

namespace OpenSim.WebServer.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup.Startup>()
                .Build();

            return host;
        }
    }
}
