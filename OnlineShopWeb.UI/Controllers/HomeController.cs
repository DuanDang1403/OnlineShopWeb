using OnlineShopWeb.Data.DAO;
using OnlineShopWeb.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopWeb.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Slide = new SlideDao().ListAll();
            var _dao = new ProductDao();
            ViewBag.ListNewProduct = _dao.ListNewProduct(4);
            ViewBag.ListFeature = _dao.ListFeatureProduct(4);
            return View();
        }

        [ChildActionOnly]
       public ActionResult Mainmenu ()
        {
            int _typeId = int.Parse(ConfigurationManager.AppSettings["Main_Menu"].ToString());
            var model = new MenuDao().GetAllByMenuTypeId(_typeId);           
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Topmenu()
        {
            int _typeId = int.Parse(ConfigurationManager.AppSettings["Top_Menu"].ToString());
            var model = new MenuDao().GetAllByMenuTypeId(_typeId);
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {

            var cart = Session[Common.Constant.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            string _id = ConfigurationManager.AppSettings["Footer"].ToString();
            var model = new FooterDao().GetAllFooter(_id);
            return PartialView(model);
        }
    }
}