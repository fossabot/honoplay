﻿using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;
using Honoplay.Application.Tenants.Queries.GetTenantDetail;
using Honoplay.Application.Trainees.Commands.CreateTrainee;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Honoplay.AdminWebAPI.Controllers
{
    [Authorize]
    public class TraineeController : BaseController
    {
        /// <summary>
        /// Retrieve status code with CreateTraineeModel
        /// </summary>
        /// <param name="command">The param is must be trainee model.</param>
        /// <returns>StatusCode with CreateTraineeModel. </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<CreateTraineeModel>>> Post(CreateTraineeCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                command.AdminUserId = userId;
                var model = await Mediator.Send(command);
                return Ok(model);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateTraineeModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }
    }
}
