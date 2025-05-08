

using Domain.Core.Base;
using Domain.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace Adapters.Inbound.WebApi.Middleware
{
    public class HttpHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpHandlingMiddleware> _logger;

        public HttpHandlingMiddleware(RequestDelegate next, ILogger<HttpHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado");
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, object validationErrors = null)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                ValidateException validationException => new BaseResponse<object>
                {
                    Success = false,
                    Message = validationException.Message ?? "Falhas na validação do Request",
                    ErrorCode = validationException.ErrorCode != 0 ? validationException.ErrorCode : 400,
                    Data = validationException.Errors?.Select(error => new
                    {
                        Field = error.PropertyName,
                        Message = error.Message
                    }).ToList()
                },

                UnauthorizedAccessException handleException => new BaseResponse<object>
                {
                    Success = false,
                    Message = handleException.Message,
                    ErrorCode = (int)HttpStatusCode.Unauthorized,
                    Data = handleException
                },

                BusinessException businessException => new BaseResponse<object>
                {
                    Success = false,
                    Message = businessException.Message,
                    ErrorCode = businessException.ErrorCode,
                    Data = businessException.Details
                },

                InternalException internalException => new BaseResponse<object>
                {
                    Success = false,
                    Message = internalException.Message,
                    ErrorCode = internalException.ErrorCode,
                    Data = internalException.Details
                },

                Exception systemException => new BaseResponse<object>
                {
                    Success = false,
                    Message = systemException.Message,
                    ErrorCode = -1,
                    Data = null // Don't expose stack trace in production
                }
            };

            context.Response.StatusCode = exception switch
            {
                ValidateException => StatusCodes.Status400BadRequest,
                BusinessException => StatusCodes.Status400BadRequest,
                InternalException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpHandlingMiddleware>();
        }
    }
}
