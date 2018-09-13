﻿using System.Web.Mvc;

namespace WebUI.Areas.Administrator
{
    public class AdministratorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Administrator_default",
                "admin/{action}/{id}",
                new {controller="Admin", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                  null,
                  "Navigation/{action}",
                  new { controller = "Navigation" }
             // namespaces: new string [] { "WebUI.Controllers" }
             );
        }
    }
}