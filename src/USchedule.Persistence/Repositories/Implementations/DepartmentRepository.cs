using USchedule.Core.Entities.Implementations;
using USchedule.Persistence.Database;

namespace USchedule.Persistence.Repositories
{
    public class DepartmentRepository: Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DataContext context) : base(context)
        {
        }
    }
}