using Honoplay.Application._Infrastructure;
using Honoplay.Application.TraineeUsers.Commands.AuthenticateTraineeUser;
using Honoplay.Application.TraineeUsers.Commands.CreateTraineeUser;
using Honoplay.Application.TraineeUsers.Commands.UpdateTraineeUser;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.TraineeWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
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
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<UpdateTraineeUserModel>>> Put([FromBody]UpdateTraineeUserCommand command)
        {
            try
            {
                command.Id = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var updateTraineeUserModel = await Mediator.Send(command);

                return Ok(updateTraineeUserModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateTraineeUserModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
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