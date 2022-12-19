using OnlineShopWeb.Data.DAO;
using OnlineShopWeb.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopWeb.UI.Controllers
{
    public class ContactController : Controller
    {
        private static ContactDao _contactDao = new ContactDao();
        // GET: Contact
        public ActionResult Index()
        {
            var _model = _contactDao.GetContact();
            return View(_model);
        }

        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, string content)
        {
            var _feedback = new Feedback();
            _feedback.Name = name;
            _feedback.Phone = mobile;
            _feedback.Address = address;
            _feedback.Email = email;
            _feedback.Content = content;
            _feedback.CreateDate = DateTime.Now;
            _feedback.Status = true;
            var id = _contactDao.Insert(_feedback);
            if (id > 0)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }

        }
    }
}