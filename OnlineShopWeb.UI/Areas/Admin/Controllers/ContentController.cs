using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopWeb.Data.DAO;
using OnlineShopWeb.Data.EF;
using PagedList.Mvc;

namespace OnlineShopWeb.UI.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        private static ContentDao _contentDao = new ContentDao();
        // GET: Admin/Content
        public ActionResult Index(string searchstring, int page = 1)
        {
            int pagesize = int.Parse(ConfigurationManager.AppSettings["Pagesize"].ToString());
            var _list = _contentDao.GetListContent(searchstring, page, pagesize);
            //SetViewBag();
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(_list);

        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content content)
        {
         
            if (ModelState.IsValid)
            {
                long _id = _contentDao.Insert(content);
                if (_id > 0)
                {
                    TempData["result"] = "Thêm mới tin tức thành công";
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    TempData["result"] = "Thêm mới tin tức không thành công";
                }

            }
            SetViewBag();
            return View();
        }
        [HttpGet] 
        public ActionResult Edit(int id)
        {
            var _dao = _contentDao.GetContentByID(id);
            SetViewBag();
            return View(_dao);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content content)
        {
            //content.ContentID = '#txtcontent';
            if (ModelState.IsValid)
            {
                string _detail = content.Detail;

                var _id = _contentDao.Update(content);
                if (_id)
                {
                    TempData["result"] = "Cập nhật tin tức thành công";
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    TempData["result"] = "Cập nhật tin tức không thành công";
                   
                }
            }
            SetViewBag();
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var _result = _contentDao.Delete(id);
            if (_result)
            {
                TempData["result"] = "Xóa tin tức thành công";
                return RedirectToAction("Index", "Content");
            }
            else
            {
                TempData["result"] = "Xóa tin tức không thành công";
                SetViewBag();
                return View();
            }
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "CategoryID", "CategoryName", selectedId);
        }
    }
}