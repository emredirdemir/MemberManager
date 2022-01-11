using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MemberManager.Filter
{
    public class UserFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? role = context.HttpContext.Session.GetString("role");
            if (!(role == "Admin" || role == "User"))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    {"action", "Login" },
                    {"controller", "User"}
                });
            }
            base.OnActionExecuting(context);    
        }
    }
}
