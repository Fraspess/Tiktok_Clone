using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.DAL.Entities;
using Tiktok_Clone.DAL.Repositories;

namespace Tiktok_Clone.BLL.Services
{
    public class GenericService<TEntity,TId, TDTO, TCreateDTO, TUpdateDTO>
        : IGenericService<TEntity, TId,TDTO, TCreateDTO, TUpdateDTO>
         where TEntity : class, IBaseEntity<TId>
         where TId : notnull
    {
        private readonly IGenericRepository<TEntity, TId> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity, TId> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateAsync(TCreateDTO dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.CreateAsync(entity);
        }

        public async Task UpdateAsync(TUpdateDTO dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(TId Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            if (entity == null)
            {
                return;
            }
            await _repository.DeleteAsync(entity);
        }

        public async Task<TDTO> GetByIdAsync(TId Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            return _mapper.Map<TDTO>(entity);
        }

        public async Task<IEnumerable<TDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<TDTO>>(entities);
        }
    }
}
