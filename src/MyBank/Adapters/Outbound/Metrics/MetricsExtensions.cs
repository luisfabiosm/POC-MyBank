using Domain.Core.Settings;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Reflection;

namespace Adapters.Outbound.Metrics
{
    public static class MetricsExtensions
    {

        public static IServiceCollection AddMetricsAdapter(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OtlpSettings>(options =>
            {
                var section = configuration.GetSection("AppSettings:Otlp");
                section.Bind(options);

                // Override specific values from environment variables or constants
                options.Endpoint = Environment.GetEnvironmentVariable("OPEN_TELEMETRY_ENDPOINT") ?? options.Endpoint;
                Console.WriteLine($"OPEN_TELEMETRY_ENDPOINT: {options.Endpoint}");
            });

            services.AddSingleton<MetricsAdapter>();

            var _resourceBuilder = ResourceBuilder.CreateDefault()
           .AddService(
               serviceName: Assembly.GetExecutingAssembly().GetName().Name ?? "",
               serviceVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0");


            services.AddOpenTelemetry()
              .WithMetrics(metrics =>
              {
                  metrics
                      .AddRuntimeInstrumentation()
                      .AddAspNetCoreInstrumentation()
                      .AddHttpClientInstrumentation()
                      .AddMeter(Assembly.GetExecutingAssembly().GetName().Name)
                      .SetResourceBuilder(_resourceBuilder)
                      .AddConsoleExporter()
                      .AddOtlpExporter(options =>
                      {
                          var section = configuration.GetSection("AppSettings:Otlp");
                          section.Bind(options);
                          options.Endpoint = Environment.GetEnvironmentVariable("OPEN_TELEMETRY_ENDPOINT") is null ? options.Endpoint : new Uri(Environment.GetEnvironmentVariable("OPEN_TELEMETRY_ENDPOINT"));

                      });
              });

            return services;
        }
    }
}
