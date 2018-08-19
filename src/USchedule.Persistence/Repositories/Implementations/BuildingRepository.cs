using Microsoft.EntityFrameworkCore;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Repositories
{
    public class BuildingRepository: Repository<Building>, IBuildingRepository
    {
        public BuildingRepository(DbContext context) : base(context)
        {
        }
    }
}