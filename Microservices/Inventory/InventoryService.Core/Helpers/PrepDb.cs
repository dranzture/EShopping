﻿using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
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

    private static async void SeedInitial(IInventoryRepository repository)
    {
        if (repository.Queryable().Any()) return;
        try
        {        
            var newInventory = new Inventory
                ("Product1", "Great Product1", 10, 10L, 8L, 15L, "dranzture");
            await repository.Create(newInventory);
            await repository.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on Inventory create: {ex.Message}");
        }
    }
}
