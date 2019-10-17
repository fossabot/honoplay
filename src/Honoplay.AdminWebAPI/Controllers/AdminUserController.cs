using Honoplay.AdminWebAPI.Interfaces;
using Honoplay.Application._Infrastructure;
using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using Honoplay.Application.AdminUsers.Commands.RegisterAdminUser;
using Honoplay.Common._Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Honoplay.AdminWebAPI.Controllers
{
    [AllowAnonymous]
    public class AdminUserController : BaseController
    {
        private readonly IAdminUserService _adminUserService;

        public AdminUserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateAdminUserCommand command)
        {
            try
            {
                command.HostName = HonoHost;
                var model = await Mediator.Send(command);

                var (user, stringToken) = _adminUserService.GenerateToken(model);

                return Ok(new { User = user, Token = stringToken });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("Register")]
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
                return Conflict(new ResponseModel<RegisterAdminUserModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<RegisterAdminUserModel>(new Error(HttpStatusCode.InternalServerError, ex)));
            }
        }
        [HttpPost("RenewToken")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult RenewToken([FromBody]string renewToken)
        {
            try
            {
                var token = _adminUserService.RenewToken(renewToken);
                return StatusCode((int)HttpStatusCode.Created, token);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<RegisterAdminUserModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<RegisterAdminUserModel>(new Error(HttpStatusCode.InternalServerError, ex)));
            }
        }
    }
}