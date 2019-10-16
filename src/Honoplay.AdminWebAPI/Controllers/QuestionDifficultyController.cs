using Honoplay.Application._Infrastructure;
using Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultiesList;
using Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultyDetail;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Honoplay.AdminWebAPI.Controllers
{
    public class QuestionDifficultyController : BaseController
    {
        /// <summary>
        /// This service retrieve all questionDifficulties by tenant id. Filtered skip and take parameters.
        /// </summary>
        /// <param name="query">GetAll questionDifficulty model</param>
        /// <returns>Get all questionDifficulties list by tenant id with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<QuestionDifficultiesListModel>>> Get([FromQuery] GetQuestionDifficultiesListQueryModel query)
        {
            try
            {
                var questionDifficultiesListModel = await Mediator.Send(new GetQuestionDifficultiesListQuery(query.Skip, query.Take));

                return Ok(questionDifficultiesListModel);
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
        /// This service retrieve questionDifficulty.
        /// </summary>
        /// <param name="id">Get questionDifficulty model</param>
        /// <returns>Get questionDifficulty by questionDifficultyId with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<QuestionDifficultyDetailModel>>> Get(int id)
        {
            try
            {
                var questionDifficultyDetailModel = await Mediator.Send(new GetQuestionDifficultyDetailQuery(id));

                return Ok(questionDifficultyDetailModel);
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
