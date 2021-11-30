#pragma warning disable IDE0011

namespace DWD.UI.Monetary.Service.Extensions
{
    using System;
    using Google.Cloud.Diagnostics.AspNetCore3;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension methods to simplify adding and configuring Google Logging
    /// </summary>
    public static class GoogleLoggingExtensions
    {
        /// <summary>
        /// Add Google Logging and Error reporting using settngs in the "GCP" section.
        /// <para>
        /// Google Logging requires Application Default Credentials. They are available if running in Google Compute Engine.
        /// Otherwise, the environment variable GOOGLE_APPLICATION_CREDENTIALS must be defined pointing to a file defining the credentials.
        /// See https://developers.google.com/accounts/docs/application-default-credentials for more information.
        /// </para>
        /// </summary>
        /// <param name="services">Services collection</param>
        /// <param name="configuration">Reference to app configuration</param>
        /// <exception cref="ArgumentNullException">Configuration is null</exception>
        /// <returns>Services collection for fluent chaining</returns>
        public static IServiceCollection AddGoogleLogging(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var gcpConfig = configuration.GetSection("GCP");
            var projectId = gcpConfig.GetValue<string>("ProjectID");
            var serviceName = gcpConfig.GetValue<string>("ServiceName");
            var version = gcpConfig.GetValue<string>("Version");

            services.AddGoogleDiagnosticsForAspNetCore(projectId, serviceName, version);
            services.AddGoogleErrorReportingForAspNetCore(new Google.Cloud.Diagnostics.Common.ErrorReportingServiceOptions
            {
                ProjectId = projectId,
                ServiceName = serviceName,
                Version = version
            });

            return services;
        }
    }
}