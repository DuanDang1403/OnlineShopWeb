using OnlineShopWeb.Data.DAO;
using OnlineShopWeb.Data.EF;
using OnlineShopWeb.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineShopWeb.UI.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // 
        public ActionResult Index(string searchstring, int page = 1, int pagesize = 3)
        {
            var _dao = new UserDao();
            var _list = _dao.GetListUsers(searchstring,page, pagesize);
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
        public ActionResult Create(User user)
        {
            var _dao = new UserDao();
            if (string.IsNullOrEmpty(user.UserName))
            {
                ModelState.AddModelError("", "Mời nhập UserName");
                return View();
            }
            else if (string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("", "Mời nhập Password");
                return View();
            }
            else
            {
                user.Password = CryptoService.EncryptMD5(user.Password);
                long _id = _dao.Insert(user);
                if (_id > 0)
                {
                    TempData["result"] = "Thêm mới người dùng thành công";
                    return RedirectToAction("Index", "User");                   
                }
                else
                {
                    TempData["result"] = "Thêm mới người dùng không thành công";
                }
                
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var _dao = new UserDao().GetUserByID(id);
            _dao.Password = null;
            return View(_dao);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            var _dao = new UserDao();
            user.UserID = _dao.GetUserByUserName(user.UserName).UserID;            
            if (ModelState.IsValid)
            {               
                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.Password = CryptoService.EncryptMD5(user.Password);
                }
                var _id = _dao.Update(user);
                if (_id)
                {
                    TempData["result"] = "Cập nhật người dùng thành công";
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    TempData["result"] = "Cập nhật người dùng không thành công";
                }
            }
            else
            {
                return View();
            }

            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var _result = new UserDao().Delete(id);
            if (_result)
            {
                TempData["result"] = "Xóa người dùng thành công";
                return RedirectToAction("Index", "User");
            }
            else
            {
                TempData["result"] = "Xóa người dùng không thành công";
                return View();
            }
        }
    }
}