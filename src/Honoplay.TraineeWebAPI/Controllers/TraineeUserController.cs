using Honoplay.AdminWebAPI.Controllers;
using Honoplay.Application.TraineeUsers.Commands.AuthenticateTraineeUser;
using Honoplay.TraineeUserWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Honoplay.TraineeUserWebAPI.Controllers
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
    }
}