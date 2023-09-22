using System.Reflection;
using InventoryService.Core.Entities;
using InventoryService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Inventory> Inventories { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}