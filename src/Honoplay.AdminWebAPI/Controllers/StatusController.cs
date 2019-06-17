using Microsoft.AspNetCore.Mvc;

namespace Honoplay.AdminWebAPI.Controllers
{
    public class StatusController : BaseController
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(HonoHost);
        }
    }
}