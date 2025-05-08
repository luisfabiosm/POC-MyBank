
// Conditional using directives

using Adapters.Outbound.Database.InMemory;
using Adapters.Outbound.Logging;
using Adapters.Outbound.Metrics;

namespace Configurations
{ 
    public static class OutboundConfiguration
    {
        public static IServiceCollection ConfigureOutboundAdapters(this IServiceCollection services, IConfiguration configuration)
        {


            #region Database

            services.AddSingleton<InMemoryDatabase>();

            #endregion


            #region Logging

            services.AddLoggingAdapter(configuration);

            #endregion region


            #region Metrics

            services.AddMetricsAdapter(configuration);

            #endregion


            return services;
        }
    }
}
