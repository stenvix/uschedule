using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public interface IDepartmentManager : IManager<DepartmentModel>
    {
        Task<IList<DepartmentModel>> GetByInstituteAsync(Guid instituteId);
        Task<DepartmentModel>  GetByTitleAsync(string departmentTitle);
        Task<DepartmentModel> GetByShortTitleAsync(string shortTitle, Guid instituteId);
        Task<DepartmentModel> GetEmpty(Guid instituteId);
    }
}