using AlicundeTest.Domain.Abstract;

namespace AlicundeTest.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AlicundeTestDbContext _dbContext;

    public UnitOfWork(AlicundeTestDbContext dbContext) => _dbContext = dbContext;

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _dbContext.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
