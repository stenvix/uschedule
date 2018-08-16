using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public interface ITeacherManager : IManager<TeacherModel>
    {
        Task<bool> Exists(string firstName, string lastName);
        Task<IList<TeacherModel>> CreateRangeAsync(IList<TeacherModel> teachers);
        Task CreateOrUpdateSubjects(Guid teacherId, IEnumerable<Guid> subjectsIds);
    }
}