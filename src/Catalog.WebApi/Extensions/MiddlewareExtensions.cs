

using Catalog.WebApi.Middleware;

namespace Catalog.WebApi.Extensions;

public static class MiddlewareExtensions
{    
    public static IApplicationBuilder UseRequestCorrelationId(this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        return app;
    }

    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }
}
