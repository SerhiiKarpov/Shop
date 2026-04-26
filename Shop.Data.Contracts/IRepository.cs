namespace Shop.Data.Contracts;

public interface IRepository<TEntity>
    where TEntity : class
{
    IQueryable<TEntity> Query { get; }
    void Add(TEntity entity);
    void Remove(TEntity entity);
}