using USchedule.Domain.Providers.Abstractions;
using USchedule.Models.DTO;

namespace USchedule.Domain.Managers.Base
{
    public class ManagerStore : IManagerStore
    {
        private readonly IServiceLocator _serviceLocator;

        #region Fields

        private IAppUnitOfWork _unitOfWork;
        private IUniversityManager _universityManager;    

        #endregion

        #region Properties

        private IAppUnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = _serviceLocator.GetInstance<IAppUnitOfWork>());

        public IUniversityManager UniversityManager => _universityManager ?? (_universityManager = _serviceLocator.GetInstance<IUniversityManager>(new LocatorParameter(typeof(IAppUnitOfWork), UnitOfWork)));
        
        #endregion

        public ManagerStore(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }       
    }
}