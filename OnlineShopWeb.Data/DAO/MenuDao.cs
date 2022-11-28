using OnlineShopWeb.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Data.DAO
{
    public class MenuDao
    {

        OnlineShopWebDBContext db = null;
        public MenuDao()
        {
            db = new OnlineShopWebDBContext();
        }

        public List<Menu> GetAllByMenuTypeId(int id)
        {
            return db.Menus.Where(x => x.TypeID == id && x.Status==true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
