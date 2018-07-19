using System;
using System.Threading.Tasks;

namespace USchedule.Domain.Managers
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChanges();
    }
}