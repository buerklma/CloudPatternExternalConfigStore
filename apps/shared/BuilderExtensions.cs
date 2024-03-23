//using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;

namespace service2
{
    public static class BuilderExtensions
    {
        public static IServiceCollection AddAzureConfigService(this IServiceCollection services, ConfigurationManager config)
        {
            // Load configuration from Azure App Configuration
            string connectionString = config.GetConnectionString("AppConfig");

            config.AddAzureAppConfiguration(options =>
            {
                options.Connect(connectionString)
                       // Load all keys that start with `Logging:` and have no label
                       .Select($"Logging:{KeyFilter.Any}", LabelFilter.Null)
                       // Configure to reload configuration if the registered sentinel key is modified
                       .ConfigureRefresh(refreshOptions =>
                            refreshOptions.Register("Logging:LogLevel:Default", LabelFilter.Null, refreshAll: true)
                                          .SetCacheExpiration(TimeSpan.FromSeconds(3)));
            });
            return services.AddAzureAppConfiguration();
        }
    }
}
