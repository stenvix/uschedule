using USchedule.Domain.Providers.Abstractions;
using USchedule.Models.DTO;
using USchedule.Persistence.Repositories;

namespace USchedule.Domain.Managers.Base
{
    public class ManagerStore : IManagerStore
    {
        private readonly IServiceLocator _serviceLocator;

        #region Fields

        private IAppUnitOfWork _unitOfWork;
        private IUniversityManager _universityManager;
        private IInstituteManager _instituteManager;
        private IDepartmentManager _departmentManager;
        private IGroupManager _groupManager;
        private ILessonManager _lessonManager;
        private ITeacherManager _teacherManager;
        private ISubjectManager _subjectManager;

        #endregion

        #region Properties

        private IAppUnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = _serviceLocator.GetInstance<IAppUnitOfWork>());

        public IUniversityManager UniversityManager => _universityManager ?? (_universityManager = _serviceLocator.GetInstance<IUniversityManager>(new LocatorParameter(typeof(IAppUnitOfWork), UnitOfWork)));
        public IInstituteManager InstituteManager => _instituteManager ?? (_instituteManager = _serviceLocator.GetInstance<IInstituteManager>(new LocatorParameter(typeof(IAppUnitOfWork), UnitOfWork)));
        public IDepartmentManager DepartmentManager => _departmentManager ?? (_departmentManager = _serviceLocator.GetInstance<IDepartmentManager>(new LocatorParameter(typeof(IAppUnitOfWork), UnitOfWork)));
        public IGroupManager GroupManager => _groupManager ?? (_groupManager = _serviceLocator.GetInstance<IGroupManager>(new LocatorParameter(typeof(IAppUnitOfWork), UnitOfWork)));
        public ILessonManager LessonManager => _lessonManager ?? (_lessonManager = _serviceLocator.GetInstance<ILessonManager>(new LocatorParameter(typeof(IAppUnitOfWork), UnitOfWork)));
        public ITeacherManager TeacherManager => _teacherManager ?? (_teacherManager = _serviceLocator.GetInstance<ITeacherManager>(new LocatorParameter(typeof(IAppUnitOfWork), UnitOfWork)));
        public ISubjectManager SubjectManager => _subjectManager ?? (_subjectManager = _serviceLocator.GetInstance<ISubjectManager>(new LocatorParameter(typeof(IAppUnitOfWork), UnitOfWork)));

        #endregion

        public ManagerStore(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }       
    }
}