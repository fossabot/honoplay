using Honoplay.Application._Infrastructure;
using Honoplay.Application.Answers.Commands.CreateAnswer;
using Honoplay.Application.Answers.Commands.UpdateAnswer;
using Honoplay.Application.Answers.Queries.GetAnswerDetail;
using Honoplay.Application.Answers.Queries.GetAnswersList;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Honoplay.AdminWebAPI.Controllers
{
    [Authorize]
    public class AnswerController : BaseController
    {
        /// <summary>
        /// This service create answer.
        /// </summary>
        /// <param name="command">Create answer model</param>
        /// <returns>CreateAnswerModel with status code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateAnswerModel>>> Post([FromBody]CreateAnswerCommand command)
        {
            try
            {
                command.CreatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var createAnswerModel = await Mediator.Send(command);

                return Created($"api/answer/{createAnswerModel.Items.Single().Text}", createAnswerModel);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateAnswerModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }
        /// <summary>
        /// This service update answer.
        /// </summary>
        /// <param name="command">Update answer model</param>
        /// <returns>UpdateAnswerModel with status code.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<UpdateAnswerModel>>> Put([FromBody]UpdateAnswerCommand command)
        {
            try
            {
                command.UpdatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var updateAnswerModel = await Mediator.Send(command);

                return Ok(updateAnswerModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateAnswerModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        /// <summary>
        /// This service update answer.
        /// </summary>
        /// <param name="query">GetAll answer model</param>
        /// <returns>Get all answers list by tenant id with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<AnswersListModel>>> Get([FromQuery]GetAnswersListQueryModel query)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var answersListModel = await Mediator.Send(new GetAnswersListQuery(tenantId, query.Skip, query.Take));

                return Ok(answersListModel);
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
        /// This service update answer.
        /// </summary>
        /// <param name="id">Get answer model</param>
        /// <returns>Get answer by tenant id and answer id with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<AnswersListModel>>> Get(int id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var answersListModel = await Mediator.Send(new GetAnswerDetailQuery(userId, id, tenantId));

                return Ok(answersListModel);
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
