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
    public class SlideController : BaseController
    {
        private static SlideDao _slideDao = new SlideDao();
        // GET: Admin/Slide
        public ActionResult Index(string searchstring, int page = 1)
        {
            int pagesize = int.Parse(ConfigurationManager.AppSettings["Pagesize"].ToString());
            var _list = _slideDao.GetListSlide(searchstring, page, pagesize);
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
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                long _id = _slideDao.Insert(slide);
                if (_id > 0)
                {
                    TempData["result"] = "Thêm mới slide thành công";
                    return RedirectToAction("Index", "Slide");
                }
                else
                {
                    TempData["result"] = "Thêm mới slide không thành công";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var _dao = _slideDao.GetSlideByID(id);
            return View(_dao);
        }

        [HttpPost]
        public ActionResult Edit(Slide Slide)
        {           
            if (ModelState.IsValid)
            {

                var _id = _slideDao.Update(Slide);
                if (_id)
                {
                    TempData["result"] = "Cập nhật slide thành công";
                    return RedirectToAction("Index", "Slide");
                }
                else
                {
                    TempData["result"] = "Cập nhật slide không thành công";
                }
            }

            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var _result = _slideDao.Delete(id);
            if (_result)
            {
                TempData["result"] = "Xóa slide thành công";
                return RedirectToAction("Index", "Slide");
            }
            else
            {
                TempData["result"] = "Xóa slide không thành công";
                return View();
            }
        }
    }
}