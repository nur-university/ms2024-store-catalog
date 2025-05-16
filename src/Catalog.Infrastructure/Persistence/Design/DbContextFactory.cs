using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Infrastructure.Persistence.StoredModel;
using Microsoft.Extensions.Configuration;

namespace Catalog.Infrastructure.Persistence.Design;

internal class DbContextFactory : IDesignTimeDbContextFactory<StoredDbContext>
{
    public StoredDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        DataBaseSettings dataBaseSettings = new();
        configuration.Bind("CatalogDatabaseConnectionString", dataBaseSettings);
        var connectionString = dataBaseSettings.ConnectionString;

        var optionsBuilder = new DbContextOptionsBuilder<StoredDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new StoredDbContext(optionsBuilder.Options);
    }

    private IConfiguration BuildConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables();

        // Aquí puedes agregar lógica condicional para consultar Vault si se requiere

        return builder.Build();
    }
}
