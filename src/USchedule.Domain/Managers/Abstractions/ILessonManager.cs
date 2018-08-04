using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public interface ILessonManager: IManager<LessonModel>
    {
        Task<IList<LessonModel>> GetByGroupAsync(Guid groupId);
    }
}