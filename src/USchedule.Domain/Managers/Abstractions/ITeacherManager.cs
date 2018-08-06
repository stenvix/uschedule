using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public interface ITeacherManager : IManager<TeacherModel>
    {
        Task<bool> Exists(string firstName, string lastName);
        Task CreateRangeAsync(IList<TeacherModel> teachers);
    }
}