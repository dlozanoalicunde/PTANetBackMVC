using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PTA.Infrastructure.DependencyInjection
{
    public static class InjectorBootStrapper
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //register Infraestructure data
            services.AddDataContext(configuration.GetConnectionString("DataContext"));
        }
    }
}