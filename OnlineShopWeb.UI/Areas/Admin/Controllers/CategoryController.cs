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

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,MetaTitle,ParentID,DisplayOrder,SeoTitle,CreateDate,CreateBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescription,Status,ShowOnHome")] Category category)
        {
            if (ModelState.IsValid)
            {
                long _id = _categoryDao.Insert(category);
                if (_id > 0)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới SP không thành công");
                }                
            }

            return View();
        }

        // GET: Admin/Category/Edit/5
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            var _list = _categoryDao.GetCategoryByID(id);
            if (_list == null)
            {
                return HttpNotFound();
            }
            return View(_list);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,MetaTitle,ParentID,DisplayOrder,SeoTitle,CreateDate,CreateBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescription,Status,ShowOnHome")] Category category)
        {
         
            category.CategoryID = _categoryDao.GetCategoryByCategoryName(category.CategoryName).CategoryID;
            if (ModelState.IsValid)
            {                
                var _id = _categoryDao.Update(category);
                if (_id)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật SP không thành công");
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
        public ActionResult Delete(long? id)
        {
            var _result = _categoryDao.Delete(id);
            if (_result)
            {
                return RedirectToAction("Index", "Category");
            }
            else
            {
                ModelState.AddModelError("", "Xóa SP không thành công");
                return View();
            }
        }

       
       
    }
}
