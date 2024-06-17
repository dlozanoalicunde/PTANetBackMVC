using AlicundeTest.Domain.Models;
using AlicundeTest.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AlicundeTest.Persistence.Repositories;

public class BanksRepository : IBanksRepository
{
    private readonly AlicundeTestDbContext _dbContext;
    public BanksRepository(AlicundeTestDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<IEnumerable<Bank>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Banks.ToListAsync(cancellationToken);
    }

    public async Task<Bank> GetBank(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Banks.Where(x => x.Id == id)
                                     .FirstOrDefaultAsync(cancellationToken);
    }

    public void Add(Bank bank)
    {
        _dbContext.Banks.Add(bank);
    }
}