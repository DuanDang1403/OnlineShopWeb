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

        public ActionResult ProductCategoryDetail (long productCategoryId, int page = 1, int pageSize = 2)
        {
            var _detail = _productCategoryDao.GetProductCategoryByID(productCategoryId);
            ViewBag.category = _detail;
            int totalRecord = 0;
            var _model = _productDao.ListProductByCategoryID(productCategoryId, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
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