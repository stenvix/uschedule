using System;
using System.Threading.Tasks;
using USchedule.Models.Domain;
using USchedule.Services.Responses.Base;

namespace USchedule.Services
{
    public interface IInstituteService
    {
        Task<ItemsResponse<InstituteModel>> GetList();
        Task<ItemsResponse<InstituteModel>> GetByUniversity(Guid universityId);
    }
}