using Catalog.Application;
using Catalog.Infrastructure;
using Catalog.WebApi;
using Catalog.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddAplication()
    .AddInfrastructure(builder.Environment)
    .AddPresentation(builder.Configuration, builder.Environment);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
    app.ApplyMigrations();
}

app.UseRouting();

app.UseHealthChecks();

app.UseRequestContextLogging();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
