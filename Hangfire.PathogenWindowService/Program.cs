using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Hangfire.PathogenWindowService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSetting("detailedErrors", "true")
                .CaptureStartupErrors(true)
                .UseStartup<Startup>()
                .Build();
    }
}