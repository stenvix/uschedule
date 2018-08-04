using System.Threading.Tasks;
using USchedule.Models.Domain;
using USchedule.Services.Responses.Base;

namespace USchedule.Services
{
    public interface IUniversityService
    {
        Task<ItemsResponse<UniversityModel>> GetList();
    }
}