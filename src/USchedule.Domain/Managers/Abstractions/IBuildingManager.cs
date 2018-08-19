using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public interface IBuildingManager: IManager<BuildingModel>
    {
        Task<BuildingModel> GetByShortTitleAsync(string title);
        Task<BuildingModel> GetSystemAsync(Guid universityId);
        Task<IList<BuildingModel>> GetAllByShortTitleAsync(IList<string> buildingNames, Guid universityId);
    }
}