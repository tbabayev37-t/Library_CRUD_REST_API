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
                KeyNotFoundException => HttpStatusCode.NotFound,      // 404
                ArgumentException => HttpStatusCode.BadRequest,       // 400
                UnauthorizedAccessException => HttpStatusCode.Unauthorized, // 401
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;

            string errorMessage = exception switch
            {
                AutoMapper.AutoMapperMappingException => "Sistemdə mapping konfiqurasiyası tapılmadı.",
                _ => exception.Message
            };
            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = errorMessage,
                detailed = exception.InnerException?.Message
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}