using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Honoplay.AdminWebAPI.Controllers
{
    public class StatusController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("OK");
        }
    }
}
