using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;
using USchedule.Services.Base;
using USchedule.Services.Responses.Base;

namespace USchedule.Services
{
    public class LessonService: BaseService, ILessonService
    {
        public LessonService(IManagerStore managerStore, ILogger<LessonService> logger) : base(managerStore, logger)
        {
        }

        public async Task<ItemsResponse<LessonModel>> GetByGroupAsync(Guid groupId)
        {
            var response = new ItemsResponse<LessonModel>();
            
            try
            {
                response.Models = await ManagerStore.LessonManager.GetByGroupAsync(groupId);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}