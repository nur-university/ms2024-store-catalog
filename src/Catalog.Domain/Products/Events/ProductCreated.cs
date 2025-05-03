using Joseco.DDD.Core.Abstractions;

namespace Catalog.Domain.Products.Events;

public record ProductCreated(Guid ProductId, string Name, string Description, Guid CategoryId) : DomainEvent;