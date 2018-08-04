using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;
using USchedule.Services.Base;
using USchedule.Services.Responses.Base;

namespace USchedule.Services
{
    public class UniversityService : BaseService, IUniversityService
    {
        public UniversityService(IManagerStore managerStore, ILogger<UniversityService> logger) : base(managerStore, logger)
        {
        }

        public async Task<ItemsResponse<UniversityModel>> GetList()
        {
            var response = new ItemsResponse<UniversityModel>();
            response.Models = await ManagerStore.UniversityManager.GetAsync();
            return response;
        }
    }
}