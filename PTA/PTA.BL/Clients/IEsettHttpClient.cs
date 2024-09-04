using PTA.BL.Dtos;

namespace PTA.BL.Contracts
{
    public interface IEsettHttpClient : IService
    {
        ValueTask<List<DistributionSystemOperatorDto>> GetDistributionSystemOperatorsAsync();
    }
}