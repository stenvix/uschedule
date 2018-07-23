using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using USchedule.Core.Entities.Abstractions;
using USchedule.Models.Domain;
using USchedule.Models.Domain.Base;
using USchedule.Models.Extensions;
using USchedule.Persistence.Repositories;

namespace USchedule.Domain.Managers
{
    public abstract class ManagerBase<TModel, TEntity> : IManager<TModel>
        where TModel : class, IModel where TEntity : class, IEntity
    {
        protected readonly IRepository<TEntity> Repository;
        protected readonly IUnitOfWork UnitOfWork;

        protected ManagerBase(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            UnitOfWork = unitOfWork;
            Repository = repository;
        }

        public virtual async Task<IList<TModel>> GetAsync()
        {
            return Mapper.Map<IEnumerable<TEntity>, IList<TModel>>(await Repository.GetAsync());
        }

        public virtual async Task<TModel> GetAsync(Guid id)
        {
            return (await Repository.GetAsync(id)).To<TModel>();
        }

        public virtual async Task<TModel> CreateAsync(TModel entity)
        {
            var result = await Repository.CreateAsync(entity.To<TEntity>());
            await UnitOfWork.SaveChanges();
            return  await GetAsync(result.Id);
        }

        public virtual async Task<TModel> UpdateAsync(TModel entity)
        {
            var result = await Repository.Update(entity.To<TEntity>());
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
            await Repository.DeleteAsync(entity.To<TEntity>());
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