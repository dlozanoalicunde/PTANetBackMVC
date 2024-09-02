using ServiceProvider = WebApplication.Model.ServiceProvider;

namespace WebApplication.Repository
{
    public interface IServiceProviderRepository
    {
        Task<List<ServiceProvider>> GetServiceProviders();
        Task<ServiceProvider> GetServiceProviderById(String businessId);
    }
}
