using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USchedule.Services.Abstractions;

namespace USchedule.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService _service;

        public UniversityController(IUniversityService service)
        {
            _service = service;
        }

        #region GET
        
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
               var response =  await _service.GetList();
                if (response.Success)
                {
                    return new JsonResult(response.Models);
                }
                else
                {
                    return new JsonResult(response);
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        #endregion
    }
}