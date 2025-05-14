using Catalog.Application.Abstractions;
using Catalog.Infrastructure.Observability;
using Catalog.Infrastructure.Persistence;
using Joseco.CommunicationExternal.RabbitMQ;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Catalog.Infrastructure.Extensions;

public static class ObservabilityExtensions
{
    public static IServiceCollection AddObservability(this IServiceCollection services, IHostEnvironment environment)
    {
        services.AddScoped<ICorrelationIdProvider, CorrelationIdProvider>();

        if(environment is IWebHostEnvironment)
        {
            services.AddServicesHealthChecks();
        }
        return services;
    }

    private static IServiceCollection AddServicesHealthChecks(this IServiceCollection services)
    {
        var databaseSettings = services.BuildServiceProvider().GetRequiredService<DataBaseSettings>();
        var connectionString = databaseSettings.ConnectionString;

        services
            .AddHealthChecks()
            .AddNpgSql(connectionString)
            .AddRabbitMqHealthCheck();

        return services;
    }
}
