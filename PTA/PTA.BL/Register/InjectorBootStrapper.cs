using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTA.Infrastructure.DependencyInjection;

namespace PTA.BL.DependencyInjection
{
    /// <summary>
    /// Class to separate the dependency registrations
    /// </summary>
    public static class InjectorBootStrapper
    {
        public static void AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            //register infraestructura data
            services.AddInfrastructure(configuration);
        }
    }
}