using Microsoft.AspNetCore.Builder;

namespace PZCheeseria.Api.Infrastructure.Middleware
{
    public static class UseApiExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionMiddleware(this IApplicationBuilder appBuilder)
        {
            return appBuilder.UseMiddleware<ApiExceptionMiddleware>();
        }
    }
}