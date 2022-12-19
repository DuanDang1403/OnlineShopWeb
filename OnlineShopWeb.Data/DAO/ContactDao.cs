using OnlineShopWeb.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Data.DAO
{
    public class ContactDao
    {
        OnlineShopWebDBContext db = null;
        public ContactDao()
        {
            db = new OnlineShopWebDBContext();
        }
        public Contact GetContact()
        {
            return db.Contacts.SingleOrDefault(x => x.Status==true);
        }

        public int Insert (Feedback entity)
        {
            db.Feedbacks.Add(entity);
            db.SaveChanges();
            return entity.FeedbackID;
        }
    }
}
