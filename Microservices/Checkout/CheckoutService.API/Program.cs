using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CheckoutService.Core;
using CheckoutService.Core.Helpers;
using CheckoutService.Core.ValueObjects;
using CheckoutService.Infrastructure;
using CheckoutService.Infrastructure.Consumer;
using CheckoutService.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment.EnvironmentName;
var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{env}.json", optional: false)
    .Build();

var appSettings = config.GetSection("AppSettings").Get<AppSettings>();
builder.Services.AddSingleton<AppSettings>(appSettings);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("CheckoutServiceDb"));
builder.Services.AddHostedService<CheckoutConsumerService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new CoreAutofacModule());
    containerBuilder.RegisterModule(new InfrastructureAutofacModule());
});
var app = builder.Build();
app.PrepDb();
app.Run();