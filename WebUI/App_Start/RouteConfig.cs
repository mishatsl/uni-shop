using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{*param}",
                defaults: new { controller = "Product", action = "Index" }
                );

            //routes.MapRoute(
            //    null,
            //    "",
            //     defaults: new { controller = "Product", action = "Index"}
            //    );
            //routes.MapRoute(
            //      null,
            //      "{controller}/{action}" 
            //      //new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            // // namespaces: new string [] { "WebUI.Controllers" }
            // );

            routes.MapRoute(
                 null,
                 "admin",
                 new { controller = "Account", action = "Login", returnUrl = UrlParameter.Optional }
            );
            routes.MapRoute(
                 null,
                 "login",
                 new { controller = "Account", action = "Login" }
            );
            routes.MapRoute(
                 name: "lastRoute",
                 url: "{controller}/{action}"
             );


        }
    }
}
