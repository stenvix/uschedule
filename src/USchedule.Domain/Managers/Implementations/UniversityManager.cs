using AutoMapper;
using Microsoft.Extensions.Logging;
using USchedule.Core.Entities.Implementations;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;
using USchedule.Persistence.Repositories;

namespace USchedule.Domain.Managers
{
    public class UniversityManager: BaseManager<UniversityModel, University>, IUniversityManager
    {
        public UniversityManager(IAppUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseManager<UniversityModel, University>> logger) : base(unitOfWork, unitOfWork.UniversityRepository, mapper, logger)
        {
        }
    }
}