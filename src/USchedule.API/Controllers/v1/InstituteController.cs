using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USchedule.Services;

namespace USchedule.API.Controllers.v1
{
    [Route("api/v1/institutes")]
    public class InstituteController: BaseController
    {
        private readonly IInstituteService _instituteService;
        private readonly IDepartmentService _departmentService;
        private readonly IGroupService _groupService;

        public InstituteController(IInstituteService instituteService, IDepartmentService departmentService, IGroupService groupService)
        {
            _instituteService = instituteService;
            _departmentService = departmentService;
            _groupService = groupService;
        }

        #region GET

        [HttpGet("{id}/departments")]
        public async Task<IActionResult> GetDepartments(Guid id)
        {
            var response = await _departmentService.GetByInstituteAsync(id);
            
            if (response.Success)
            {
                return new JsonResult(response.Models);
            }

            return HandleResponse(response);
        }

        [HttpGet("{id}/groups")]
        public async Task<IActionResult> GetGroups(Guid id)
        {
            var response = await _groupService.GetByInstitute(id);
            if (response.Success)
            {
                return new JsonResult(response.Models);
            }

            return HandleResponse(response);
        }
        
        #endregion
    }
}