using Microsoft.AspNetCore.Builder;
using CoinTree.Api.Middleware;

namespace CoinTree.Api.Extensions
{
    public static class ConfigureCustomMiddleware
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }

        public static IApplicationBuilder UseRouteHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RouteHandlerMiddleware>();
        }
    }
}
