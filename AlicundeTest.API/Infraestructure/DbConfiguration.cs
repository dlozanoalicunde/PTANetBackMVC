using AlicundeTest.Domain.Abstract;
using AlicundeTest.Persistence;
using AlicundeTest.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;

namespace AlicundeTest.API.Infraestructure;

public static class DbConfiguration
{

    public static void DatabaseConfiguration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AlicundeTestDbContext>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        EnsureDatabase(app);
        ApplyMigrations(app);
        SeedDatabase(app);
    }


    /// <summary>
    /// Database initial data
    /// </summary>
    /// <param name="app"></param>
    public async static void SeedDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AlicundeTestDbContext>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        if (context.Banks.OrderBy(x => x.CreationDateUtc).FirstOrDefault() == null)
        {
            logger.LogInformation("Empty bank table");
            try
            {
                logger.LogInformation("Seeding database...");
                await InitialSeed.Seed(context, unitOfWork);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException, "Error seeding database: ");
                throw;
            }
        }
    }

    /// <summary>
    /// Applies migrations
    /// </summary>
    /// <param name="app"></param>
    public static void ApplyMigrations(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AlicundeTestDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        if (context.Database.HasPendingModelChanges())
        {
            logger.LogInformation("Database model has pending changes");
            try
            {
                logger.LogInformation("Applying changes...");
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException, "Error applying changes: ");
                throw;
            }
        }
        else
        {
            logger.LogInformation("No pending DB model changes");
        }
    }

    /// <summary>
    /// If the DB does not exist, it is created
    /// </summary>
    /// <param name="app"></param>
    public static void EnsureDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AlicundeTestDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        if (!context.Database.CanConnect())
        {
            logger.LogInformation("Database does not exist");
            try
            {
                logger.LogInformation("Creating database...");
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException, "Error creating database: ");
                throw;
            }
        }
        else
        {
            logger.LogInformation("Database exists");
        }
    }
}
