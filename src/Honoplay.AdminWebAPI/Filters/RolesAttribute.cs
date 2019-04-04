using Honoplay.AdminWebAPI.Enums;
using Honoplay.AdminWebAPI.Extensions;
using Honoplay.AdminWebAPI.TestEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Honoplay.AdminWebAPI.Filters
{
    public class RolesAttribute : ActionFilterAttribute
    {
        private readonly Roles[] _roles;
        public RolesAttribute(params Roles[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var currentUser = context.HttpContext.Session.Get<User>("user");

            if (currentUser != null && _roles.Any(s => s.ToString() == currentUser.Role))
            {
                context.Result = new RedirectResult("/swagger");
                return;
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
