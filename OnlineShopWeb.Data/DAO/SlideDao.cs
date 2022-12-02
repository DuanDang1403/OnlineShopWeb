using OnlineShopWeb.Data.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Data.DAO
{
    public class SlideDao
    {
        OnlineShopWebDBContext db = null;
        public SlideDao()
        {
            db = new OnlineShopWebDBContext();
        }
        public long Insert(Slide entity)
        {
            entity.CreateDate = DateTime.Now;
            db.Slides.Add(entity);
            db.SaveChanges();
            return entity.SlideID;
        }
        public bool Delete(long? id)
        {
            if (id==null)
            {
                return false;
            }
            else
            {
                try
                {
                    var _result = db.Slides.Find(id);
                    if (_result == null)
                    {
                        return false;
                    }
                    db.Slides.Remove(_result);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        public bool Update(Slide entity)
        {
            try
            {
                var _result = db.Slides.Find(entity.SlideID);              
                _result.Image = entity.Image;
                _result.Link = entity.Link;
                _result.DisplayOrder = entity.DisplayOrder;
                _result.Image = entity.Image;
                _result.Description = entity.Description;
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
        public IEnumerable<Slide> GetListSlide(string searchstring, int page, int pagesize)
        {
            IQueryable<Slide> _model = db.Slides;
            if (!string.IsNullOrEmpty(searchstring))
            {
                _model = _model.Where(x => x.Description.Contains(searchstring));
            }
            return _model.OrderBy(x => x.SlideID).ToPagedList(page, pagesize);
        }
     
        public Slide GetSlideByID(long? Id)
        {
            if (Id == null)
            {
                return null;
            }
            return db.Slides.Find(Id);
        }
    }
}
