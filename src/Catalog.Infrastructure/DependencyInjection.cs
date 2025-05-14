using Catalog.Application.Abstractions;
using Catalog.Domain.Categories;
using Catalog.Domain.Products;
using Catalog.Infrastructure.Extensions;
using Catalog.Infrastructure.Observability;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Persistence.DomainModel;
using Catalog.Infrastructure.Persistence.Repositories;
using Catalog.Infrastructure.Persistence.StoredModel;
using Joseco.Communication.External.RabbitMQ.Services;
using Joseco.CommunicationExternal.RabbitMQ;
using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.EFCore;
using Joseco.Outbox.EFCore.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nur.Store2025.Access.Catalog.Permissions;
using Nur.Store2025.Security;
using Nur.Store2025.Security.Config;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddSecrets(configuration, environment)
            .AddDatabase()
            .AddSecurity(environment)
            .AddRabbitMQ()
            .AddObservability(environment);

        return services;
    }

}
