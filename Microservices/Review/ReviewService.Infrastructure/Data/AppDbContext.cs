using ReviewService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ReviewService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}