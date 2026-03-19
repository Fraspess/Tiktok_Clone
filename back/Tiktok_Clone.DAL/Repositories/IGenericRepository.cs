using Tiktok_Clone.DAL.Entities;

namespace Tiktok_Clone.DAL.Repositories;

public interface IGenericRepository<TEntity, TId>
    where TEntity : class, IBaseEntity<TId>
    where TId : notnull
{
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TId id);
    Task<TEntity> GetByIdAsync(TId id);
    IQueryable<TEntity> GetAll();
}