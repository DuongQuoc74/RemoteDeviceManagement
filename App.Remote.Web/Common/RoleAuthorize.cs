using Jabil.Pico.Web.BLL.Services;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Jabil.Pico.Web.Common
{
    public class RoleAttribute : AuthorizeAttribute
    {
        public EnumRole RoleId { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var userRoleService = DependencyResolver.Current.GetService<IUserRoleService>();
            var userName = HttpContext.Current.User.Identity.Name;
            var user = userRoleService.GetByName(userName);

            if (user == null || user.RoleId != (int)RoleId)
            {
                filterContext.Result = new HttpStatusCodeResult(403);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                                       { "action", "Unauthorised" },
                                       { "controller", "Home" },
                                       { "Area", String.Empty }
                                });
            }
        }
    }
}