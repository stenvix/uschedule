using System.Threading.Tasks;
using USchedule.Persistence.Database;
using USchedule.Persistence.Repositories;

namespace USchedule.Domain.Managers.Base
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        #region Fields

        protected readonly DataContext Context;
        private IUniversityRepository _universityRepository;

        #endregion


        #region Properties
        
        public IUniversityRepository UniversityRepository => _universityRepository ?? (_universityRepository = new UniversityRepository(Context));

        #endregion

        public AppUnitOfWork(DataContext context)
        {
            Context = context;
        }

        public Task SaveChanges()
        {
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

    }
}