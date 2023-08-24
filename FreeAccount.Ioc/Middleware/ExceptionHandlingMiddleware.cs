using Microsoft.AspNetCore.Http;
using Serilog;
using FreeAccount.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeAccount.Ioc.Middleware
{
    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(ILogger logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {                
                var response = await MessageExceptionAsync(exception);

                if(response.Status == 500) 
                    _logger.Error(exception, $"Traking: {response.Id} - {exception.Message}");
                else
                    _logger.Error(JsonSerializer.Serialize(response));

                await HandleExceptionAsync(context, response);
            }
        }

        private static async Task<Response> MessageExceptionAsync(Exception exception)
        {
            var statusCode = GetStatusCode(exception);

            var response = new Response
            {
                Id = Guid.NewGuid(),
                Title = GetTitle(exception),
                Status = statusCode,
                Detail = exception.Message,
                Errors = GetErrors(exception)
            };

            return response;
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Response response)
        {

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = response.Status;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

        private static string GetTitle(Exception exception) =>
            exception switch
            {
                Domain.Exceptions.ApplicationException applicationException => applicationException.Title,
                _ => "Server Error"
            };

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;

            if (exception is ValidationException validationException)
            {
                errors = validationException.ErrorsDictionary;
            }

            return errors;
        }

        public class Response
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public int Status { get; set; }
            public string Detail { get; set; }
            public IReadOnlyDictionary<string, string[]> Errors { get; set; }
        }
    }
}
