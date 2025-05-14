using Catalog.Application.Abstractions;

namespace Catalog.WebApi.Middleware;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private
    const string _correlationIdHeader = "X-Correlation-Id";

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ICorrelationIdProvider correlationIdProvider)
    {
        // get or generate the request correlation id
        var requestCorrelationId = GetCorrelationId(context, correlationIdProvider);

        // add the correlation id to the http response header
        AddCorrelationIdHeaderToResponse(context, requestCorrelationId);

        await _next(context);
    }

    private string GetCorrelationId(HttpContext context, ICorrelationIdProvider correlationIdProvider)
    {
        if (context.Request.Headers.TryGetValue(_correlationIdHeader, out
            var existingCorrelationId))
        {
            correlationIdProvider.SetCorrelationId(existingCorrelationId);
            return existingCorrelationId;
        }
        return correlationIdProvider.GetCorrelationId();
    }

    private static void AddCorrelationIdHeaderToResponse(HttpContext context, string correlationId)
    {
        context.Response.OnStarting(() => {
            context.Response.Headers.Add(_correlationIdHeader, new[] {
        correlationId
      });
            return Task.CompletedTask;
        });
    }
}
