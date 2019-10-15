using Honoplay.Application._Infrastructure;
using Honoplay.Application.Tags.Commands.CreateTag;
using Honoplay.Application.Tags.Commands.UpdateTag;
using Honoplay.Application.Tags.Queries.GetTagDetail;
using Honoplay.Application.Tags.Queries.GetTagsList;
using Honoplay.Application.Tags.Queries.GetTagsListByQuestionId;
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
    public class TagController : BaseController
    {
        /// <summary>
        /// This service create tag.
        /// </summary>
        /// <param name="command">Create tag model</param>
        /// <returns>CreateTagModel with status code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateTagModel>>> Post([FromBody] CreateTagCommand command)
        {
            try
            {
                command.CreatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var createTagModel = await Mediator.Send(command);

                return Created($"api/tag/{createTagModel.Items}", createTagModel);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateTagModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        /// <summary>
        /// This service update tag.
        /// </summary>
        /// <param name="command">Update tag model</param>
        /// <returns>UpdateTagModel with status code.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<UpdateTagModel>>> Put([FromBody] UpdateTagCommand command)
        {
            try
            {
                command.UpdatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var updateTagModel = await Mediator.Send(command);

                return Ok(updateTagModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateTagModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        /// <summary>
        /// This service update tag.
        /// </summary>
        /// <param name="query">GetAll tag model</param>
        /// <returns>Get all tags list by tenant id with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TagsListModel>>> Get([FromQuery] GetTagsListQueryModel query)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var tagsListModel = await Mediator.Send(new GetTagsListQuery(tenantId, query.Skip, query.Take));

                return Ok(tagsListModel);
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
        /// This service update tag.
        /// </summary>
        /// <param name="id">Get tag model</param>
        /// <returns>Get tag by tenant id and tag id with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TagsListModel>>> Get(int id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var tagsListModel = await Mediator.Send(new GetTagDetailQuery(userId, id, tenantId));

                return Ok(tagsListModel);
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
        /// This service retrieve session by question id
        /// </summary>
        /// <param name="questionId">Get tag model</param>
        /// <returns>Get tag by tenant id and tag id with status code.</returns>
        [HttpGet]
        [Route("/Question/{questionId}/Tag")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TagsListModel>>> GetQuestionsListByQuestionId(int questionId)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var tagsListModel = await Mediator.Send(new GetTagsListByQuestionIdQuery(questionId, tenantId));

                return Ok(tagsListModel);
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
