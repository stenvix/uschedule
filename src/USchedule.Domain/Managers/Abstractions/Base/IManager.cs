using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace USchedule.Domain.Managers.Base
{
    public interface IManager<TModel> : IDisposable where TModel : class
    {
        Task<IList<TModel>> GetAsync();
        Task<TModel> GetAsync(Guid id);
        Task<TModel> CreateAsync(TModel entity);
        Task<TModel> UpdateAsync(TModel entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(TModel entity);
        Task<bool> Exists(Guid id);
    }
}