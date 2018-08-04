using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USchedule.Services;

namespace USchedule.API.Controllers.v1
{
    [Route("api/v1/universities")]
    public class UniversityController : BaseController
    {
        private readonly IUniversityService _universityService;
        private readonly IInstituteService _instituteService;

        public UniversityController(IUniversityService universityService, IInstituteService instituteService)
        {
            _universityService = universityService;
            _instituteService = instituteService;
        }

        #region GET

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var response = await _universityService.GetList();
            
            if (response.Success)
            {
                return new JsonResult(response.Models);
            }

            return HandleResponse(response);
        }

        [HttpGet("{id}/institutes")]
        public async Task<IActionResult> GetInstitutes(Guid id)
        {
            var response = await _instituteService.GetByUniversity(id);
            
            if (response.Success)
            {
                return new JsonResult(response.Models);
            }

            return HandleResponse(response);
        }

        #endregion
    }
}