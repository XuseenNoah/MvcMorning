using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMorning.Models;

namespace MvcMorning.Helpers
{
    public class PermisionRequired :ActionFilterAttribute
    {
        LoginEnter.Permissions expectedPermissions;
        //LoginForm.Permissions ex;

        public PermisionRequired(LoginEnter.Permissions permissions = LoginEnter.Permissions.None)
        {
            this.expectedPermissions = permissions;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            ActionResult unauthorizedActionResult = new ViewResult { ViewName = "Unauthorized" };
            ActionResult logoutActionResult = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Logout", returnUrl = filterContext.HttpContext.Request.RawUrl }));

            var user = filterContext.HttpContext.Session["User"] as LoginEnter;

            if (user == null)
            {
                filterContext.Result = logoutActionResult;
                return;
            }

            var userCurrentPermissions = user.CurrentPermissions;
            //var userCurrentPermissions = (LoginEnter.Permissions)HttpContext.Current.Cache[string.Format("{0}'s CurrentPermision", user.Username)];

            if (((userCurrentPermissions & expectedPermissions) != expectedPermissions))
                filterContext.Result = unauthorizedActionResult;
        }
    }
}