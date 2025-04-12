using AdvertisingPlatform.API.Middleware;
using AdvertisingPlatform.App.Repository;
using AdvertisingPlatform.App.Services;
using AdvertisingPlatform.App.Services.Interfaces;
using AdvertisingPlatform.App.Services.Interfaces.Providers;
using AdvertisingPlatform.App.Services.Interfaces.Validators;
using AdvertisingPlatform.App.Services.Providers;
using AdvertisingPlatform.App.Services.Validators;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddSingleton<IPlatformRepository, PlatformRepository>()
    .AddScoped<IAdvService, AdvService>()
    .AddScoped<IAdvertisingInfoProvider, AdvertisingInfoProvider>()
    .AddScoped<ILocationsProvider, LocationsProvider>()
    .AddScoped<IAdvertisingLineValidator, AdvertisingLineValidator>()
    .AddScoped<ILocationsLineValidator, LocationsLineValidator>()
    .AddScoped<ILocationValidator, LocationValidator>()
    .AddScoped<IPlatformLocationsValidator, PlatformLocationsValidator>()
    .AddScoped<IPlatformValidator, PlatformValidator>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
