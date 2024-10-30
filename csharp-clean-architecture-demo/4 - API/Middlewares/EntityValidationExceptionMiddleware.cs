using _2___Services._Exceptions;
using _2___Services.Exceptions;
using System.Net;
using System.Text.Json;

namespace _4___API.Middlewares
{
    public class EntityValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public EntityValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EntityValidationException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, EntityValidationException exception)
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
