using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Services.Responses.Base;
using USchedule.Shared.Models;

namespace USchedule.Services
{
    public interface IImportService
    {
        Task<BaseResponse> ImportDepartments(IList<DepartmentSharedModel> departments);
        Task<BaseResponse> ImportInstitutes(IList<InstituteSharedModel> institutes);
    }
}