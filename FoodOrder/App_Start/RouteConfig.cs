using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FoodOrder
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Restaurants",
                url: "{controller}/{action}",
                defaults: new { controller = "Restaurants", action = "Index" }
                );

            routes.MapRoute(
                name: "FoodItems",
                url: "{controller}/{action}",
                defaults: new { controller = "FoodItems", action = "Index" }
                );

            routes.MapRoute(
                name: "FCategory",
                url: "{controller}/{action}",
                defaults: new { controller = "FCategories", action = "Index" }
                );

            routes.MapRoute(
                name: "ResCat",
                url: "{controller}/{action}",
                defaults: new { controller = "ResCats", action = "Index" }
                );
            routes.MapRoute(
                name: "Users",
                url: "{controller}/{action}",
                defaults: new { controller = "Users", action = "Index" }
                );

            routes.MapRoute(
                name: "Retrive",
                url: "{controller}/{action}",
                defaults: new { controller = "Restaurants", action = "Retrive" }
                );
        }
    }
}
