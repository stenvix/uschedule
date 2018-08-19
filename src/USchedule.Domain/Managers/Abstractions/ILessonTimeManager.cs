using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public interface ILessonTimeManager: IManager<LessonTimeModel>
    {
        Task<LessonTimeModel> GetByNumberAsync(int lessonNumber);
        Task<IList<LessonTimeModel>> GetAllByNumberAsync(IEnumerable<int> lessonNumbers, Guid universityId);
        Task<IList<LessonTimeModel>> GetByUniversityAsync(Guid universityId);
    }
}