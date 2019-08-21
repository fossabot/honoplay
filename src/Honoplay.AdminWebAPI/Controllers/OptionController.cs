using Honoplay.Application._Infrastructure;
using Honoplay.Application.Options.Commands.CreateOption;
using Honoplay.Application.Options.Commands.UpdateOption;
using Honoplay.Application.Options.Queries.GetOptionDetail;
using Honoplay.Application.Options.Queries.GetOptionsList;
using Honoplay.Application.Options.Queries.GetOptionsListByQuestionId;
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
    public class OptionController : BaseController
    {
        /// <summary>
        /// This service create option.
        /// </summary>
        /// <param name="command">Create option model</param>
        /// <returns>CreateOptionModel with status code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateOptionModel>>> Post([FromBody] CreateOptionCommand command)
        {
            try
            {
                command.CreatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var createOptionModel = await Mediator.Send(command);

                return Created($"api/option/{createOptionModel.Items}", createOptionModel);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateOptionModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        /// <summary>
        /// This service update option.
        /// </summary>
        /// <param name="command">Update option model</param>
        /// <returns>UpdateOptionModel with status code.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<UpdateOptionModel>>> Put([FromBody] UpdateOptionCommand command)
        {
            try
            {
                command.UpdatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var updateOptionModel = await Mediator.Send(command);

                return Ok(updateOptionModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateOptionModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        /// <summary>
        /// This service update option.
        /// </summary>
        /// <param name="query">GetAll option model</param>
        /// <returns>Get all options list by tenant id with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<OptionsListModel>>> Get([FromQuery] GetOptionsListQueryModel query)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var optionsListModel = await Mediator.Send(new GetOptionsListQuery(tenantId, query.Skip, query.Take));

                return Ok(optionsListModel);
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
        /// This service update option.
        /// </summary>
        /// <param name="id">Get option model</param>
        /// <returns>Get option by tenant id and option id with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<OptionsListModel>>> Get(int id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var optionsListModel = await Mediator.Send(new GetOptionDetailQuery(userId, id, tenantId));

                return Ok(optionsListModel);
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
        /// This service update option.
        /// </summary>
        /// <param name="id">Get option model</param>
        /// <returns>Get option by tenant id and option id with status code.</returns>
        [HttpGet]
        [Route("/api/Question/{questionId}/Option")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<OptionsListModel>>> GetQuestionsListByQuestionId(int questionId)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var optionsListModel = await Mediator.Send(new GetOptionsListByQuestionIdQuery(questionId, tenantId));

                return Ok(optionsListModel);
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
