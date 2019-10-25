using Honoplay.Application._Infrastructure;
using Honoplay.Application.ContentFiles.Commands.CreateContentFile;
using Honoplay.Application.ContentFiles.Commands.UpdateContentFile;
using Honoplay.Application.ContentFiles.Queries.GetContentFileDetail;
using Honoplay.Application.ContentFiles.Queries.GetContentFilesList;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Honoplay.AdminWebAPI.Controllers
{
    [Authorize]
    public class ContentFileController : BaseController
    {

        /// <summary>
        /// This service create contentFile.
        /// </summary>
        /// <param name="command">Create contentFile model</param>
        /// <returns>CreateContentFileModel with status code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateContentFileModel>>> Post([FromBody]CreateContentFileCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                command.CreatedBy = userId;
                command.TenantId = tenantId;

                var createContentFileModel = await Mediator.Send(command);
                return Created($"api/traineeUser", createContentFileModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateContentFileModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        /// <summary>
        /// This service update contentFile.
        /// </summary>
        /// <param name="command">Update contentFile model</param>
        /// <returns>UpdateContentFileModel with status code.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<UpdateContentFileModel>>> Put([FromBody]UpdateContentFileCommand command)
        {
            try
            {
                command.UpdatedBy = Claims[ClaimTypes.Sid].ToInt();
                command.TenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var updateContentFileModel = await Mediator.Send(command);

                return Ok(updateContentFileModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateContentFileModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }
        /// <summary>
        /// This service retrieve all contentFiles by tenant id. Filtered skip and take parameters.
        /// </summary>
        /// <param name="query">GetAll contentFile model</param>
        /// <returns>Get all contentFiles list by tenant id with status code.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<ContentFilesListModel>>> Get([FromQuery] GetContentFilesListQueryModel query)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var contentFilesListModel = await Mediator.Send(new GetContentFilesListQuery(tenantId, query.Skip, query.Take));

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

        /// <summary>
        /// This service retrieve contentFile.
        /// </summary>
        /// <param name="id">Get contentFile model</param>
        /// <returns>Get contentFile by contentFileId with status code.</returns>
        [HttpGet("GetFile/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<ContentFileDetailModel>>> GetFile(Guid id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var contentFilesListModel = await Mediator.Send(new GetContentFileDetailQuery(userId, id, tenantId));
                var file = contentFilesListModel.Items.FirstOrDefault();

                return File(file.Data, file.ContentType);
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