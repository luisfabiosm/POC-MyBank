using System;
using System.Reflection;
using Domain.Core.Interfaces.Outbound;
using Domain.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

namespace Adapters.Outbound.Logging
{
    public static class LoggingExtensions
    {

        public static IServiceCollection AddLoggingAdapter(this IServiceCollection services, IConfiguration configuration)
        {


            services.Configure<OtlpSettings>(options =>
            {
                var section = configuration.GetSection("AppSettings:Otlp");
                section.Bind(options);

                // Override specific values from environment variables or constants
                options.Endpoint = Environment.GetEnvironmentVariable("OPEN_TELEMETRY_ENDPOINT") ?? options.Endpoint;
                Console.WriteLine($"OPEN_TELEMETRY_ENDPOINT: {options.Endpoint}");
            });

            // Configuração do Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // Configuração do ILogger
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });



            // Registrar o LoggingAdapter
            services.AddSingleton<ILoggingAdapter>(provider =>
            {
                var _logger = provider.GetRequiredService<ILogger<LoggingAdapter>>();
                return new LoggingAdapter(Assembly.GetExecutingAssembly().GetName().Name);
            });

            // Configuração do OpenTelemetry
            var _resourceBuilder = ResourceBuilder.CreateDefault()
              .AddService(
                  serviceName: Assembly.GetExecutingAssembly().GetName().Name ?? "",
                  serviceVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0");


            services.AddOpenTelemetry()
              .WithTracing(tracing =>
              {
                  tracing
                     .AddSource(Assembly.GetExecutingAssembly().GetName().Name)
                     .AddConsoleExporter()
                     .AddHttpClientInstrumentation()
                     .SetResourceBuilder(_resourceBuilder)
                     .SetSampler(new TraceIdRatioBasedSampler(1.0))
                     .AddAspNetCoreInstrumentation()
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
