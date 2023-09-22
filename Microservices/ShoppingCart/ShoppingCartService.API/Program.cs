using System;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingCartService.API.SyncDataServices.Grpc;
using ShoppingCartService.Core;
using ShoppingCartService.Core.Extensions;
using ShoppingCartService.Core.Helpers;
using ShoppingCartService.Core.ValueObjects;
using ShoppingCartService.Infrastructure;
using ShoppingCartService.Infrastructure.Consumer;
using ShoppingCartService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var env = builder.Environment.EnvironmentName;
var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{env}.json", optional: false)
    .Build();

var appSettings = config.GetSection("AppSettings").Get<AppSettings>();
builder.Services.AddSingleton<AppSettings>(appSettings);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ShoppingCartServiceDb"));
builder.Services.AddHostedService<ShoppingCartConsumerService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
    options.Interceptors.Add<LoggerInterceptor>();
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddGrpcReflection();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new CoreAutofacModule());
    containerBuilder.RegisterModule(new InfrastructureAutofacModule());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.MapControllers();
app.MapGrpcService<ShoppingCartGrpcService>();
app.MapGet("/protos/shopping_cart.proto",
    async context => { await context.Response.WriteAsync(File.ReadAllText("Protos/shopping_cart.proto")); });

app.MapGrpcReflectionService();

app.PrepDb();

app.Use(async (context, next) =>
{
    Console.WriteLine($"---> Incoming request: {context.Request.Protocol}");
    await next.Invoke();
});

app.Run();