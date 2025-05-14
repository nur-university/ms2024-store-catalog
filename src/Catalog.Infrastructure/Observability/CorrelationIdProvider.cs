using Catalog.Application.Abstractions;

namespace Catalog.Infrastructure.Observability;

internal class CorrelationIdProvider : ICorrelationIdProvider
{
    private string _correlationId;

    public CorrelationIdProvider()
    {
        _correlationId = Guid.NewGuid().ToString();
    }
    public string GetCorrelationId()
    {
        return _correlationId;
    }

    public void SetCorrelationId(string correlationId)
    {
        _correlationId = correlationId;
    }
}
