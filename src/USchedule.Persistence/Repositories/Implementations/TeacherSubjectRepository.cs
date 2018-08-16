using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using USchedule.Core.Entities.Implementations;
using USchedule.Persistence.Database;

namespace USchedule.Persistence.Repositories
{
    public class TeacherSubjectRepository : Repository<TeacherSubject>, ITeacherSubjectRepository
    {
        public TeacherSubjectRepository(DataContext context) : base(context)
        {
        }

        public Task<List<TeacherSubject>> GetByTeacherAsync(Guid teacherId)
        {
            return Set.Where(i => i.TeacherId == teacherId).ToListAsync();
        }
    }
}