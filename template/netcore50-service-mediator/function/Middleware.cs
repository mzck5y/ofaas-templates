using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
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
                    result = JsonSerializer.Serialize(errors);
                    break;
                case ArgumentNullException argNullEx:
                    object error = new
                    {
                        TraceId = context.TraceIdentifier,
                        Message = "Validation failed.",
                        Errors = new[]
                        {
                            new
                            {
                                PropertyName = argNullEx.ParamName,
                                ErrorMessage = "Request cannot be null",
                                ErrorCode = "Empty"
                            }
                        }
                    };
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(error);
                    break;
                default:
                    _logger.LogError(exception, exception.Message);

                    code = HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(new
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
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
