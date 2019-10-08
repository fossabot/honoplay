using Honoplay.Application._Infrastructure;
using Honoplay.Application.ContentFiles.Queries.GetContentFileDetail;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Honoplay.AdminWebAPI.Controllers
{
    public class ContentFileController : BaseController
    {
        /// <summary>
        /// This service retrieve contentFile.
        /// </summary>
        /// <param name="id">Get contentFile model</param>
        /// <returns>Get contentFile by contentFileId with status code.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<ContentFileDetailModel>>> Get(Guid id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var contentFilesListModel = await Mediator.Send(new GetContentFileDetailQuery(userId, id, tenantId));

                return Ok(contentFilesListModel);
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
