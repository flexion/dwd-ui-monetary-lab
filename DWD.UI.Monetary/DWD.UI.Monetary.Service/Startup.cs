#pragma warning disable IDE0052
#pragma warning disable CA1801
#pragma warning disable IDE0060

namespace DWD.UI.Monetary.Service
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using Domain.UseCases;
    using Frameworks;
    using Gateways;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Npgsql;

    /// <summary>
    /// Configure the service during start up.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        /// <summary>
        /// Reference to configuration data.
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Configure services in IoC container.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <remarks>This method gets called by the runtime. Use this method to add services to the container.</remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // var sqlConnectionString = this.Configuration["PostgreSqlConnectionString"];


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Monetary Endpoint Demo",
                    Version = "v1",
                    Description = "An initial lab-safe implementation."
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // services.AddDbContext<ClaimantWageContext>(options => options.UseNpgsql(sqlConnectionString));
            ConfigureDbContext(services, this.Configuration);
            services
                .AddTransient<ICalculateBasePeriod, CalculateBasePeriod>()
                .AddScoped<IClaimantWageRepository, ClaimantWageDbRepository>();
        }

        /// <summary>
        /// Configure http pipeline.
        /// </summary>
        /// <param name="app">Application builder reference.</param>
        /// <param name="env">Environment reference.</param>
        /// <remarks>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</remarks>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // nothing yet
            }

            // always generate swagger doc
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DWD.UI.Monetary.Service v1"));

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        /// <summary>
        /// Chooses where to get the DB credentials and creates the Connection.
        /// </summary>
        /// <param name="services">framework services collection</param>
        /// <param name="config">framework config</param>
        private static void ConfigureDbContext(IServiceCollection services, IConfiguration config)
        {
            var instanceConnectionName = Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME");

            NpgsqlConnectionStringBuilder connectionString;
            if (string.IsNullOrEmpty(instanceConnectionName))
            {
                var dbSettings = config.GetSection("SqlConnection");
                connectionString = new NpgsqlConnectionStringBuilder
                {
                    Host = dbSettings["Host"],
                    Username = dbSettings["User"],
                    Password = config["SqlConnection:Password"],
                    Database = dbSettings["Database"],
                    SslMode = SslMode.Disable,
                    Pooling = true
                };
            }
            else
            {
                var dbSocketDir = Environment.GetEnvironmentVariable("DB_SOCKET_PATH") ?? "/cloudsql";
                connectionString = new NpgsqlConnectionStringBuilder()
                {
                    // Remember - storing secrets in plain text is potentially unsafe. Consider using
                    // something like https://cloud.google.com/secret-manager/docs/overview to help keep
                    // secrets secret.
                    Host = $"{dbSocketDir}/{instanceConnectionName}",
                    Username = Environment.GetEnvironmentVariable("DB_USER"), // e.g. 'my-db-user
                    Password = Environment.GetEnvironmentVariable("DB_PASS"), // e.g. 'my-db-password'
                    Database = Environment.GetEnvironmentVariable("DB_NAME"), // e.g. 'my-database'
                    SslMode = SslMode.Disable,
                    Pooling = true
                };
            }

            _ = services.AddDbContext<ClaimantWageContext>(options =>
                  options.UseNpgsql(connectionString.ToString()));
        }
    }
}
