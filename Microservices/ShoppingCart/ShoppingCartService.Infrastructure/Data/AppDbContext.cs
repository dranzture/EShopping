using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Models;
using ShoppingCartService.Core.ValueObjects;
using ShoppingCartService.Infrastructure.Interfaces;

namespace ShoppingCartService.Infrastructure.Data;

public class AppDbContext : DbContext
{ 
    private readonly IDomainEventDispatcher? _dispatcher;
    public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher? dispatcher) : base(options)
    {
        _dispatcher = dispatcher;
    }

    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    
    public DbSet<ShoppingItem> ShoppingItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents != null && e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }
        

    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}