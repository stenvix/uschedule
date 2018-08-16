using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using USchedule.Core.Entities.Implementations;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public class UniversityManager: BaseManager<UniversityModel, University>, IUniversityManager
    {
        public UniversityManager(IAppUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseManager<UniversityModel, University>> logger) : base(unitOfWork, unitOfWork.UniversityRepository, mapper, logger)
        {
        }

        public async Task<UniversityModel> GetByShortTitleAsync(string shortTitle)
        {
            var entity = await Repository.FindAsync(i => i.ShortTitle == shortTitle);
            return Mapper.Map<UniversityModel>(entity);
        }
    }
}