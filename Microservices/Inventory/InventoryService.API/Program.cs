using Autofac;
using Autofac.Extensions.DependencyInjection;
using InventoryService.API.SyncDataServices.Grpc;
using InventoryService.Core;
using InventoryService.Infrastructure;
using InventoryService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using InventoryService.Core.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var env = builder.Environment.EnvironmentName;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InventoryServiceDb"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
    
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<GrpcService>();
    endpoints.MapGet("/protos/inventory.proto", async context =>
    {
        await context.Response.WriteAsync(File.ReadAllText("Protos/inventory.proto"));
    });
});



app.PrepDb();

app.Run();