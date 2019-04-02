using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using Honoplay.Application.AdminUsers.Commands.RegisterAdminUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Honoplay.AdminWebAPI.Enums;
using Honoplay.AdminWebAPI.Filters;
using Honoplay.AdminWebAPI.Interfaces;
using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Honoplay.AdminWebAPI.Controllers
{
    [AllowAnonymous]
    public class AdminUserController : BaseController
    {
        private readonly IUserService _userService;

        public AdminUserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [Roles(roles: new[] { Roles.AdminUser, Roles.Trainer })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateAdminUserCommand command)
        {
            try
            {
                var model = await Mediator.Send(command);

                var result = _userService.Authenticate(model);

                return Ok(new { User = result.user, Token = result.stringToken });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody]RegisterAdminUserCommand command)
        {
            try
            {
                var model = await Mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, model);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<AdminUserRegisterModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<AdminUserRegisterModel>(new Error(HttpStatusCode.InternalServerError, ex)));
            }
        }
    }
}