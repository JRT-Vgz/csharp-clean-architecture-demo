using _2___Services._Exceptions;
using System.Net;
using System.Text.Json;

namespace _4___API.Middlewares
{
    public class HttpRequestExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public HttpRequestExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpRequestException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpRequestException exception)
        {
            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.NotFound;

            var result = JsonSerializer.Serialize(new
            {
                exception = exception.GetType().Name,
                errors = exception.Message,
                detail = exception.InnerException?.Message
            });

            await response.WriteAsync(result);
        }
    }
}
