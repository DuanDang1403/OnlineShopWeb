using OnlineShopWeb.Data.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Data.DAO
{
    public class ContentDao
    {
        OnlineShopWebDBContext db = null;
        public ContentDao()
        {
            db = new OnlineShopWebDBContext();
        }
        public long Insert(Content entity)
        {
            entity.CreateDate = DateTime.Now;
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ContentID;
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
                    var _result = db.Contents.Find(id);
                    if (_result == null)
                    {
                        return false;
                    }
                    db.Contents.Remove(_result);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        public bool Update(Content entity)
        {
            try
            {
                var _result = db.Contents.Find(entity.ContentID);
                _result.ContentName = entity.ContentName;
                _result.Description = entity.Description;
                _result.MetaDescription = entity.MetaDescription;
                _result.MetaTitle = entity.MetaTitle;
                _result.Image = entity.Image;
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
        public IEnumerable<Content> GetListContent(string searchstring, int page, int pagesize)
        {
            IQueryable<Content> _model = db.Contents;
            if (!string.IsNullOrEmpty(searchstring))
            {
                _model = _model.Where(x => x.ContentName.Contains(searchstring));
            }
            return _model.OrderBy(x => x.ContentID).ToPagedList(page, pagesize);
        }
        public Content GetContentByContentName(string contentName)
        {
            return db.Contents.FirstOrDefault(x => x.ContentName == contentName);
        }

        public Content GetContentByID(long? contentId)
        {
            if (contentId == null)
            {
                return null;
            }
            return db.Contents.Find(contentId);
        }

    }
}
