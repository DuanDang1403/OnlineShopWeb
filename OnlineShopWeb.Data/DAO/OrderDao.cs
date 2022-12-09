using OnlineShopWeb.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Data.DAO
{
    public class OrderDao
    {
        OnlineShopWebDBContext db = null;
        public OrderDao()
        {
            db = new OnlineShopWebDBContext();
        }
        public long Insert (Order entity)
        {        
            db.Orders.Add(entity);
            db.SaveChanges();
            return entity.OrderID;
        }
    }
   
}
