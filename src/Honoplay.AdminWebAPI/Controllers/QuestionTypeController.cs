using Honoplay.Application._Infrastructure;
using Honoplay.Application.QuestionTypes.Queries.GetQuestionTypeDetail;
using Honoplay.Application.QuestionTypes.Queries.GetQuestionTypesList;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Honoplay.AdminWebAPI.Controllers
{
    public class QuestionTypeController : BaseController
    {
        /// <summary>
        /// This service retrieve all questionTypes by tenant id. Filtered skip and take parameters.
        /// </summary>
        /// <param name="query">GetAll questionType model</param>
        /// <returns>Get all questionTypes list by tenant id with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<QuestionTypesListModel>>> Get([FromQuery] GetQuestionTypesListQueryModel query)
        {
            try
            {
                var questionTypesListModel = await Mediator.Send(new GetQuestionTypesListQuery(query.Skip, query.Take));

                return Ok(questionTypesListModel);
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
        /// This service retrieve questionType.
        /// </summary>
        /// <param name="id">Get questionType model</param>
        /// <returns>Get questionType by questionTypeId with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<QuestionTypeDetailModel>>> Get(int id)
        {
            try
            {
                var questionTypesListModel = await Mediator.Send(new GetQuestionTypeDetailQuery(id));

                return Ok(questionTypesListModel);
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
