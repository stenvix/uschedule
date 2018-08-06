using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using USchedule.Core.Entities.Implementations;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public class InstituteManager: BaseManager<InstituteModel, Institute>, IInstituteManager
    {
        public InstituteManager(IAppUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseManager<InstituteModel, Institute>> logger) : base(unitOfWork, unitOfWork.InstituteRepository, mapper, logger)
        {
        }

        public async Task<IList<InstituteModel>> GetByUniversityAsync(Guid universityId)
        {
            var entities = await Repository.FindAllAsync(i => i.UniversityId == universityId);
            return Mapper.Map<IEnumerable<Institute>, IList<InstituteModel>>(entities);
        }

        public async Task<InstituteModel> GetByShortTitleAsync(string shortTitle)
        {
            var entity = await Repository.FindAsync(i => i.ShortTitle == shortTitle);
            return Mapper.Map<InstituteModel>(entity);
        }
    }
}