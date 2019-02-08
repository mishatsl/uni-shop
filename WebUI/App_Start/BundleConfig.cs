using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
namespace WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive.*",
                        "~/Scripts/jquery.unobtrusive-ajax.*",
                        "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/store_js").Include(
                        "~/js/slick.min.js",
                        "~/js/nouislider.min.js",
                        "~/js/jquery.zoom.min.js"
                   //     "~/js/main.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/head_js") 
                .Include(
                "~/js/html5shiv.min.js",
                "~/js/respond.min.js"));
            

          //  BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/js/bootstrap.min.js"));

          
                      

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/css/bootstrap.min.css",
                      "~/css/slick.css",
                      "~/css/slick-theme.css",
                      "~/css/nouislider.min.css",
                      "~/css/font-awesome.min.css",
                     // "~/Content/themes/base/all.css",
                      "~/css/style.css"));
        }
    }
}

		