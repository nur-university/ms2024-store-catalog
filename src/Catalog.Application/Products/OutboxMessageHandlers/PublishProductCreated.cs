using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;
using MediatR;

namespace Catalog.Application.Products.OutboxMessageHandlers;

class PublishProductCreated(IExternalPublisher integrationBusService) : INotificationHandler<OutboxMessage<Domain.Products.Events.ProductCreated>>
{
    public async Task Handle(OutboxMessage<Domain.Products.Events.ProductCreated> notification, CancellationToken cancellationToken)
    {
        Nur.Store2025.Integration.Catalog.ProductCreated message = new(
            notification.Content.ProductId,
            notification.Content.Name,
            notification.Content.Description
        );

        await integrationBusService.PublishAsync(message);  
    }
}
