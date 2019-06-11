using Honoplay.Application._Infrastructure;
using Honoplay.Application.Trainers.Commands.CreateTrainer;
using Honoplay.Application.Trainers.Commands.UpdateTrainer;
using Honoplay.Application.Trainers.Queries.GetTrainerDetail;
using Honoplay.Application.Trainers.Queries.GetTrainersList;
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
    public class TrainerController : BaseController
    {
        // GET: api/<controller>{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TrainerDetailModel>>> Get(int id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();

                var models = await Mediator.Send(new GetTrainerDetailQuery(userId, id, HonoHost));

                return Ok(models);

            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TrainersListModel>>> Get([FromQuery] GetTrainersListQueryModel query)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();

                var models = await Mediator.Send(new GetTrainersListQuery(userId, HonoHost, query.Skip, query.Take));

                return Ok(models);

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

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<ResponseModel<CreateTrainerModel>>> Post([FromBody]CreateTrainerCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                command.CreatedBy = userId;
                command.HostName = HonoHost;

                var model = await Mediator.Send(command);
                return Created($"api/Trainer/{model.Items.Single().Name}", model);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateTrainerModel>(new Error(HttpStatusCode.Conflict, ex)));
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
        public async Task<ActionResult<ResponseModel<UpdateTrainerModel>>> Put([FromBody]UpdateTrainerCommand command)
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
                return Conflict(new ResponseModel<UpdateTrainerModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }
    }
}
