using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;

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