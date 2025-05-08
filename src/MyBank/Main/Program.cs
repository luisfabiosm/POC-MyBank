using Adapters.Inbound.WebApi.Extensions;
using Configurations;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);


var configuration = new ConfigurationBuilder()

    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.ConfigureSwagger("APISample","v1");
builder.Services.ConfigureMicroservice(configuration);
Console.WriteLine($"Serviço: {Assembly.GetExecutingAssembly().GetName()} Versão: {Assembly.GetExecutingAssembly().GetName().Version}");


var app = builder.Build();
app.UseMicroserviceExtensions();
