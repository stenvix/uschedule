using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(DbContext context) : base(context)
        {
        }

        public Task<List<Teacher>> GetExistedAsync(IEnumerable<Teacher> teachers)
        {
            var teacherNames = teachers.Select(i => new {i.FirstName, i.LastName});
            return Set.Where(i=> teacherNames.Contains(new {i.FirstName, i.LastName})).ToListAsync();
        }
    }
}