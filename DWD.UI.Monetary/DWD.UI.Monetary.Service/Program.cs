// Namespace does not match folder structure, possible Roslyn analyzer bug - see: https://github.com/dotnet/roslyn/issues/55014
#pragma warning disable IDE0130
namespace DWD.UI.Monetary.Service;
#pragma warning restore IDE0130
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

/// <summary>
/// Main program class that creates the web host
/// </summary>
[ExcludeFromCodeCoverage]
public static class Program
{
    /// <summary>
    /// Application entry point
    /// </summary>
    /// <param name="args">Command line arguments</param>
    public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

    /// <summary>
    /// Configure the host.
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>A host builder</returns>
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}
