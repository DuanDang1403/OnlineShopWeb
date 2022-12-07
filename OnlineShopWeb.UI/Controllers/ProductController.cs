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
        private static ProductDao _productDao = new ProductDao();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            //var _detail = _productDao.GetListAll();
            //ViewBag.product = _detail;
            var _list = _productCategoryDao.ListAll();
            return PartialView(_list);

        }

        public ActionResult ProductCategoryDetail (long productCategoryId)
        {
            var _detail = _productCategoryDao.GetProductCategoryByID(productCategoryId);
            ViewBag.category = _detail;
            var _model = _productDao.ListProductByCategoryID(productCategoryId);
            return View(_model);
        }

        public ActionResult ProductDetail(long productid)
        {  
            var _detail = _productDao.GetProductByID(productid);
            ViewBag.category = _productCategoryDao.GetProductCategoryByID(_detail.ProductCategoryID.Value);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProducts(productid);
            //ViewBag.product = _detail;
            return View(_detail);
        }
    }
}