using _2___Services.Exceptions;
using System.Net;
using System.Text.Json;

namespace _4___API.Mddlewares
{
    public class RequestValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (RequestValidationException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, RequestValidationException exception)
        {
            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.Conflict;

            var result = JsonSerializer.Serialize(new
            {
                exception = exception.GetType().Name,
                errors = exception.Errors,
                detail = exception.InnerException?.Message
            });

            await response.WriteAsync(result);
        }

    }
}
