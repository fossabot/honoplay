using Honoplay.Application._Infrastructure;
using Honoplay.Application.TrainingSerieses.Commands.CreateTrainingSeries;
using Honoplay.Application.TrainingSerieses.Commands.UpdateTrainingSeries;
using Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesDetail;
using Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesesList;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Honoplay.AdminWebAPI.Controllers
{
    [Authorize]
    public class TrainingSeriesController : BaseController
    {
        /// <summary>
        /// This service create trainingSeries.
        /// </summary>
        /// <param name="command">Create trainingSeries model</param>
        /// <returns>CreateTrainingSeriesModel with status code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateTrainingSeriesModel>>> Post([FromBody]CreateTrainingSeriesCommand command)
        {
            try
            {
                command.CreatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var createTrainingSeriesModel = await Mediator.Send(command);

                return Created($"api/trainingSeries/{createTrainingSeriesModel.Items}", createTrainingSeriesModel);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateTrainingSeriesModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }
        /// <summary>
        /// This service update trainingSeries.
        /// </summary>
        /// <param name="command">Update trainingSeries model</param>
        /// <returns>UpdateTrainingSeriesModel with status code.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<UpdateTrainingSeriesModel>>> Put([FromBody]UpdateTrainingSeriesCommand command)
        {
            try
            {
                command.UpdatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var updateTrainingSeriesModel = await Mediator.Send(command);

                return Ok(updateTrainingSeriesModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateTrainingSeriesModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        /// <summary>
        /// This service update trainingSeries.
        /// </summary>
        /// <param name="query">GetAll trainingSeries model</param>
        /// <returns>Get all trainingSerieses list by tenant id with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TrainingSeriesesListModel>>> Get([FromQuery]GetTrainingSeriesesListQueryModel query)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var trainingSeriesesListModel = await Mediator.Send(new GetTrainingSeriesesListQuery(tenantId, query.Skip, query.Take));

                return Ok(trainingSeriesesListModel);
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
        /// This service update trainingSeries.
        /// </summary>
        /// <param name="id">Get trainingSeries model</param>
        /// <returns>Get trainingSeries by tenant id and trainingSeries id with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TrainingSeriesesListModel>>> Get(int id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var trainingSeriesesListModel = await Mediator.Send(new GetTrainingSeriesDetailQuery(userId, id, tenantId));

                return Ok(trainingSeriesesListModel);
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
