using OnlineShopWeb.Data.DAO;
using OnlineShopWeb.Data.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopWeb.UI.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        ProductCategoryDao _productCategoryDao = new ProductCategoryDao();

        // GET: Admin/Category

        public ActionResult Index(string searchstring, int page = 1)
        {
            int pagesize = int.Parse(ConfigurationManager.AppSettings["Pagesize"].ToString());
            var _list = _productCategoryDao.GetListProductCategory(searchstring, page, pagesize);
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(_list);

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {

            if (string.IsNullOrEmpty(productCategory.ProductCategoryName))
            {
                ModelState.AddModelError("", "Mời nhập tên loại sản phẩm");
                return View();
            }

            else
            {
               
                long _id = _productCategoryDao.Insert(productCategory);
                if (_id > 0)
                {
                    TempData["result"] = "Thêm mới loại sản phẩm thành công";
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    TempData["result"] = "Thêm mới loại sản phẩm không thành công";
                }

            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var _dao = _productCategoryDao.GetProductCategoryByID(id);
            return View(_dao);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory)
        {
            //product.ProductID = _productDao.GetProductByID(product.ProductID).ProductID;
            if (ModelState.IsValid)
            {

                var _id = _productCategoryDao.Update(productCategory);
                if (_id)
                {
                    TempData["result"] = "Cập nhật loại sản phẩm thành công";
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    TempData["result"] = "Cập nhật loại sản phẩm không thành công";
                }
            }

            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var _result = _productCategoryDao.Delete(id);
            if (_result)
            {
                TempData["result"] = "Xóa loại sản phẩm thành công";
                return RedirectToAction("Index", "ProductCategory");
            }
            else
            {
                TempData["result"] = "Xóa loại sản phẩm không thành công";
                return View();
            }
        }
    }
}