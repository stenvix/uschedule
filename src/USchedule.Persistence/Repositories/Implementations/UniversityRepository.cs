using Microsoft.EntityFrameworkCore;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Repositories
{
    public class UniversityRepository: Repository<University>, IUniversityRepository
    {
        public UniversityRepository(DbContext context) : base(context)
        {
        }
    }
}