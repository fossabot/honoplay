﻿using Honoplay.Application._Infrastructure;
using Honoplay.Application.Tenants.Commands.CreateTenant;
using Honoplay.Application.Tenants.Commands.UpdateTenant;
using Honoplay.Application.Tenants.Queries.GetTenantDetail;
using Honoplay.Application.Tenants.Queries.GetTenantsList;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Honoplay.AdminWebAPI.Controllers
{
    [Authorize]
    public class TenantController : BaseController
    {
        /// <summary>
        /// Retrieve the tenant by ID.
        /// </summary>
        /// <param name="id">The ID of the desired Tenant</param>
        /// <returns>Tenant </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TenantDetailModel>>> Get(Guid id)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var command = new GetTenantDetailQuery(userId, id);
                var tenantDetailModel = await Mediator.Send(command);

                return Ok(tenantDetailModel);
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
        /// Retrieve the tenants by the Skip and Take.
        /// </summary>
        /// <param name="command"> Retrieve the tenants by the Skip and Take.</param>
        /// <remarks>How many items skipped</remarks>
        /// <returns>Tenants</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<TenantsListModel>>> Get([FromQuery]GetTenantsListQueryModel command)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var tenantsListModel = await Mediator.Send(new GetTenantsListQuery(tenantId, command.Skip, command.Take));

                return Ok(tenantsListModel);
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
        /// Retrieve the tenants by the Skip and Take.
        /// </summary>
        /// <param name="command"> Retrieve the tenants by the Skip and Take.</param>
        /// <returns>Created Tenant</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<CreateTenantModel>>> Post([FromBody]CreateTenantCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                command.CreatedBy = userId;

                var createTenantModel = await Mediator.Send(command);

                return Created($"api/tenant/{createTenantModel.Items.Single().Id}", createTenantModel);
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseModel<UpdateTenantModel>>> Put([FromBody]UpdateTenantCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                command.UpdatedBy = userId;

                var updateTenantModel = await Mediator.Send(command);

                return Ok(updateTenantModel);
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