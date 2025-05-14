using Catalog.Application;
using Catalog.Infrastructure;
using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.EFCore;


var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddAplication()
                .AddInfrastructure(builder.Environment);
builder.Services.AddOutboxBackgroundService<DomainEvent>();

var host = builder.Build();
host.Run();
