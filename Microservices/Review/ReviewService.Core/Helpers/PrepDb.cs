using ReviewService.Core.Interfaces;
using ReviewService.Core.Models;

namespace ReviewService.Core.Helpers;

public static class Helpers
{
    public static void PrepDb(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.CreateScope();

        SeedInitial(serviceScope.ServiceProvider.GetService<IReviewRepository>());
    }

    private static async void SeedInitial(IReviewRepository repository)
    {
        var guid = new Guid("50d2dffe-f725-48fd-b736-5fc85e53eb68");
        if (repository.Queryable().Any()) return;
        try
        {
            var newReview = new Review(guid, 1, "dranzture", 5, "Excellent Product");
            await repository.Create(newReview);
            await repository.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on Review create: {ex.Message}");
        }
    }
}