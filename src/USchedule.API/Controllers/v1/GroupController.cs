using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USchedule.Services;

namespace USchedule.API.Controllers.v1
{
    [Route("api/v1/groups")]
    public class GroupController: BaseController
    {
        private readonly IGroupService _groupService;
        private readonly ILessonService _lessonService;

        public GroupController(IGroupService groupService, ILessonService lessonService)
        {
            _groupService = groupService;
            _lessonService = lessonService;
        }

        #region GET

        [HttpGet("{id}/lessons")]
        public async Task<IActionResult> GetLessons(Guid id)
        {
            var response = await _lessonService.GetByGroupAsync(id);
            
            if (response.Success)
            {
                return new JsonResult(response.Models);
            }

            return HandleResponse(response);
        }

        #endregion
    }
}