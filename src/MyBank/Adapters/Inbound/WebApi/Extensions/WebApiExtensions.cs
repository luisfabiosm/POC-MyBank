using Adapters.Inbound.WebApi.Bank.Endpoints;
using Adapters.Inbound.WebApi.Bank.Mapping;
using Adapters.Inbound.WebApi.Middleware;
using Microsoft.OpenApi.Models;

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
            services.AddHealthChecks();

           // services.AddJwtAuthentication(configuration);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });




            return services;
        }


        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, string apiName, string version = "v1")
        {
            services.AddSwaggerGen(options =>
            {
         
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My Bank API",
                    Version = "v1",
                    Description = "API para operações bancárias, incluindo gerenciamento de contas, pagamentos PIX e autenticação.",
                    Contact = new OpenApiContact
                    {
                        Name = "Arch and Dev Team",
                        Email = "fabio.backside@gmail.com"
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Cabeçalho de autorização JWT usando o esquema Bearer. Exemplo: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                options.OperationFilter<SwaggerMinimalApiOperationFilter>();

            });

            return services;
        }

        public static void UseAPIExtensions(this WebApplication app)
        {

            if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName != "Production")
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBank API v1");
                    options.RoutePrefix = "swagger";
                    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    options.DefaultModelsExpandDepth(-1);
                });
            }

            app.UseHttpsRedirection();
            app.UseHttpHandlingMiddleware();
            app.AddSecurityEndpoints();
            app.AddPixEndpoints();
            app.AddAccountEndpoints();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapHealthChecks("/health");

            app.Run();
        }

    }
}
