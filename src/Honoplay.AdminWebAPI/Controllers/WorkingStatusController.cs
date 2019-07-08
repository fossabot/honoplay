﻿using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Honoplay.Application._Infrastructure;
using Honoplay.Application.Departments.Commands.CreateDepartment;
using Honoplay.Application.Departments.Queries.GetDepartmentsList;
using Honoplay.Application.Tenants.Commands.UpdateTenant;
using Honoplay.Application.WorkingStatuses.Commands.CreateWorkingStatus;
using Honoplay.Application.WorkingStatuses.Commands.UpdateWorkingStatus;
using Honoplay.Application.WorkingStatuses.Queries;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Honoplay.AdminWebAPI.Controllers
{
    [Authorize]
    public class WorkingStatusController : BaseController
    {
        [HttpPost]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseModel<CreateWorkingStatusModel>>> Post([FromBody]CreateWorkingStatusCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                command.CreatedBy = userId;
                command.HostName = HonoHost;

                var model = await Mediator.Send(command);
                return Created($"api/workingStatus/{model.Items.Single().Name}", model);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateWorkingStatusModel>(new Error(HttpStatusCode.Conflict, ex)));
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<UpdateWorkingStatusModel>>> Put([FromBody]UpdateWorkingStatusCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                command.UpdatedBy = userId;
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
                return Conflict(new ResponseModel<UpdateWorkingStatusModel>(new Error(HttpStatusCode.Conflict, ex)));
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
        public async Task<ActionResult<ResponseModel<WorkingStatusesListModel>>> Get([FromQuery] GetWorkingStatusesListQueryModel query)
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
