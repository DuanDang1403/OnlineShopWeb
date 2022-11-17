using OnlineShopWeb.Data.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Data.DAO
{
    public class CategoryDao
    {
        OnlineShopWebDBContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopWebDBContext();
        }
        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.CategoryID;
        }

        public bool Delete(long? id)
        {
            if(id == null)
            {
                return false;
            }
            else
            {
                try
                {
                    var _result = db.Categories.Find(id);
                    if (_result == null)
                    {
                        return false;
                    }
                    db.Categories.Remove(_result);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
           
        }

        public bool Update(Category entity)
        {
            try
            {
                var _result = db.Categories.Find(entity.CategoryID);
                _result.CategoryName = entity.CategoryName;                
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
        public IEnumerable<Category> GetListCategory(string searchstring, int page, int pagesize)
        {
            IQueryable<Category> _model = db.Categories;
            if (!string.IsNullOrEmpty(searchstring))
            {
                _model = _model.Where(x => x.CategoryName.Contains(searchstring));
            }
            return _model.OrderBy(x => x.CategoryID).ToPagedList(page, pagesize);
        }
        public Category GetCategoryByCategoryName(string categoryName)
        {
            return db.Categories.FirstOrDefault(x => x.CategoryName == categoryName);
        }

        public Category GetCategoryByID(long? CategoryId)
        {
            if (CategoryId == null)
            {
                return null;
            }
            return db.Categories.Find(CategoryId);          
        }
       
    }
}
