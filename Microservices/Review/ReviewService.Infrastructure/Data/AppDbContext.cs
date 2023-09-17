using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ReviewService.Core.Models;

namespace ReviewService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Review> Reviews { get; set; }
    
}