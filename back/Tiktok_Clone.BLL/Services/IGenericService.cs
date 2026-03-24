using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.DAL.Entities;

namespace Tiktok_Clone.BLL.Services
{
    public interface IGenericService<TEntity, TId, TDTO, TCreateDTO, TUpdateDTO>
        where TEntity : class, IBaseEntity<TId>
        where TId : notnull
    {
        Task CreateAsync(TCreateDTO dto);
        Task UpdateAsync(TUpdateDTO dto);

        Task DeleteAsync(TId Id);

        Task<TDTO> GetByIdAsync(TId Id);

        Task<IEnumerable<TDTO>> GetAllAsync();
    }
}
