using Application.Abstractions.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });

        return services;
    }
}
