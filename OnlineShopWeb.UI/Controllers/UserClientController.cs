using BotDetect.Web.Mvc;
using OnlineShopWeb.Data.DAO;
using OnlineShopWeb.Data.EF;
using OnlineShopWeb.UI.Common;
using OnlineShopWeb.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopWeb.UI.Controllers
{
    public class UserClientController : Controller
    {
        private static UserDao _userDao = null; 
        // GET: UserClient
        [HttpGet]
        public ActionResult Register()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View();
        }

        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "registrationCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                _userDao = new UserDao();
                var _checkUserName = _userDao.CheckUserName(registerModel.UserName);
                if (_checkUserName == false)
                {
                    var _checkEmail = _userDao.CheckEmail(registerModel.Email);
                    if(_checkEmail == false)
                    {
                        var _user = new User();
                        _user.UserName = registerModel.UserName;
                        _user.Email = registerModel.Email;
                        _user.Password = CryptoService.EncryptMD5(registerModel.Password);
                        _user.Address = registerModel.Address;
                        _user.CreateDate = DateTime.Now;
                        _user.Phone = registerModel.Phone;
                        _user.Name = registerModel.Name;
                        _user.Status = true;
                        var _userId = _userDao.Insert(_user);
                        if (_userId > 0)
                        {
                            TempData["result"] = "Đăng ký tài khoản thành công";
                            return RedirectToAction("Register", "UserClient");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Đăng ký không thành công");
                            
                        }
                    }
                    else
                    {
                        TempData["result"] = "Email đã được đăng ký";
                        return RedirectToAction("Register", "UserClient");
                        //ModelState.AddModelError("", "Email đã được đăng ký");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
            }
            return View(registerModel);
        }

        [HttpGet]
        public ActionResult ClientLogin()
        {
                return View();
        }
        [HttpPost]
        public ActionResult ClientLogin(ClientLoginModel clientLoginModel)
        {
            if (ModelState.IsValid)
            {
                _userDao = new UserDao();
                clientLoginModel.Password = CryptoService.EncryptMD5(clientLoginModel.Password);
                var _checkUserNameLogin = _userDao.Login(clientLoginModel.UserName,clientLoginModel.Password);
                if (_checkUserNameLogin == 0)
                {
                    var _user = _userDao.GetUserByUserName(clientLoginModel.UserName);
                    var _userSession = new UserLogin();
                    _userSession.UserID = _user.UserID;
                    _userSession.UserName = _user.UserName;
                    Session.Add(Constant.User_Login, _userSession);
                    return Redirect("/");
                }
                else if (_checkUserNameLogin == 1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa");
                }
                else if(_checkUserNameLogin == 2)
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản chưa được đăng ký");
                }
            }
            return View(clientLoginModel);
        }

        public ActionResult ClientLogout()
        {
          Session[Constant.User_Login] = null;
            return Redirect("/");
        }
    }
}