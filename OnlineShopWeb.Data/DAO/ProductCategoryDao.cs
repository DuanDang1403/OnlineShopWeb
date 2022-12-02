using OnlineShopWeb.Data.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Data.DAO
{
    public class ProductCategoryDao
    {
        OnlineShopWebDBContext db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopWebDBContext();
        }
        public long Insert(ProductCategory entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.CreateBy = "admin";
            db.ProductCategories.Add(entity);
            db.SaveChanges();
            return entity.ProductCategoryID;
        }

        public bool Delete(long? id)
        {
            if (id == null)
            {
                return false;
            }
            else
            {
                try
                {
                    var _result = db.ProductCategories.Find(id);
                    if (_result == null)
                    {
                        return false;
                    }
                    db.ProductCategories.Remove(_result);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        public bool Update(ProductCategory entity)
        {
            try
            {
                var _result = db.ProductCategories.Find(entity.ProductCategoryID);
                _result.ProductCategoryName = entity.ProductCategoryName;
                _result.DisplayOrder = entity.DisplayOrder;               
                _result.MetaTitle = entity.MetaTitle;
                _result.ModifiedBy = entity.ModifiedBy;
                _result.ModifiedDate = DateTime.Now;
                _result.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public IEnumerable<ProductCategory> GetListProductCategory(string searchstring, int page, int pagesize)
        {
            IQueryable<ProductCategory> _model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchstring))
            {
                _model = _model.Where(x => x.ProductCategoryName.Contains(searchstring));
            }
            return _model.OrderBy(x => x.ProductCategoryID).ToPagedList(page, pagesize);
        }
        public ProductCategory GetProductCategoryByName(string productCategoryName)
        {
            return db.ProductCategories.FirstOrDefault(x => x.ProductCategoryName == productCategoryName);
        }

        public ProductCategory GetProductCategoryByID(long? ProductCategoryId)
        {
            if (ProductCategoryId == null)
            {
                return null;
            }
            return db.ProductCategories.Find(ProductCategoryId);
        }
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).ToList();
        }

    }
}
