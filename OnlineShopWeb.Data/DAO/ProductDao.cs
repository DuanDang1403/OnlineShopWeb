using OnlineShopWeb.Data.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Data.DAO
{
    public class ProductDao
    {
        OnlineShopWebDBContext db = null;
        public ProductDao()
        {
            db = new OnlineShopWebDBContext();
        }
        public long Insert(Product entity)
        {
            entity.CreateDate = DateTime.Now;
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ProductID;
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
                    var _result = db.Products.Find(id);
                    if (_result == null)
                    {
                        return false;
                    }
                    db.Products.Remove(_result);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        public bool Update(Product entity)
        {
            try
            {
                var _result = db.Products.Find(entity.ProductID);
                _result.ProductName = entity.ProductName;
                _result.ProductCode = entity.ProductCode;
                _result.MetaTitle = entity.MetaTitle;
                _result.Description = entity.Description;
                _result.Image = entity.Image;
                _result.Warranty = entity.Warranty;
                _result.Quantity = entity.Quantity;
                _result.Price = entity.Price;
                _result.PromotionPrice = entity.PromotionPrice;
                _result.MetaKeywords = entity.MetaKeywords;
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
        public IEnumerable<Product> GetListProduct(string searchstring, int page, int pagesize)
        {
            IQueryable<Product> _model = db.Products;
            if (!string.IsNullOrEmpty(searchstring))
            {
                _model = _model.Where(x => x.ProductName.Contains(searchstring)|| x.Description.Contains(searchstring));
            }
            return _model.OrderBy(x => x.ProductID).ToPagedList(page, pagesize);
        }
        public Product GetProductByProductName(string productName)
        {
            return db.Products.FirstOrDefault(x => x.ProductName == productName);
        }

        public Product GetProductByID(long? productId)
        {
            if (productId == null)
            {
                return null;
            }
            return db.Products.Find(productId);
        }
    }
}
