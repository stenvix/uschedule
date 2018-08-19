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

        public Task<TeacherSubject> GetBySubjectAsync(Guid subjectId, string lastName, string firstName)
        {
            var firstNameLike = $"%{firstName}%";
            return Set.Where(i =>
                i.SubjectId == subjectId && i.Teacher.LastName == lastName &&
                EF.Functions.Like(i.Teacher.FirstName, firstNameLike)).FirstOrDefaultAsync();
        }

        public Task<List<TeacherSubject>> GetBySubjectsAsync(IEnumerable<Guid> subjectsIds, Guid universityId)
        {
            return Set
                .Include(i => i.Teacher)
                .Where(i => subjectsIds.Contains(i.SubjectId) && i.Subject.UniversityId == universityId).ToListAsync();
        }

        public Task<List<TeacherSubject>> GetIds(IEnumerable<TeacherSubject> entities)
        {
            return Set.Where(i=> entities.Any(ts=>ts.SubjectId == i.SubjectId && ts.TeacherId == i.TeacherId )).ToListAsync();
        }
    }
}