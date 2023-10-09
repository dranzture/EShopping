using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShippingService.Core.Entities;
using ShippingService.Core.Enums;
using ShippingService.Core.Interfaces;

namespace ShippingService.Core.Helpers;

public static class Helpers
{
    public static void PrepDb(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.CreateScope();

        SeedInitial(serviceScope.ServiceProvider.GetService<IShippingItemRepository>());
    }

    private static async Task SeedInitial(IShippingItemRepository repository)
    {
        var queryable = await repository.Queryable();
        if (queryable.Any()) return;
        try
        {
            var newOrder = new ShippingItem(new Guid(), ShippingStatus.LabelCreated, "dranzture");
            await repository.AddAsync(newOrder);
            await repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on order create: {ex.Message}");
        }
    }
}