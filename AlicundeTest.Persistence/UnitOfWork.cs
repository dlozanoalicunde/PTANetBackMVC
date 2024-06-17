namespace AlicundeTest.Persistence;

public class UnitOfWork
{
    private readonly AlicundeTestDbContext _dbContext;

    public UnitOfWork(AlicundeTestDbContext dbContext) => _dbContext = dbContext;

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _dbContext.SaveChangesAsync(cancellationToken);
}
