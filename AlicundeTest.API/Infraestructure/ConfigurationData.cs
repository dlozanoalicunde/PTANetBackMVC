using AlicundeTest.Domain.Abstract;
using AlicundeTest.Persistence;
using AlicundeTest.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;

namespace AlicundeTest.API.Infraestructure;

public  static class ConfigurationData
{
    /// <summary>
    /// DB schema modifications and data seed
    /// </summary>
    public async static void ConfigureData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AlicundeTestDbContext>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        if (context.Database.HasPendingModelChanges())
            await context.Database.MigrateAsync();

        if (context.Banks.FirstOrDefault() == null)
            await InitialSeed.Seed(context, unitOfWork);
    }
}
