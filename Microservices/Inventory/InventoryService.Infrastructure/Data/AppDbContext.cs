using System.Reflection;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Inventory> Inventories { get; set; }
    
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}