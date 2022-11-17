using OnlineShopWeb.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopWeb.UI.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        //initilizing culture on controller initialization
        protected override void OnActionExecuting(ActionExecutingContext executingContext)
        {
            var _session = Session[Constant.User_Login];
            if (_session == null)
            {
                executingContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    Controller = "Login",
                    Action = "Index",
                    Areas = "Admin"
                }));
            }
            base.OnActionExecuting(executingContext);
        }
    }
}