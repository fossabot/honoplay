using Honoplay.Application._Infrastructure;
using Honoplay.Application.TraineeUsers.Commands.AuthenticateTraineeUser;
using Honoplay.Application.TraineeUsers.Commands.CreateTraineeUser;
using Honoplay.Common._Exceptions;
using Honoplay.TraineeWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Honoplay.TraineeWebAPI.Controllers
{
    [Authorize]
    public class TraineeUserController : BaseController
    {
        private readonly ITraineeUserService _traineeUserService;

        public TraineeUserController(ITraineeUserService traineeUserService)
        {
            _traineeUserService = traineeUserService;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateTraineeUserCommand command)
        {
            try
            {
                command.HostName = HonoHost;
                var model = await Mediator.Send(command);

                var (user, stringToken) = _traineeUserService.GenerateToken(model);

                return Ok(new { User = user, Token = stringToken });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("RenewToken")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RenewToken([FromBody]string renewToken)
        {
            try
            {
                var token = _traineeUserService.RenewToken(renewToken);
                return StatusCode((int)HttpStatusCode.Created, token);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateTraineeUserModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<CreateTraineeUserModel>(new Error(HttpStatusCode.InternalServerError, ex)));
            }
        }
    }
}