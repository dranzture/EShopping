using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using OrderService.API.SyncDataServices;
using OrderService.API.SyncDataServices.Grpc;
using OrderService.Core;
using OrderService.Core.Extensions;
using OrderService.Core.Helpers;
using OrderService.Core.ValueObjects;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Consumer;
using OrderService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var env = builder.Environment.EnvironmentName;
var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{env}.json", optional: false)
    .Build();

var appSettings = config.GetSection("AppSettings").Get<AppSettings>();
builder.Services.AddSingleton<AppSettings>(appSettings);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("OrderServiceDb"));
builder.Services.AddHostedService<OrderConsumer>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
app.MapGet("/protos/order.proto",
    async context => { await context.Response.WriteAsync(File.ReadAllText("Protos/order.proto")); });

app.MapGrpcReflectionService();

//app.PrepDb();

app.Use(async (context, next) =>
{
    Console.WriteLine($"---> Incoming request: {context.Request.Protocol}");
    await next.Invoke();
});

app.Run();