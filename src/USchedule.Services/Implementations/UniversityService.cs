using System.Threading.Tasks;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;
using USchedule.Services.Abstractions;
using USchedule.Services.Responses.Base;

namespace USchedule.Services.Implementations
{
    public class UniversityService : IUniversityService
    {
        private readonly IManagerStore _managerStore;

        public UniversityService(IManagerStore managerStore)
        {
            _managerStore = managerStore;
        }

        public async Task<ItemsResponse<UniversityModel>> GetList()
        {
            var response = new ItemsResponse<UniversityModel>();
            response.Models =  await _managerStore.UniversityManager.GetAsync();
            return response;
        }
    }
}