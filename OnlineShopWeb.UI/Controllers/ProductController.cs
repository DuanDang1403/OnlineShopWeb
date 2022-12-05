using OnlineShopWeb.Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopWeb.UI.Controllers
{
    public class ProductController : Controller
    {
        private static ProductCategoryDao _productCategoryDao = new ProductCategoryDao();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var _list = _productCategoryDao.ListAll();
            return PartialView(_list);

        }

        public ActionResult ProductCategoryDetail (long categoryid)
        {
            var _detail = new ProductCategoryDao().GetProductCategoryByID(categoryid);
            return View(_detail);
        }

        public ActionResult ProductDetail(long productid)
        {
            var _detail = new ProductDao().GetProductByID(productid);
            return View(_detail);
        }
    }
}