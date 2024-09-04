using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static void AddAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));

            foreach (var type in typesFromAssemblies)
            {
                var firstOrDefault = type.FindInterfaces((typeObj, criteriaObj) => ((Type)criteriaObj!).IsAssignableFrom(typeObj) && ((Type)criteriaObj) != typeObj, typeof(T)).FirstOrDefault();

                if (firstOrDefault != null)
                    services.Add(new ServiceDescriptor(firstOrDefault, type, lifetime));
            }
        }
    }
}