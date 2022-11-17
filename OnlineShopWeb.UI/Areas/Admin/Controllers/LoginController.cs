using OnlineShopWeb.Data.DAO;
using OnlineShopWeb.UI.Areas.Admin.Models;
using OnlineShopWeb.UI.Common;
using System.Web.Mvc;

namespace OnlineShopWeb.UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {             
                var _userDao = new UserDao();
                model.Password = CryptoService.EncryptMD5(model.Password);
                var _result = _userDao.Login(model.UserName, model.Password);
                if (_result==0)
                {
              
                    var _user = _userDao.GetUserByUserName(model.UserName);
                   var _userSession = new UserLogin();
                    _userSession.UserID = _user.UserID;
                    _userSession.UserName = _user.UserName;
                    Session.Add(Constant.User_Login, _userSession);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (_result == -1)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                    }
                   else if (_result == 1)
                    {
                        ModelState.AddModelError("", "Tài khoản bị khóa");
                    }
                    else if (_result == 2)
                    {
                        ModelState.AddModelError("", "Sai UserName hoặc Password");
                    }

                }
            }
            
            return View("Index");
        }

       
    }
}