
// Conditional using directives

using Adapters.Outbound.Logging;
using Adapters.Outbound.Metrics;

namespace Configurations
{ 
    public static class OutboundConfiguration
    {
        public static IServiceCollection ConfigureOutboundAdapters(this IServiceCollection services, IConfiguration configuration)
        {

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
