using Tiktok_Clone.DAL.Entities;

namespace Tiktok_Clone.DAL.Repositories;

public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId>
    where TEntity : class, IBaseEntity<TId>
    where TId : notnull
{
    public Task CreateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> GetByIdAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TEntity> GetAll()
    {
        throw new NotImplementedException();
    }
}