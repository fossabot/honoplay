using Honoplay.Application._Infrastructure;
using Honoplay.Application.Avatars.Queries.GetAvatarDetail;
using Honoplay.Application.Avatars.Queries.GetAvatarsList;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Honoplay.TraineeWebAPI.Controllers
{
    [Authorize]
    public class AvatarController : BaseController
    {
        /// <summary>
        /// This service retrieve all avatars. Filtered skip and take parameters.
        /// </summary>
        /// <param name="query">GetAll avatar model</param>
        /// <returns>Get all avatars list with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<AvatarsListModel>>> Get([FromQuery] GetAvatarsListQueryModel query)
        {
            try
            {
                var avatarsListModel = await Mediator.Send(new GetAvatarsListQuery(query.Skip, query.Take));

                return Ok(avatarsListModel);

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
        /// This service retrieve avatar.
        /// </summary>
        /// <param name="id">Get avatar model</param>
        /// <returns>Get avatar by avatarId with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<AvatarDetailModel>>> Get(int id)
        {
            try
            {
                var avatarsListModel = await Mediator.Send(new GetAvatarDetailQuery(id));

                return Ok(avatarsListModel);
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
