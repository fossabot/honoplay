using Honoplay.Application._Infrastructure;
using Honoplay.Application.QuestionCategories.Commands.CreateQuestionCategory;
using Honoplay.Application.QuestionCategories.Commands.UpdateQuestionCategory;
using Honoplay.Application.QuestionCategories.Queries.GetQuestionCategoriesList;
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
    public class QuestionCategoryController : BaseController
    {
        /// <summary>
        /// This service create contentFile.
        /// </summary>
        /// <param name="command">Create contentFile model</param>
        /// <returns>CreateQuestionCategoryModel with status code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateQuestionCategoryModel>>> Post([FromBody]CreateQuestionCategoryCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                command.CreatedBy = userId;
                command.TenantId = tenantId;

                var createQuestionCategoryModel = await Mediator.Send(command);
                return Created($"api/traineeUser", createQuestionCategoryModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateQuestionCategoryModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<UpdateQuestionCategoryModel>>> Put([FromBody]UpdateQuestionCategoryCommand command)
        {
            try
            {
                command.UpdatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var updateQuestionCategoryModel = await Mediator.Send(command);

                return Ok(updateQuestionCategoryModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateQuestionCategoryModel>(new Error(HttpStatusCode.Conflict, ex)));
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
        public async Task<ActionResult<ResponseModel<QuestionCategoriesListModel>>> Get([FromQuery] GetQuestionCategoriesListQueryModel query)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var questionCategoriesListModel = await Mediator.Send(new GetQuestionCategoriesListQuery(userId, tenantId, query.Skip, query.Take));

                return Ok(questionCategoriesListModel);
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
