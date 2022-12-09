using OnlineShopWeb.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Data.DAO
{
    public class OrderDetailDao
    {
        OnlineShopWebDBContext db = null;

        public OrderDetailDao()
        {
            db = new OnlineShopWebDBContext();
        }
         public void Insert (OrderDetail entity)
        {
            db.OrderDetails.Add(entity);
            db.SaveChanges();
        }
    }
}
