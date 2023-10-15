using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ShippingService.API.SyncDataServices.Grpc;
using ShippingService.Core;
using ShippingService.Core.Extensions;
using ShippingService.Core.Helpers;
using ShippingService.Core.ValueObjects;
using ShippingService.Infrastructure;
using ShippingService.Infrastructure.Consumers;
using ShippingService.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var env = builder.Environment.EnvironmentName;
var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{env}.json", optional: false)
    .Build();

var appSettings = config.GetSection("AppSettings").Get<AppSettings>();
builder.Services.AddSingleton<AppSettings>(appSettings!);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ShippingServiceDb"));
builder.Services.AddHostedService<CreateShippingItemConsumer>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
    options.Interceptors.Add<LoggerInterceptor>();
});
builder.Services.AddGrpcReflection();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new CoreAutofacModule());
    containerBuilder.RegisterModule(new InfrastructureAutofacModule());
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

app.MapControllers();
app.MapGrpcService<GrpcService>();
app.MapGet("/protos/shipping_item.proto",
    async context => { await context.Response.WriteAsync(File.ReadAllText("Protos/shipping_item.proto")); });

app.MapGrpcReflectionService();

app.PrepDb();

app.Use(async (context, next) =>
{
    Console.WriteLine($"---> Incoming request: {context.Request.Protocol}");
    await next.Invoke();
});

app.Run();