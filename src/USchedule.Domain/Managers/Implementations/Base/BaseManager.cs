﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using USchedule.Core.Entities.Abstractions;
using USchedule.Models.Domain.Base;
using USchedule.Persistence.Repositories;

namespace USchedule.Domain.Managers.Base
{
    public abstract class BaseManager<TModel, TEntity> : IManager<TModel>
        where TModel : class, IModel where TEntity : class, IEntity
    {
        protected ILogger<BaseManager<TModel, TEntity>> Logger;
        protected readonly IRepository<TEntity> Repository;
        private readonly IMapper _mapper;
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseManager(IUnitOfWork unitOfWork, IRepository<TEntity> repository, IMapper mapper,ILogger<BaseManager<TModel, TEntity>> logger)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
            Repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<IList<TModel>> GetAsync()
        {
            return _mapper.Map<IEnumerable<TEntity>, IList<TModel>>(await Repository.GetAsync());
        }

        public virtual async Task<TModel> GetAsync(Guid id)
        {
            return _mapper.Map<TEntity, TModel>(await Repository.GetAsync(id));
        }

        public virtual async Task<TModel> CreateAsync(TModel entity)
        {
            var result = await Repository.CreateAsync(_mapper.Map<TEntity>(entity));
            await UnitOfWork.SaveChanges();
            return  await GetAsync(result.Id);
        }

        public virtual async Task<TModel> UpdateAsync(TModel entity)
        {
            var result = await Repository.Update(_mapper.Map<TEntity>(entity));
            await UnitOfWork.SaveChanges();
            return await GetAsync(result.Id);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await Repository.DeleteAsync(id);
            await UnitOfWork.SaveChanges();
        }

        public virtual async Task DeleteAsync(TModel entity)
        {
            await Repository.DeleteAsync(_mapper.Map<TEntity>(entity));
            await UnitOfWork.SaveChanges();
        }

        public Task<bool> Exists(Guid id)
        {
            return Repository.Exists(e => e.Id == id);
        }

        public virtual void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}