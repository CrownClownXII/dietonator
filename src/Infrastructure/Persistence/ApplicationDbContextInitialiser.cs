using Dietonator.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dietonator.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            var hasPendingMigration = await _context.Database.GetPendingMigrationsAsync();

            if (hasPendingMigration.Any())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        //// Default roles
        //var administratorRole = new IdentityRole("Administrator");

        //if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        //{
        //    await _roleManager.CreateAsync(administratorRole);
        //}

        //// Default users
        //var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        //if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        //{
        //    await _userManager.CreateAsync(administrator, "Administrator1!");
        //    await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        //}

        if (_context.Products.Any()) {
            return;
        }

        var bun = new Product("Bun", 273.0f, 8.9f, 11.5f, 55.9f);
        var guacamole = new Product("Guacamole", 146.0f, 1.5f, 13.4f, 2.1f);
        var eggs = new Product("Eggs", 155.0f, 13.0f, 11.0f, 1.1f);
        var mozzarella = new Product("Mozzarella", 180.0f, 22.0f, 10.0f, 0.5f);

        _context.Products.Add(bun);
        _context.Products.Add(guacamole);
        _context.Products.Add(eggs);
        _context.Products.Add(mozzarella);

        await _context.SaveChangesAsync();
    }
}
