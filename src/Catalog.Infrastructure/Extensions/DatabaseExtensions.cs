using Catalog.Domain.Categories;
using Catalog.Domain.Products;
using Catalog.Infrastructure.Persistence.DomainModel;
using Catalog.Infrastructure.Persistence.Repositories;
using Catalog.Infrastructure.Persistence.StoredModel;
using Catalog.Infrastructure.Persistence;
using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joseco.Outbox.EFCore;

namespace Catalog.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        var databaseSettings = services.BuildServiceProvider().GetRequiredService<DataBaseSettings>();
        var connectionString = databaseSettings.ConnectionString;

        void optionsAction(DbContextOptionsBuilder options) =>
                options
                    .UseNpgsql(connectionString);

        services.AddDbContext<StoredDbContext>(optionsAction)
            .AddDbContext<DomainDbContext>(optionsAction);

        services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IDatabase, StoredDbContext>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IProductRepository, ProductRepository>()

            .AddScoped<IOutboxDatabase<DomainEvent>, UnitOfWork>()
            .AddOutbox<DomainEvent>();

        return services;
    }
}
