using AuthenticationService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationService.Core.Helpers;

public static class Helpers
{
    private static readonly string[] ApplicationRoles = { "Owner", "Admin", "Customer" };

    public static void PrepDb(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.CreateScope();

        SeedInitial(serviceScope.ServiceProvider.GetService<UserManager<User>>(),
            serviceScope.ServiceProvider.GetService<RoleManager<Role>>());
    }

    private static async void SeedInitial(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        foreach (var role in ApplicationRoles)
        {
            if (!roleManager.Roles.Any(e => e.Name == role))
            {
                var appRole = new Role(role, role.ToUpper());
                await roleManager.CreateAsync(appRole);
            }
        }


        if (await userManager.FindByEmailAsync("polatcoban@gmail.com") != null) return;
        try
        {
            var newUser = new User("Polat", "Coban", "polatcoban@gmail.com", "dranzture");
            await userManager.CreateAsync(newUser, "qaz123");

            await userManager.AddToRolesAsync(newUser, ApplicationRoles);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on User craete: {ex.Message}");
        }
    }
}