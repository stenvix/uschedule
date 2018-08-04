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
    public class DepartmentService: BaseService, IDepartmentService
    {
        public DepartmentService(IManagerStore managerStore, ILogger<DepartmentService> logger) : base(managerStore, logger)
        {
            
        }

        public async Task<ItemsResponse<DepartmentModel>> GetByInstituteAsync(Guid instituteId)
        {
            var response = new ItemsResponse<DepartmentModel>();
            try
            {
                response.Models = await ManagerStore.DepartmentManager.GetByInstituteAsync(instituteId);
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