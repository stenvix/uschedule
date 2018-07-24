using System.Threading.Tasks;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;
using USchedule.Services.Responses.Base;

namespace USchedule.Services.Abstractions
{
    public interface IUniversityService
    {
        Task<ItemsResponse<UniversityModel>> GetList();
    }
}