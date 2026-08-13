using System.Net;
using System.Text.Json;

namespace CRUD_REST_API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                // Register zamanı istifadəçi mövcuddursa (400)
                // Argument xətaları üçün (400)
                ArgumentException => HttpStatusCode.BadRequest,

                // Login zamanı şifrə yanlış olduqda (401)
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,

                // Login zamanı istifadəçi tapılmadıqda (404)
                KeyNotFoundException => HttpStatusCode.NotFound,

                // Digər gözlənilməyən xətalar üçün (500)
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = exception.Message,
                detailed = exception.InnerException?.Message
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}