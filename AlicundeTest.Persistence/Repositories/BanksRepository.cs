using AlicundeTest.Domain.Models;
using AlicundeTest.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlicundeTest.Persistence.Repositories;

public class BanksRepository : IBanksRepository
{
    private readonly AlicundeTestDbContext _dbContext;
    public BanksRepository(AlicundeTestDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<IEnumerable<Bank>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Banks.ToListAsync();
    }

    public async Task<Bank> GetBank(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Banks.Where(x => x.Id == id)
                                     .FirstOrDefaultAsync(cancellationToken);
    }
}