﻿using Honoplay.AdminWebAPI.Controllers;
using Honoplay.Application.TrainerUsers.Commands.AuthenticateTrainerUser;
using Honoplay.TrainerUserWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Honoplay.TrainerUserWebAPI.Controllers
{
    [AllowAnonymous]
    public class TrainerUserController : BaseController
    {
        private readonly ITrainerUserService _trainerUserService;

        public TrainerUserController(ITrainerUserService trainerUserService)
        {
            _trainerUserService = trainerUserService;
        }

        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateTrainerUserCommand command)
        {
            try
            {
                command.HostName = HonoHost;
                var model = await Mediator.Send(command);

                var (user, stringToken) = _trainerUserService.GenerateToken(model);

                return Ok(new { User = user, Token = stringToken });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}