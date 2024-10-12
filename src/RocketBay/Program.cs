using RocketBay.Components;
using ElectronNET.API;
using ElectronNET.API.Entities;

namespace RocketBay
{
    public class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseElectron(args);
                    webBuilder.UseEnvironment("Development");
                    webBuilder.UseStartup<Startup>();
                });
    }
}