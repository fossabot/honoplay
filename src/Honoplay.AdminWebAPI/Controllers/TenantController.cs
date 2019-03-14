using Honoplay.Application.Tenants.Commands.CreateTenant;
using Honoplay.Application.Tenants.Commands.UpdateTenant;
using Honoplay.Application.Tenants.Queries.GetTenantDetail;
using Honoplay.Application.Tenants.Queries.GetTenantsList;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;

namespace Honoplay.AdminWebAPI.Controllers
{
    [Authorize]
    public class TenantController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<TenantDetailModel>>> Get(Guid id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var command = new GetTenantDetailQuery(userId, id);
                var model = await Mediator.Send(command);
                return Ok(model);
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

        [HttpGet]
        public async Task<ActionResult<ResponseModel<TenantsListModel>>> Get([FromQuery]GetTenantsListQueryModel command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();

                var model = await Mediator.Send(new GetTenantsListQuery(userId, command.Skip, command.Take));
                return Ok(model);
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

        [HttpPost]
        public async Task<ActionResult<ResponseModel<CreateTenantModel>>> Post([FromBody]CreateTenantCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                command.CreatedBy = userId;

                var model = await Mediator.Send(command);
                return Created($"api/tenant/{model.Items.Single().Id}", model);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateTenantModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel<UpdateTenantModel>>> Put([FromBody]UpdateTenantCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                command.UpdatedBy = userId;

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
    }
}