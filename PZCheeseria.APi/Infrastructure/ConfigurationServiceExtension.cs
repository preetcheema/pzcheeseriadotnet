using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PZCheeseria.Api.Configuration;

namespace PZCheeseria.Api.Infrastructure
{
    public static class ConfigurationServiceExtension
    {

        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration config)
        {
         
            services.Configure<ConnectionStrings>(config.GetSection("ConnectionStrings"));

        
            services.AddSingleton(m => m.GetRequiredService<IOptions<ConnectionStrings>>().Value);

            return services;
        }
    }
}