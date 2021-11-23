#pragma warning disable IDE0052
#pragma warning disable CA1801
#pragma warning disable IDE0060

namespace DWD.UI.Monetary.Service
{
    using System;
    using System.IO;
    using System.Reflection;
    using DWD.UI.Monetary.Domain.UseCases;
    using DWD.UI.Monetary.Service.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// Configure the service during start up.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            this.Environment = env;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Reference to configuration data.
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Reference to host environment.
        /// </summary>
        private IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Configure services in IoC container.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <remarks>This method gets called by the runtime. Use this method to add services to the container.</remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            if (this.Environment.IsStaging() || this.Environment.IsProduction())
            {
                services.AddGoogleLogging(this.Configuration);
            }

            services.AddControllers();
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

            services.AddTransient<ICalculateBasePeriod, CalculateBasePeriod>();
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
    }
}
