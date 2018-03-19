using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoctorApprove
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.asmx/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "LogOn", id = UrlParameter.Optional }
            );
            
            routes.Ignore("{*allasmx}", new { allasmx = @".*\.asmx(/.*)?" });
            routes.IgnoreRoute("{*x}", new { x = @".*\.asmx(/.*)?" });
        }
    }
}