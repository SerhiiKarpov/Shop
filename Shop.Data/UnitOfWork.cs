using Microsoft.EntityFrameworkCore;
using Shop.Data.Contracts;

namespace Shop.Data;

internal sealed class UnitOfWork : DbContext, IUnitOfWork
{
    public UnitOfWork(DbContextOptions<UnitOfWork> options)
        : base(options)
    {
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class =>
        new Repository<TEntity>(Set<TEntity>());

    public Task SaveAsync(CancellationToken cancellationToken = default) =>
        SaveChangesAsync(cancellationToken);
}