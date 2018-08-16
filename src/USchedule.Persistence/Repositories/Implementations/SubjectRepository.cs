using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using USchedule.Core.Entities.Implementations;
using USchedule.Persistence.Database;

namespace USchedule.Persistence.Repositories
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        public SubjectRepository(DataContext context) : base(context)
        {
        }

        public Task<List<Subject>> GetExistedAsync(IList<Subject> entities)
        {
            var subjectTitles = entities.Select(i => i.Title).Distinct();
            return Set.Where(i => subjectTitles.Contains(i.Title)).ToListAsync();
        }
    }
}