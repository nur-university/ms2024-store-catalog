using Catalog.Domain.Categories;
using Catalog.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.EFCore.Persistence;
using System.Reflection;

namespace Catalog.Infrastructure.Persistence.DomainModel;

internal class DomainDbContext(DbContextOptions<DomainDbContext> options) :
    DbContext (options)
{
    public DbSet<Category> Category { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<OutboxMessage<DomainEvent>> OutboxMessages { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddOutboxModel<DomainEvent>();
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<DomainEvent>();
    }
}
