using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Repositories
{
    public interface ITeacherSubjectRepository : IRepository<TeacherSubject>
    {
        Task<List<TeacherSubject>> GetByTeacherAsync(Guid teacherId);
        Task<TeacherSubject> GetBySubjectAsync(Guid subjectId, string lastName, string firstName);
        Task<List<TeacherSubject>> GetBySubjectsAsync(IEnumerable<Guid> subjectsIds, Guid universityId);
        Task<List<TeacherSubject>> GetIds(IEnumerable<TeacherSubject> entities);
    }
}