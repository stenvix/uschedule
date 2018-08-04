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
        private IInstituteRepository _instituteRepository;
        private IDepartmentRepository _departmentRepository;
        private IGroupRepository _groupRepository;
        private ILessonRepository _lessonRepository;

        #endregion

        #region Properties
        
        public IUniversityRepository UniversityRepository => _universityRepository ?? (_universityRepository = new UniversityRepository(Context));
        public ILessonRepository LessonRepository => _lessonRepository ?? (_lessonRepository = new LessonRepository(Context));
        public IGroupRepository GroupRepository => _groupRepository ?? (_groupRepository = new GroupRepository(Context));
        public IInstituteRepository InstituteRepository => _instituteRepository ?? (_instituteRepository = new InstituteRepository(Context));
        public IDepartmentRepository DepartmentRepository => _departmentRepository ?? (_departmentRepository = new DepartmentRepository(Context));

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