
using Adapters.Inbound.WebApi.Extensions;
using Domain.Core.Settings;



namespace Configurations
{
    public static class MainConfiguration
    {

        public static IServiceCollection ConfigureMicroservice(this IServiceCollection services, IConfiguration configuration)
        {
            AppSettings appSettings = new();
            configuration.GetSection("AppSettings").Bind(appSettings);


            services.ConfigureInboundAdapters(configuration);
            services.ConfigureOutboundAdapters(configuration);
            services.ConfigureDomainAdapters(configuration);

            return services;
        }

        public static void UseMicroserviceExtensions(this WebApplication app)
        {
            app.UseAPIExtensions();

        }
    }
}
