using Tiktok_Clone.DAL.Entities;

namespace Tiktok_Clone.DAL.Repositories;

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