using Honoplay.AdminWebAPI.Enums;
using Honoplay.AdminWebAPI.Extensions;
using Honoplay.AdminWebAPI.Filters;
using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Honoplay.AdminWebAPI.TestEntities;

namespace Honoplay.AdminWebAPI.Controllers
{
    public class CustomAuthenticationTestController : BaseController
    {
        // GET api/values
        [HttpGet]
        [Roles(Roles.AdminUser, Roles.Trainer)]
        public ActionResult GetValues()
        {
            return new JsonResult(new[] { "value1", "value2" });
        }

        // POST api/Login/User
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> Login([FromBody]AuthenticateAdminUserCommand command)
        {
            var model = await Mediator.Send(command);

            var user = new User
            {
                Mail = model.Email,
                Role = "AdminUser"
            };

            HttpContext.Session.Set("user", user);

            return Ok();
        }
    }
}