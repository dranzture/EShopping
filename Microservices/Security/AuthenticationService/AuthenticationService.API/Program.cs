using AuthenticationService.Core.Extensions;
using AuthenticationService.Core.Helpers;
using AuthenticationService.Infrastructure.Data;
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment.EnvironmentName;
var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{env}.json", optional: false)
    .AddJsonFile($"appsecrets.{env}.json", optional: false)
    .Build();

var appSecrets = config.GetSection("AppSecrets").Get<AppSecrets>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AuthenticationService"));

builder.Services.AddSingleton<AppSecrets>(appSecrets);
builder.Services.AddAuthenticationSettings(appSecrets);
builder.Services.AddAuthorization();

builder.Services.AddIdentity<User, Role>(e =>
{
    e.User.RequireUniqueEmail = true;   
    e.Password.RequireDigit = true;
    e.Password.RequireLowercase = true;
    e.Password.RequireNonAlphanumeric = true;
    e.Password.RequireUppercase = true;
    e.Password.RequiredLength = 6;
    e.Lockout.DefaultLockoutTimeSpan = new TimeSpan(15);
    e.Lockout.MaxFailedAccessAttempts = 10;
}).AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

app.PrepDb();
app.UseAuthentication();
app.UseAuthorization();
app.Run();