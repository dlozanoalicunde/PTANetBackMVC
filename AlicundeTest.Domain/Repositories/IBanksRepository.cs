using AlicundeTest.Domain.Models;

namespace AlicundeTest.Domain.Repositories;

public interface IBanksRepository
{
    Task<IEnumerable<Bank>> GetAll(CancellationToken cancellationToken = default);
    Task<Bank> GetBank(Guid id, CancellationToken cancellationToken = default);
}
