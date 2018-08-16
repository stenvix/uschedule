using System.Threading.Tasks;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public interface IUniversityManager: IManager<UniversityModel>
    {
        Task<UniversityModel> GetByShortTitleAsync(string shortTitle);
    }
}