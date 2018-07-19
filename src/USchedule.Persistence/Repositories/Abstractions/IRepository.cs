using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using USchedule.Core.Entities.Abstractions;

namespace USchedule.Persistence.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> With<TProperty>(Expression<Func<TEntity, TProperty>> include);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(TEntity entity);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);
    }
}