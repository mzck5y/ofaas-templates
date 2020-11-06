using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }

    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result;
            HttpStatusCode code;

            switch (exception)
            {
                case ValidationException validationException:
                    object errors = new
                    {
                        TraceId = context.TraceIdentifier,
                        Message = "Validation failed.",
                        Errors = (exception as ValidationException).Errors.Select(x => new
                        {
                            x.PropertyName,
                            x.ErrorMessage,
                            x.ErrorCode
                        })
                    };
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(errors);
                    break;

                default:
                    _logger.LogError(exception, exception.Message);

                    code = HttpStatusCode.InternalServerError;
                    result = JsonConvert.SerializeObject(new
                    {
                        Message = "An Internal error occured.",
                        TraceId = context.TraceIdentifier
                    });
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
