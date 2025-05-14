using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nur.Store2025.Access.Catalog.Permissions;
using Nur.Store2025.Security;
using Nur.Store2025.Security.Config;

namespace Catalog.Infrastructure.Extensions;

public static class SecurityExtensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, IHostEnvironment environment)
    {
        if (environment is IWebHostEnvironment)
        {
            var jwtOptions = services.BuildServiceProvider().GetRequiredService<JwtOptions>();
            services.AddSecurity(jwtOptions, CatalogPermission.PermissionsList);
        }
        return services;
    }
}
