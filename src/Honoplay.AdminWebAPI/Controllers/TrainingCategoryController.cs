using Honoplay.Application._Infrastructure;
using Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoriesList;
using Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoryDetail;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Honoplay.AdminWebAPI.Controllers
{
    [Authorize]
    public class TrainingCategoryController : BaseController
    {
        /// <summary>
        /// This service retrieve all trainingCategories by tenant id. Filtered skip and take parameters.
        /// </summary>
        /// <param name="query">GetAll trainingCategory model</param>
        /// <returns>Get all trainingCategories list by tenant id with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TrainingCategoriesListModel>>> Get([FromQuery] GetTrainingCategoriesListQueryModel query)
        {
            try
            {
                var trainingCategoriesListModel = await Mediator.Send(new GetTrainingCategoriesListQuery(query.Skip, query.Take));

                return Ok(trainingCategoriesListModel);
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
        /// This service retrieve trainingCategory.
        /// </summary>
        /// <param name="id">Get trainingCategory model</param>
        /// <returns>Get trainingCategory by trainingCategoryId with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TrainingCategoryDetailModel>>> Get(int id)
        {
            try
            {
                var trainingCategoryDetailModel = await Mediator.Send(new GetTrainingCategoryDetailQuery(id));

                return Ok(trainingCategoryDetailModel);
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
