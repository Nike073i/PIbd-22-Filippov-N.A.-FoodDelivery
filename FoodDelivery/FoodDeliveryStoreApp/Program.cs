using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FoodDeliveryStoreApp
{
    public class Program
    {
        public static bool authorized;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
