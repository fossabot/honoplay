using Honoplay.Application._Infrastructure;
using Honoplay.Application.Departments.Commands.CreateDepartment;
using Honoplay.Application.Departments.Queries.GetDepartmentsList;
using Honoplay.Application.Tenants.Commands.UpdateTenant;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Honoplay.AdminWebAPI.Controllers
{
    [Authorize]
    public class DepartmentController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateDepartmentModel>>> Post([FromBody]CreateDepartmentCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                command.AdminUserId = userId;
                command.HostName = HonoHost;

                var model = await Mediator.Send(command);
                return Ok(model);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateTenantModel>(new Error(HttpStatusCode.Conflict, ex)));
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
        public async Task<ActionResult<ResponseModel<DepartmentsListModel>>> Get([FromQuery] GetDepartmentsListQueryModel query)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();

                var models = await Mediator.Send(new GetDepartmentsListQuery(userId, HonoHost, query.Skip, query.Take));

                return Ok(models);

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
