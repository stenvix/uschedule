using System;
using System.Threading.Tasks;

namespace USchedule.Domain.Managers.Base
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChanges();
    }
}