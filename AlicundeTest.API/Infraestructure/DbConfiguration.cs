using AlicundeTest.Domain.Abstract;
using AlicundeTest.Persistence;
using AlicundeTest.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AlicundeTest.API.Infraestructure;

public static class DbConfiguration
{
    /// <summary>
    /// Method that esures DB creation, applies the last migrations and seeds DB
    /// </summary>
    /// <param name="app"></param>
    public static WebApplication DatabaseInitialization(this WebApplication app)
    {
        DatabaseExists(app);
        ApplyMigrations(app);
        SeedDatabase(app);

        return app;
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
                await InitialSeed.Seed(context, unitOfWork, logger);
                logger.LogInformation("Seeding completed");
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
            string errorMessage = "Database model has pending migrations, apply _update_database.ps1";
            logger.LogError(errorMessage);
            throw new InvalidOperationException(errorMessage);
        }
    }

    /// <summary>
    /// If the DB does not exist, it is created
    /// </summary>
    /// <param name="app"></param>
    public static void DatabaseExists(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AlicundeTestDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        if (!context.Database.CanConnect())
        {
            string errorMessage = "Database does not exists, apply _update_database.ps1";
            logger.LogError(errorMessage);
            throw new InvalidOperationException(errorMessage);
        }
    }

    /// <summary>
    /// SQL Server configuration for EF Core using connection string for containers or dotnet cli
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseStringName = "Default";
        string connectionString;

        if (Environment.GetEnvironmentVariable("Container") == "true")
        {
            databaseStringName = "Container";
        }

        if (configuration.GetConnectionString(databaseStringName).IsNullOrEmpty())
        {
            throw new InvalidOperationException($"{databaseStringName} ConnectionString not configured");
        }
        connectionString = configuration.GetConnectionString(databaseStringName);

        services.AddDbContext<AlicundeTestDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlServer(connectionString);
        });

        return services;
    }
}
