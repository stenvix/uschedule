using USchedule.Core.Entities.Implementations;
using USchedule.Persistence.Database;

namespace USchedule.Persistence.Repositories
{
    public class InstituteRepository: Repository<Institute>, IInstituteRepository
    {
        public InstituteRepository(DataContext context) : base(context)
        {
        }
    }
}