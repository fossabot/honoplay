using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Honoplay.AdminWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator = null;

        protected IMediator Mediator => _mediator
            ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        public Dictionary<string, string> Claims => User.Claims.ToDictionary(x => x.Type, x => x.Value);

        public string HonoHost => Request.Headers.TryGetValue("HonoHost", out var value) ? value[0] : Request.Host.Host;
    }
}