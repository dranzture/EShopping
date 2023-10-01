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
            var newPaymentMethod =
                new PaymentMethod("dranzture");
            newPaymentMethod.AddPaymentMethod(new CreditCard("dranzture", 1234567890123456, true));

            await repository.AddAsync(newPaymentMethod);
            await repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on Inventory create: {ex.Message}");
        }
    }
}