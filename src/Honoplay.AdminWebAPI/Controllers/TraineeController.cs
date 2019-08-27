using Honoplay.Application._Infrastructure;
using Honoplay.Application.TraineeUsers.Commands.CreateTraineeUser;
using Honoplay.Application.TraineeUsers.Commands.UpdateTraineeUser;
using Honoplay.Application.TraineeUsers.Queries.GetTraineeUserDetail;
using Honoplay.Application.TraineeUsers.Queries.GetTraineeUsersList;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Honoplay.AdminWebAPI.Controllers
{
    [Authorize]
    public class TraineeUserController : BaseController
    {
        /// <summary>
        /// This service create traineeUser.
        /// </summary>
        /// <param name="command">Create traineeUser model</param>
        /// <returns>CreateTraineeUserModel with status code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateTraineeUserModel>>> Post([FromBody]CreateTraineeUserCommand command)
        {
            try
            {
                command.CreatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var createTraineeUserModel = await Mediator.Send(command);

                return Created($"api/traineeUser/{createTraineeUserModel.Items.Single().Name}", createTraineeUserModel);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateTraineeUserModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
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
                command.UpdatedBy = Claims[ClaimTypes.Sid].ToInt();
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TraineeUsersListModel>>> Get([FromQuery]GetTraineeUsersListQueryModel query)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var traineeUsersListModel = await Mediator.Send(new GetTraineeUsersListQuery(userId, tenantId, query.Skip, query.Take));

                return Ok(traineeUsersListModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TraineeUsersListModel>>> Get(int id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var traineeUsersListModel = await Mediator.Send(new GetTraineeUserDetailQuery(id, userId, tenantId));

                return Ok(traineeUsersListModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }
    }
}
