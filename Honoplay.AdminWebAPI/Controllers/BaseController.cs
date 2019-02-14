﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
#nullable enable
namespace Honoplay.AdminWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator 
            ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
