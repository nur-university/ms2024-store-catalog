using Catalog.Infrastructure.Persistence.DomainModel;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.EFCore.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Joseco.DDD.Core.Abstractions;
using System.Collections.Immutable;

namespace Catalog.Infrastructure.Persistence;

internal class UnitOfWork : IUnitOfWork, IOutboxDatabase<DomainEvent>
{
    private readonly DomainDbContext _dbContext;
    private readonly IPublisher _publisher;

    private int _transactionCount = 0;

    public UnitOfWork(DomainDbContext dbContext, IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("-------------------------> UnitOfWork DB Context Id: " + _dbContext.ContextId);
        _transactionCount++;

        var domainEvents = _dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x =>
            {
                var domainEvents = x.Entity
                                .DomainEvents
                                .ToImmutableArray();
                x.Entity.ClearDomainEvents();

                return domainEvents;
            })
            .SelectMany(domainEvents => domainEvents)
            .ToList();

        foreach (var e in domainEvents)
        {
            await _publisher.Publish(e, cancellationToken);
        }

        if (_transactionCount == 1)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            _transactionCount--;
        }
    }

    public DbSet<OutboxMessage<DomainEvent>> GetOutboxMessages()
    {
        return _dbContext.OutboxMessages;
    }
}
