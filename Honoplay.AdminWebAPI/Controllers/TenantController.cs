using Honoplay.Application.Exceptions;
using Honoplay.Application.Infrastructure;
using Honoplay.Application.Tenants.Commands.CreateTenant;
using Honoplay.Application.Tenants.Commands.UpdateTenant;
using Honoplay.Application.Tenants.Queries.GetTenantDetail;
using Honoplay.Application.Tenants.Queries.GetTenantsList;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Honoplay.AdminWebAPI.Controllers
{
    public class TenantController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<TenantDetailModel>>> Get(Guid id)
        {
            try
            {
                var command = new GetTenantDetailQuery(id);
                var model = await Mediator.Send(command);
                return Ok(model);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt(), new ResponseModel<TenantDetailModel>(new Error(HttpStatusCode.InternalServerError, ex)));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<TenantsListModel>>> Get([FromBody]GetTenantsListQuery command)
        {
            try
            {
                var model = await Mediator.Send(command);
                return Ok(model);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt(), new ResponseModel<TenantsListModel>(new Error(HttpStatusCode.InternalServerError, ex)));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<CreateTenantModel>>> Post([FromBody]CreateTenantCommand command)
        {
            try
            {
                var model = await Mediator.Send(command);
                return Created($"api/tenant/{model.Items.Single().Id}", model);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateTenantModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt(), new ResponseModel<CreateTenantModel>(new Error(HttpStatusCode.InternalServerError, ex)));
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel<UpdateTenantModel>>> Put([FromBody]UpdateTenantCommand command)
        {
            try
            {
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
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt(), new ResponseModel<UpdateTenantModel>(new Error(HttpStatusCode.InternalServerError, ex)));
            }
        }
    }
}