using Catalog.Application;
using Catalog.Infrastructure;
using Catalog.WorkerService;


var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddAplication()
                .AddInfrastructure(builder.Environment);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
