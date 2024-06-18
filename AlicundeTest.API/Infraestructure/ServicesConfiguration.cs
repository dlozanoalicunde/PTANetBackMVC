using AlicundeTest.Application.Banks.Queries.GetBank;
using AlicundeTest.Domain.Abstract;
using AlicundeTest.Domain.Repositories;
using AlicundeTest.Persistence.Repositories;
using AlicundeTest.Persistence;

namespace AlicundeTest.API.Infraestructure;

public static class ServicesConfiguration
{
    /// <summary>
    /// Custom contracts for DP and MediatR
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection CustomServices(this IServiceCollection services)
    {
        // Mediator
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetBankHandler).Assembly));

        // Respositories
        services.AddTransient<IBanksRepository, BanksRepository>();

        // Unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
