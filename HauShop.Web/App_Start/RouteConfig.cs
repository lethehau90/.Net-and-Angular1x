using System.Web.Mvc;
using System.Web.Routing;

namespace HauShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Corfirm Order",
                url: "xac-nhan-don-hang.html",
                defaults: new { controller = "ShoppingCart", action = "ConfirmOrder", id = UrlParameter.Optional },
                namespaces: new string[] { "HauShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Cancel Order",
                url: "huy-don-hang.html",
                defaults: new { controller = "ShoppingCart", action = "CancelOrder", id = UrlParameter.Optional },
                namespaces: new string[] { "HauShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap.html",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "HauShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "lien-he.html",
                defaults: new { controller = "Contact", action = "index", id = UrlParameter.Optional },
                namespaces: new string[] { "HauShop.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Resgister",
               url: "dang-ky.html",
               defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
               namespaces: new string[] { "HauShop.Web.Controllers" }
           );

            routes.MapRoute(
               name: "ShoppingCart",
               url: "gio-hang.html",
               defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "HauShop.Web.Controllers" }
           );

            routes.MapRoute(
               name: "Checkout",
               url: "thanh-toan.html",
               defaults: new { controller = "ShoppingCart", action = "Checkout", alias = UrlParameter.Optional },
               namespaces: new string[] { "HauShop.Web.Controllers" }
           );

            routes.MapRoute(
                name: "page",
                url: "trang/{alias}.html",
                defaults: new { controller = "Page", action = "index", alias = UrlParameter.Optional },
                namespaces: new string[] { "HauShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Search",
                url: "tim-kiem.html",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                namespaces: new string[] { "HauShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "product Category",
                url: "{alias}.pc-{id}.html",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new string[] { "HauShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "product",
                url: "{alias}.p-{id}.html",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "HauShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "tag",
                url: "{tag}/{tagid}.html",
                defaults: new { controller = "Product", action = "ListByTag", tagid = UrlParameter.Optional },
                namespaces: new string[] { "HauShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "HauShop.Web.Controllers" }
            );
        }
    }
}