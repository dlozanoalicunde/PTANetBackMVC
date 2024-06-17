using AlicundeTest.Domain.Models;

namespace AlicundeTest.Domain.Repositories;

public interface IBanksRepository
{
    Task<Bank> GetAll(CancellationToken cancellationToken = default);
    Task<IEnumerable<Bank>> GetBank(Guid id, CancellationToken cancellationToken = default);
}
