namespace AlicundeTest.Domain.Abstract;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
