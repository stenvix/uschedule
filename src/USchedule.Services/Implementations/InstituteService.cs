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
    public class InstituteService: BaseService, IInstituteService
    {
        public InstituteService(IManagerStore managerStore, ILogger<InstituteService> logger) : base(managerStore, logger)
        {
        }

        public async Task<ItemsResponse<InstituteModel>> GetList()
        {
            var response = new ItemsResponse<InstituteModel>();
            try
            {
                response.Models = await ManagerStore.InstituteManager.GetAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<ItemsResponse<InstituteModel>> GetByUniversity(Guid universityId)
        {
            var response = new ItemsResponse<InstituteModel>();
            try
            {
                response.Models = await ManagerStore.InstituteManager.GetByUniversityAsync(universityId);
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