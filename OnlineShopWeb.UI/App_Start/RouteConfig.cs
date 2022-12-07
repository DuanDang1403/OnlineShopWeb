using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShopWeb.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{metatitle}-{productCategoryId}",
                defaults: new { controller = "Product", action = "ProductCategoryDetail", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShopWeb.UI.Controllers" }
            );
            routes.MapRoute(
              name: "Product detail",
              url: "chi-tiet/{metatitle}-{productid}",
              defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShopWeb.UI.Controllers" }
          );

            routes.MapRoute(
             name: "Aboutl",
             url: "gioi-thieu",
             defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "OnlineShopWeb.UI.Controllers" }
         );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShopWeb.UI.Controllers"}
            );
        }
    }
}
