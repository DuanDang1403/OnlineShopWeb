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
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

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
             name: "About",
             url: "gioi-thieu",
             defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "OnlineShopWeb.UI.Controllers" }
         );
            routes.MapRoute(
             name: "Contact",
             url: "lien-he",
             defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "OnlineShopWeb.UI.Controllers" }
         );

            routes.MapRoute(
            name: "Product",
            url: "san-pham",
            defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShopWeb.UI.Controllers" }
        );
                 routes.MapRoute(
           name: "Cart",
           url: "gio-hang",
           defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
           namespaces: new[] { "OnlineShop.Controllers" }
        );


            routes.MapRoute(
            name: "Add Cart",
            url: "them-gio-hang/{productid}",
            defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShop.Controllers" }
         );

            routes.MapRoute(
          name: "Đăng ký",
          url: "dang-ky",
          defaults: new { controller = "UserClient", action = "Register", id = UrlParameter.Optional },
          namespaces: new[] { "OnlineShop.Controllers" }
       );

            routes.MapRoute(
          name: "Payment",
          url: "thanh-toan",
          defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
          namespaces: new[] { "OnlineShop.Controllers" }
       );

         routes.MapRoute(
         name: "Success",
         url: "hoan-thanh",
         defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
         namespaces: new[] { "OnlineShop.Controllers" }
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
