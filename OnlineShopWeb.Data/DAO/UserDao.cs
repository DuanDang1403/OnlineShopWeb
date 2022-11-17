using OnlineShopWeb.Data.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Data.DAO
{
    public class UserDao
    {
        OnlineShopWebDBContext db = null;
        public UserDao()
        {
            db = new OnlineShopWebDBContext();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.UserID;
        }

        public bool Delete(int id)
        {
            try
            {
                var _user = db.Users.Find(id);
                db.Users.Remove(_user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(User entity)
        {
            try
            {
                var _user = db.Users.Find(entity.UserID);
                _user.Name = entity.Name;
                _user.Email = entity.Email;
                _user.Phone = entity.Phone;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    _user.Password = entity.Password;
                }               
                _user.Address = entity.Address;
                _user.ModifiedBy = entity.ModifiedBy;
                _user.ModifiedDate = DateTime.Now;
                _user.CreateDate = entity.CreateDate;
                _user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public IEnumerable<User> GetListUsers(string searchstring, int page, int pagesize)
        {
            IQueryable<User> _model = db.Users;
            if (!string.IsNullOrEmpty(searchstring))
            {
                _model = _model.Where(x => x.UserName.Contains(searchstring) || x.Name.Contains(searchstring));
            }
            return _model.OrderBy(x => x.UserID).ToPagedList(page,pagesize);
        }
        public User GetUserByUserName(string userName)
        {
            return db.Users.FirstOrDefault(x => x.UserName == userName);
        }

        public User GetUserByID(int UserId)
        {
            return db.Users.Find(UserId);
            //return db.Users.FirstOrDefault(x => x.UserID == UserId);
        }
        public int Login(string userName, string passWord)
        {
            var _result = db.Users.FirstOrDefault(x => x.UserName == userName);
            if(_result == null)
            {
                return -1;// không tồn tại
            }
            else
            {
                if (_result.Status == false)
                {
                    return 1;// bị khóa
                }
                else
                {
                    if (_result.Password.ToUpper() != passWord || _result.UserName != userName)
                    {
                        return 2;// sai password hoặc username
                    }
                    return 0;//thành công
                }
            }
        }
    }
}
