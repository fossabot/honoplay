using Honoplay.Application._Infrastructure;
using Honoplay.Application.Trainees.Commands.CreateTrainee;
using Honoplay.Application.Trainees.Commands.UpdateTrainee;
using Honoplay.Application.Trainees.Queries.GetTraineeDetail;
using Honoplay.Application.Trainees.Queries.GetTraineesList;
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
    public class TraineeController : BaseController
    {
        /// <summary>
        /// This service create trainee.
        /// </summary>
        /// <param name="command">Create trainee model</param>
        /// <returns>CreateTraineeModel with status code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateTraineeModel>>> Post([FromBody]CreateTraineeCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                command.CreatedBy = userId;
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var model = await Mediator.Send(command);
                return Created($"api/trainee/{model.Items.Single().Name}", model);
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<UpdateTraineeModel>>> Put([FromBody]UpdateTraineeCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                command.UpdatedBy = userId;
                command.HostName = HonoHost;

                var model = await Mediator.Send(command);
                return Ok(model);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateTraineeModel>(new Error(HttpStatusCode.Conflict, ex)));
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
        public async Task<ActionResult<ResponseModel<TraineesListModel>>> Get([FromQuery]GetTraineesListQueryModel query)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();

                var model = await Mediator.Send(new GetTraineesListQuery(userId, HonoHost, query.Skip, query.Take));
                return Ok(model);
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
        public async Task<ActionResult<ResponseModel<TraineesListModel>>> Get(int id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();

                var model = await Mediator.Send(new GetTraineeDetailQuery(id, userId, HonoHost));
                return Ok(model);
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
