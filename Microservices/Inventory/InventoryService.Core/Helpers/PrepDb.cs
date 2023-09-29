using InventoryService.Core.Entities;
using InventoryService.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Core.Helpers;

public static class Helpers
{
    public static void PrepDb(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.CreateScope();

        SeedInitial(serviceScope.ServiceProvider.GetService<IInventoryRepository>());
    }

    private static  async Task SeedInitial(IInventoryRepository repository)
    {
        var queryable = repository.Queryable();
        if (queryable.Any()) return;
        try
        {        
            var newInventory = new Inventory
                ("Product1", "Great Product1", 10, 10.0M, 8.0M, 15M, 45.5M);
            newInventory.UpdateCreatedFields("dranzture");
            await repository.AddAsync(newInventory);
            await repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on Inventory create: {ex.Message}");
        }
    }
}

