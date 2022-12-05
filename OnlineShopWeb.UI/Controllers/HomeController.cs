using OnlineShopWeb.Data.DAO;
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
            ViewBag.ListNewProduct = _dao.ListNewproduct(4);
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
        public ActionResult Footer()
        {
            string _id = ConfigurationManager.AppSettings["Footer"].ToString();
            var model = new FooterDao().GetAllFooter(_id);
            return PartialView(model);
        }
    }
}