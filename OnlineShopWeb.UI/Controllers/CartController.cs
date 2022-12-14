using OnlineShopWeb.Data.DAO;
using OnlineShopWeb.Data.EF;
using OnlineShopWeb.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShopWeb.UI.Controllers
{
    public class CartController : Controller
    {
     
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[Common.Constant.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public JsonResult DeleteAll()
        {
            Session[Common.Constant.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[Common.Constant.CartSession];
            sessionCart.RemoveAll(x => x.Product.ProductID == id);
            Session[Common.Constant.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[Common.Constant.CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ProductID == item.Product.ProductID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[Common.Constant.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(long productId)
        {
            int quantity = 1;
            var product = new ProductDao().GetProductByID(productId);
            var cart = Session[Common.Constant.CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ProductID == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.ProductID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[Common.Constant.CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[Common.Constant.CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[Common.Constant.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            var _order = new Order();
            _order.CreateDate = DateTime.Now;
            _order.ShipAddress = address;
            _order.ShipMobile = mobile;
            _order.ShipName = shipName;
            _order.ShipEmail = email;
            _order.Status = true;
            try
            {
                var id = new OrderDao().Insert(_order);
                var cart = (List<CartItem>)Session[Common.Constant.CartSession];
                List<long> _productid = new List<long>();
                var detailDao = new OrderDetailDao();               
                decimal total = 0;
                foreach (var item in cart)
                {
                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                    var _orderDetail = new OrderDetail();
                    _orderDetail.ProductID = item.Product.ProductID;
                    _orderDetail.OrderID = id;
                    _orderDetail.Price = item.Product.Price;
                    _orderDetail.Quantity = item.Quantity;
                    _orderDetail.TotalPrice = (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                    detailDao.Insert(_orderDetail);
                    _productid.Add(item.Product.ProductID);
                }
                
                foreach (var cartItem in _productid)
                {
                    cart.RemoveAll(x => x.Product.ProductID == cartItem);
                    
                }
                Session[Common.Constant.CartSession] = cart;
                //string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

                //content = content.Replace("{{CustomerName}}", shipName);
                //content = content.Replace("{{Phone}}", mobile);
                //content = content.Replace("{{Email}}", email);
                //content = content.Replace("{{Address}}", address);
                //content = content.Replace("{{Total}}", total.ToString("N0"));
                //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                //new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop", content);
                //new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
            }
            catch (Exception ex)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}