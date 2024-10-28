using _2___Services.Exceptions;
using System.Net;
using System.Text.Json;

namespace _4___API.Middlewares
{
    public class NotFoundExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public NotFoundExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, NotFoundException exception)
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
