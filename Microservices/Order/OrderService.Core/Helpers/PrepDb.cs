﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Core.Entities;
using OrderService.Core.Enums;
using OrderService.Core.Interfaces;

namespace OrderService.Core.Helpers;

public static class Helpers
{
    public static void PrepDb(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.CreateScope();

        SeedInitial(serviceScope.ServiceProvider.GetService<IOrderRepository>());
    }

    private static async Task SeedInitial(IOrderRepository repository)
    {
        var queryable = await repository.Queryable();
        if (queryable.Any()) return;
        try
        {
            var newOrder = new Order(new Guid("26c9ee61-3518-42e7-8568-9a7c68d7f4c9"), OrderStatus.PaymentFailed, "dranzture");
            await repository.AddAsync(newOrder);
            await repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on order create: {ex.Message}");
        }
    }
}