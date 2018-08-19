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
    public class BuildingManager: BaseManager<BuildingModel, Building>, IBuildingManager
    {
        public BuildingManager(IAppUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseManager<BuildingModel, Building>> logger) : base(unitOfWork, unitOfWork.BuildingRepository, mapper, logger)
        {
        }

        public async Task<BuildingModel> GetByShortTitleAsync(string title)
        {
            var entity = await Repository.FindAsync(i => i.ShortTitle == title);
            return Mapper.Map<BuildingModel>(entity);
        }
        
        //TODO: Check if exists
        public async Task<BuildingModel> GetSystemAsync(Guid universityId)
        {
            var entity = await Repository.FindAsync(i => i.UniversityId == universityId && i.IsSystem);
            return Mapper.Map<BuildingModel>(entity);
        }

        public async Task<IList<BuildingModel>> GetAllByShortTitleAsync(IList<string> buildingNames, Guid universityId)
        {
            var entities = await Repository.FindAllAsync(i=>i.UniversityId == universityId && buildingNames.Contains(i.ShortTitle));
            return Mapper.Map<IList<BuildingModel>>(entities);
        }
    }
}