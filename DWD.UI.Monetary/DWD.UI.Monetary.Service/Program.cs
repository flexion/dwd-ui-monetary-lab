#pragma warning disable CA1052

namespace DWD.UI.Monetary.Service
{
    using System;
    using System.IO;
    using Google.Cloud.Diagnostics.Common;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);
                    var configuration = builder.Build();

                    webBuilder.ConfigureLogging(builder => builder
                        .AddGoogle(new LoggingServiceOptions
                        {
                            ProjectId = configuration.GetSection("GCP").GetValue<string>("ProjectID")
                        }));

                    webBuilder.UseStartup<Startup>();
                });
    }
}
