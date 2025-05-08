using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Adapters.Inbound.WebApi.Extensions
{

    public class SwaggerMinimalApiOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var metadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;
            var httpMethodMetadata = metadata.FirstOrDefault(m => m is Microsoft.AspNetCore.Routing.HttpMethodMetadata)
                as Microsoft.AspNetCore.Routing.HttpMethodMetadata;

            if (httpMethodMetadata?.HttpMethods.Contains("GET") == true && operation.RequestBody == null)
            {
                var fromBodyParameters = context.ApiDescription.ParameterDescriptions
                    .Where(p => p.BindingInfo?.BindingSource?.Id == "Body")
                    .ToList();

                if (fromBodyParameters.Any())
                {
                    var paramType = fromBodyParameters.First().Type;

                    var schema = context.SchemaGenerator.GenerateSchema(paramType, context.SchemaRepository);

                    operation.RequestBody = new OpenApiRequestBody
                    {
                        Content = new Dictionary<string, OpenApiMediaType>
                        {
                            ["application/json"] = new OpenApiMediaType
                            {
                                Schema = schema
                            }
                        },
                        Required = true
                    };

                    foreach (var param in fromBodyParameters)
                    {
                        var paramToRemove = operation.Parameters.FirstOrDefault(p => p.Name == param.Name);
                        if (paramToRemove != null)
                        {
                            operation.Parameters.Remove(paramToRemove);
                        }
                    }
                }
            }
        }
    }
}