using WebApp.Models;

namespace WebApp.Helpers
{
    public interface IHandleServiceProviders
    {
        void BalanceServiceProviders(BalanceServiceProviders request);
        BalanceServiceProviders BalanceServiceProvidersById(Guid request);
        void UpdateBalanceServiceProvidersById(BalanceServiceProviders request);
        void DeleteBalanceServiceProvidersById(Guid request);
    }
}
