using CheckoutService.Core.Entities;
using CheckoutService.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CheckoutService.Core.Helpers;

public static class Helpers
{
    public static void PrepDb(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.CreateScope();

        SeedInitial(serviceScope.ServiceProvider.GetService<IPaymentMethodRepository>());
    }

    private static async Task SeedInitial(IPaymentMethodRepository repository)
    {
        var queryable = await repository.Queryable();
        if (queryable.Any()) return;
        try
        {
            var newInventory = new PaymentMethod("dranzture", new CreditCard("dranzture", 1234567890123456, true));
            await repository.AddAsync(newInventory);
            await repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on Inventory create: {ex.Message}");
        }
    }
}