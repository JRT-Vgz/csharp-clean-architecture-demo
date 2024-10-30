using _2___Services._Exceptions;
using System.Net;
using System.Text.Json;

namespace _4___API.Middlewares
{
    public class NotImplementedExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public NotImplementedExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotImplementedException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, NotImplementedException exception)
        {
            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.NotImplemented;

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
