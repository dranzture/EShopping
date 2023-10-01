using System.Reflection;
using CheckoutService.Core.Entities;
using CheckoutService.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CheckoutService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    
    public DbSet<CreditCard> CreditCards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}