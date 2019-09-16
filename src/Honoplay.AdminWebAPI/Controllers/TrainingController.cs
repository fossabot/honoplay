using Honoplay.Application._Infrastructure;
using Honoplay.Application.Trainings.Commands.CreateTraining;
using Honoplay.Application.Trainings.Commands.UpdateTraining;
using Honoplay.Application.Trainings.Queries.GetTrainingDetail;
using Honoplay.Application.Trainings.Queries.GetTrainingsList;
using Honoplay.Application.Trainings.Queries.GetTrainingsListByTrainingSeriesId;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Honoplay.AdminWebAPI.Controllers
{
    [Authorize]
    public class TrainingController : BaseController
    {
        /// <summary>
        /// This service create training.
        /// </summary>
        /// <param name="command">Create training model</param>
        /// <returns>CreateTrainingModel with status code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateTrainingModel>>> Post([FromBody]CreateTrainingCommand command)
        {
            try
            {
                command.CreatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var createTrainingModel = await Mediator.Send(command);

                return Created($"api/training/{createTrainingModel.Items}", createTrainingModel);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateTrainingModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }
        /// <summary>
        /// This service update training.
        /// </summary>
        /// <param name="command">Update training model</param>
        /// <returns>UpdateTrainingModel with status code.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<UpdateTrainingModel>>> Put([FromBody]UpdateTrainingCommand command)
        {
            try
            {
                command.UpdatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var updateTrainingModel = await Mediator.Send(command);

                return Ok(updateTrainingModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateTrainingModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        /// <summary>
        /// This service retrieve all trainings by tenant id. Filtered skip and take parameters.
        /// </summary>
        /// <param name="query">GetAll training model</param>
        /// <returns>Get all trainings list by tenant id with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TrainingsListModel>>> Get([FromQuery]GetTrainingsListQueryModel query)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var trainingsListModel = await Mediator.Send(new GetTrainingsListQuery(tenantId, query.Skip, query.Take));

                return Ok(trainingsListModel);
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
        /// This service retrieve training by training id
        /// </summary>
        /// <param name="id">Get training model</param>
        /// <returns>Get training by tenant id and training id with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TrainingsListModel>>> Get(int id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var trainingsListModel = await Mediator.Send(new GetTrainingDetailQuery(userId, id, tenantId));

                return Ok(trainingsListModel);
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
        /// This service retrieve training by trainingSeriesId
        /// </summary>
        /// <param name="trainingSeriesId">Get trainings list </param>
        /// <returns>Get training by tenant id and trainingSeries id with status code.</returns>
        [HttpGet]
        [Route("/TrainingSeries/{trainingSeriesId}/Training")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TrainingsListModel>>> GetByTrainingSeriesId(int trainingSeriesId)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var trainingsListModel = await Mediator.Send(new GetTrainingsListByTrainingSeriesIdQuery(userId, trainingSeriesId, tenantId));

                return Ok(trainingsListModel);
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
