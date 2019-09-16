using Honoplay.Application._Infrastructure;
using Honoplay.Application.Classrooms.Commands.CreateClassroom;
using Honoplay.Application.Classrooms.Commands.UpdateClassroom;
using Honoplay.Application.Classrooms.Queries.GetClassroomDetail;
using Honoplay.Application.Classrooms.Queries.GetClassroomsList;
using Honoplay.Application.Classrooms.Queries.GetClassroomsListByTrainingId;
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
    public class ClassroomController : BaseController
    {
        /// <summary>
        /// This service create classroom.
        /// </summary>
        /// <param name="command">Create classroom model</param>
        /// <returns>CreateClassroomModel with status code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateClassroomModel>>> Post([FromBody]CreateClassroomCommand command)
        {
            try
            {
                command.CreatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var createClassroomModel = await Mediator.Send(command);

                return Created($"api/classroom/{createClassroomModel.Items}", createClassroomModel);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateClassroomModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }
        /// <summary>
        /// This service update classroom.
        /// </summary>
        /// <param name="command">Update classroom model</param>
        /// <returns>UpdateClassroomModel with status code.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<UpdateClassroomModel>>> Put([FromBody]UpdateClassroomCommand command)
        {
            try
            {
                command.UpdatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var updateClassroomModel = await Mediator.Send(command);

                return Ok(updateClassroomModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateClassroomModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        /// <summary>
        /// This service retrieve all classrooms by tenant id. Filtered skip and take parameters.
        /// </summary>
        /// <param name="query">GetAll classroom model</param>
        /// <returns>Get all classrooms list by tenant id with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<ClassroomsListModel>>> Get([FromQuery]GetClassroomsListQueryModel query)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var classroomsListModel = await Mediator.Send(new GetClassroomsListQuery(tenantId, query.Skip, query.Take));

                return Ok(classroomsListModel);
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
        /// This service retrieve classroom by classroom id
        /// </summary>
        /// <param name="id">Get classroom model</param>
        /// <returns>Get classroom by tenant id and classroom id with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<ClassroomsListModel>>> Get(int id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var classroomsListModel = await Mediator.Send(new GetClassroomDetailQuery(userId, id, tenantId));

                return Ok(classroomsListModel);
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
        /// This service retrieve classroom list by training id
        /// </summary>
        /// <param name="trainingId">Get classrooms</param>
        /// <returns>Get classrooms by tenant id and training id with status code.</returns>
        [HttpGet]
        [Route("/Training/{trainingId}/Classroom")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<ClassroomsListByTrainingIdModel>>> GetByTrainingId(int trainingId)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var classroomsListModel = await Mediator.Send(new GetClassroomsListByTrainingIdQuery(tenantId, trainingId));

                return Ok(classroomsListModel);
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
