using Kanbersky.Customer.Core.Helpers;
using Kanbersky.Customer.Core.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Kanbersky.Customer.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Methods
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var logServerSettings = BaseHelpers.GetConfigurationRoot(args).GetSection("ElasticsearchSettings").Get<ElasticsearchSettings>();
            Log.Logger = new LoggerHelpers(logServerSettings).Register(typeof(Startup).Assembly.GetName().Name, LogEventLevel.Warning);

            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
