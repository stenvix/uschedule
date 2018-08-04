using Microsoft.AspNetCore.Mvc;
using USchedule.Services.Responses.Base;

namespace USchedule.API.Controllers.v1
{
    public abstract class BaseController: ControllerBase
    {
        protected IActionResult HandleResponse(BaseResponse response)
        {
            return new StatusCodeResult((int)response.StatusCode);
        }
    }
}