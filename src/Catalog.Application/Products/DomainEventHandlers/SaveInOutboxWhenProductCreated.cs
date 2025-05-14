using Catalog.Application.Abstractions;
using Catalog.Domain.Products.Events;
using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;
using MediatR;

namespace Catalog.Application.Products.DomainEventHandlers;

internal class SaveInOutboxWhenProductCreated(IOutboxService<DomainEvent> outboxService, 
    IUnitOfWork unitOfWork,
    ICorrelationIdProvider correlationIdProvider) : INotificationHandler<ProductCreated>
{
    public async Task Handle(ProductCreated domainEvent, CancellationToken cancellationToken)
    {
        var correlationId = correlationIdProvider.GetCorrelationId();
        OutboxMessage<DomainEvent> outboxMessage = new(domainEvent, correlationId);
        await outboxService.AddAsync(outboxMessage);     
        await unitOfWork.CommitAsync(cancellationToken);

    }
}
