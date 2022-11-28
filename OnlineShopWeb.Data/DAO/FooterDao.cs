using OnlineShopWeb.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Data.DAO
{
    public class FooterDao
    {
        OnlineShopWebDBContext db = null;
        public FooterDao()
        {
            db = new OnlineShopWebDBContext();
        }

        public Footer GetAllFooter(string id)
        {
            return db.Footers.FirstOrDefault(x => x.FooterID == id && x.Status == true);
        }
    }
}
