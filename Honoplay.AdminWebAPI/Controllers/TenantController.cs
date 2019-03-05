using Honoplay.Application.Exceptions;
using Honoplay.Application.Infrastructure;
using Honoplay.Application.Tenants.Commands.CreateTenant;
using Honoplay.Application.Tenants.Commands.UpdateTenant;
using Honoplay.Application.Tenants.Queries.GetTenantDetail;
using Honoplay.Application.Tenants.Queries.GetTenantsList;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Honoplay.AdminWebAPI.Controllers
{
    public class TenantController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromBody]GetTenantDetailQuery command)
        {
            try
            {
                var model = await Mediator.Send(command);
                return Ok(model);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(HttpStatusCode.NotFound.ToInt(), new ResponseModel<TenantDetailModel>(new Error(404, HttpStatusCode.NotFound.ToString(), ex)));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt(), new ResponseModel<TenantDetailModel>(new Error(500, HttpStatusCode.InternalServerError.ToString(), ex)));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody]GetTenantsListQuery command)
        {
            try
            {
                var model = await Mediator.Send(command);
                return Ok(model);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(HttpStatusCode.NotFound.ToInt(), new ResponseModel<TenantsListModel>(new Error(404, HttpStatusCode.NotFound.ToString(), ex)));
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<TenantsListModel>(new Error(409, HttpStatusCode.Conflict.ToString(), ex)));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt(), new ResponseModel<TenantsListModel>(new Error(500, HttpStatusCode.InternalServerError.ToString(), ex)));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateTenantCommand command)
        {
            try
            {
                var model = await Mediator.Send(command);
                return StatusCode(HttpStatusCode.Created.ToInt(), model);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateTenantModel>(new Error(409, HttpStatusCode.Conflict.ToString(), ex)));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt(), new ResponseModel<CreateTenantModel>(new Error(500, HttpStatusCode.InternalServerError.ToString(), ex)));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateTenantCommand command)
        {
            try
            {
                var model = await Mediator.Send(command);
                return StatusCode(HttpStatusCode.OK.ToInt(), model);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(HttpStatusCode.NotFound.ToInt(), new ResponseModel<UpdateTenantModel>(new Error(404, HttpStatusCode.NotFound.ToString(), ex)));
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<UpdateTenantModel>(new Error(409, HttpStatusCode.Conflict.ToString(), ex)));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt(), new ResponseModel<UpdateTenantModel>(new Error(500, HttpStatusCode.InternalServerError.ToString(), ex)));
            }
        }
    }
}