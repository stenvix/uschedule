using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using USchedule.Core.Entities.Abstractions;

namespace USchedule.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        public Repository(DbContext context)
        {
            Context = context;
            Set = context.Set<TEntity>();
        }

        protected DbContext Context { get; }
        protected DbSet<TEntity> Set { get; }

        public virtual Task<IEnumerable<TEntity>> GetAsync()
        {
            return Task.Run(() => Include().AsEnumerable());
        }

        public virtual Task<TEntity> GetAsync(Guid id)
        {
            return Include().FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return Set.AnyAsync(filter);
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Include().FirstOrDefaultAsync(filter);
        }

        public Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Task.FromResult(Include().Where(filter).AsEnumerable());
        }

        public IQueryable<TEntity> With<TProperty>(Expression<Func<TEntity, TProperty>> include)
        {
            return Include().Include(include);
        }
        
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
                return entity;
            }
            return (await Set.AddAsync(entity))?.Entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            return await Task.Run(() =>
            {
                var entry = Context.Entry(entity);

                var local = Set.Local.FirstOrDefault(i => i.Id == entity.Id);
                if (local != null)
                    Context.Entry(local).State = EntityState.Detached;

                if (entry.State == EntityState.Detached)
                    Set.Attach(entity);

                if (entry.State != EntityState.Modified)
                {
                    entry.State = EntityState.Modified;
                }

                Set.Update(entity);
                return entity;
            });
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await Set.FindAsync(id);
            if (entity != null)
                await DeleteAsync(entity);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            //TODO: Fix caching problem
            return Task.Run(() =>
            {
                if (entity == null) return;

                var local = Set.Local.FirstOrDefault(i => i.Id == entity.Id);
                if (local != null)
                    Context.Entry(local).State = EntityState.Detached;
                var entry = Context.Entry(entity);
                if (entry.State != EntityState.Deleted)
                    Set.Remove(entity);
            });
        }

        protected virtual IQueryable<TEntity> Include()
        {
            return Set.AsNoTracking();
        }
    }
}