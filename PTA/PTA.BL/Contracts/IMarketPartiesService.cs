using PTA.BL.Dtos;
using System.Net;

namespace PTA.BL.Contracts
{
    public interface IMarketPartiesService : IService
    {
        ValueTask<List<DistributionSystemOperatorDto>> GetDistributionSystemOperatorsAsync();

        ValueTask<DistributionSystemOperatorDto> GetDistributionSystemOperatorByIdAsync(int id);

        ValueTask<HttpStatusCode> CreateAsync(DistributionSystemOperatorDto dto);

        ValueTask LoadAsync(List<DistributionSystemOperatorDto> dtos);

        ValueTask DeleteAsync(int key);

        ValueTask UpdateAsync(int key, DistributionSystemOperatorDto dto);
    }
}