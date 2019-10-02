using Honoplay.Application._Infrastructure;
using Honoplay.Application.TraineeGroups.Commands.CreateTraineeGroup;
using Honoplay.Application.TraineeGroups.Commands.UpdateTraineeGroup;
using Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupDetail;
using Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupsList;
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
    public class TraineeGroupController : BaseController
    {
        /// <summary>
        /// This service create traineeGroup.
        /// </summary>
        /// <param name="command">Create traineeGroup model</param>
        /// <returns>CreateTraineeGroupModel with status code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateTraineeGroupModel>>> Post([FromBody]CreateTraineeGroupCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                command.CreatedBy = userId;
                command.TenantId = tenantId;

                var createTraineeGroupModel = await Mediator.Send(command);
                return Created($"api/traineeUser",createTraineeGroupModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateTraineeGroupModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        /// <summary>
        /// This service update traineeGroup.
        /// </summary>
        /// <param name="command">Update traineeGroup model</param>
        /// <returns>UpdateTraineeGroupModel with status code.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<UpdateTraineeGroupModel>>> Put([FromBody]UpdateTraineeGroupCommand command)
        {
            try
            {
                command.UpdatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var updateTraineeGroupModel = await Mediator.Send(command);

                return Ok(updateTraineeGroupModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateTraineeGroupModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }
        /// <summary>
        /// This service retrieve all traineeGroups by tenant id. Filtered skip and take parameters.
        /// </summary>
        /// <param name="query">GetAll traineeGroup model</param>
        /// <returns>Get all traineeGroups list by tenant id with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TraineeGroupsListModel>>> Get([FromQuery] GetTraineeGroupsListQueryModel query)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var traineeGroupsListModel = await Mediator.Send(new GetTraineeGroupsListQuery(tenantId, query.Skip, query.Take));

                return Ok(traineeGroupsListModel);

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
        /// <summary>
        /// This service retrieve traineeGroup by traineeGroup id
        /// </summary>
        /// <param name="id">Get traineeGroup model</param>
        /// <returns>Get traineeGroup by tenant id and traineeGroup id with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TraineeGroupsListModel>>> Get(int id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var traineeGroupsListModel = await Mediator.Send(new GetTraineeGroupDetailQuery(id, userId, tenantId));

                return Ok(traineeGroupsListModel);
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