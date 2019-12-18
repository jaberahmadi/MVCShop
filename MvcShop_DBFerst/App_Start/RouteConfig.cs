using System.Web.Mvc;
using System.Web.Routing;

namespace IdentitySample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Product",
                url: "Products/{productName}",
                defaults: new { controller = "Home", action = "ShowProducts", productName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Groups",
                url: "Groups/{groupName}",
                defaults: new { controller = "Home", action = "ShowGroups", groupName = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}