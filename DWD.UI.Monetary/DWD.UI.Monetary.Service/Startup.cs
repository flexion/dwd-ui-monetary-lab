namespace DWD.UI.Monetary.Service
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using DWD.UI.Monetary.Domain.Interfaces;
    using DWD.UI.Monetary.Domain.Utilities;
    using DWD.UI.Monetary.Domain.UseCases;
    using DWD.UI.Monetary.Service.Extensions;
    using DWD.UI.Monetary.Service.Frameworks;
    using DWD.UI.Monetary.Service.Gateways;
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
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">Reference to hosting environment.</param>
        /// <param name="configuration">Reference to the app configuration.</param>
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            this.env = env;
            this.config = configuration;
        }

        /// <summary>
        /// Reference to configuration data.
        /// </summary>
        private readonly IConfiguration config;

        /// <summary>
        /// Reference to host environment.
        /// </summary>
        private readonly IWebHostEnvironment env;

        /// <summary>
        /// Configure services in IoC container.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <remarks>This method gets called by the runtime. Use this method to add services to the container.</remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            if (this.env.IsStaging() || this.env.IsProduction())
            {
                services.AddGoogleLogging(this.config);
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Monetary Endpoint Demo",
                    Version = "v1",
                    Description = "An initial lab-safe implementation.",
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            var connectionString = GetPgConnectionString(this.config);
            services.AddDbContext<ClaimantWageContext>(options => options.UseNpgsql(connectionString));

            services
                .AddTransient<ICalculateBasePeriod, CalculateBasePeriod>()
                .AddSingleton<ICalculateBenefitYear, CalculateBenefitYear>()
                .AddScoped<IClaimantWageRepository, ClaimantWageDbRepository>()
                .AddScoped<ICalendarQuarter, CalendarQuarter>()
                .AddScoped<IClaimantWageRepository, ClaimantWageDbRepository>()
                .AddScoped<ICheckEligibilityOfMonetaryRequirements, CheckEligibilityOfMonetaryRequirements>()
                .AddScoped<IEligibilityBasisGateway, StubEligibilityBasisGateway>();
        }

        /// <summary>
        /// Configure http pipeline.
        /// </summary>
        /// <param name="app">Application builder reference.</param>
        /// <param name="hostenv">Environment reference.</param>
        /// <remarks>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </remarks>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment hostenv)
        {
            if (hostenv.IsDevelopment())
            {
                // nothing yet
            }

            // always generate swagger doc
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Monetary API - Swagger docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DWD.UI.Monetary.Service v1");
                c.EnableDeepLinking();
                c.DefaultModelsExpandDepth(0);
            });

            // app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        /// <summary>
        /// Chooses where to get the Postgres DB credentials.
        /// </summary>
        private static string GetPgConnectionString(IConfiguration config)
        {
            var instanceConnectionName = config.GetValue<string>("INSTANCE_CONNECTION_NAME");

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
                    Pooling = true,
                };
            }
            else
            {
                var dbSocketDir = config.GetValue<string>("DB_SOCKET_PATH") ?? "/cloudsql";
                connectionString = new NpgsqlConnectionStringBuilder()
                {
                    // Remember - storing secrets in plain text is potentially unsafe. Consider using
                    // something like https://cloud.google.com/secret-manager/docs/overview to help keep
                    // secrets secret.
                    Host = $"{dbSocketDir}/{instanceConnectionName}",
                    Username = config.GetValue<string>("DB_USER"), // e.g. 'my-db-user
                    Password = config.GetValue<string>("DB_PASS"), // e.g. 'my-db-password'
                    Database = config.GetValue<string>("DB_NAME"), // e.g. 'my-database'
                    SslMode = SslMode.Disable,
                    Pooling = true,
                };
            }

            return connectionString.ToString();
        }
    }
}
