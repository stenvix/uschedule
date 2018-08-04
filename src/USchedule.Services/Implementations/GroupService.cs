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
    public class GroupService: BaseService, IGroupService
    {
        public GroupService(IManagerStore managerStore, ILogger<GroupService> logger) : base(managerStore, logger)
        {
        }

        public async Task<ItemsResponse<GroupModel>> GetByInstitute(Guid instituteId)
        {
            var response = new ItemsResponse<GroupModel>();
            try
            {
                response.Models = await ManagerStore.GroupManager.GetByInstituteAsync(instituteId);
            }
            catch (Exception e)
            {
                Logger.LogError(e,e.Message);
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }

            return response;
        }
    }
}