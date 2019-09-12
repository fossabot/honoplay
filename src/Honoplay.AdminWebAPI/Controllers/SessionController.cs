using Honoplay.Application._Infrastructure;
using Honoplay.Application.Sessions.Commands.CreateSession;
using Honoplay.Application.Sessions.Commands.UpdateSession;
using Honoplay.Application.Sessions.Queries.GetSessionDetail;
using Honoplay.Application.Sessions.Queries.GetSessionsList;
using Honoplay.Application.Sessions.Queries.GetSessionsListByClassroomId;
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
    public class SessionController : BaseController
    {
        /// <summary>
        /// This service create session.
        /// </summary>
        /// <param name="command">Create session model</param>
        /// <returns>CreateSessionModel with status code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateSessionModel>>> Post([FromBody]CreateSessionCommand command)
        {
            try
            {
                command.CreatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var createSessionModel = await Mediator.Send(command);

                return Created($"api/session/{createSessionModel.Items}", createSessionModel);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateSessionModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }
        /// <summary>
        /// This service update session.
        /// </summary>
        /// <param name="command">Update session model</param>
        /// <returns>UpdateSessionModel with status code.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<UpdateSessionModel>>> Put([FromBody]UpdateSessionCommand command)
        {
            try
            {
                command.UpdatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var updateSessionModel = await Mediator.Send(command);

                return Ok(updateSessionModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateSessionModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        /// <summary>
        /// This service retrieve all sessions by tenant id. Filtered skip and take parameters.
        /// </summary>
        /// <param name="query">GetAll session model</param>
        /// <returns>Get all sessions list by tenant id with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<SessionsListModel>>> Get([FromQuery]GetSessionsListQueryModel query)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var sessionsListModel = await Mediator.Send(new GetSessionsListQuery(tenantId, query.Skip, query.Take));

                return Ok(sessionsListModel);
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
        /// This service retrieve session by session id
        /// </summary>
        /// <param name="id">Get session model</param>
        /// <returns>Get session by tenant id and session id with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<SessionsListModel>>> Get(int id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var sessionsListModel = await Mediator.Send(new GetSessionDetailQuery(userId, id, tenantId));

                return Ok(sessionsListModel);
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
        /// This service retrieve session by session id
        /// </summary>
        /// <param name="classroomId">Get session model</param>
        /// <returns>Get session by tenant id and classroom id with status code.</returns>
        [HttpGet]
        [Route("/Classroom/{classroomId}/Session")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<SessionsListByClassroomIdModel>>> GetSessionsListByClassroomId(int classroomId)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var sessionsListModel = await Mediator.Send(new GetSessionsListByClassroomIdQuery(classroomId, tenantId));

                return Ok(sessionsListModel);
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
