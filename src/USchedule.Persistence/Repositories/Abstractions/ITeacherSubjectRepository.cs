using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Repositories
{
    public interface ITeacherSubjectRepository : IRepository<TeacherSubject>
    {
        Task<List<TeacherSubject>> GetByTeacherAsync(Guid teacherId);
    }
}