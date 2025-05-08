using Adapters.Inbound.WebApi.Bank.Endpoints;
using Adapters.Inbound.WebApi.Bank.Mapping;
using Adapters.Inbound.WebApi.Middleware;

namespace Adapters.Inbound.WebApi.Extensions
{
    public static class WebApiExtensions
    {
        public static IServiceCollection addWebApiEndpoints(this IServiceCollection services, IConfiguration configuration)
        {

            services.ConfigureHttpJsonOptions(options => {
                options.SerializerOptions.DefaultIgnoreCondition =
                    System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });
            services.AddScoped<MappingHttpRequestToTransaction>();
            services.AddEndpointsApiExplorer();

            return services;
        }


        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, string apiName, string version = "v1")
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version, new() { Title = apiName, Version = version });
            });

            return services;
        }

        public static void UseAPIExtensions(this WebApplication app)
        {

            if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName != "Production")
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseHttpHandlingMiddleware();
            app.AddSecurityEndpoints();
            app.AddPixEndpoints();
            app.AddAccountEndpoints();

            app.Run();
        }
    }
}
