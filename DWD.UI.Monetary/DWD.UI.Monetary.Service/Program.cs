#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace DWD.UI.Monetary.Service
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
