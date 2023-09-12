using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrchestratorService.Core.Interfaces;
using OrchestratorService.Core.Models;
using OrchestratorService.Infrastructure.SyncDataServices;
using GrpcInventoryServiceClient =  OrchestratorService.Infrastructure.SyncDataServices.GrpcInventoryService;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment.EnvironmentName;
var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{env}.json", optional: false)
    .Build();
var appSettings = config.GetSection("AppSettings").Get<AppSettings>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(appSettings!);
builder.Services.AddScoped<IGrpcAuthService, GrpcAuthService>();
builder.Services.AddScoped<IGrpcInventoryService, GrpcInventoryServiceClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(e =>
    e.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = "PolatCoban",
        ValidAudience = "EShopping.NET",
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orchestrator API", Version = "v1" });
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orchestrator API v1"));
}

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Use(async (context, next) =>
{
    await next.Invoke();
});

app.Run();