// Namespace does not match folder structure, possible Roslyn analyzer bug - see: https://github.com/dotnet/roslyn/issues/55014
#pragma warning disable IDE0130
namespace DWD.UI.Monetary.Service;
#pragma warning restore IDE0130
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