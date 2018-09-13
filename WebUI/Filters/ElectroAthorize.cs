using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI.Filters
{
    public class ElectroAthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string Controller = filterContext.RouteData.Values["returnUrl"].ToString();
            if(filterContext.RouteData.Values["returnUrl"]!=null)
            {
                string returnUrl = filterContext.RouteData.Values["returnUrl"].ToString();
                if (returnUrl =="/admin")
                {
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Account", action = "Login" }));
                    return;
                }
            }
            filterContext.Result = new RedirectToRouteResult(new
            RouteValueDictionary(new { controller = "Account", action = "Login" }));
        }
    }
}