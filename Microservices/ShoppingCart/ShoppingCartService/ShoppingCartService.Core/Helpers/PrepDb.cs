using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Models;

namespace ShoppingCartService.Core.Helpers;

public static class Helpers
{
    public static void PrepDb(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.CreateScope();

        SeedInitial(serviceScope.ServiceProvider.GetService<IShoppingCartRepository>());
    }

    private static async Task SeedInitial(IShoppingCartRepository repository)
    {
        var queryable = await repository.Queryable();
        if (queryable.Any()) return;
        try
        {
            var newInventory = new ShoppingCart("dranzture", new Guid());
            newInventory.AddItem(new Inventory("Product1", "Product1", 2, 1, 1, 1, 1, "dranzture"), 2, "dranzture");
            await repository.AddAsync(newInventory);
            await repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on Inventory create: {ex.Message}");
        }
    }
}