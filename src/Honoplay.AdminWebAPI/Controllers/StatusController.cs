using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Honoplay.AdminWebAPI.Controllers
{
    public class StatusController : BaseController
    {
        [HttpGet]
        [Authorize(Roles = "AdminUser")]
        public IActionResult Get()
        {
            return Ok(HonoHost);
        }
    }
}