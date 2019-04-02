using System.Collections.Generic;
using Honoplay.AdminWebAPI.Enums;
using Honoplay.AdminWebAPI.Extensions;
using Honoplay.AdminWebAPI.Filters;
using Honoplay.AdminWebAPI.TestEntities;
using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Honoplay.AdminWebAPI.Controllers
{
    public class CustomAuthenticationTestController : BaseController
    {
        // GET api/values
        [HttpGet]
        [Roles(Roles.AdminUser, Roles.Trainer)]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(new[] { "value1", "value2" });
        }

        // POST api/Login/User
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult<string> Login([FromBody]User user)
        {
            if (user.Mail == "admin" && user.Password == "123")
            {
                HttpContext.Session.Set("user", user);
                return Ok();
            }

            return BadRequest();
        }
    }
}