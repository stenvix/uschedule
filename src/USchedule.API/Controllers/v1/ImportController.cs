using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USchedule.Services;
using USchedule.Shared.Models;

namespace USchedule.API.Controllers.v1
{
    [Route("api/v1/import")]
    public class ImportController : BaseController
    {
        private readonly IImportService _importService;

        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        #region POST

        [HttpPost("departments")]
        public async Task<IActionResult> ImportDepartments([FromBody] IList<DepartmentSharedModel> departments)
        {
            var response = await _importService.ImportDepartments(departments);

            if (response.Success)
            {
                return Ok();
            }

            return HandleResponse(response);
        }

        [HttpPost("institutes")]
        public async Task<IActionResult> ImportInstitutes([FromBody] IList<InstituteSharedModel> institutes)
        {
            var response = await _importService.ImportInstitutes(institutes);
            
            if (response.Success)
            {
                return Ok();
            }

            return HandleResponse(response);
        }

        #endregion
    }
}