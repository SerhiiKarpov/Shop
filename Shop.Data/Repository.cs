using Microsoft.EntityFrameworkCore;
using Shop.Data.Contracts;

namespace Shop.Data;

internal sealed class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _set;

    public Repository(DbSet<TEntity> set)
    {
        _set = set ?? throw new ArgumentNullException(nameof(set));
    }

    public IQueryable<TEntity> Query => _set.AsQueryable();

    public void Add(TEntity entity) => _set.Add(entity);

    public void Remove(TEntity entity) => _set.Remove(entity);
}