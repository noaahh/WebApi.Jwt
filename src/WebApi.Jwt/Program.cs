using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApi.Jwt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .CaptureStartupErrors(true);
        }
    }
}