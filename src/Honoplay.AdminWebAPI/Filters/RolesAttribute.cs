using Honoplay.AdminWebAPI.Enums;
using Honoplay.AdminWebAPI.Extensions;
using Honoplay.AdminWebAPI.TestEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
            if (currentUser != null)
            {
                foreach (var role in _roles)
                {
                    if (currentUser.Role != role.ToString())
                    {
                        context.Result = new RedirectResult("/swagger");
                        return;
                    }
                }
            }
            context.Result = new UnauthorizedResult();
        }
    }
}
