using System;
using System.Threading.Tasks;
using USchedule.Models.Domain;
using USchedule.Services.Responses.Base;

namespace USchedule.Services
{
    public interface ILessonService
    {
        Task<ItemsResponse<LessonModel>> GetByGroupAsync(Guid groupId);
    }
}