using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Kanbersky.Customer.Core.Helpers
{
    public static class BaseHelpers
    {
        public static IConfigurationRoot GetConfigurationRoot(string[] args)
        {
            //var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", false, true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }
    }
}
