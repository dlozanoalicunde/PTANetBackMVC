using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PTA.Infrastructure.DB;

namespace PTA.Infrastructure.DependencyInjection
{
    internal static class DbContextConfiguration
    {
        internal static void AddDataContext(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
        }
    }
}