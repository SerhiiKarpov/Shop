namespace Shop.Data.Contracts;

public interface IUnitOfWork
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    Task SaveAsync(CancellationToken cancellationToken = default);
}
