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
    public class ProductController : BaseController
    {
        private static ProductDao _productDao = new ProductDao();
        // GET: Admin/Product
        public ActionResult Index(string searchstring, int page = 1)
        {
            int pagesize = int.Parse(ConfigurationManager.AppSettings["Pagesize"].ToString());
            var _list = _productDao.GetListProduct(searchstring, page, pagesize);
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
        public ActionResult Create(Product product)
        {
            
            if (string.IsNullOrEmpty(product.ProductName))
            {
                ModelState.AddModelError("", "Mời nhập tên sản phẩm");
                return View();
            }
            else if (string.IsNullOrEmpty(product.ProductCode))
            {
                ModelState.AddModelError("", "Mời nhập code sản phẩm");
                return View();
            }
            else
            {
                
                long _id = _productDao.Insert(product);
                if (_id > 0)
                {
                    TempData["result"] = "Thêm mới sản phẩm thành công";
                    return RedirectToAction("Index", "Product");                   
                }
                else
                {
                    TempData["result"] = "Thêm mới sản phẩm không thành công";
                }

            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var _dao = _productDao.GetProductByID(id);            
            return View(_dao);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {          
            //product.ProductID = _productDao.GetProductByID(product.ProductID).ProductID;
            if (ModelState.IsValid)
            {
                
                var _id = _productDao.Update(product);
                if (_id)
                {
                    TempData["result"] = "Cập nhật sản phẩm thành công";
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    TempData["result"] = "Cập nhật sản phẩm không thành công";
                }
            }
           
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var _result = _productDao.Delete(id);
            if (_result)
            {
                TempData["result"] = "Xóa sản phẩm thành công";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                TempData["result"] = "Xóa sản phẩm không thành công";
                return View();
            }
        }
    }
}