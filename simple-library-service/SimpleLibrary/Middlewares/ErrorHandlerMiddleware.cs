using System.Net;
using SimpleLibrary.Domain.Shared.Exceptions;

namespace SimpleLibrary.Middlewares
{
    public class ErrorHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (HttpErrorException exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpErrorException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.Status;

            await context.Response.WriteAsJsonAsync(new HttpErrorResponse
            {
                Status = exception.Status,
                Message = exception.Message,
                Endpoint = context.Request.Path,
                Timestamp = DateTime.UtcNow,
                Error = ToDescription(exception.Status)
            });

        }

        private static string ToDescription(HttpStatusCode statusCode)
        {
            var name = Enum.GetName(typeof(HttpStatusCode), statusCode) ?? string.Empty;
            var description = string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x.ToString() : x.ToString()));
            return description;
        }
    }
}

