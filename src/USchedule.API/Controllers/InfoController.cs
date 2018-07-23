using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USchedule.Persistence.Database;

namespace USchedule.API.Controllers
{
    [Route("api/[controller]")]
    public class InfoController: ControllerBase
    {
        private readonly DataContext _dataContext;

        public InfoController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("database")]
        public async Task<IActionResult> GetDatabaseInfo()
        {
            var provider = _dataContext.Database.ProviderName;
            var isDbExist = !await _dataContext.Database.EnsureCreatedAsync();
            
            return new JsonResult( new {Provider=provider, DbExists=isDbExist});
        }
    }
}