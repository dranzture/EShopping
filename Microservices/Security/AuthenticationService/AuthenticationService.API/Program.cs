using AuthenticationService;
using AuthenticationService.Core.Extensions;
using AuthenticationService.Core.Helpers;
using AuthenticationService.Core.Interfaces;
using AuthenticationService.Core.Services;
using AuthenticationService.Infrastructure.Data;
using AuthenticationService.Models;
using AuthenticationService.SyncDataServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment.EnvironmentName;
var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{env}.json", optional: false)
    .AddJsonFile($"appsecrets.{env}.json", optional: false)
    .Build();

var appSecrets = config.GetSection("AppSecrets").Get<AppSecrets>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AuthenticationServiceDb"));

builder.Services.AddSingleton<AppSecrets>(appSecrets);
builder.Services.AddAuthenticationSettings(appSecrets);
builder.Services.AddScoped<ILoggingUserService, LoggingUserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddGrpc();
builder.Services.AddIdentity<User, Role>(e =>
{
    e.User.RequireUniqueEmail = true;   
    e.Password.RequireDigit = true;
    e.Password.RequireLowercase = true;
    e.Password.RequireUppercase = false;
    e.Password.RequireNonAlphanumeric = false;
    e.Password.RequiredLength = 6;
    e.Lockout.DefaultLockoutTimeSpan = new TimeSpan(15);
    e.Lockout.MaxFailedAccessAttempts = 10;
}).AddEntityFrameworkStores<AppDbContext>();



var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<GrpcService>();

    endpoints.MapGet("/protos/authentication.proto", async context =>
    {
        await context.Response.WriteAsync(File.ReadAllText("Protos/authentication.proto"));
    });
});


app.PrepDb();

app.Run();