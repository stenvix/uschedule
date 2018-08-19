using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public interface ISubjectManager : IManager<SubjectModel>
    {
        Task<IList<SubjectModel>> CreateRangeAsync(Guid universityId, IList<SubjectModel> models);
        Task<SubjectModel> GetByTitleAsync(string title);
        Task<IList<SubjectModel>> GetAllByTitleAsync(IEnumerable<string> subjectsTitles, Guid universityId);
    }
}