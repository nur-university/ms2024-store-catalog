using Catalog.Infrastructure.Persistence.StoredModel.Entities;
using Joseco.Outbox.EFCore;
using Microsoft.EntityFrameworkCore;
using Joseco.DDD.Core.Abstractions;

namespace Catalog.Infrastructure.Persistence.StoredModel;

internal class StoredDbContext : DbContext, IDatabase
{
    public DbSet<CategoryModel> Category { get; set; }
    public DbSet<ProductModel> Product { get; set; }

    public StoredDbContext(DbContextOptions<StoredDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddOutboxModel<DomainEvent>();
    }

    public void Migrate()
    {
        Database.Migrate();
    }
}
