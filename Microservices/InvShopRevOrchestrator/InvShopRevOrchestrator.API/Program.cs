using System.Text;
using InvShopRevOrchestrator.API.Helpers;
using InvShopRevOrchestrator.Core.Interfaces;
using InvShopRevOrchestrator.Core.Services;
using InvShopRevOrchestrator.Core.ValueObjects;
using GrpcInventoryServiceClient = InvShopRevOrchestrator.Infrastructure.SyncDataServices.GrpcInventoryService;
using GrpcReviewServiceClient = InvShopRevOrchestrator.Infrastructure.SyncDataServices.GrpcReviewService;
using GrpcShoppingCartServiceClient = InvShopRevOrchestrator.Infrastructure.SyncDataServices.GrpcShoppingCartService;

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
builder.Services.AddScoped<IGrpcInventoryService, GrpcInventoryServiceClient>();
builder.Services.AddScoped<IGrpcReviewService, GrpcReviewServiceClient>();
builder.Services.AddScoped<IGrpcShoppingCartService, GrpcShoppingCartServiceClient>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthenticationSettings(appSecrets!);

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