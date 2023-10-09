using OrchestratorService.API.Helpers;
using OrchestratorService.Core.Interfaces;
using OrchestratorService.Core.Models;
using OrchestratorService.Core.ValueObjects;
using OrchestratorService.Infrastructure.SyncDataServices;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment.EnvironmentName;
var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{env}.json", optional: false)
    .AddJsonFile($"appsecrets.{env}.json", optional: false)
    .Build();

var appSettings = config.GetSection("AppSettings").Get<AppSettings>();
var appSecrets = config.GetSection("AppSecrets").Get<AppSecrets>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(appSettings!);

builder.Services.AddScoped<IGrpcAuthService, GrpcAuthService>();
builder.Services.AddScoped<IGrpcOrderService, OrchestratorService.Infrastructure.SyncDataServices.GrpcOrderService>();
builder.Services.AddScoped<IGrpcShippingItemService, OrchestratorService.Infrastructure.SyncDataServices.GrpcShippingItemService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthenticationSettings(appSecrets);

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddSwaggerSettings();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orchestrator API v1"));
}

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();