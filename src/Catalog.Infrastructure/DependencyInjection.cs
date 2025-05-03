using Catalog.Domain.Categories;
using Catalog.Domain.Products;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Persistence.DomainModel;
using Catalog.Infrastructure.Persistence.Repositories;
using Catalog.Infrastructure.Persistence.StoredModel;
using Joseco.Communication.External.RabbitMQ.Services;
using Joseco.CommunicationExternal.RabbitMQ;
using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.EFCore;
using Joseco.Outbox.EFCore.Persistence;
using Joseco.Secrets.Contrats;
using Joseco.Secrets.HashicorpVault;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nur.Store2025.Access.Catalog.Permissions;
using Nur.Store2025.Security;
using Nur.Store2025.Security.Config;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
    private const string JwtOptionsSecretName = "JwtOptions";
    private const string RabbitMqSettingsSecretName = "RabbitMqSettings";
    private const string CatalogDatabaseConnectionStringSecretName = "CatalogDatabaseConnectionString";

    private const string VaultMountPoint = "secrets";

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IHostEnvironment environment)
    {
        services.AddVault()
            .AddDatabase()
            .AddSecurity(environment)
            .AddRabbitMQ();
            

        if (environment is IWebHostEnvironment)
        {
            services.AddServicesHealthChecks();
        }

        

        return services;
    }

    private static IServiceCollection AddVault(this IServiceCollection services)
    {
        string? vaultUrl = Environment.GetEnvironmentVariable("VAULT_URL");
        string? vaultToken = Environment.GetEnvironmentVariable("VAULT_TOKEN");

        if (string.IsNullOrEmpty(vaultUrl) || string.IsNullOrEmpty(vaultToken))
        {
            throw new InvalidOperationException("Vault URL or Token is not set in environment variables.");
        }

        var settings = new VaultSettings()
        {
            VaultUrl = vaultUrl,
            VaultToken = vaultToken
        };

        services.AddHashicorpVault(settings)
            .LoadSecretsFromVault();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
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

    private static IServiceCollection AddSecurity(this IServiceCollection services, IHostEnvironment environment)
    {
        if (environment is IWebHostEnvironment)
        {
            var jwtOptions = services.BuildServiceProvider().GetRequiredService<JwtOptions>();
            services.AddSecurity(jwtOptions, CatalogPermission.PermissionsList);
        }
        return services;
    }

    private static IServiceCollection AddRabbitMQ(this  IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var rabbitMqSettings = serviceProvider.GetRequiredService<RabbitMqSettings>();

        services.AddRabbitMQ(rabbitMqSettings);

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

    private static void LoadSecretsFromVault(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

        using var scope = scopeFactory.CreateScope();
        var secretManager = scope.ServiceProvider.GetRequiredService<ISecretManager>();

        Task[] tasks = [
                LoadAndRegister<JwtOptions>(secretManager, services, JwtOptionsSecretName, VaultMountPoint),
                LoadAndRegister<RabbitMqSettings>(secretManager, services, RabbitMqSettingsSecretName, VaultMountPoint),
                LoadAndRegister<DataBaseSettings>(secretManager, services, CatalogDatabaseConnectionStringSecretName, VaultMountPoint)
            ];


        Task.WaitAll(tasks);
    }

    private static async Task LoadAndRegister<T>(ISecretManager secretManager, IServiceCollection services,
        string secretName, string mountPoint) where T : class, new()
    {
        T secret = await secretManager.Get<T>(secretName, mountPoint);
        services.AddSingleton<T>(secret);
    }
}
