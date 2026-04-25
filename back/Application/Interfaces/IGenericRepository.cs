using Domain.Entities;

namespace Application.Interfaces;

public interface IGenericRepository<TEntity, TId>
    where TEntity : class, IBaseEntity<TId>
    where TId : notnull
{
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(TId id);
    IQueryable<TEntity> GetAll();
    Task SaveChangesAsync();
    public TEntity? GetTracked(Func<TEntity, bool> predicate);
}