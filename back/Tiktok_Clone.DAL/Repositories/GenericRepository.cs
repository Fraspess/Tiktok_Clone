using Microsoft.EntityFrameworkCore;
using Tiktok_Clone.DAL.Entities;
namespace Tiktok_Clone.DAL.Repositories;

public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId>
    where TEntity : class, IBaseEntity<TId>
    where TId : notnull
{

    protected readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        var entity = await _context.Set<TEntity>()
            .FirstOrDefaultAsync(e => e.Id.Equals(id));
        return entity;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _context.Set<TEntity>();
    }
}