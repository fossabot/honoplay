using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

#nullable enable

namespace Honoplay.AdminWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator = null;

        protected IMediator Mediator => _mediator
            ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}