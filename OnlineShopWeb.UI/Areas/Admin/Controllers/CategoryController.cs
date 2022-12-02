using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShopWeb.Data.EF;
using OnlineShopWeb.Data.DAO;

namespace OnlineShopWeb.UI.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        CategoryDao _categoryDao = new CategoryDao();

        // GET: Admin/Category
       
        public ActionResult Index(string searchstring, int page = 1, int pagesize = 3)
        {             
            var _list = _categoryDao.GetListCategory(searchstring, page, pagesize);
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(_list);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(long? id)
        {
            var _list = _categoryDao.GetCategoryByID(id);
            if (_list == null)
            {
                return HttpNotFound();
            }
            return View(_list);
        }

        // GET: Admin/Category/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]     
        public ActionResult Create(Category category)
        {
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                ModelState.AddModelError("", "Mời nhập tên danh mục");
                return View();
            }
           
            else
            {
                long _id = _categoryDao.Insert(category);
                if (_id > 0)
                {
                    TempData["result"] = "Thêm mới loại danh mục thành công";
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    TempData["result"] = "Thêm mới loại danh mục không thành công";
                }

            }

            return View();
        }

        // GET: Admin/Category/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var _list = _categoryDao.GetCategoryByID(id);           
            return View(_list);
        }

        
        [HttpPost]    
        public ActionResult Edit(Category category)
        {         
           
            if (ModelState.IsValid)
            {                
                var _id = _categoryDao.Update(category);
                if (_id)
                {
                    TempData["result"] = "Cập nhật loại danh mục thành công";
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    TempData["result"] = "Cập nhật loại danh mục không thành công";
                }
            }
            else
            {
                return View();
            }

            return View();
        }

        // GET: Admin/Category/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var _result = _categoryDao.Delete(id);
            if (_result)
            {
                TempData["result"] = "xóa loại danh mục thành công";
                return RedirectToAction("Index", "Category");
            }
            else
            {
                TempData["result"] = "xóa loại danh mục không thành công";
                return View();
            }
        }

       
       
    }
}
